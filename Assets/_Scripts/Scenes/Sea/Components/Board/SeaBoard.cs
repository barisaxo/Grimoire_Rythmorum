using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sea
{
    public class Board
    {
        public Board(int size, Transform parent, Color color)
        {
            Size = size;

            for (int x = 0; x < SubGridSize; x++)
                for (int z = 0; z < SubGridSize; z++)
                    SubTiles.Add(new SubTile(
                        new Vector3Int(x, 0, z),
                        parent,
                        color));

        }

        public int Size;
        public int SubGridSize => Size * 3;
        public List<SubTile> SubTiles = new();
    }
}