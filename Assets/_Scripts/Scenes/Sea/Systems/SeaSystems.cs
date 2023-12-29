using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sea;

public static class SeaSystems
{
    public static event Action SailingStart;
    public static void StartSailing() => SailingStart?.Invoke();

    public static event Action SailingFinished;
    public static void EndSailing() => SailingFinished?.Invoke();

    public static Vector3 UpdateMap(this Scene scene, Vector3 dir)
    {
        scene.UpdateBoardSubTiles();
        scene.UpdateNPCShips();
        UpdateMapObjects(scene);
        var newV3 = UpdatePlayerShipPos(scene, dir);

        if (scene.DebugTimer > scene.DebugInterval) scene.DebugTimer -= scene.DebugInterval;
        else scene.DebugTimer += Time.fixedDeltaTime;

        return newV3;
    }

    private static void UpdateBoardSubTiles(this Scene scene)
    {
        foreach (SubTile tile in scene.Board.SubTiles)
            tile.GO.transform.position = new Vector3(
                (float)(tile.Loc.x - scene.Board.Size - scene.Ship.GlobalPos.x).Smod(scene.Board.Size),
                0,
                (float)(tile.Loc.z - scene.Board.Size - scene.Ship.GlobalPos.y).Smod(scene.Board.Size));
    }

    private static Vector2 UpdatePlayerShipPos(this Scene scene, Vector2 dir)
    {
        scene.Ship.Bark.GO.SetActive(false);
        if (dir.y != 0)
        {
            Vector2 forward2D = new(scene.Ship.GO.transform.forward.x, scene.Ship.GO.transform.forward.z);
            bool noNearNPCs = true;

            var localRegions = scene.Map.AdjacentRegions(scene.Ship);

            Vector2 shipPos = new(scene.Ship.GO.transform.position.x, scene.Ship.GO.transform.position.z);

            foreach (GameObject npcGO in scene.UsedShips)
            {
                if (Vector3.Distance(npcGO.transform.position, scene.Ship.GO.transform.position) < 1.25f)
                {
                    scene.Ship.Bark.GO.SetActive(true);
                    noNearNPCs = false;

                    CapsuleCollider npcCol = npcGO.GetComponentInChildren<CapsuleCollider>();

                    Vector3 b = npcCol.ClosestPoint(scene.Ship.GO.transform.position);
                    Vector3 a = scene.Ship.CapsuleCollider.ClosestPoint(npcGO.transform.position);
                    Vector3 b2 = npcCol.ClosestPoint(b);
                    Vector3 a2 = scene.Ship.CapsuleCollider.ClosestPoint(a);
                    Vector3 b3 = npcCol.ClosestPoint(b2);
                    Vector3 a3 = scene.Ship.CapsuleCollider.ClosestPoint(a2);

                    Vector2 a3v2 = new(a3.x, a3.z);
                    Vector2 b3v2 = new(b3.x, b3.z);

                    if (Vector2.Distance(a3v2, b3v2) < .95f)
                    {
                        if (shipPos.IsMovingTowards(forward2D * dir.y, b3v2, 45))
                        {
                            Vector3 leftOf = scene.Ship.GO.transform.rotation * Vector3.left;
                            Vector3 rightOf = scene.Ship.GO.transform.rotation * Vector3.right;
                            Vector2 left = new(leftOf.x, leftOf.z);
                            Vector2 right = new(rightOf.x, rightOf.z);

                            dir.x += Vector2.Distance(a3v2 + left, b3v2) > Vector2.Distance(a3v2 + right, b3v2) ? -1 : 1;

                            Debug.Log(Vector2.Distance(a3v2 + left, b3v2) + " " + Vector2.Distance(a3v2 + right, b3v2) + " " + dir.x);
                            dir.y *= Vector2.Distance(a3v2, b3v2);
                        }
                    }

                    foreach (Region region in localRegions)
                        foreach (NPCShip npc in region.NPCs)
                            if (npc.GO == npcGO) scene.NearestNPC = npc;
                }
            }

            if (noNearNPCs) scene.NearestNPC = null;

            foreach (GameObject rock in scene.UsedRocks)
            {
                if (Vector3.Distance(rock.transform.position, scene.Ship.GO.transform.position) < 1f)
                {
                    CapsuleCollider rockCol = rock.GetComponentInChildren<CapsuleCollider>();

                    Vector3 b = rockCol.ClosestPoint(scene.Ship.GO.transform.position);
                    Vector3 a = scene.Ship.CapsuleCollider.ClosestPoint(rockCol.transform.position);
                    Vector3 b2 = rockCol.ClosestPoint(b);
                    Vector3 a2 = scene.Ship.CapsuleCollider.ClosestPoint(a);
                    Vector3 b3 = rockCol.ClosestPoint(b2);
                    Vector3 a3 = scene.Ship.CapsuleCollider.ClosestPoint(a2);

                    Vector2 a3v2 = new(a3.x, a3.z);
                    Vector2 b3v2 = new(b3.x, b3.z);

                    if (Vector2.Distance(a3v2, b3v2) < .95f)
                    {
                        if (shipPos.IsMovingTowards(forward2D * dir.y, b3v2, 45))
                        {
                            Vector3 leftOf = scene.Ship.GO.transform.rotation * Vector3.left;
                            Vector3 rightOf = scene.Ship.GO.transform.rotation * Vector3.right;
                            Vector2 left = new(leftOf.x, leftOf.z);
                            Vector2 right = new(rightOf.x, rightOf.z);

                            dir.x += Vector2.Distance(a3v2 + left, b3v2) > Vector2.Distance(a3v2 + right, b3v2) ? 1 : -1;

                            Debug.Log(Vector2.Distance(a3v2 + left, b3v2) + " " + Vector2.Distance(a3v2 + right, b3v2) + " " + dir.x);
                            dir.y *= Vector2.Distance(a3v2, b3v2);
                        }
                    }
                }
            }

            Vector2 posDelta = Time.fixedDeltaTime * dir.y * scene.Ship.MoveSpeed * forward2D;
            scene.Ship.GlobalPos += posDelta;
            scene.Swells.Offset += (posDelta.y + posDelta.x) * .5f;
        }

        scene.Ship.Rotate(dir.x);
        Debug.Log(dir.x);
        return dir;
    }

