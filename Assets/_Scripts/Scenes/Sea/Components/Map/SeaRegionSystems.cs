using Sea;
using Sea.Maps;
using UnityEngine;
using System.Collections.Generic;

public static class SeaRegionSystems
{
    public static List<Cell> InitializeCells(this Region region, Data.Two.QuestData questData)
    {
        Debug.Log("Initializing region: " + region.Coord);
        List<Cell> cells = new();
        int size = region.Resolution;
        int halfSize = (int)(size * .5f);
        // Debug.Log(size);

        if (WorldMapScene.Io.Map.Features.TryGetValue(region.Coord, out Feature[] Features))
            foreach (Feature feature in Features)
            {
                switch (feature)
                {
                    case Feature.Cove:
                        cells.Add(new Cell(size - 1, 1) { Type = CellType.Cove });
                        Debug.Log((cells[^1].Coord + (region.Coord * size)).GlobalCoordsToLatLongs(Sea.WorldMapScene.Io.Map.GlobalSize));
                        break;

                    case Feature.LightHouse:
                        cells.Add(new Cell(halfSize, halfSize) { Type = CellType.Lighthouse });
                        break;
                }
            }

        // AddQuestItems();
        if (!WorldMapScene.Io.Map.ShippingLanes.Contains(region.Coord)) AddFish();
        AddBottle();
        if (!WorldMapScene.Io.Map.IsHighSeas(region.Coord)) AddRocks();

        // foreach (var cell in cells) { Debug.Log("Region: " + region.Coord + ", cell: " + cell.Coord + ", type: " + cell.Type); }
        return cells;

        void AddRocks()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    if (x > size - 6 && x < size && y > size - 6 && y < size &&
                    //    (x < halfSize + 2 || x > halfSize - 2) &&
                    //    (y < halfSize - 2 || y > halfSize + 2) &&
                        Random.value > .85f)
                    {
                        cells.Add(new Cell(x, y)
                        {
                            Type = CellType.Rocks
                        });
                    }
                }
        }

        void AddFish()
        {
            int numOfFish = 0;

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    if (!(x == size - 1 && y == 1) &&
                       ((x > 3 && y > 3) || (x < size - 3 && y < size - 3) ||
                        (x > 3 && y < size - 3) || (x < size - 3 && y > 3) ||
                        (x > halfSize - 2 && y > halfSize - 2 && x < halfSize + 2 && y < halfSize + 2))
                         &&
                        (numOfFish < 3 && x + y > numOfFish * 8 && Random.value > .9f))
                    {
                        foreach (Cell cell in cells) if (cell.Coord.x == x && cell.Coord.y == y) continue;
                        int ii = Random.Range(1, 4);
                        for (int i = 0; i < ii; i++)
                        {
                            numOfFish++;
                            cells.Add(new Cell(x, y)
                            {
                                Type = CellType.Fish,
                                // RotY = Random.Range(0, 360)
                            });
                        }
                    }
                }
        }

        void AddBottle()
        {
            for (int i = 0; i < 1; i++)
                cells.Add(new Cell(Random.Range(3, size - 3), Random.Range(3, size - 3))
                {
                    Type = CellType.Bottle
                });
        }
        // void AddQuestItems()
        // {
        //     foreach (var item in questData.Items)
        //     {
        //         if (questData.GetQuest(item) is not null)
        //         {
        //             if ((questData.GetQuest(item)?.QuestLocation / WorldMapScene.Io.Map.RegionSize) == region.Coord)
        //             {
        //                 Cell newCell = new(questData.GetQuest(item).QuestLocation.Smod(size))
        //                 {
        //                     Type = item switch
        //                     {
        //                         Data.Two.Navigation => CellType.Gramo,
        //                         Data.Two.Bounty => CellType.Bounty,
        //                         _ => throw new System.Exception(),
        //                     }
        //                 };

        //                 foreach (var cell in cells)
        //                     if (cell.Type == newCell.Type)
        //                     {
        //                         Debug.Log("Preventing Duplicate " + cell.Type);
        //                         return;
        //                     }

        //                 Debug.Log("Adding " + newCell.Type);
        //                 cells.Add(newCell);
        //             }
        //         }
        //     }
        // }
    }


    public static bool IsCellOccupied(this List<Cell> cells, int x, int y) => cells.IsCellOccupied(new Vector2Int(x, y));
    public static bool IsCellOccupied(this List<Cell> cells, Vector2Int v2i)
    {
        foreach (Cell cell in cells) if (cell.Coord == v2i) return true;
        return false;
    }



    public static NPCShip[] SetUpNPCs(this Region region, Data.Two.Manager dataManager)
    {
        NPCShip[] ships = new NPCShip[(int)(region.Resolution * .2f)];

        for (int i = 0; i < ships.Length; i++)
        {
            var path = region.PatrolPattern(i);
            Data.Two.IHull hull = GetHull();
            ships[i] = new(path, GetShipType(), hull, region.Coord * region.Resolution, GetShipStats(hull)) { };
        }

        return ships;

        NPCShipType GetShipType()
        {
            return region.R switch
            {
                R.o => NPCShipType.Pirate,
                _ => NPCShipType.Trade
            };
        }

        ShipStats.ShipStats GetShipStats(Data.Two.IHull hull)
        {
            var stats = new ShipStats.ShipStats(
                new ShipStats.HullStats(hull, Data.Two.WoodEnum.GetRandomWood()),
                new ShipStats.CannonStats(Data.Two.CannonEnum.GetRandomCannon(), Data.Two.MetalEnum.GetRandomMetal()),
                new ShipStats.RiggingStats((Data.Two.ICloth)Data.Two.ClothEnum.GetRandomCloth())
            );

            return stats;
        }

        Data.Two.IHull GetHull()
        {
            // Debug.Log(region.Coord + " " + Mathf.Abs(region.Coord.x - (WorldMapScene.Io.Map.Size * .5f)) + " " + Mathf.Abs(region.Coord.y - (WorldMapScene.Io.Map.Size * .5f)) + " " + WorldMapScene.Io.Map.Size);
            return Mathf.Abs(Mathf.Abs(region.Coord.x - (WorldMapScene.Io.Map.RegionResolution * .5f)) + Mathf.Abs(region.Coord.y - (WorldMapScene.Io.Map.RegionResolution * .5f))) switch
            {
                < 3 => new Data.Two.Sloop(),
                3 => Random.value < .65f ? new Data.Two.Sloop() : new Data.Two.Brig(),
                4 => Random.value < .65f ? new Data.Two.Brig() : new Data.Two.Schooner(),
                5 => Random.value < .65f ? new Data.Two.Schooner() : new Data.Two.Cutter(),
                6 => Random.value < .65f ? new Data.Two.Cutter() : new Data.Two.Frigate(),
                7 => Random.value < .65f ? new Data.Two.Frigate() : new Data.Two.Barque(),
                _ => Random.value < .50f ? new Data.Two.Frigate() : new Data.Two.Barque(),
            };
        }
    }

    public static Vector2Int[] PatrolPattern(this Region region, int i)
    {
        Vector2Int[] nodes = Nodes(i % 7, region.Resolution);
        List<Vector2Int> path = new();

        for (int p = 0; p < nodes.Length; p++)
        {
            var leg = nodes[p].NewSailingPath(
                nodes[(p + 1) % nodes.Length],
                region.Cells.ToArray(),
                region.Resolution);

            for (int n = 0; n < leg.Length; n++) path.Add(leg[n]);
        }

        return path.ToArray();
    }

    public static Vector2Int[] Nodes(int i, int size) => i switch
    {
        0 => new Vector2Int[4] { new(6, 6), new(size - 7, 6), new(size - 7, size - 7), new(6, size - 7) },
        1 => new Vector2Int[4] { new(7, 7), new(size - 8, 7), new(size - 8, size - 8), new(7, size - 8) },
        2 => new Vector2Int[4] { new(8, 8), new(size - 9, 8), new(size - 9, size - 9), new(8, size - 9) },
        3 => new Vector2Int[4] { new(9, 9), new(size - 10, 9), new(size - 10, size - 10), new(9, size - 10) },
        4 => new Vector2Int[4] { new(10, 10), new(size - 11, 10), new(size - 11, size - 11), new(10, size - 11) },
        5 => new Vector2Int[4] { new(11, 11), new(size - 12, 11), new(size - 12, size - 12), new(11, size - 12) },
        _ => new Vector2Int[4] { new(12, 12), new(size - 13, 12), new(size - 13, size - 13), new(12, size - 13) },
    };

    // public static Vector2Int RandA(int size) => new(0, Random.Range(size - 3, size));
    // public static Vector2Int RandB(int size) => new(Random.Range(0, 3), Random.Range(0, 3));
    // public static Vector2Int RandC(int size) => new(Random.Range(size - 3, size), Random.Range(size - 3, size));
    // public static Vector2Int RandD(int size) => new(Random.Range(size - 3, size), Random.Range(0, 3));
}