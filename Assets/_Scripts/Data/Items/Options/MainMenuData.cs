using System.Collections.Generic;

namespace Data.Two
{
    [System.Serializable]
    public class MainMenuData : IData
    {
        // private Dictionary<MainOption, int> _datum;
        // private Dictionary<MainOption, int> Datum => _datum ??= SetUpDatum();

        // private Dictionary<MainOption, int> SetUpDatum()
        // {
        //     Dictionary<MainOption, int> datum = new();
        //     for (int i = 0; i < Items.Length; i++)
        //         if (Items[i] is BGMusic) datum.TryAdd((MainOption)Items[i], 35);
        //         else if (Items[i] is Drums) datum.TryAdd((MainOption)Items[i], 75);
        //         else if (Items[i] is Chords) datum.TryAdd((MainOption)Items[i], 95);
        //         else if (Items[i] is Bass) datum.TryAdd((MainOption)Items[i], 95);
        //         else if (Items[i] is SoundFX) datum.TryAdd((MainOption)Items[i], 95);
        //         else datum.TryAdd((MainOption)Items[i], 20);

        //     return datum;
        // }

        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                IItem[] SetUp()
                {
                    var enums = Enumeration.All<MainOptionEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = MainOptionEnum.ToItem(enums[i]);

                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not MainOption) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            // if (item is not MainOption) throw new System.Exception(item.GetType().ToString());
            // return Datum[(MainOption)item].ToString();
            return null;
        }

        public int GetLevel(IItem item)
        {
            // if (item is not MainOption) throw new System.Exception(item.GetType().ToString());
            // return Datum[(MainOption)item];
            return 0;
        }

        public void AdjustLevel(IItem item, int i) { }

        public void SetLevel(IItem item, int level)
        {
            // if (item is not MainOption) throw new System.Exception(item.GetType().ToString());
            // Datum[(MainOption)item] = level;
            // PersistentData.Save(this);
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset() { }

        public IPersistentData PersistentData { get; } = new NotPersistentData();
    }

}