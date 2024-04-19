using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Player
{
    [System.Serializable]
    public class SkillsData : IData
    {
        private Dictionary<DataItem, int> _playerDatum;
        public Dictionary<DataItem, int> SkillsDatum => _playerDatum ??= SetUpSkillsDatum();

        Dictionary<DataItem, int> SetUpSkillsDatum()
        {
            Dictionary<DataItem, int> playerDatum = new();
            foreach (var item in DataItems) playerDatum.TryAdd((DataItem)item, 0);
            return playerDatum;
        }

        public void Reset() => _playerDatum = SetUpSkillsDatum();
        public string GetDisplayLevel(DataEnum item)
        {
            var Item = item as DataItem;
            return item.Name + " lvl " + SkillsDatum[(DataItem)item] + " : " + item.Description + " + " + Item.Per + "% per level";
        }

        public int GetLevel(DataEnum item) => SkillsDatum[(DataItem)item];

        public void IncreaseLevel(DataEnum item)
        {
            SkillsDatum[(DataItem)item]++;
            PersistentData.Save(this);
        }

        public void DecreaseLevel(DataEnum item)
        {
            SkillsDatum[(DataItem)item] -= SkillsDatum[(DataItem)item] > 0 ? 1 : 0;
            PersistentData.Save(this);
        }

        public void SetLevel(DataEnum item, int newVolumeLevel)
        {
            SkillsDatum[(DataItem)item] = newVolumeLevel;
            PersistentData.Save(this);
        }

        public DataEnum[] DataItems { get; } = Enumeration.All<DataItem>();

        public bool InventoryIsFull(int Space) => false;

        [System.Serializable]
        public class DataItem : DataEnum
        {
            public DataItem() : base(0, "") { }
            public DataItem(int id, string name) : base(id, name) { }
            public DataItem(int id, string name, string description, int per, int cost, int max) : base(id, name)
            {
                Description = description;
                Per = per;
                Cost = cost;
                MaxLevel = max;
            }
            public readonly int Per;
            public readonly int Cost;
            public readonly int MaxLevel;
            public static DataItem PatternRecognition = new(0, "Pattern Recognition", "You are better at finding patterns.", 2, 4, 50);
            public static DataItem Apophenia = new(1, "Apophenia", "Retain some unsaved patterns on death", 1, 8, 50);
            public static DataItem Salvaging = new(2, "Salvaging", "You are better at salvaging shipwrecks.", 2, 4, 50);
            public static DataItem Fishing = new(3, "Fishing", "You are better at catching fish.", 2, 5, 50);
            public static DataItem CelestialNavigation = new(4, "Celestial Navigation", "You are better at celestial navigation.", 100, 5000, 2);
            public static DataItem Gramophone = new(5, "Gramophone", "You are better at unlocking Gramophones.", 100, 7500, 2);

            // public static DataItem GramoSolved = new(4, "Gramophone Solved");
            // public static DataItem GramoFailed = new(5, "Gramophone Failed");
            // public static DataItem FishCaught = new(6, "Fish Caught");
            // public static DataItem FishLost = new(7, "Fish Lost");
            // public static DataItem Hit = new(8, "Batterie Hit");
            // public static DataItem Miss = new(9, "Batterie Miss");
            // public static DataItem Patterns = new(10, "Pattern");
        }

        private SkillsData() { }

        public static SkillsData GetData()
        {
            SkillsData data = new();
            if (data.PersistentData.TryLoadData() is not SkillsData loadData) return data;
            for (int i = 0; i < data.DataItems.Length; i++)
                try { data.SetLevel(data.DataItems[i], loadData.GetLevel(data.DataItems[i])); }
                catch { }
            data.PersistentData.Save(data);
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Skills.Data");

    }
}