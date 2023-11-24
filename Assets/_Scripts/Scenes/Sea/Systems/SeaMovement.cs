using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//TODO
//Roaming ships, separate ship type from mapTile, have list of ships, store A* paths to nodes and patrol
//Inspect mode: overlay to inspect ships
//fix state to allow disabling of map.
// //circular map, and fog extensions?? (probably not)
//pan camera up to sky and overlay puzzle or batterie
//create battery scene with ships
//create VFX for cannons
//dialogs for before and after battles, describing spoils etc

public static class Ship_Movement
{
    public static event Action SailingStart;
    public static void StartSailing() => SailingStart?.Invoke();

    public static event Action SailingFinished;
    public static void EndSailing() => SailingFinished?.Invoke();

    public static Vector3 DirectionPressed(this SeaScene sea, Vector3 dir)
    {
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
                    posDelta.z *= .95f;
                    sea.DayCounterText.TextString = "...";
                    sea.NearNPCShip = npc;
                    noNearNPCs = false;

                    CapsuleCollider npcBox = npc.GO.GetComponent<CapsuleCollider>();
                    Rigidbody rb = npc.GO.GetComponent<Rigidbody>();

                    Vector3 a = npcBox.ClosestPoint(sea.Ship.GO.transform.position);
                    Vector3 b = sea.Ship.CapsuleCollider.ClosestPoint(npc.GO.transform.position);
                    Vector3 a2 = npcBox.ClosestPoint(b);
                    Vector3 b2 = sea.Ship.CapsuleCollider.ClosestPoint(a);

                    if (Vector3.Distance(a2, b2) < .2f)
                    {
                        sea.DayCounterText.TextString = "!!!";
                        posDelta.z *= .1f;
                    }
                }
            }
            if (noNearNPCs) sea.NearNPCShip = null;

            if (sea.IsInBounds(posDelta.x, posDelta.z) &&
               sea.Map[sea.MapIndex(posDelta.x, posDelta.z)].Type == SeaMapTileType.OpenSea)
            {
                sea.Ship.Pos += posDelta;
                sea.Swells.Offset += (posDelta.z + posDelta.x) * .45f;
            }

            else if (sea.IsInBounds(0, posDelta.z) &&
                sea.Map[sea.MapIndex(0, posDelta.z)].Type == SeaMapTileType.OpenSea)
            {
                dir.z = Mathf.Clamp(dir.z, -.2f, .2f);
                sea.Ship.Pos += new Vector3(0, 0, posDelta.z);
                sea.Swells.Offset += posDelta.z;
            }

            else if (sea.IsInBounds(posDelta.x, 0) &&
                sea.Map[sea.MapIndex(posDelta.x, 0)].Type == SeaMapTileType.OpenSea)
            {
                dir.z = Mathf.Clamp(dir.z, -.2f, .2f);
                sea.Ship.Pos += new Vector3(posDelta.x, 0, 0);
                sea.Swells.Offset += posDelta.x;
            }
        }

        ShowObjects(sea);
        sea.MoveNPCShips();
        return dir;
    }

    private static void ShowObjects(SeaScene sea)
    {
        foreach (SeaGridTile tile in sea.Board)
        {
            tile.GO.transform.position = new Vector3(
            (float)((((float)(tile.Loc.x - sea.BoardSize - sea.Ship.Pos.x) % sea.BoardSize) + sea.BoardSize)),
            0,
            (float)(((float)((tile.Loc.z - sea.BoardSize - sea.Ship.Pos.z) % sea.BoardSize) + sea.BoardSize)));

        }

        for (int x = -1; x < sea.BoardSize + 1; x++)
            for (int z = -1; z < sea.BoardSize + 1; z++)
            {
                if (!sea.IsInBounds(x, z)) continue;
                var coord = sea.OffsetMapCoords(x, z);

                if (x == -1 || z == -1 || x == sea.BoardSize || z == sea.BoardSize)
                {
                    if (sea.Map[sea.OffsetMapIndex(x, z)].GO != null)
                    {
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
                                sea.UnusedShips.Add(ship.GO);
                                ship.GO.SetActive(false);
                                ship.GO = null;
                            }
                        }
                    }

                    continue;
                }

                if (sea.Map[sea.OffsetMapIndex(x, z)].GO == null)
                {
                    switch (sea.Map[sea.OffsetMapIndex(x, z)].Type)
                    {
                        case SeaMapTileType.Rocks:
                            if (sea.UnusedRocks.Count > 0)
                            {
                                // Debug.Log("Reactivating rocks: of " + sea.UnusedRocks.Count);
                                sea.Map[sea.OffsetMapIndex(x, z)].GO = sea.UnusedRocks[0];
                                sea.UnusedRocks.RemoveAt(0);
                                sea.Map[sea.OffsetMapIndex(x, z)].GO.SetActive(true);
                            }
                            else sea.Map[sea.OffsetMapIndex(x, z)].GO = Assets.Rocks;
                            break;
                            //         SeaMapTileType.Rocks => sea.UnusedRocks.Count > 0 ? sea.UnusedRocks[,
                            //         _ => null,
                            //  };
                    }
                }

                if (sea.Map[sea.OffsetMapIndex(x, z)].GO != null)
                {
                    sea.Map[sea.OffsetMapIndex(x, z)].GO.transform.position = sea.Map[sea.OffsetMapIndex(x, z)].Loc - sea.Ship.Pos + sea.BoardCenter + new Vector3(.75f, -.5f, .75f);
                    sea.Map[sea.OffsetMapIndex(x, z)].GO.transform.rotation = Quaternion.Euler(
                         new Vector3(0, sea.Map[sea.OffsetMapIndex(x, z)].RotY, 0));  // Debug.Log("Rocks: " + sea.Map[sea.OffsetMapIndex(x, z)].GO.transform.position + "; " + sea.Ship.Pos + " " + sea.Map[sea.OffsetMapIndex(x, z)].Coord);
                }

                foreach (NPCShip npc in sea.NPCShips)
                {
                    if (npc.Coords == coord)
                    {
                        if (npc.GO == null)
                        {
                            if (sea.UnusedShips.Count > 0)
                            {
                                npc.GO = sea.UnusedShips[0];
                                sea.UnusedShips.RemoveAt(0);
                                npc.GO.SetActive(true);
                            }
                            else
                            {
                                npc.GO = Assets.Schooner;
                            }
                        }

                        if (npc.GO != null)
                        {
                            npc.GO.transform.position = npc.Pos - sea.Ship.Pos + new Vector3(sea.BoardCenter.x + (1 * .3f), .1f, sea.BoardCenter.z + (1 * .3f));
                            npc.GO.transform.rotation = Quaternion.Euler(new Vector3(0, npc.RotY, 0));

                            // Debug.Log(sea.Ship.Pos + " " + npc.Pos);
                        }
                    }
                }



            }


    }



    #region INTERNAL

    public static Vector3Int OffsetMapCoords(this SeaScene sea, int x, int z) =>
        new Vector3Int(x + sea.Ship.Coord.x - sea.BoardOffset, 0, z + sea.Ship.Coord.z - sea.BoardOffset);

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


    #endregion


    public static void MoveNPCShips(this SeaScene Sea)
    {
        for (int i = 0; i < Sea.NPCShips.Count; i++)
        {
            // if (Sea.NPCShips[i].PathDirection &&
            //     Sea.NPCShips[i].PatrolIndex >= Sea.NPCShips[i].PatrolPath.Length - 1)
            //     Sea.NPCShips[i].PathDirection = false;
            // else if (!Sea.NPCShips[i].PathDirection &&
            //          Sea.NPCShips[i].PatrolIndex <= 0)
            //     Sea.NPCShips[i].PathDirection = true;

            int pd = Sea.NPCShips[i].PathDirection ? 1 : -1;

            while (pd + Sea.NPCShips[i].PatrolIndex > Sea.NPCShips[i].PatrolPath.Length - 1) Sea.NPCShips[i].PatrolIndex--;
            while (pd + Sea.NPCShips[i].PatrolIndex < 0) Sea.NPCShips[i].PatrolIndex++;

            var dir = Sea.NPCShips[i].PatrolPath[Sea.NPCShips[i].PatrolIndex % Sea.NPCShips[i].PatrolPath.Length] - Sea.NPCShips[i].Coords;
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
                _ when dir == Vector3Int.forward => !Mathf.DeltaAngle(Sea.NPCShips[i].RotY, 180).IsPOM(1, 0),
                _ when dir == Vector3Int.back => !Mathf.DeltaAngle(Sea.NPCShips[i].RotY, 0).IsPOM(1, 0),
                _ when dir == Vector3Int.right => !Mathf.DeltaAngle(Sea.NPCShips[i].RotY, 270).IsPOM(1, 0),
                _ when dir == Vector3Int.left => !Mathf.DeltaAngle(Sea.NPCShips[i].RotY, 90).IsPOM(1, 0),
                _ => false,
            };

            if (turning)
            {
                Sea.NPCShips[i].NewRotY = dir switch
                {
                    _ when dir == Vector3Int.forward => 180,
                    _ when dir == Vector3Int.back => 0,
                    _ when dir == Vector3Int.right => 270,
                    _ when dir == Vector3Int.left => 90,
                    _ => Sea.NPCShips[i].NewRotY,
                };

                Sea.NPCShips[i].RotY += Mathf.Sign(Mathf.DeltaAngle(Sea.NPCShips[i].RotY, Sea.NPCShips[i].NewRotY));
                // Debug.Log(Sea.Ship.Pos + " " + Sea.NPCShips[i].Pos + " " + Sea.NPCShips[i].RotY + " " + Sea.NPCShips[i].NewRotY + " " + Mathf.DeltaAngle(Sea.NPCShips[i].RotY, Sea.NPCShips[i].NewRotY));
            }

            if (!waitForOtherShips)
            {
                Sea.NPCShips[i].StuckDelta = 0;
                Sea.NPCShips[i].Pos += posDelta;
            }

            if (Sea.NPCShips[i].Coords == Sea.NPCShips[i].PatrolPath[Sea.NPCShips[i].PatrolIndex])
            {
                Sea.NPCShips[i].PatrolIndex += pd;
            }
        }
    }

}

