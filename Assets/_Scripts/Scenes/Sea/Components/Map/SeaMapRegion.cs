using System;
using System.Collections.Generic;

namespace Sea
{
    public class Region
    {
        /// <summary>
        /// The resolution of the cell subdivision inside the region.
        /// </summary>
        public readonly int Resolution;
        public readonly UnityEngine.Vector2Int Coord;
        public Sea.Maps.R R;

        private NPCShip[] _npcs;
        public NPCShip[] NPCs => _npcs ??= this.SetUpNPCs();

        private List<Cell> _cells;
        public List<Cell> Cells => _cells ??= this.InitializeCells(QuestData);
        readonly Data.Inventory.QuestData QuestData;

        public Region(UnityEngine.Vector2Int region, int resolution, Data.Inventory.QuestData data, Sea.Maps.R r)
        {
            Resolution = resolution;
            Coord = region;
            QuestData = data;
            R = r;
        }
    }

}
