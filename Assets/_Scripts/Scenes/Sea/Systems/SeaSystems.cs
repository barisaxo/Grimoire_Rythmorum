using UnityEngine;
using Sea;
using Sea.Maps;

public static class SeaSystems
{
    public static Vector3 UpdateMap(this Sea.WorldMapScene scene, State current, DataManager data, Vector3 dir)
    {
        scene.UpdateLocalRegions(current);
        scene.UpdateBoardSubTiles();
        scene.UpdateNPCShipLocations();
        scene.UpdateMapObjects(current, data);
        return UpdatePlayerShipPos(scene, dir);
    }

    private static void UpdateBoardSubTiles(this Sea.WorldMapScene scene)
    {
        scene.Map.Territories.TryGetValue(scene.Ship.RegionCoord, out R region);

        if (scene.Board.SeaColor != scene.Map.GetSeaColorFromRegion(region))
        {
            scene.Board.SeaColor = scene.Map.GetSeaColorFromRegion(region);
            scene.HUD.BlipRegionUpdate(region.GetRegionFromMapR());
        }

        foreach (SubTile tile in scene.Board.SubTiles)
            tile.GO.transform.position = new Vector3(
                (float)(tile.Loc.x - scene.Board.Size - scene.Ship.GlobalLoc.x).Smod(scene.Board.Size),
                0,
                (float)(tile.Loc.z - scene.Board.Size - scene.Ship.GlobalLoc.y).Smod(scene.Board.Size));
    }

    private static Vector2 UpdatePlayerShipPos(this Sea.WorldMapScene scene, Vector2 dir)
    {
        scene.CheckInteractables();

        if (Mathf.Approximately(dir.y, 0))
        {
            scene.Ship.Rotate(dir.x);
            return dir;
        }

        Vector2 forward2D = new(scene.Ship.GO.transform.forward.x, scene.Ship.GO.transform.forward.z);

        dir = CheckCollisions(dir);

        Vector2 posDelta = Time.fixedDeltaTime * dir.y * scene.Ship.MoveSpeed * forward2D;
        scene.Ship.GlobalLoc += posDelta;
        scene.Board.Swells.Offset += (posDelta.y + posDelta.x) * .5f;
        scene.Ship.Rotate(dir.x);
        return dir;

        Vector2 CheckCollisions(Vector2 dir)
        {
            if (Physics.Raycast(
                    scene.Ship.GO.transform.position,
                    dir.y > 0 ? scene.Ship.GO.transform.forward : -scene.Ship.GO.transform.forward,
                    out RaycastHit hit,
                    1.25f)
                )
            {
                foreach (NPCShip ship in scene.NPCShips)
                    if (hit.collider == ship.SceneObject.Collidable.GetCollider && ship.SceneObject.Collidable != null)
                        return ship.SceneObject.Collidable.CollisionResults(scene, ship.SceneObject.Telemeter, dir, hit);

                foreach (ISceneObject so in scene.SceneObjects)
                    if (hit.transform == so.GO.transform && so.Collidable != null)
                        return so.Collidable.CollisionResults(scene, so.Telemeter, dir, hit);
            }
            return dir;
        }
    }

    public static void CheckInteractables(this Sea.WorldMapScene scene)
    {
        scene.NearestNPC = null;
        scene.NearestInteractableCell = null;
        scene.Ship.ConfirmPopup.GO.SetActive(false);

        foreach (NPCShip npc in scene.NPCShips)
            if (npc.SceneObject.Interactable is not NoInteraction &&
                Vector3.Distance(npc.SceneObject.GO.transform.position, scene.Ship.GO.transform.position) < 1.5f)
            {
                scene.NearestNPC = npc;
                scene.Ship.ConfirmPopup.GO.SetActive(true);
                scene.Ship.ConfirmPopup.TextString = "Hail";
                return;
            }

        foreach (Region region in scene.LocalRegions)
        {
            foreach (Cell cell in region.Cells)
            {
                if (cell.SceneObject != null &&
                    cell.SceneObject.Telemeter.IsInRange(cell.SceneObject.Collidable.GetCollider, scene.Ship.CapsuleCollider) &&
                    cell.SceneObject.Interactable.SubsequentState != null)
                {
                    if (cell.SceneObject.Interactable.PopupText != null)
                    {
                        scene.Ship.ConfirmPopup.GO.SetActive(true);
                        scene.Ship.ConfirmPopup.TextString = cell.SceneObject.Interactable.PopupText;
                    }
                    scene.NearestInteractableCell = cell;
                    return;
                }
            }
        }
    }

