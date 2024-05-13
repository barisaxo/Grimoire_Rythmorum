using System.Collections.Generic;

namespace Data.Two
{
    [System.Serializable]
    public class SettingsData : IData
    {
        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                static IItem[] SetUp()
                {
                    var enums = Enumeration.All<SettingsOptionEnum>();
                    var items = new IItem[enums.Length];
                    for (int i = 0; i < enums.Length; i++)
                        items[i] = SettingsOptionEnum.ToItem(enums[i]);

                    return items;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not Wood) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not Wood) throw new System.Exception(item.GetType().ToString());
            return null;
        }

        // public void DecreaseLevel(IItem item) { }
        // public void DecreaseLevel(IItem item, int i) { }

        public int GetLevel(IItem item)
        {
            return -1;
        }

        // public void IncreaseLevel(IItem item) { }
        public void AdjustLevel(IItem item, int i) { }

        public void SetLevel(IItem item, int level)
        {
        }


        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
        }

        public static SettingsData GetData()
        {
            return new SettingsData();
        }

        public IPersistentData PersistentData { get; } = new NotPersistentData();
    }

}