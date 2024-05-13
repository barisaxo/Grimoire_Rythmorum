using System.Collections.Generic;
using System;

namespace Data.Two
{
    [Serializable]
    public class InventoryData : IData
    {
        private Dictionary<Currency, int> _datum;
        private Dictionary<Currency, int> Datum => _datum ??= SetUpDatum();
        private Dictionary<Currency, int> SetUpDatum()
        {
            Dictionary<Currency, int> datum = new();
            for (int i = 0; i < Items.Length; i++) datum.TryAdd((Currency)Items[i], 500000);
            return datum;
        }

        public void Reset() => _datum = SetUpDatum();
        public string GetDisplayLevel(IItem item) => Datum[(Currency)item].ToString();
        public int GetLevel(IItem item) => Datum[(Currency)item];
        public void SetLevel(IItem item, int newInventoryLevel)
        {
            throw new System.NotImplementedException("You probably shouldn't be using this");
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not Currency) throw new ArgumentException(item.Name);
            Datum[(Currency)item] = Datum[(Currency)item] + i < 0 ? 0 : Datum[(Currency)item] + i;
        }

        // public void IncreaseLevel(IItem item)
        // {
        //     if (item is not Currency) throw new ArgumentException(item.Name);
        //     Datum[(Currency)item]++;
        // }
        // public void IncreaseLevel(IItem item, int i)
        // {
        //     if (item is not Currency) throw new ArgumentException(item.Name);
        //     Datum[(Currency)item] += i;
        // }
        // public void DecreaseLevel(IItem item)
        // {
        //     if (item is not Currency) throw new ArgumentException(item.Name);
        //     Datum[(Currency)item]--;
        // }
        // public void DecreaseLevel(IItem item, int i)
        // {
        //     if (item is not Currency) throw new ArgumentException(item.Name);
        //     Datum[(Currency)item] -= i;
        // }

        private IItem[] _items;
        public IItem[] Items => _items ??= new IItem[]{
            new Gold(),
            new Material(),
            new Ration(),
        };
        public bool InventoryIsFull(int Space) => false;

        string IData.GetDescription(IItem item)
        {
            throw new NotImplementedException();
        }

        public IPersistentData PersistentData { get; } = new NotPersistentData();

    }
}