    private static void UpdateLocalRegions(this Sea.WorldMapScene scene, State currentState)
    {
        scene.LocalRegions = scene.Map.RegionsAdjacentTo(scene.Ship);
    }

    private static void UpdateMapObjects(this Sea.WorldMapScene scene, State currentState, DataManager data)
    {
        for (int x = -2; x < scene.Board.Size + 2; x++)
            for (int y = -2; y < scene.Board.Size + 2; y++)
                ShowObject(x, y);

        void ShowObject(int x, int y)
        {
            Vector2Int offsetGlobalCoord = scene.BoardOffsetGlobalCoords(x, y);

            if (IsBorder(x, y, scene, offsetGlobalCoord, scene.LocalRegions)) return;

            foreach (Region region in scene.LocalRegions)
                foreach (NPCShip npc in region.NPCs)
                    if (UpdateShipObject(currentState, scene, npc, offsetGlobalCoord))
                        break;

            Cell cell = scene.GetCellOrDefaultFromBoardLoc(x, y, offsetGlobalCoord);
            if (cell is null) return;
            UpdateCellObject(scene, currentState, cell, offsetGlobalCoord, data);
        }
    }

    private static void UpdateCellObject(Sea.WorldMapScene scene, State currentState, Cell cell, Vector2Int offsetGlobalCoord, DataManager data)
    {
        if (cell.SceneObject is null)
        {
            cell.InstantiateNewSceneObject(currentState, data);
            if (cell.SceneObject is not null)
            {
                scene.SceneObjects.Add(cell.SceneObject);
                // cell.SceneObject.Instantiator.Instantiate();
            }
        }
        if (cell.SceneObject is not null)
        {
            cell.GO.transform.SetPositionAndRotation(
                cell.SceneObject.UpdatePosition.NewPosition(scene, scene.RegionFromOffsetGlobalCoord(offsetGlobalCoord), cell.Coord),
                Quaternion.Euler(cell.SceneObject.Instantiator.Rot));
            cell.SceneObject.GO.transform.localScale = cell.SceneObject.Instantiator.Scale;
        }
    }

    private static bool IsBorder(int x, int y, Sea.WorldMapScene scene, Vector2Int offsetGlobalCoord, Region[] localRegions)
    {
        if (x <= -1 || y <= -1 || x >= scene.Board.Size || y >= scene.Board.Size)
        {
            Cell cell1 = scene.GetCellOrDefaultFromBoardLoc(x, y, offsetGlobalCoord);
            if (cell1 is not null && cell1.SceneObject is not null) DeactivateCellBorderObject(scene, cell1);
            foreach (NPCShip npc in scene.NPCShips)
                // foreach (Region region in localRegions)
                //     foreach (NPCShip npc in region.NPCs)
                if (DeactivateNPCBorderObject(scene, npc, offsetGlobalCoord))
                    return true;
            // continue;

            return true;
        }
        return false;
    }

    private static bool DeactivateNPCBorderObject(Sea.WorldMapScene scene, NPCShip npc, Vector2Int offsetGlobalCoord)
    {
        if (npc.GlobalCoords == offsetGlobalCoord)
        {
            if (npc.SceneObject != null)
            {
                scene.RockTheBoat.RemoveBoat(npc.SceneObject.GO.transform);
                scene.NPCShips.Remove(npc);
                npc.DestroySceneObject();
            }
            return true;
        }
        return false;
    }

    private static void DeactivateCellBorderObject(Sea.WorldMapScene scene, Cell cell)
    {
        if (cell.SceneObject != null)
        {
            scene.SceneObjects.Remove(cell.SceneObject);
            cell.DestroySceneObject();
        }
    }

