using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Inventory
{
    [System.Serializable]
    public class MaterialsData : IData
    {
        private Dictionary<DataItem, int> _materials;
        private Dictionary<DataItem, int> Materials => _materials ??= SetUpMats();

        Dictionary<DataItem, int> SetUpMats()
        {
            Dictionary<DataItem, int> materials = new();
            foreach (var item in DataItems) materials.TryAdd((DataItem)item, 0);
            return materials;
        }

        public void Reset() => _materials = SetUpMats();
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
            public DataItem(int id, string name, string description, float modifier) : base(id, name)
            {
                Description = description;
                Modifier = modifier;
            }
            public DataItem(int id, string name, string description, float modifier, Func<Sprite> sprite) : base(id, name)
            {
                Description = description;
                Modifier = modifier;
                Sprite = sprite;
            }

            public readonly float Modifier;
            public readonly Func<Sprite> Sprite = null;

            public static DataItem Hemp = new(0, "Hemp", "Inexpensive but heavy sailcloth", 1);
            public static DataItem Cotton = new(1, "Cotton", "Moderately inexpensive, moderately light sailcloth", 1.5f);
            public static DataItem Linen = new(2, "Linen", "Expensive, light sailcloth", 2.25f);
            public static DataItem Silk = new(3, "Silk", "Very expensive, very light sailcloth", 3f);
            public static DataItem Pine = new(4, "Pine", "Inexpensive but soft timber", 1f);
            public static DataItem Fir = new(5, "Fir", "Moderately inexpensive, moderately hard timber", 1.5f);
            public static DataItem Oak = new(6, "Oak", "Expensive, hard timber", 2.25f);
            public static DataItem Teak = new(7, "Teak", "Very expensive, very hard timber", 3f);
            public static DataItem WroughtIron = new(8, "WroughtIron", "Inexpensive but weak metal", 1f);
            public static DataItem CastIron = new(9, "CastIron", "Moderately inexpensive, moderately strong metal", 1.5f);
            public static DataItem Bronze = new(10, "Bronze", "Expensive, strong metal", 2.25f);
            public static DataItem Patina = new(11, "Patina", "Very expensive, very strong metal", 3f);
        }

        private MaterialsData() { }

        public static MaterialsData GetData()
        {
            MaterialsData data = new();
            if (data.PersistentData.TryLoadData() is not MaterialsData loadData) return data;
            for (int i = 0; i < data.DataItems.Length; i++)
                try { data.SetLevel(data.DataItems[i], loadData.GetLevel(data.DataItems[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Materials.Data");
    }
}