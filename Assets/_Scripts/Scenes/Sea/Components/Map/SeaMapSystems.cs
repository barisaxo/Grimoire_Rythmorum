using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;
using Sea.Maps;

public static class SeaMapSystems
{
    public static bool IsInRange(this Sea.WorldMapScene sea, Vector3Int v) =>
    v.x > -1 && v.x < sea.Map.RegionSize && v.z > -1 && v.z < sea.Map.RegionSize;

    public static int RegionIndexFromGlobalCoord(this Sea.Maps.WorldMap map, Vector2Int globalCoord) =>
      new Vector2Int(globalCoord.x / map.RegionSize, globalCoord.y / map.RegionSize).Vec2ToInt(map.Size);

    public static int CellIndex(this Region region, Vector2Int localCoord) =>
        localCoord.Vec2ToInt(region.Size);

    public static bool IsCellOpen(this Region region, Vector2Int tile)
    {
        if (region.Cells[region.CellIndex(tile)].Type ==
            CellType.Rocks) return false;
        return true;
    }

    public static Region[] RegionsAdjacentTo(this Sea.Maps.WorldMap map, PlayerShip ship)
    {
        int upper = map.RegionSize - 7;
        List<Region> regions = new() { map.Regions[map.RegionIndexFromGlobalCoord(ship.GlobalCoord)] };
        int x = 0, y = 0;
        if (ship.LocalCoord(map.RegionSize).x < 7)
        {
            regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(
                  new Vector2Int((int)(ship.GlobalLoc.x - map.RegionSize), (int)(ship.GlobalLoc.y)).Smod(map.GlobalSize))]);
            x = -1;
        }
        else if (ship.LocalCoord(map.RegionSize).x > upper)
        {
            regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(
                  new Vector2Int((int)(ship.GlobalLoc.x + map.RegionSize), (int)(ship.GlobalLoc.y)).Smod(map.GlobalSize))]);
            x = +1;
        }

        if (ship.LocalCoord(map.RegionSize).y < 7)
        {
            regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(
                  new Vector2Int((int)(ship.GlobalLoc.x), (int)(ship.GlobalLoc.y - map.RegionSize)).Smod(map.GlobalSize))]);
            y = -1;
        }
        else if (ship.LocalCoord(map.RegionSize).y > upper)
        {
            regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(
                  new Vector2Int((int)(ship.GlobalLoc.x), (int)(ship.GlobalLoc.y + map.RegionSize)).Smod(map.GlobalSize))]);
            y = +1;
        }

        switch (x, y)
        {
            case (1, 1):
                regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(new Vector2Int(
                        (int)(ship.GlobalLoc.x + map.RegionSize),
                        (int)(ship.GlobalLoc.y + map.RegionSize))
                    .Smod(map.GlobalSize))]);
                break;
            case (1, -1):
                regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(new Vector2Int(
                        (int)(ship.GlobalLoc.x + map.RegionSize),
                        (int)(ship.GlobalLoc.y - map.RegionSize))
                    .Smod(map.GlobalSize))]);
                break;
            case (-1, 1):
                regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(new Vector2Int(
                        (int)(ship.GlobalLoc.x - map.RegionSize),
                        (int)(ship.GlobalLoc.y + map.RegionSize))
                    .Smod(map.GlobalSize))]);
                break;
            case (-1, -1):
                regions.Add(map.Regions[map.RegionIndexFromGlobalCoord(new Vector2Int(
                        (int)(ship.GlobalLoc.x - map.RegionSize),
                        (int)(ship.GlobalLoc.y - map.RegionSize))
                    .Smod(map.GlobalSize))]);
                break;
        }

        return regions.ToArray();
    }


    public static Color GetSeaColorFromRegion(this Sea.Maps.WorldMap map, R region) => region switch
    {
        _ when region == R.a => Color.red,
        _ when region == R.d => Color.yellow,
        _ when region == R.m => Color.blue,
        _ when region == R.l => Color.cyan,
        _ when region == R.p => Color.magenta,
        _ when region == R.i => Color.green,
        _ when region == R.s => Color.white,
        _ => Color.black,
    };

    public static MusicTheory.RegionalMode GetRegionFromMapR(this R r) => r switch
    {
        _ when r == R.a => MusicTheory.RegionalMode.Aeolian,
        _ when r == R.d => MusicTheory.RegionalMode.Dorian,
        _ when r == R.m => MusicTheory.RegionalMode.MixoLydian,
        _ when r == R.l => MusicTheory.RegionalMode.Lydian,
        _ when r == R.p => MusicTheory.RegionalMode.Phrygian,
        _ when r == R.i => MusicTheory.RegionalMode.Ionian,
        _ when r == R.s => MusicTheory.RegionalMode.Locrian,
        _ => (MusicTheory.RegionalMode)(-1),
    };

    public static bool IsHighSeas(this Sea.Maps.WorldMap map, Vector2Int region) => map.Features.ContainsKey(region);
}