    private static bool UpdateShipObject(State currentState, Sea.WorldMapScene scene, NPCShip npc, Vector2Int offsetGlobalCoord)
    {
        if (npc.GlobalCoords != offsetGlobalCoord) return false;

        if (npc.SceneObject is null)
        {
            npc.InstantiateNewSceneObject(currentState);
            npc.SceneObject.GO.transform.localScale = Vector3.one * .45f;
            npc.SceneObject.GO.transform.SetParent(Sea.WorldMapScene.Io.TheSea.transform);
            scene.RockTheBoat.AddBoat(npc.SceneObject.GO.transform, npc.Sway);
            scene.NPCShips.Add(npc);
        }

        npc.SceneObject?.GO.transform.SetPositionAndRotation(
                new Vector3(
                    (scene.Board.Center() + .3f + npc.GlobalPos.x - scene.Ship.GlobalLoc.x).Smod(scene.Map.GlobalSize),
                    0,
                    (scene.Board.Center() + .3f + npc.GlobalPos.y - scene.Ship.GlobalLoc.y).Smod(scene.Map.GlobalSize)),
                Quaternion.Euler(new Vector3(0, npc.RotY, 0)));

        return true;
    }

    public static void UpdateNPCShipLocations(this Sea.WorldMapScene scene)
    {
        foreach (Region region in scene.LocalRegions)
            foreach (NPCShip npc in region.NPCs)
            {
                int pathDirection = npc.PathDirection ? 1 : -1;

                Vector2 dir = npc.PatrolPath[npc.PatrolIndex] - npc.LocalCoords;
                dir.Normalize();
                Vector2 posDelta = new(Time.fixedDeltaTime * npc.MoveSpeed * dir.x, Time.fixedDeltaTime * npc.MoveSpeed * dir.y);

                if ((npc.GlobalCoords + posDelta).IsPOM(1, scene.Ship.GlobalLoc)) continue;

                bool waitForOtherShips = false;

                for (int ii = 0; ii < region.NPCs.Length; ii++)
                {
                    if (region.NPCs[ii].LocalCoords == npc.PatrolPath[npc.PatrolIndex])
                    {
                        waitForOtherShips = true;
                        npc.StuckDelta += Time.fixedDeltaTime;
                        break;
                    }
                }

                if (waitForOtherShips && npc.StuckDelta >= npc.StuckTimer)
                {
                    npc.PathDirection = !npc.PathDirection;
                    npc.PatrolIndex += npc.PathDirection ? 1 : -1;
                    npc.StuckDelta = 0;
                    continue;
                }

                bool turning = dir switch
                {
                    _ when dir == Vector2Int.up => !Mathf.DeltaAngle(npc.RotY, 0).IsPOM(1, 0),
                    _ when dir == Vector2Int.down => !Mathf.DeltaAngle(npc.RotY, 180).IsPOM(1, 0),
                    _ when dir == Vector2Int.right => !Mathf.DeltaAngle(npc.RotY, 90).IsPOM(1, 0),
                    _ when dir == Vector2Int.left => !Mathf.DeltaAngle(npc.RotY, 270).IsPOM(1, 0),
                    _ => false,
                };

                if (turning)
                {
                    npc.NewRotY = dir switch
                    {
                        _ when dir == Vector2Int.up => 0,
                        _ when dir == Vector2Int.down => 180,
                        _ when dir == Vector2Int.right => 90,
                        _ when dir == Vector2Int.left => 270,
                        _ => npc.NewRotY,
                    };

                    npc.RotY += Mathf.Sign(Mathf.DeltaAngle(npc.RotY, npc.NewRotY));
                }

                if (!waitForOtherShips)
                {
                    npc.StuckDelta = 0;
                    npc.Pos += posDelta;
                }

                if (npc.LocalCoords == npc.PatrolPath[npc.PatrolIndex])
                {
                    npc.PatrolIndex += pathDirection;
                }
            }
    }

    public static void ClearCell(this WorldMapScene scene)
    {
        if (scene.NearestInteractableCell == null) return;
        scene.Ship.Region.Cells.Remove(scene.NearestInteractableCell);
        // scene.NearestInteractableCell.Type = Sea.CellType.OpenSea;
        scene.SceneObjects.Remove(scene.NearestInteractableCell.SceneObject);
        Debug.Log(scene.Ship.Region.Cells.Count + " " + scene.SceneObjects.Count);
        scene.NearestInteractableCell.DestroySceneObject();
        scene.NearestInteractableCell = null;
    }

    public static Region RegionFromOffsetGlobalCoord(this Sea.WorldMapScene scene, Vector2Int offsetV2i) =>
        scene.Map.Regions[scene.Map.RegionIndexFromGlobalCoord(offsetV2i)];

