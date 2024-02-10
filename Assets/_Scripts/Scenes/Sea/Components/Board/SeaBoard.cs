using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sea
{
    public class Board
    {
        public Board(int size, Transform parent)
        {
            Size = size;
            HalfSize = Size * .5f;
            SubGridSize = size * SubDivision;
            SubGridInversion = 1f / (float)SubDivision;
            HalfSubGrid = (float)SubGridSize * SubGridInversion;
            for (int x = 0; x < SubGridSize; x++)
                for (int z = 0; z < SubGridSize; z++)
                    SubTiles.Add(new SubTile(
                        new Vector3Int(x, 0, z),
                        parent,
                        SeaSystems.MyCyan,
                        SubGridInversion));
        }

        public Color SeaColor = SeaSystems.MyCyan;
        public Swells Swells;
        public readonly int Size;
        public int SubDivision = 2;
        public readonly int SubGridSize;
        public readonly float HalfSize;
        public readonly float HalfSubGrid;
        public readonly float SubGridInversion;
        public List<SubTile> SubTiles = new();
    }
}