static class Helpers
{
    // internal static float ChanceToFindIslandLoc(this SeaScene Scene) =>
    //  (5 + (Scene.StartingShips - Scene.NumOfCardShips)) / Scene.NumOfCardShips;



    /// <summary>
    /// a is ± 1 of b
    /// </summary>
    public static bool IsPM1(this float a, float b)
    {
        return a < b + 1 && a > b - 1;
    }


    /// <summary>
    /// a is ± n of b
    /// </summary>
    public static bool IsPOM(this float a, float n, float b)
    {
        return a < b + n && a > b - n;
    }


    /// <summary>
    /// A grid positions listed index.
    /// </summary>
    /// <param name="vector2">Vector2 grid position</param>
    /// <returns>(x * height) + y</returns>
    public static int Vec2ToInt(this Vector2 gridPosition, int boardSize)
    { return ((int)gridPosition.x * boardSize) + (int)gridPosition.y; }


    /// <summary>
    /// A grid positions listed index.
    /// </summary>
    /// <param name="vector2">Vector2 grid position</param>
    /// <returns>(x * height) + y</returns>
    public static int Vec2ToInt(this Vector2Int gridPosition, int boardSize)
    { return (gridPosition.x * boardSize) + gridPosition.y; }

    /// <summary>
    /// A grid positions listed index.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>(x * height) + y</returns>
    public static int Vec2ToInt(int x, int y, int boardSize)
    { return (x * boardSize) + y; }
    //TODOThis really should be (X * height) + Y


    /// <summary>
    /// For flat maps in Unity3D. Uses X and Z and ignores the Y axis. Assumes an even square grid.
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <param name="boardSize"></param>
    /// <returns>(x * boardSize.z) + z</returns>
    public static int Vec3ToInt(this Vector3Int gridPosition, int boardSize) =>
        ((int)gridPosition.x * boardSize) + (int)gridPosition.z;


    /// <summary>
    /// Always returns positive remainder.
    /// </summary>
    public static int SignedMod(this int a, int b)
    {
        b *= b < 0 ? -1 : 1;
        var x = a % b;
        x += x < 0 ? b : 0;
        return x;
    }
    /// <summary>
    /// Always returns positive remainder.
    /// </summary>
    public static float SignedMod(this float a, float b)
    {
        b *= b < 0 ? -1 : 1;
        var x = a % b;
        x += x < 0 ? b : 0;
        return x;
    }
}
