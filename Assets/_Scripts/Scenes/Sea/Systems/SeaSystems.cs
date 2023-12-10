using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//TODO
//Inspect mode: overlay to inspect ships
//fix state to allow disabling of map.
//dialogs for before and after battles, describing spoils etc

public static class SeaSystems
{
    public static event Action SailingStart;
    public static void StartSailing() => SailingStart?.Invoke();

    public static event Action SailingFinished;
    public static void EndSailing() => SailingFinished?.Invoke();

    public static Vector3 UpdateMap(this SeaScene sea, Vector3 dir)
    {
        sea.UpdateNPCShips();
        UpdateMapObjects(sea);
        var newV3 = UpdateShipPos(sea, dir);
        return newV3;
    }

    private static Vector3 UpdateShipPos(this SeaScene sea, Vector3 dir)
    {
        sea.Ship.Bark.GO.SetActive(false);
        if (dir.x != 0) sea.Ship.Parent.transform.Rotate(new Vector3(0, dir.x * Time.fixedDeltaTime * sea.Ship.RotateSpeed, 0));

        if (dir.z != 0)
        {
            Vector3 posDelta = Time.fixedDeltaTime * dir.z * sea.Ship.MoveSpeed * sea.Ship.GO.transform.forward;

            bool noNearNPCs = true;

            foreach (NPCShip npc in sea.NPCShips)
            {
                if ((sea.Ship.Pos.x + posDelta.x).IsPOM(1f, npc.Pos.x) &&
                    (sea.Ship.Pos.z + posDelta.z).IsPOM(1f, npc.Pos.z))
                {
                    posDelta.z *= .9f;
                    // sea.DayCounterText.TextString = "...";
                    sea.Ship.Bark.GO.SetActive(true);
                    sea.NearNPCShip = npc;
                    noNearNPCs = false;

                    CapsuleCollider npcCol = npc.GO.GetComponent<CapsuleCollider>();

                    Vector3 b = npcCol.ClosestPoint(sea.Ship.GO.transform.position);
                    Vector3 a = sea.Ship.CapsuleCollider.ClosestPoint(npc.GO.transform.position);
                    Vector3 b2 = npcCol.ClosestPoint(b);
                    Vector3 a2 = sea.Ship.CapsuleCollider.ClosestPoint(a);
                    Vector3 b3 = npcCol.ClosestPoint(b2);
                    Vector3 a3 = sea.Ship.CapsuleCollider.ClosestPoint(a2);

                    if (Vector3.Distance(a3, b3) < .2f)
                    {
                        // sea.DayCounterText.TextString = "!!!";
                        if (b3.x - a3.x > b3.z - a3.z)
                            posDelta.z *= Mathf.Clamp(sea.NearNPCShip.GO.transform.position.z - sea.Ship.GO.transform.position.z, 0.1f, 1);
                        else posDelta.x *= Mathf.Clamp(sea.NearNPCShip.GO.transform.position.x - sea.Ship.GO.transform.position.x, 0.1f, 1);
                    }
                }
            }
            if (noNearNPCs) sea.NearNPCShip = null;

            if (sea.IsInBounds(posDelta.x, posDelta.z) &&
               (sea.Map[sea.MapIndex(posDelta.x, posDelta.z)].Type == SeaMapTileType.OpenSea ||
                sea.Map[sea.MapIndex(posDelta.x, posDelta.z)].Type == SeaMapTileType.Center))
            {
                sea.Ship.Pos += posDelta;
                sea.Swells.Offset += (posDelta.z + posDelta.x) * .45f;
            }

            else if (sea.IsInBounds(0, posDelta.z) &&
                    (sea.Map[sea.MapIndex(0, posDelta.z)].Type == SeaMapTileType.OpenSea ||
                     sea.Map[sea.MapIndex(posDelta.x, posDelta.z)].Type == SeaMapTileType.Center))
            {
                dir.z = Mathf.Clamp(dir.z, -.2f, .2f);
                sea.Ship.Pos += new Vector3(0, 0, posDelta.z);
                sea.Swells.Offset += posDelta.z;
            }

            else if (sea.IsInBounds(posDelta.x, 0) &&
                    (sea.Map[sea.MapIndex(posDelta.x, 0)].Type == SeaMapTileType.OpenSea ||
                     sea.Map[sea.MapIndex(posDelta.x, posDelta.z)].Type == SeaMapTileType.Center))
            {
                dir.z = Mathf.Clamp(dir.z, -.2f, .2f);
                sea.Ship.Pos += new Vector3(posDelta.x, 0, 0);
                sea.Swells.Offset += posDelta.x;
            }
        }
        return dir;
    }

