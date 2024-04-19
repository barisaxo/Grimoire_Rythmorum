using System.Collections.Generic;

namespace Data.Inventory
{
    [System.Serializable]
    public class FishData : IData
    {
        private Dictionary<DataItem, int> _fish;
        private Dictionary<DataItem, int> Fish => _fish ??= SetUpFish();

        Dictionary<DataItem, int> SetUpFish()
        {
            Dictionary<DataItem, int> fish = new();
            foreach (var item in DataItems) fish.TryAdd((DataItem)item, 0);
            return fish;
        }

        public void Reset() => _fish = SetUpFish();
        public string GetDisplayLevel(DataEnum item) => Fish[(DataItem)item].ToString();

        public int GetLevel(DataEnum item) => Fish[(DataItem)item];

        public void IncreaseLevel(DataEnum item)
        {
            Fish[(DataItem)item] += Fish[(DataItem)item] + 1 > 999 ? 0 : 1;
            PersistentData.Save(this);
        }

        public void DecreaseLevel(DataEnum item)
        {
            Fish[(DataItem)item] -= Fish[(DataItem)item] - 1 < 0 ? 0 : 1;
            PersistentData.Save(this);
        }

        public void SetLevel(DataEnum item, int newVolumeLevel)
        {
            Fish[(DataItem)item] = newVolumeLevel;
            PersistentData.Save(this);
        }

        public DataEnum[] DataItems => Enumeration.All<DataItem>();

        public bool InventoryIsFull(int Space)
        {
            int i = 0;
            foreach (var item in DataItems) i += GetLevel(item);
            return i >= Space;
        }

        [System.Serializable]
        public class DataItem : DataEnum
        {
            public DataItem() : base(0, "") { }
            public DataItem(int id, string name) : base(id, name) { }
            public DataItem(int id, string name, string description) : base(id, name) => Description = description;
            public static DataItem SailFish = new(0, "Sailfish", "You eat this");
            public static DataItem Tuna = new(1, "Tuna", "You eat this");
            public static DataItem Carp = new(2, "Carp", "You eat this");
            public static DataItem Halibut = new(3, "Halibut", "You eat this");
            public static DataItem Sturgeon = new(4, "Sturgeon", "You eat this");
            public static DataItem Shark = new(5, "Shark", "This eat you");
        }

        private FishData() { }


        public static FishData GetData()
        {
            FishData data = new();
            if (data.PersistentData.TryLoadData() is not FishData loadData) return data;
            for (int i = 0; i < data.DataItems.Length; i++)
                try { data.SetLevel(data.DataItems[i], loadData.GetLevel(data.DataItems[i])); }
                catch { }
            data.PersistentData.Save(data);
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Fish.Data");
    }
}