    static bool IsMovingTowards(this Vector2 transformPosition, Vector2 transformDirection, Vector2 targetPosition, float approach)
    {
        Vector2 toTarget = targetPosition - transformPosition;
        transformDirection.Normalize();
        toTarget.Normalize();
        float dotProduct = Vector2.Dot(transformDirection, toTarget);
        float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
        return angle < approach;
    }
    private static bool NewCoordIsOpen(this Scene scene, Vector2 posDelta)
    {
        Region localRegion = scene.Map.Regions[scene.NewMapRegionIndex(posDelta)];
        Cell dCell = localRegion.Cells[scene.RegionCellIndex(posDelta, localRegion)];

        return dCell.Type == CellType.OpenSea || dCell.Type == CellType.Center;
    }

    private static CellType NewCoordCellType(this Scene scene, Vector2 posDelta)
    {
        Region localRegion = scene.Map.Regions[scene.NewMapRegionIndex(posDelta)];
        Cell dCell = localRegion.Cells[scene.RegionCellIndex(posDelta, localRegion)];

        return dCell.Type;
    }

    private static Cell NewCoordCell(this Scene scene, Vector2 posDelta)
    {
        Region localRegion = scene.Map.Regions[scene.NewMapRegionIndex(posDelta)];
        Cell dCell = localRegion.Cells[scene.RegionCellIndex(posDelta, localRegion)];

        return dCell;
    }

    private static CellType NewCoordYCellType(this Scene scene, Vector2 posDelta)
    {
        Region localRegion = scene.Map.Regions[scene.NewMapRegionIndex(new Vector2(0, posDelta.y))];
        Cell dCell = localRegion.Cells[scene.RegionCellIndex(new Vector2(0, posDelta.y), localRegion)];
        return dCell.Type;
    }

