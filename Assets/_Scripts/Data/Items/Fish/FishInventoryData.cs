using System.Collections.Generic;

namespace Data.Two
{
    public class FishInventoryData : IData
    {
        private Dictionary<Fish, int> _datum;
        private Dictionary<Fish, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<Fish, int> SetUpDatum()
        {
            Dictionary<Fish, int> datum = new();
            foreach (IItem item in Items) datum.TryAdd(item as Fish, 0);
            return datum;
        }

        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                static IItem[] SetUp()
                {
                    var enums = Enumeration.All<FishEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < enums.Length; i++)
                        temp[i] = FishEnum.ToItem(enums[i]);
                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not Fish) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not Fish) throw new System.Exception(item.GetType().ToString());
            return Datum[(Fish)item].ToString();
        }


        public int GetLevel(IItem item)
        {
            if (item is not Fish) throw new System.Exception(item.GetType().ToString());
            return Datum[(Fish)item];
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not Fish) throw new System.Exception(item.GetType().ToString());
            Datum[(Fish)item] =
                Datum[(Fish)item] + i > 999 ? 999 :
                Datum[(Fish)item] + i < 0 ? 0 :
                Datum[(Fish)item] + i;
        }

        // public void DecreaseLevel(IItem item)
        // {
        //     if (item is not Fish) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Fish)item] = Datum[(Fish)item] - 1 < 0 ? 0 : Datum[(Fish)item] - 1;
        // }
        // public void DecreaseLevel(IItem item, int i)
        // {
        //     if (item is not Fish) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Fish)item] = Datum[(Fish)item] - i < 0 ? 0 : Datum[(Fish)item] - i;
        // }

        // public void IncreaseLevel(IItem item)
        // {
        //     if (item is not Fish) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Fish)item] = Datum[(Fish)item] + 1 > 999 ? 999 : Datum[(Fish)item] + 1;
        // }
        // public void IncreaseLevel(IItem item, int i)
        // {
        //     if (item is not Fish) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Fish)item] = Datum[(Fish)item] + i > 999 ? 999 : Datum[(Fish)item] + i;
        // }

        public void SetLevel(IItem item, int level)
        {
            if (item is not Fish) throw new System.Exception(item.GetType().ToString());
            Datum[(Fish)item] = level;
        }

        public bool InventoryIsFull(int i)
        {
            int space = 0;
            foreach (var item in Items) i += GetLevel(item);
            return space >= i;
        }

        public void Reset()
        {
            _datum = SetUpDatum();
        }

        public IPersistentData PersistentData { get; } = new NotPersistentData();
    }

}