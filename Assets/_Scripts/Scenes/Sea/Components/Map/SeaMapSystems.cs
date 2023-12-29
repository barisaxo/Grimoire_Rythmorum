using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;

public static class SeaMapSystems
{
    public static bool IsInRange(this Scene sea, Vector3Int v) =>
    v.x > -1 && v.x < sea.Map.RegionSize && v.z > -1 && v.z < sea.Map.RegionSize;

    // public static int RegionIndex(this Map map, int x, int y) =>
    //     new Vector2(x, y).Vec2ToInt(map.RegionSize);

    public static int RegionIndexFromGlobalCoord(this Map map, Vector2Int globalCoord) =>
      new Vector2Int(globalCoord.x / map.RegionSize, globalCoord.y / map.RegionSize).Vec2ToInt(map.Size);

    public static int CellIndex(this Region region, Vector2Int localCoord) =>
        localCoord.Vec2ToInt(region.Size);

    public static bool IsCellOpen(this Region region, Vector2Int tile)
    {
        if (region.Cells[region.CellIndex(tile)].Type ==
            CellType.Rocks) return false;
        return true;
    }

    public static Region[] AdjacentRegions(this Map map, PlayerShip ship)
    {
        int upper = map.RegionSize - 7;
        List<Region> regions = new() { map.Regions[map.RegionIndexFromGlobalCoord(ship.GlobalCoord)] };
        int x = 0, y = 0;
        if (ship.LocalCoord(map.RegionSize).x < 7)
        {
            regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(
                  new Vector2Int((int)(ship.GlobalPos.x - map.RegionSize), (int)(ship.GlobalPos.y)).Smod(map.GlobalSize))]);
            x = -1;
        }
        else if (ship.LocalCoord(map.RegionSize).x > upper)
        {
            regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(
                  new Vector2Int((int)(ship.GlobalPos.x + map.RegionSize), (int)(ship.GlobalPos.y)).Smod(map.GlobalSize))]);
            x = +1;
        }
        if (ship.LocalCoord(map.RegionSize).y < 7)
        {
            regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(
                  new Vector2Int((int)(ship.GlobalPos.x), (int)(ship.GlobalPos.y - map.RegionSize)).Smod(map.GlobalSize))]);
            y = -1;
        }
        else if (ship.LocalCoord(map.RegionSize).y > upper)
        {
            regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(
                  new Vector2Int((int)(ship.GlobalPos.x), (int)(ship.GlobalPos.y + map.RegionSize)).Smod(map.GlobalSize))]);
            y = +1;
        }

        switch (x, y)
        {
            case (1, 1):
                regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(new Vector2Int(
                        (int)(ship.GlobalPos.x + map.RegionSize),
                        (int)(ship.GlobalPos.y + map.RegionSize))
                    .Smod(map.GlobalSize))]);
                break;
            case (1, -1):
                regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(new Vector2Int(
                        (int)(ship.GlobalPos.x + map.RegionSize),
                        (int)(ship.GlobalPos.y - map.RegionSize))
                    .Smod(map.GlobalSize))]);
                break;
            case (-1, 1):
                regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(new Vector2Int(
                        (int)(ship.GlobalPos.x - map.RegionSize),
                        (int)(ship.GlobalPos.y + map.RegionSize))
                    .Smod(map.GlobalSize))]);
                break;
            case (-1, -1):
                regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(new Vector2Int(
                        (int)(ship.GlobalPos.x - map.RegionSize),
                        (int)(ship.GlobalPos.y - map.RegionSize))
                    .Smod(map.GlobalSize))]);
                break;
        }

        // for (int x = -1; x < 2; x++)
        //     for (int y = -1; y < 2; y++)
        //         regions.Add(map.Regions[map.RegionIndex((ship.Region + new Vector2Int(x, y)).Smod(map.Size))]);
        return regions.ToArray();
    }

    // public static void InitializeMapSegments(this Map map)
    // {
    //     map.Regions = new Region[map.Size * map.Size];
    //     int i = 0;
    //     for (int x = 0; x < map.Size; x++)
    //         for (int y = 0; y < map.Size; y++)
    //         {
    //             map.Regions[i] = new Region(map.RegionSize);
    //             i++;
    //         }
    // }

    // public static Vector3Int[][] GetMapIndices(this SeaScene sea, int BoardSize)
    // {
    //     int index = 0;
    //     int segment = 0;
    //     Vector3Int[][] mapIndices = new Vector3Int[sea.Map.Size * sea.Map.Size][];

    //     for (int u = 0; u < sea.Map.RegionSize; u += BoardSize)
    //     {
    //         for (int v = 0; v < sea.Map.RegionSize; v += BoardSize)
    //         {
    //             mapIndices[segment] = MapSegment(u, v);
    //             segment++;
    //         }
    //     }

    //     return mapIndices;

    //     Vector3Int[] MapSegment(int u, int v)
    //     {
    //         List<Vector3Int> MapSegment = new();

    //         for (int x = 0; x < BoardSize; x++)
    //             for (int z = 0; z < BoardSize; z++)
    //             {
    //                 MapSegment.Add(new Vector3Int(u + x, index, v + z));
    //                 index++;
    //             }

    //         return MapSegment.ToArray();
    //     }
    // }


    // public static void AddMapFeatures(this SeaScene sea, int BoardSize)
    // {
    //     for (int u = 0; u < sea.Map.Size; u++)
    //         for (int v = 0; v < sea.Map.Size; v++)
    //         {
    //             Vector3Int[] locSegment = sea.Map.Regions[(u * sea.Map.Size) + v];
    //             SeaMapTile[] mapSegment = sea.GetTilesFromSegment(locSegment);

    //             for (int x = 0; x < BoardSize; x++)
    //                 for (int z = 0; z < BoardSize; z++)
    //                 {
    //                     int segmentIndex = (x * BoardSize) + z;
    //                     int mapIndex = new Vector2(locSegment[segmentIndex].x, locSegment[segmentIndex].z).Vec2ToInt(sea.Map.RegionSize);

    //                     sea.Map.Regions[mapIndex].Type = Random.Range(0, 10) switch
    //                     {
    //                         0 => SeaMapTileType.Rocks,
    //                         _ => SeaMapTileType.OpenSea,
    //                     };

    //                     if (ClearCenter(sea.Map.Regions[mapIndex].Loc.x, sea.Map.Regions[mapIndex].Loc.z))
    //                     {
    //                         sea.Map.Regions[mapIndex].Type = SeaMapTileType.Center;
    //                         continue;
    //                     }

    //                     if (sea.Map.Regions[mapIndex].Type == SeaMapTileType.Rocks)
    //                         for (int nX = -1; nX < 2; nX++)
    //                             for (int nZ = -1; nZ < 2; nZ++)
    //                             {
    //                                 if (Mathf.Abs(nX) == Mathf.Abs(nZ)) continue;

    //                                 Vector3Int loc = new(nX + x, 0, nZ + z);
    //                                 if (!(nX + x > -1 && nX + x < 11 && nZ + z > -1 && nZ + z < 11)) continue;

    //                                 if (!sea.Map.Regions[mapIndex].IsAStarOpen) continue;

    //                                 if (!mapSegment[(int)(mapSegment.Length * .5f)]
    //                                     .IsTileReachable(mapSegment[loc.Vec3ToInt(BoardSize)], mapSegment, BoardSize))
    //                                 {
    //                                     sea.Map.Regions[mapIndex].Type = SeaMapTileType.OpenSea;
    //                                     break;
    //                                 }
    //                             }
    //                 }
    //         }


    //     bool ClearCenter(int x, int z)
    //     {
    //         for (int i = -5; i < 6; i++)
    //             for (int j = -5; j < 6; j++)
    //                 if (x == Mathf.FloorToInt(sea.Map.RegionSize * .5f) + i &&
    //                     z == Mathf.FloorToInt(sea.Map.RegionSize * .5f) + j)
    //                     return true;

    //         return false;
    //     }
    // }




}
