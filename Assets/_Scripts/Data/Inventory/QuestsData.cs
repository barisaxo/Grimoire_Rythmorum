using System.Collections;
using System.Collections.Generic;

namespace Data.Inventory
{
    [System.Serializable]
    public class QuestData : IData
    {
        private Dictionary<DataItem, int> _quests;
        private Dictionary<DataItem, int> Quests => _quests ??= SetUpQuests();

        Dictionary<DataItem, int> SetUpQuests()
        {
            Dictionary<DataItem, int> quests = new();
            foreach (var item in DataItems) quests.TryAdd((DataItem)item, 0);
            return quests;
        }

        public string GetDisplayLevel(DataEnum item) => Quests[(DataItem)item] == 1 ? "Active" : "None active";

        public int GetLevel(DataEnum item) => Quests[(DataItem)item];

        public void IncreaseLevel(DataEnum item)
        {
            Quests[(DataItem)item] = 1;
            PersistentData.Save(this);
        }

        public void DecreaseLevel(DataEnum item)
        {
            Quests[(DataItem)item] = 0;
            PersistentData.Save(this);
        }

        public void SetLevel(DataEnum item, int newVolumeLevel)
        {
            Quests[(DataItem)item] = newVolumeLevel;
            PersistentData.Save(this);
        }

        public DataEnum[] DataItems => Enumeration.All<DataItem>();

        [System.Serializable]
        public class DataItem : DataEnum
        {
            public DataItem() : base(0, "") { }
            public DataItem(int id, string name) : base(id, name) { }
            public DataItem(int id, string name, string description) : base(id, name) => Description = description;
            public static DataItem StarChart = new(0, "Star Chart", "Sail to the location");
            public static DataItem Bounty = new(0, "Bounty", "Hunt down the pirate ship");
            public static DataItem Fishing = new(0, "Fishing", "Catch the fish");
        }

        private QuestData() { }

        public static QuestData GetData()
        {
            QuestData data = new();
            var loadData = data.PersistentData.TryLoadData();
            if (loadData is null) return data;
            data = (QuestData)loadData;
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Quest.Data");
    }
}