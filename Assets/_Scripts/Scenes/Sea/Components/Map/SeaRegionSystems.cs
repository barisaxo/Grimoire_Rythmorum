using Sea;
using UnityEngine;
using System.Collections.Generic;

public static class SeaRegionSystems
{
    public static Cell[] InitializeCells(this Region region, int size)
    {
        Cell[] cells = new Cell[size * size];
        int index = 0;
        int halfSize = (int)(size * .5f);

        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
            {
                if (((x > 3 && y > 3) || (x < size - 3 && y < size - 3) ||
                    (x > 3 && y < size - 3) || (x < size - 3 && y > 3) ||
                    (x > halfSize - 2 && y > halfSize - 2 && x < halfSize + 2 && y < halfSize + 2)) &&
                    Random.value > .95f)
                {
                    cells[index] = new(x, y)
                    {
                        Type = CellType.Rocks,
                        RotY = Random.Range(0, 360)
                    };
                }
                else
                {
                    cells[index] = new(x, y)
                    {
                        Type = CellType.OpenSea
                    };
                }
                index++;
            }

        return cells;
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
            var leg = region.Cells[nodes[p].Vec2ToInt(region.Size)].NewSailingPath(
                region.Cells[nodes[(p + 1) % nodes.Length].Vec2ToInt(region.Size)],
                region.Cells,
                region.Size);

            for (int n = 0; n < leg.Length; n++) path.Add(leg[n].Coord);
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