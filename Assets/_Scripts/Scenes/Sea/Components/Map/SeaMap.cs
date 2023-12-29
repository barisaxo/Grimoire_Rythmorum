using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sea
{
    public class Map
    {
        public int Size;
        public int RegionSize;
        public int GlobalSize;
        public Region[] Regions;

        public Map(int mapSize, int regionSize)
        {
            Size = mapSize;
            RegionSize = regionSize;
            GlobalSize = Size * RegionSize;
            Regions = new Region[Size * Size];

            int i = 0;
            for (int x = 0; x < Size; x++)
                for (int y = 0; y < Size; y++)
                    Regions[i++] = new(new Vector2Int(x, y), RegionSize);
        }

        // public SeaMap InitializeRegions()
        // {

        //     return this;
        // }
    }
}