    private static CellType NewCoordXCellType(this Scene scene, Vector2 posDelta)
    {
        Region localRegion = scene.Map.Regions[scene.NewMapRegionIndex(new Vector2(posDelta.x, 0))];
        Cell dCell = localRegion.Cells[scene.RegionCellIndex(new Vector2(posDelta.x, 0), localRegion)];
        return dCell.Type;
    }

    private static bool NewCoordYIsOpen(this Scene scene, Vector2 posDelta)
    {
        Region localRegion = scene.Map.Regions[scene.NewMapRegionIndex(new Vector2(0, posDelta.y))];
        Cell dCell = localRegion.Cells[scene.RegionCellIndex(new Vector2(0, posDelta.y), localRegion)];
        return dCell.Type == CellType.OpenSea || dCell.Type == CellType.Center;
    }

    private static bool NewCoordXIsOpen(this Scene scene, Vector2 posDelta)
    {
        Region localRegion = scene.Map.Regions[scene.NewMapRegionIndex(new Vector2(posDelta.x, 0))];
        Cell dCell = localRegion.Cells[scene.RegionCellIndex(new Vector2(posDelta.x, 0), localRegion)];
        return dCell.Type == CellType.OpenSea || dCell.Type == CellType.Center;
    }

    private static void UpdateMapObjects(Scene scene)
    {
        var localRegions = scene.Map.AdjacentRegions(scene.Ship);

        for (int x = -1; x < scene.Board.Size + 1; x++)
            for (int y = -1; y < scene.Board.Size + 1; y++)
                ShowObject(x, y);

        void ShowObject(int x, int y)
        {
            Vector2Int offsetGlobalCoord = scene.BoardOffsetGlobalCoords(x, y);
            Cell cell = scene.GetCellFromBoardLoc(x, y, offsetGlobalCoord);

            if (IsBorder(x, y, scene, offsetGlobalCoord, cell, localRegions)) return;

            UpdateCellObject(scene, cell, offsetGlobalCoord);

            foreach (Region region in localRegions)
                foreach (NPCShip npc in region.NPCs)
                    if (ShipCollisionDetection(scene, npc, offsetGlobalCoord))
                        break;
        }
    }

    private static void UpdateCellObject(Scene scene, Cell cell, Vector2Int offsetGlobalCoord)
    {
        if (cell.GO == null)
        {
            switch (cell.Type)
            {
                case CellType.Rocks:
                    if (scene.UnusedRocks.Count > 0)
                    {
                        scene.UsedRocks.Add(scene.UnusedRocks[0]);
                        cell.GO = scene.UnusedRocks[0];
                        cell.GO.SetActive(true);
                        scene.UnusedRocks.RemoveAt(0);
                    }
                    else
                    {
                        cell.GO = Assets.Rocks;
                        cell.GO.transform.SetParent(Scene.Io.TheSea.transform);
                        scene.UsedRocks.Add(cell.GO);
                    }
                    break;
            }
        }
        if (cell.GO != null)
        {
            Region localRegion = scene.RegionFromOffsetGlobalCoord(offsetGlobalCoord);

            Vector3 rockPos = new(
                (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cell.Coord).x - scene.Ship.GlobalPos.x).Smod(scene.Map.GlobalSize),
                -.5f,
                (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cell.Coord).y - scene.Ship.GlobalPos.y).Smod(scene.Map.GlobalSize));

