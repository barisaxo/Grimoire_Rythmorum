using System.Collections;
using System.Collections.Generic;
using Quests;

namespace Data.Inventory
{
    [System.Serializable]
    public class QuestData : IData
    {
        private Dictionary<DataItem, IQuest> _quests;
        private Dictionary<DataItem, IQuest> Quests => _quests ??= SetUpQuests();

        Dictionary<DataItem, IQuest> SetUpQuests()
        {
            Dictionary<DataItem, IQuest> quests = new();
            foreach (var item in DataItems) quests.TryAdd((DataItem)item, null);
            return quests;
        }

        public void Reset() => _quests = SetUpQuests();

        public string GetDisplayLevel(DataEnum item) => Quests[(DataItem)item] is not null ?
            "Active" : "None active";

        public int GetLevel(DataEnum item) => Quests[(DataItem)item] is null ? 0 : 1;

        public void IncreaseLevel(DataEnum item)
        {
            // PersistentData.Save(this);
            throw new System.Exception();
        }

        public void DecreaseLevel(DataEnum item)
        {
            throw new System.Exception();
            // Quests[(DataItem)item] = null;
            // PersistentData.Save(this);
        }

        public void SetQuest(DataEnum item, IQuest quest)
        {
            Quests[(DataItem)item] = quest;
            PersistentData.Save(this);
        }

        public IQuest GetQuest(DataEnum item) =>
            Quests[(DataItem)item] is null ? null : Quests[(DataItem)item];


        public void SetLevel(DataEnum item, int newVolumeLevel)
        {
            throw new System.Exception();
            // Quests[(DataItem)item] = newVolumeLevel;
            // PersistentData.Save(this);
        }

        public bool InventoryIsFull(int i) => false;
        public DataEnum[] DataItems => Enumeration.All<DataItem>();

        [System.Serializable]
        public class DataItem : DataEnum
        {
            public DataItem() : base(0, "") { }
            public DataItem(int id, string name) : base(id, name) { }
            public DataItem(int id, string name, string description) : base(id, name) => Description = description;
            public static DataItem StarChart = new(0, "Star Chart", "Sail to the location");
            public static DataItem Bounty = new(1, "Bounty", "Hunt down the pirate ship");
            public static DataItem Fishing = new(2, "Fishing", "Catch the fish");
        }

        private QuestData() { }

        public static QuestData GetData()
        {
            QuestData data = new();
            if (data.PersistentData.TryLoadData() is not QuestData loadData) return data;
            for (int i = 0; i < data.DataItems.Length; i++)
                try { data.SetLevel(data.DataItems[i], loadData.GetLevel(data.DataItems[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Quest.Data");

    }
}
// private Quests.NavigationQuest _navigationQuest;
// public Quests.NavigationQuest NavigationQuest
// {
//     get => _navigationQuest;
//     set
//     {
//         _navigationQuest = value;
//         PersistentData.Save(this);
//     }
// }