    private static void UpdateMapObjects(SeaScene sea)
    {
        foreach (SeaGridTile tile in sea.Board)
        {
            tile.GO.transform.position = new Vector3(
                (float)(((tile.Loc.x - sea.BoardSize - sea.Ship.Pos.x) % sea.BoardSize) + sea.BoardSize),
                0,
                (float)(((tile.Loc.z - sea.BoardSize - sea.Ship.Pos.z) % sea.BoardSize) + sea.BoardSize));
        }

        for (int x = -1; x < sea.BoardSize + 1; x++)
            for (int z = -1; z < sea.BoardSize + 1; z++)
                ShowObject(x, z);

        void ShowObject(int x, int z)
        {
            if (!sea.IsInBounds(x, z)) return;
            var coord = sea.OffsetMapCoords(x, z);
            if (x == -1 || z == -1 || x == sea.BoardSize || z == sea.BoardSize)
            {
                if (sea.Map[sea.OffsetMapIndex(x, z)].GO != null)
                {
                    sea.UsedRocks.Remove(sea.Map[sea.OffsetMapIndex(x, z)].GO);
                    sea.UnusedRocks.Add(sea.Map[sea.OffsetMapIndex(x, z)].GO);
                    sea.Map[sea.OffsetMapIndex(x, z)].GO.SetActive(false);
                    sea.Map[sea.OffsetMapIndex(x, z)].GO = null;
                }

                foreach (NPCShip ship in sea.NPCShips)
                {
                    if (ship.Coords == coord)
                    {
                        if (ship.GO != null)
                        {
                            sea.UsedShips.Remove(ship.GO);
                            sea.RockTheBoat.RemoveBoat(ship.GO.transform);
                            sea.UnusedShips.Add(ship.GO);
                            ship.GO.SetActive(false);
                            ship.GO = null;
                        }
                        continue;
                    }
                }

                return;
            }

            if (sea.Map[sea.OffsetMapIndex(x, z)].GO == null)
            {
                switch (sea.Map[sea.OffsetMapIndex(x, z)].Type)
                {
                    case SeaMapTileType.Rocks:
                        if (sea.UnusedRocks.Count > 0)
                        {
                            sea.UsedRocks.Add(sea.UnusedRocks[0]);
                            sea.Map[sea.OffsetMapIndex(x, z)].GO = sea.UnusedRocks[0];
                            sea.UnusedRocks.RemoveAt(0);
                            sea.Map[sea.OffsetMapIndex(x, z)].GO.SetActive(true);
                        }
                        else
                        {
                            sea.Map[sea.OffsetMapIndex(x, z)].GO = Assets.Rocks;
                            sea.Map[sea.OffsetMapIndex(x, z)].GO.transform.SetParent(SeaScene.Io.TheSea.transform);
                            sea.UsedRocks.Add(sea.Map[sea.OffsetMapIndex(x, z)].GO);
                        }
                        break;
                }
            }

            if (sea.Map[sea.OffsetMapIndex(x, z)].GO != null)
                sea.Map[sea.OffsetMapIndex(x, z)].GO.transform.SetPositionAndRotation(
                    new Vector3(
                        sea.Map[sea.OffsetMapIndex(x, z)].Loc.x - sea.Ship.Pos.x + sea.BoardCenter.x + .75f,
                        -.5f,
                        sea.Map[sea.OffsetMapIndex(x, z)].Loc.z - sea.Ship.Pos.z + sea.BoardCenter.z + .75f),
                    Quaternion.Euler(new Vector3(0, sea.Map[sea.OffsetMapIndex(x, z)].RotY, 0)));

            foreach (NPCShip npc in sea.NPCShips)
            {
                if (npc.Coords == coord)
                {
                    if (npc.GO == null)
                    {
                        if (sea.UnusedShips.Count > 0)
                        {
                            npc.GO = sea.UnusedShips[0];
                            sea.UsedShips.Add(npc.GO);
                            sea.RockTheBoat.AddBoat(npc.GO.transform, npc.Sway);
                            sea.UnusedShips.RemoveAt(0);
                            npc.GO.SetActive(true);
                        }
                        else
                        {
                            npc.GO = Assets.Schooner;
                            npc.GO.transform.SetParent(SeaScene.Io.TheSea.transform);
                            sea.RockTheBoat.AddBoat(npc.GO.transform, npc.Sway);
                            sea.UsedShips.Add(npc.GO);
                        }
                    }

                    if (npc.GO != null)
                        npc.GO.transform.SetPositionAndRotation(
                            npc.Pos - sea.Ship.Pos + new Vector3(sea.BoardCenter.x + (1 * .3f), .1f, sea.BoardCenter.z + (1 * .3f)),
                            Quaternion.Euler(new Vector3(0, npc.RotY, 0)));

                    break;
                }
            }
        }
    }

