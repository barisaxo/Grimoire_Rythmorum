using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sea
{
    public class Cell : ITile
    {
        public CellType Type;
        public Vector2Int Coord { get; }
        public bool HasGramo;
        public float RotY;
        public bool IsAStarOpen => Type == CellType.OpenSea;

        public GameObject GO;

        public Cell(Vector2Int coord)
        {
            Coord = coord;
        }

        public Cell(int x, int y)
        {
            Coord = new(x, y);
        }

        public void ClearGOs()
        {
            GO = null;
        }
    }

    public enum CellType { OpenSea, Cave, Rocks, Center, Lighthouse }
}