using System.Collections;
using System.Collections.Generic;
using Quests;

namespace Data
{
    [System.Serializable]
    public class QuestData : IData
    {
        private Dictionary<IQuest, Quests.IQuest> _quests;
        private Dictionary<IQuest, Quests.IQuest> Quests => _quests ??= SetUpQuests();

        Dictionary<IQuest, Quests.IQuest> SetUpQuests()
        {
            Dictionary<IQuest, Quests.IQuest> quests = new();
            foreach (var item in Items) quests.TryAdd((IQuest)item, null);
            return quests;
        }

        public void Reset() => _quests = SetUpQuests();

        public string GetDisplayLevel(IItem item) =>
         Quests[(IQuest)item] is not null ? "Active" : "None active";


        public int GetLevel(IItem item) => Quests[(IQuest)item] is null ? 0 : 1;

        // public void IncreaseLevel(IItem item)
        // {
        //     // PersistentData.Save(this);
        //     throw new System.Exception();
        // }
        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IQuest || i != 0) throw new System.Exception(item.Name + " " + i);
            if (i == 0) Quests[(IQuest)item] = null;
        }

        // public void DecreaseLevel(IItem item)
        // {
        //     throw new System.Exception();
        //     // Quests[(QuestEnum)item] = null;
        //     // PersistentData.Save(this);
        // }
        // public void DecreaseLevel(IItem item, int i) { }

        public void SetQuest(IItem item, Quests.IQuest quest)
        {
            Quests[(IQuest)item] = quest;
            PersistentData.Save(this);
        }

        public Quests.IQuest GetQuest(IItem item) =>
            Quests[(IQuest)item] is null ? null : Quests[(IQuest)item];


        public void SetLevel(IItem item, int newVolumeLevel)
        {
            throw new System.Exception();
            // Quests[(QuestEnum)item] = newVolumeLevel;
            // PersistentData.Save(this);
        }

        public bool InventoryIsFull(int i) => false;

        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                static IItem[] SetUp()
                {
                    var enums = Enumeration.All<QuestEnum>();
                    var items = new IItem[enums.Length];
                    for (int i = 0; i < enums.Length; i++)
                        items[i] = QuestEnum.ToItem(enums[i]);

                    return items;
                }
            }
        }

        string IData.GetDescription(IItem item)
        {
            throw new System.NotImplementedException();
        }

        public IPersistentData PersistentData { get; } = new NotPersistentData();

        // IItem[] IData.Items => throw new System.NotImplementedException();
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
