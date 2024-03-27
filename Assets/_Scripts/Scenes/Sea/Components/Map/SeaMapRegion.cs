using System;
using System.Collections.Generic;

namespace Sea
{
    public class Region
    {
        public readonly int Size;
        public readonly UnityEngine.Vector2Int Coord;
        public MusicTheory.RegionalMode RegionalMode;

        private NPCShip[] _npcs;
        public NPCShip[] NPCs => _npcs ??= this.SetUpNPCs();

        private List<Cell> _cells;
        public List<Cell> Cells => _cells ??= this.InitializeCells(Size, QuestData);
        readonly Data.Inventory.QuestData QuestData;
        public Region(UnityEngine.Vector2Int region, int size, Data.Inventory.QuestData data)
        {
            Size = size;
            Coord = region;
            QuestData = data;
        }
    }

}