            cell.GO.transform.SetPositionAndRotation(rockPos,
                Quaternion.Euler(new Vector3(0, cell.RotY, 0)));
        }
    }

    private static bool IsBorder(int x, int y, Scene scene, Vector2Int offsetGlobalCoord, Cell cell, Region[] localRegions)
    {
        if (x == -1 || y == -1 || x == scene.Board.Size || y == scene.Board.Size)
        {
            DeactivateCellBorderObject(scene, cell);

            foreach (Region region in localRegions)
                foreach (NPCShip npc in region.NPCs)
                    if (DeactivateNPCBorderObject(scene, npc, offsetGlobalCoord))
                        return true;

            return true;
        }
        return false;
    }

    private static bool DeactivateNPCBorderObject(Scene scene, NPCShip npc, Vector2Int offsetGlobalCoord)
    {
        if (npc.GlobalCoords == offsetGlobalCoord)
        {
            if (npc.GO != null)
            {
                scene.UsedShips.Remove(npc.GO);
                scene.RockTheBoat.RemoveBoat(npc.GO.transform);
                scene.UnusedShips.Add(npc.GO);
                npc.GO.SetActive(false);
                npc.GO = null;
            }
            return true;
        }
        return false;
    }

    private static void DeactivateCellBorderObject(Scene scene, Cell cell)
    {
        if (cell.GO != null)
        {
            scene.UsedRocks.Remove(cell.GO);
            scene.UnusedRocks.Add(cell.GO);
            cell.GO.SetActive(false);
            cell.GO = null;
        }
    }

    private static bool ShipCollisionDetection(Scene scene, NPCShip npc, Vector2Int offsetGlobalCoord)
    {
        if (npc.GlobalCoords != offsetGlobalCoord) return false;

        if (npc.GO == null)
        {
            if (scene.UnusedShips.Count > 0)
            {
                npc.GO = scene.UnusedShips[0];
                scene.UsedShips.Add(npc.GO);
                scene.RockTheBoat.AddBoat(npc.GO.transform, npc.Sway);
                scene.UnusedShips.RemoveAt(0);
                npc.GO.SetActive(true);
            }
            else
            {
                npc.GO = Assets.Schooner;
                npc.GO.transform.localScale = Vector3.one * .6f;
                npc.GO.transform.SetParent(Scene.Io.TheSea.transform);
                scene.RockTheBoat.AddBoat(npc.GO.transform, npc.Sway);
                scene.UsedShips.Add(npc.GO);
            }
        }

        if (npc.GO != null)
        {
            npc.GO.transform.SetPositionAndRotation(
                new Vector3(
                    (scene.Board.Center() + .3f + npc.GlobalPos.x - scene.Ship.GlobalPos.x).Smod(scene.Map.GlobalSize),
                    .2f,
                    (scene.Board.Center() + .3f + npc.GlobalPos.y - scene.Ship.GlobalPos.y).Smod(scene.Map.GlobalSize)),
                Quaternion.Euler(new Vector3(0, npc.RotY, 0)));
        }

        return true;
    }

    public static void UpdateNPCShips(this Scene scene)
    {
        var localRegions = scene.Map.AdjacentRegions(scene.Ship);
        foreach (Region region in localRegions)
        {
            // if (scene.DebugTimer > scene.DebugInterval) Debug.Log(region.NPCs.Length + " " + region.Coord);

            foreach (NPCShip npc in region.NPCs)
            {
                int pathDirection = npc.PathDirection ? 1 : -1;

                Vector2 dir = npc.PatrolPath[npc.PatrolIndex] - npc.LocalCoords;
                dir.Normalize();
                Vector2 posDelta = new(Time.fixedDeltaTime * npc.MoveSpeed * dir.x, Time.fixedDeltaTime * npc.MoveSpeed * dir.y);

                if ((npc.GlobalCoords + posDelta).IsPOM(1, scene.Ship.GlobalPos)) continue;

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
    }

    public static Region RegionFromOffsetGlobalCoord(this Scene scene, Vector2Int offsetV2i) =>
        scene.Map.Regions[scene.Map.RegionIndexFromGlobalCoord(offsetV2i)];

    public static Cell GetCellFromBoardLoc(this Scene scene, int x, int y, Vector2Int offsetGlobalCoord)
    {
        Region region = scene.RegionFromOffsetGlobalCoord(offsetGlobalCoord);
        return region.Cells[region.CellIndex(scene.OffsetLocalCoords(x, y))];
    }

    public static Cell GetCellFromBoardLoc(this Scene scene, Vector2Int v2i)
    {
        Region region = scene.Map.Regions[scene.Map.RegionIndexFromGlobalCoord(scene.Ship.GlobalCoord + v2i)];
        return region.Cells[region.CellIndex(scene.OffsetLocalCoords(v2i))];
    }

    public static Vector2Int OffsetLocalCoords(this Scene scene, int x, int y)
    {
        Vector2Int v2i = new(x, y);
        return scene.OffsetLocalCoords(v2i);
    }

    public static Vector2Int OffsetLocalCoords(this Scene scene, Vector2Int boardLoc) =>
            (boardLoc + scene.Ship.LocalCoord(scene.Map.RegionSize) - scene.Board.CenterV2i()).Smod(scene.Map.RegionSize);

    public static Vector2Int BoardOffsetGlobalCoords(this Scene scene, int x, int y) =>
        scene.BoardOffsetGlobalCoords(new Vector2Int(x, y));

    public static Vector2Int BoardOffsetGlobalCoords(this Scene scene, Vector2Int v2i)
    {
        Vector2 v2 = v2i + scene.Ship.GlobalCoord - scene.Board.CenterV2i();
        return new Vector2Int((int)v2.x, (int)v2.y).Smod(scene.Map.GlobalSize);
    }

    public static int OffsetRegionCellIndex(this Scene scene, int x, int z) => new Vector2(
            (x + scene.Ship.GlobalPos.x - scene.Board.Center()).Smod(scene.Map.RegionSize),
            (z + scene.Ship.GlobalPos.y - scene.Board.Center()).Smod(scene.Map.RegionSize))
        .Vec2ToInt(scene.Map.RegionSize);

    public static int NewMapRegionIndex(this Scene scene, Vector2 posDelta) =>
       new Vector2Int((int)((posDelta + scene.Ship.GlobalPos).x.Smod(scene.Map.GlobalSize) / scene.Map.RegionSize),
                      (int)((posDelta + scene.Ship.GlobalPos).y.Smod(scene.Map.GlobalSize) / scene.Map.RegionSize))
        .Vec2ToInt(scene.Map.Size);

    public static int RegionCellIndex(this Scene scene, Vector2 deltaPos, Region region) =>
        (deltaPos + scene.Ship.LocalCoord(region.Size)).Vec2ToInt(region.Size);

    public static int BoardIndex(this Scene scene, int x, int y) =>
        new Vector2(x, y).Vec2ToInt(scene.Board.Size);

    public static bool IsInBounds(this Scene scene, int x, int y) =>
        x + scene.Ship.GlobalCoord.x - scene.Board.Center() > -1 &&
        x + scene.Ship.GlobalCoord.x - scene.Board.Center() < scene.Map.RegionSize &&
        y + scene.Ship.GlobalCoord.y - scene.Board.Center() > -1 &&
        y + scene.Ship.GlobalCoord.y - scene.Board.Center() < scene.Map.RegionSize;

    public static bool IsInBounds(this Scene scene, float x, float y) =>
        x + scene.Ship.GlobalPos.x - scene.Board.Center() > -1 &&
        x + scene.Ship.GlobalPos.x - scene.Board.Center() < scene.Map.RegionSize &&
        y + scene.Ship.GlobalPos.y - scene.Board.Center() > -1 &&
        y + scene.Ship.GlobalPos.y - scene.Board.Center() < scene.Map.RegionSize;

    public static void SetUpSeaCam(this Scene scene)
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

    public static Color GetRandomSeaColor(this Scene _) => UnityEngine.Random.Range(0, 7) switch
    {
        1 => MyBlue,
        2 => MyCyan,
        3 => MyGreen,
        4 => MyYellow,
        5 => MyRed,
        6 => MyMagenta,
        _ => MyGrey
    };


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