    public static Cell GetCellOrDefaultFromBoardLoc(this Sea.WorldMapScene scene, int x, int y, Vector2Int offsetGlobalCoord)
    {
        Region region = scene.RegionFromOffsetGlobalCoord(offsetGlobalCoord);
        foreach (Cell cell in region.Cells) if (cell.Coord == scene.OffsetLocalCoords(x, y)) return cell;
        return null;
    }

    public static Cell GetCellFromBoardLoc(this Sea.WorldMapScene scene, Vector2Int v2i)
    {
        Region region = scene.Map.Regions[scene.Map.RegionIndexFromGlobalCoord(scene.Ship.GlobalCoord + v2i)];
        return region.Cells[region.CellIndex(scene.OffsetLocalCoords(v2i))];
    }

    public static Vector2Int OffsetLocalCoords(this Sea.WorldMapScene scene, int x, int y)
    {
        Vector2Int v2i = new(x, y);
        return scene.OffsetLocalCoords(v2i);
    }

    public static Vector2Int OffsetLocalCoords(this Sea.WorldMapScene scene, Vector2Int boardLoc) =>
            (boardLoc + scene.Ship.LocalCoord(scene.Map.RegionSize) - scene.Board.CenterV2i()).Smod(scene.Map.RegionSize);

    public static Vector2Int BoardOffsetGlobalCoords(this Sea.WorldMapScene scene, int x, int y) =>
        scene.BoardOffsetGlobalCoords(new Vector2Int(x, y));

    public static Vector2Int BoardOffsetGlobalCoords(this Sea.WorldMapScene scene, Vector2Int v2i)
    {
        Vector2 v2 = v2i + scene.Ship.GlobalCoord - scene.Board.CenterV2i();
        return new Vector2Int((int)v2.x, (int)v2.y).Smod(scene.Map.GlobalSize);
    }

    public static void SetUpSeaCam(this Sea.WorldMapScene scene)
    {
        Cam.Io.Camera.transform.SetPositionAndRotation(scene.Ship.Parent.transform.parent.position, scene.Ship.Parent.transform.rotation);
        Cam.Io.Camera.transform.Translate(Vector3.up - (Cam.Io.Camera.transform.forward * 2));
        Cam.Io.Camera.transform.LookAt(scene.Ship.GO.transform, Vector3.up);
        Cam.Io.Camera.transform.Rotate(new Vector3(-10, 0, 0));

        Light l = new GameObject(nameof(Light)).AddComponent<Light>();
        l.transform.SetParent(scene.TheSea.transform);
        l.type = LightType.Directional;
        l.color = new(.9f, .8f, .65f);
        l.shadows = LightShadows.None;
        l.transform.SetPositionAndRotation(Cam.Io.Camera.transform.position, Cam.Io.Camera.transform.rotation);
        l.transform.Rotate(new Vector3(45, 0, 0));
        l.intensity = 1.95f;
    }

    public static NPCShip CheckNMETriggers(this Sea.WorldMapScene Scene)
    {
        foreach (NPCShip npc in Scene.NPCShips)
            if (npc.SceneObject.Triggerable is PirateTrigger &&
                npc.SceneObject.Telemeter.GetDistance(Scene.Ship.PosV2, npc.SceneObject.GO.transform) < npc.ThreatRange)
                return npc;
        return null;
    }

    public static Color MyGrey => new(.45f, .45f, .45f, .35f);
    public static Color MyRed => new(.85f, .15f, .35f, .35f);
    public static Color MyGreen => new(.25f, .55f, .25f, .35f);
    public static Color MyBlue => new(.15f, .35f, .85f, .35f);
    public static Color MyCyan => new(.15f, .65f, .55f, .35f);
    public static Color MyYellow => new(.75f, .65f, .15f, .35f);
    public static Color MyMagenta => new(.85f, .15f, .75f, .35f);

    public static Color OverlayWhite => new(.3f, .3f, .3f, .8f);
    public static Color OverlayGrey => new(.5f, .5f, .1f, .2f);
    public static Color OverlayGreen => new(0, 1f, .0f, .2f);
    public static Color OverlayRed => new(1f, 0f, 0f, .2f);
}

