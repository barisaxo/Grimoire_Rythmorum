
namespace Data.Options
{
    public class OptionsData : IData
    {
        public string GetDisplayLevel(DataEnum item) => 0.ToString();
        public int GetLevel(DataEnum item) => 0;
        public void SetLevel(DataEnum item, int _) { }
        public void IncreaseLevel(DataEnum item) { }
        public void DecreaseLevel(DataEnum item) { }
        public DataEnum[] DataItems => Enumeration.All<DataItem>();

        public bool InventoryIsFull(int Space) => false;

        public class DataItem : DataEnum
        {
            public DataItem() : base(0, "") { }
            public DataItem(int id, string name) : base(id, name) { }
            public static DataItem VOLUME = new(0, "VOLUME");
            public static DataItem GAMEPLAY = new(1, "GAMEPLAY");
            public static DataItem CONTROLS = new(2, "CONTROLS");
        }
        public IPersistentData PersistentData { get; } = new NotPersistentData();
    }
}