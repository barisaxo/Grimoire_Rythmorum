using System;

namespace Sea
{
    public class Region
    {
        public readonly int Size;
        public readonly UnityEngine.Vector2Int Coord;
        public MusicTheory.RegionalMode RegionalMode;

        private NPCShip[] _npcs;
        public NPCShip[] NPCs => _npcs ??= this.SetUpNPCs();

        private Cell[] _cells;
        public Cell[] Cells => _cells ??= this.InitializeCells(Size);

        public Region(UnityEngine.Vector2Int region, int size)
        {
            Size = size;
            Coord = region;
        }


    }

}
