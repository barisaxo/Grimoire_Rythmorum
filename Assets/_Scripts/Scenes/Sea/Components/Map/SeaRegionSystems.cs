using Sea;
using Sea.Maps;
using UnityEngine;
using System.Collections.Generic;

public static class SeaRegionSystems
{
    public static List<Cell> InitializeCells(this Region region, int size, Data.Inventory.QuestData questData)
    {
        List<Cell> cells = new();
        int halfSize = (int)(size * .5f);

        if (WorldMapScene.Io.Map.Features.TryGetValue(region.Coord, out Feature[] Features))
            foreach (Feature feature in Features)
            {
                switch (feature)
                {
                    case Feature.Cove:
                        cells.Add(new Cell(halfSize, halfSize) { Type = CellType.Cove });
                        break;

                    case Feature.LightHouse:
                        cells.Add(new Cell(halfSize / 2, halfSize / 2) { Type = CellType.Lighthouse });
                        break;
                }
            }

        AddQuestItems();
        if (!WorldMapScene.Io.Map.ShippingLanes.Contains(region.Coord)) AddFish();
        AddBottle();
        if (!WorldMapScene.Io.Map.IsHighSeas(region.Coord)) AddRocks();

        return cells;

        void AddRocks()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    if (x > 3 && y > 3 && x < size - 3 && y < size - 3 &&
                       (x < halfSize + 2 || x > halfSize - 2) &&
                       (y < halfSize - 2 || y > halfSize + 2) &&
                        Random.value > .95f)
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
                    if (
                       ((x > 3 && y > 3) || (x < size - 3 && y < size - 3) ||
                        (x > 3 && y < size - 3) || (x < size - 3 && y > 3) ||
                        (x > halfSize - 2 && y > halfSize - 2 && x < halfSize + 2 && y < halfSize + 2))
                         &&
                        (numOfFish < 3 && x + y > numOfFish * 8 && Random.value > .9f))
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

        void AddBottle()
        {
            for (int i = 0; i < 1; i++)
                cells.Add(new Cell(Random.Range(3, size - 3), Random.Range(3, size - 3))
                {
                    Type = CellType.Bottle
                });
        }
        void AddQuestItems()
        {
            foreach (var data in questData.DataItems)
            {
                if (questData.GetQuest(data) is not null)
                {
                    if ((questData.GetQuest(data)?.QuestLocation / WorldMapScene.Io.Map.RegionSize) == region.Coord)
                    {
                        Debug.Log("ADDING QUEST: " + questData.GetQuest(data) + " " + questData.GetQuest(data)?.QuestLocation / WorldMapScene.Io.Map.RegionSize + " " + region.Coord);

                        cells.Add(new Cell(questData.GetQuest(data).QuestLocation.Smod(size))
                        {
                            Type = data switch
                            {
                                _ when data == Data.Inventory.QuestData.DataItem.StarChart =>
                                    CellType.Gramo,
                                _ => throw new System.Exception(),
                            }
                        });
                    }
                }
            }
        }
    }


    public static bool IsCellOccupied(this List<Cell> cells, int x, int y) => cells.IsCellOccupied(new Vector2Int(x, y));
    public static bool IsCellOccupied(this List<Cell> cells, Vector2Int v2i)
    {
        foreach (Cell cell in cells) if (cell.Coord == v2i) return true;
        return false;
    }



    public static NPCShip[] SetUpNPCs(this Region region)
    {
        NPCShip[] ships = new NPCShip[(int)(region.Size * .2f)];

        for (int i = 0; i < ships.Length; i++)
        {
            var path = region.PatrolPattern(i);
            ships[i] = new(path, GetShipType(), GetHull(), region.Coord * region.Size) { };
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

        Data.Equipment.HullData GetHull()
        {
            // Debug.Log(region.Coord + " " + Mathf.Abs(region.Coord.x - (WorldMapScene.Io.Map.Size * .5f)) + " " + Mathf.Abs(region.Coord.y - (WorldMapScene.Io.Map.Size * .5f)) + " " + WorldMapScene.Io.Map.Size);
            return Mathf.Abs(Mathf.Abs(region.Coord.x - (WorldMapScene.Io.Map.Size * .5f)) + Mathf.Abs(region.Coord.y - (WorldMapScene.Io.Map.Size * .5f))) switch
            {
                < 3 => Data.Equipment.HullData.Sloop,
                3 => Random.value < .65f ? Data.Equipment.HullData.Sloop : Data.Equipment.HullData.Schooner,
                4 => Random.value < .35f ? Data.Equipment.HullData.Sloop : Data.Equipment.HullData.Schooner,
                5 => Data.Equipment.HullData.Schooner,
                6 => Random.value < .65f ? Data.Equipment.HullData.Schooner : Data.Equipment.HullData.Frigate,
                7 => Random.value < .35f ? Data.Equipment.HullData.Schooner : Data.Equipment.HullData.Frigate,
                _ => Data.Equipment.HullData.Frigate
            };
        }
    }

    public static Vector2Int[] PatrolPattern(this Region region, int i)
    {
        Vector2Int[] nodes = Nodes(i % 7, region.Size);
        List<Vector2Int> path = new();

        for (int p = 0; p < nodes.Length; p++)
        {
            var leg = nodes[p].NewSailingPath(
                nodes[(p + 1) % nodes.Length],
                region.Cells.ToArray(),
                region.Size);

            for (int n = 0; n < leg.Length; n++) path.Add(leg[n]);
        }

        return path.ToArray();
    }

    public static Vector2Int[] Nodes(int i, int size) => i switch
    {
        0 => new Vector2Int[4] { new(0, 0), new(size - 1, 0), new(size - 1, size - 1), new(0, size - 1) },
        1 => new Vector2Int[4] { new(1, 1), new(size - 2, 1), new(size - 2, size - 2), new(1, size - 2) },
        2 => new Vector2Int[4] { new(2, 2), new(size - 3, 2), new(size - 3, size - 2), new(3, size - 2) },
        3 => new Vector2Int[4] { new(3, 3), new(size - 4, 3), new(size - 4, size - 3), new(4, size - 3) },
        4 => new Vector2Int[4] { new(4, 4), new(size - 5, 4), new(size - 5, size - 4), new(5, size - 4) },
        5 => new Vector2Int[4] { new(5, 5), new(size - 6, 5), new(size - 6, size - 6), new(6, size - 5) },
        _ => new Vector2Int[4] { new(6, 6), new(size - 7, 6), new(size - 7, size - 7), new(7, size - 6) },
    };

    // public static Vector2Int RandA(int size) => new(0, Random.Range(size - 3, size));
    // public static Vector2Int RandB(int size) => new(Random.Range(0, 3), Random.Range(0, 3));
    // public static Vector2Int RandC(int size) => new(Random.Range(size - 3, size), Random.Range(size - 3, size));
    // public static Vector2Int RandD(int size) => new(Random.Range(size - 3, size), Random.Range(0, 3));
}