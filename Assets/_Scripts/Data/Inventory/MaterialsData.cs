using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Inventory
{
    [System.Serializable]
    public class MaterialsData : IData
    {
        private Dictionary<DataItem, int> _materials;
        private Dictionary<DataItem, int> Materials => _materials ??= SetUpFish();

        Dictionary<DataItem, int> SetUpFish()
        {
            Dictionary<DataItem, int> materials = new();
            foreach (var item in DataItems) materials.TryAdd((DataItem)item, 0);
            return materials;
        }

        public string GetDisplayLevel(DataEnum item) => Materials[(DataItem)item].ToString();

        public int GetLevel(DataEnum item) => Materials[(DataItem)item];

        public void IncreaseLevel(DataEnum item)
        {
            // if (StorageIsFull()) return;
            Materials[(DataItem)item] = +1 > 999 ? 999 : Materials[(DataItem)item] + 1;
        }

        public void DecreaseLevel(DataEnum item) =>
            Materials[(DataItem)item] = -1 < 0 ? 0 : Materials[(DataItem)item] - 1;

        public void SetLevel(DataEnum item, int newVolumeLevel) =>
            Materials[(DataItem)item] = newVolumeLevel;

        public DataEnum[] DataItems => Enumeration.All<DataItem>();

        [System.Serializable]
        public class DataItem : DataEnum
        {
            public DataItem() : base(0, "") { }
            public DataItem(int id, string name) : base(id, name) { }
            public DataItem(int id, string name, string description) : base(id, name) => Description = description;
            public static DataItem Hemp = new(0, "Hemp", "Inexpensive but heavy sailcloth");
            public static DataItem Cotton = new(1, "Cotton", "Moderately inexpensive, moderately heavy sailcloth");
            public static DataItem Linen = new(2, "Linen", "Expensive, moderately light sailcloth");
            public static DataItem Silk = new(3, "Silk", "Very expensive, very light sailcloth");
            public static DataItem Pine = new(4, "Pine", "Inexpensive but soft timber");
            public static DataItem Fir = new(5, "Fir", "Moderately inexpensive, moderately soft timber");
            public static DataItem Oak = new(6, "Oak", "Expensive, moderately hard timber");
            public static DataItem Teak = new(7, "Teak", "Very expensive, very hard timber");
            public static DataItem WroughtIron = new(8, "WroughtIron", "Inexpensive but weak metal");
            public static DataItem CastIron = new(9, "CastIron", "Moderately inexpensive, moderately weak metal");
            public static DataItem Bronze = new(10, "Bronze", "Expensive, moderately strong metal");
            public static DataItem Patina = new(11, "Patina", "Very expensive, very strong metal");
        }

        private MaterialsData() { }
        public static MaterialsData GetData()
        {
            MaterialsData data = new();
            var loadData = data.PersistentData.TryLoadData();
            if (loadData is null) return data;
            data = (MaterialsData)loadData;
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Materials.Data");
    }
}