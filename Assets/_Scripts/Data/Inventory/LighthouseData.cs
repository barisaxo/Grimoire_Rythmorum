using System.Collections.Generic;

namespace Data.Inventory
{
    [System.Serializable]
    public class LighthouseData : IData
    {
        private Dictionary<DataItem, int> _lighthouses;
        private Dictionary<DataItem, int> Lighthouses => _lighthouses ??= SetUpLightHouses();
        Dictionary<DataItem, int> SetUpLightHouses()
        {
            Dictionary<DataItem, int> ship = new();
            foreach (var item in DataItems) ship.TryAdd((DataItem)item, 0);
            return ship;
        }

        public void Reset() => _lighthouses = SetUpLightHouses();

        public string GetDisplayLevel(DataEnum item) => Lighthouses[(DataItem)item] == 1 ? "Active" : "Not active";

        public int GetLevel(DataEnum item) => Lighthouses[(DataItem)item];// Lighthouses.GetValueOrDefault((DataItem)item);

        public void IncreaseLevel(DataEnum item)
        {
            Lighthouses[(DataItem)item] = 1;
            PersistentData.Save(this);
        }

        public void DecreaseLevel(DataEnum item)
        {
            Lighthouses[(DataItem)item] = 0;
            PersistentData.Save(this);
        }

        public void SetLevel(DataEnum item, int count)
        {
            Lighthouses[(DataItem)item] = count;
            PersistentData.Save(this);
        }

        public DataEnum[] DataItems => Enumeration.All<DataItem>();

        public bool InventoryIsFull(int i) => false;

        [System.Serializable]
        public class DataItem : DataEnum
        {
            public DataItem() : base(0, "") { }
            public DataItem(int id, string name) : base(id, name) { }
            public static DataItem Ios = new(0, "Ios");
            public static DataItem Doria = new(1, "Doria");
            public static DataItem Phrygia = new(2, "Phrygia");
            public static DataItem Lydia = new(3, "Lydia");
            public static DataItem MixoLydia = new(4, "MixoLydia");
            public static DataItem Aeolia = new(5, "Aeolia");
            public static DataItem Locria = new(6, "Locria");
            public static DataItem Chromatica = new(7, "Chromatica");
        }

        private LighthouseData() { }

        public static LighthouseData GetData()
        {
            LighthouseData data = new();
            if (data.PersistentData.TryLoadData() is not LighthouseData loadData) return data;
            for (int i = 0; i < data.DataItems.Length; i++)
                try { data.SetLevel(data.DataItems[i], loadData.GetLevel(data.DataItems[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Lighthouse.Data");
    }
}