    public static void UpdateNPCShips(this SeaScene Sea)
    {
        for (int i = 0; i < Sea.NPCShips.Count; i++)
        {
            int pathDirection = Sea.NPCShips[i].PathDirection ? 1 : -1;

            Vector3Int dir = Sea.NPCShips[i].PatrolPath[Sea.NPCShips[i].PatrolIndex] - Sea.NPCShips[i].Coords;
            Vector3 posDelta = Time.deltaTime * Sea.NPCShips[i].MoveSpeed * (Vector3)dir;

            if ((Sea.NPCShips[i].Pos.x + posDelta.x).IsPOM(.6f, Sea.Ship.Pos.x) &&
                    (Sea.NPCShips[i].Pos.z + posDelta.z).IsPOM(.6f, Sea.Ship.Pos.z))
                continue;

            bool waitForOtherShips = false;

            for (int ii = 0; ii < Sea.NPCShips.Count; ii++)
            {
                if (Sea.NPCShips[ii].Coords == Sea.NPCShips[i].PatrolPath[Sea.NPCShips[i].PatrolIndex])
                {
                    waitForOtherShips = true;
                    Sea.NPCShips[i].StuckDelta += Time.deltaTime;
                    break;
                }
            }

            if (waitForOtherShips && Sea.NPCShips[i].StuckDelta >= Sea.NPCShips[i].StuckTimer)
            {
                Sea.NPCShips[i].PathDirection = !Sea.NPCShips[i].PathDirection;
                Sea.NPCShips[i].PatrolIndex += Sea.NPCShips[i].PathDirection ? 1 : -1;
                Sea.NPCShips[i].StuckDelta = 0;
                continue;
            }

            bool turning = dir switch
            {
                _ when dir == Vector3Int.forward => !Mathf.DeltaAngle(Sea.NPCShips[i].RotY, 0).IsPOM(1, 0),
                _ when dir == Vector3Int.back => !Mathf.DeltaAngle(Sea.NPCShips[i].RotY, 180).IsPOM(1, 0),
                _ when dir == Vector3Int.right => !Mathf.DeltaAngle(Sea.NPCShips[i].RotY, 90).IsPOM(1, 0),
                _ when dir == Vector3Int.left => !Mathf.DeltaAngle(Sea.NPCShips[i].RotY, 270).IsPOM(1, 0),
                _ => false,
            };

            if (turning)
            {
                Sea.NPCShips[i].NewRotY = dir switch
                {
                    _ when dir == Vector3Int.forward => 0,
                    _ when dir == Vector3Int.back => 180,
                    _ when dir == Vector3Int.right => 90,
                    _ when dir == Vector3Int.left => 270,
                    _ => Sea.NPCShips[i].NewRotY,
                };

                Sea.NPCShips[i].RotY += Mathf.Sign(Mathf.DeltaAngle(Sea.NPCShips[i].RotY, Sea.NPCShips[i].NewRotY));
            }

            if (!waitForOtherShips)
            {
                Sea.NPCShips[i].StuckDelta = 0;
                Sea.NPCShips[i].Pos += posDelta;
            }

            if (Sea.NPCShips[i].Coords == Sea.NPCShips[i].PatrolPath[Sea.NPCShips[i].PatrolIndex])
            {
                Sea.NPCShips[i].PatrolIndex += pathDirection;
            }
        }
    }

    public static Vector3Int OffsetMapCoords(this SeaScene sea, int x, int z) =>
        new(x + sea.Ship.Coord.x - sea.BoardOffset, 0, z + sea.Ship.Coord.z - sea.BoardOffset);

    public static int OffsetMapIndex(this SeaScene sea, int x, int z) =>
        new Vector2(x + sea.Ship.Pos.x - sea.BoardOffset, z + sea.Ship.Pos.z - sea.BoardOffset)
        .Vec2ToInt(sea.MapSize);

    public static int MapIndex(this SeaScene sea, float x, float z) =>
        new Vector2(x + sea.Ship.Pos.x, z + sea.Ship.Pos.z)
        .Vec2ToInt(sea.MapSize);

    public static int BoardIndex(this SeaScene sea, int x, int z) => new Vector2(x, z).Vec2ToInt(sea.BoardSize);

    public static bool IsInBounds(this SeaScene sea, int x, int z) => x + sea.Ship.Coord.x - sea.BoardOffset > -1 && x + sea.Ship.Coord.x - sea.BoardOffset < sea.MapSize &&
                                       z + sea.Ship.Coord.z - sea.BoardOffset > -1 && z + sea.Ship.Coord.z - sea.BoardOffset < sea.MapSize;

    public static bool IsInBounds(this SeaScene sea, float x, float z) => x + sea.Ship.Pos.x - sea.BoardOffset > -1 && x + sea.Ship.Pos.x - sea.BoardOffset < sea.MapSize &&
                                       z + sea.Ship.Pos.z - sea.BoardOffset > -1 && z + sea.Ship.Pos.z - sea.BoardOffset < sea.MapSize;



}

