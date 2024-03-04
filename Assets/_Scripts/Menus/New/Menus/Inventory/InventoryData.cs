
namespace Data.Inventory
{
    [System.Serializable]
    public class InventoryData : IData
    {
        private (DataItem volumeItem, int level)[] _volumeLevels;
        private (DataItem volumeItem, int level)[] VolumeLevels => _volumeLevels ??= SetUpVolumeLevels();
        private (DataItem volumeItem, int level)[] SetUpVolumeLevels()
        {
            var items = DataItems;
            var levels = new (DataItem volumeItem, int level)[items.Length];
            return levels;
        }

        public string GetDisplayLevel(DataEnum item) => 0.ToString();
        public int GetLevel(DataEnum item) => 0;
        public void SetLevel(DataEnum item, int newVolumeLevel) { }
        public void IncreaseLevel(DataEnum item) { }
        public void DecreaseLevel(DataEnum item) { }
        public DataEnum[] DataItems => Enumeration.All<DataItem>();

        public class DataItem : DataEnum
        {
            public DataItem() : base(0, "") { }
            public DataItem(int id, string name) : base(id, name) { }
            public static DataItem Quests = new(0, nameof(Quests).CapsCase());
            public static DataItem Materials = new(1, nameof(Materials).CapsCase());
            public static DataItem Fish = new(2, nameof(Fish).CapsCase());
            public static DataItem StarCharts = new(3, nameof(StarCharts).CapsCase());
            public static DataItem Gramophones = new(4, nameof(Gramophones).CapsCase());
            public static DataItem Lighthouses = new(5, nameof(Lighthouses).CapsCase());
            public static DataItem Ship = new(6, nameof(Ship).CapsCase());
        }
        public IPersistentData PersistentData { get; } = new NotPersistentData();
    }
}