using Sea;
using Sea.Maps;
using UnityEngine;
using System.Collections.Generic;

public static class SeaRegionSystems
{
    public static List<Cell> InitializeCells(this Region region, int size)
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

        if (!WorldMapScene.Io.Map.ShippingLanes.Contains(region.Coord)) AddFish();
        AddBottle();
        if (!WorldMapScene.Io.Map.IsHighSeas(region.Coord)) AddRocks();

        return cells;

        void AddRocks()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    if (
                       ((x > 3 && y > 3) || (x < size - 3 && y < size - 3) ||
                        (x > 3 && y < size - 3) || (x < size - 3 && y > 3) ||
                        (x > halfSize - 2 && y > halfSize - 2 && x < halfSize + 2 && y < halfSize + 2))
                         &&
                        (Random.value > .95f))
                    {
                        cells.Add(new Cell(x, y)
                        {
                            Type = CellType.Rocks,
                            // RotY = Random.Range(0, 360)
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
            ships[i] = new(path, region.Coord * region.Size);
        }
        return ships;
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
        0 => new Vector2Int[3] { RandA(size), RandB(size), RandC(size) },
        1 => new Vector2Int[3] { RandA(size), RandB(size), RandD(size) },
        2 => new Vector2Int[3] { RandA(size), RandC(size), RandD(size) },
        3 => new Vector2Int[3] { RandB(size), RandC(size), RandD(size) },
        4 => new Vector2Int[4] { RandA(size), RandB(size), RandC(size), RandD(size) },
        5 => new Vector2Int[4] { RandA(size), RandC(size), RandB(size), RandD(size) },
        _ => new Vector2Int[4] { RandA(size), RandB(size), RandD(size), RandC(size) },
    };

    public static Vector2Int RandA(int size) => new(Random.Range(0, 3), Random.Range(size - 3, size));
    public static Vector2Int RandB(int size) => new(Random.Range(0, 3), Random.Range(0, 3));
    public static Vector2Int RandC(int size) => new(Random.Range(size - 3, size), Random.Range(size - 3, size));
    public static Vector2Int RandD(int size) => new(Random.Range(size - 3, size), Random.Range(0, 3));
}