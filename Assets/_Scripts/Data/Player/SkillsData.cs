using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Player
{
    [System.Serializable]
    public class SkillsData : IData
    {
        private Dictionary<DataItem, int> _skillsDatum;
        public Dictionary<DataItem, int> SkillsDatum => _skillsDatum ??= SetUpSkillsDatum();

        Dictionary<DataItem, int> SetUpSkillsDatum()
        {
            Dictionary<DataItem, int> skillsDatum = new();
            foreach (var item in DataItems) skillsDatum.TryAdd((DataItem)item, 0);
            return skillsDatum;
        }

        public void Reset() => _skillsDatum = SetUpSkillsDatum();
        public string GetDisplayLevel(DataEnum item)
        {
            var Item = item as DataItem;
            return item.Name + " lvl " + SkillsDatum[(DataItem)item];
        }

        public float GetBonusRatio(DataEnum item) => 1 + (.01f * SkillsDatum[(DataItem)item] * ((DataItem)item).Per);
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

        public int GetSkillCost(DataEnum item) => (SkillsDatum[(DataItem)item] + 1) * ((DataItem)item).Cost;
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
            public readonly int Cost;//1:1275,2:2550,3:3825,4:5100,5:6375,6:7650,7:8925,8:10200,9:11475
            public readonly int MaxLevel;

            public static DataItem Apophenia = new(0, "Apophenia", "Find more patterns, even if they aren't really there.", 2, 8, 50);
            public static DataItem PatternRecognition = new(1, "Pattern Recognition", "Retain some unsaved patterns when you lose your ship.", 1, 10, 50);
            public static DataItem Preparation = new(2, "Preparation", "Leave the Cove with extra materials, rations, and gold.", 2, 7, 50);
            public static DataItem PulsePerception = new(3, "Pulse Perception", "Fishing for beats is quicker.", 2, 2, 50);
            public static DataItem CelestialNavigation = new(4, "Celestial Navigation", "Get an extra chance to triangulate Star Charts.", 100, 5000, 2);
            public static DataItem Touch = new(5, "Touch", "Get an extra chance to unlock Gramophones.", 100, 7500, 2);
            // public static DataItem TimeDilation = new(6, "Time Dilation", "Higher damage potential with cannons.", 1, 17, 50);//19125

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

//+ " : " + item.Description + " + " + Item.Per + "% per level"