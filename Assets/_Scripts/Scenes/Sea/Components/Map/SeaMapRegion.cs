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
        public NPCShip[] NPCs => _npcs ??= this.SetUpNPCs(DataManager);

        private List<Cell> _cells;
        public List<Cell> Cells => _cells ??= this.InitializeCells(DataManager.Quests);
        readonly Data.Manager DataManager;

        public Region(UnityEngine.Vector2Int region, int resolution, Data.Manager dataManager, Sea.Maps.R r)
        {
            Resolution = resolution;
            Coord = region;
            DataManager = dataManager;
            R = r;
        }
    }

}
