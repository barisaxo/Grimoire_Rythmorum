using System.Collections.Generic;
using System;

namespace Data.Two
{
    [Serializable]
    public class InventoryData : IData
    {
        private Dictionary<ICurrency, int> _datum;
        private Dictionary<ICurrency, int> Datum => _datum ??= SetUpDatum();
        private Dictionary<ICurrency, int> SetUpDatum()
        {
            Dictionary<ICurrency, int> datum = new();
            for (int i = 0; i < Items.Length; i++)
            {
                datum.TryAdd((ICurrency)Items[i], Items[i] switch
                {
                    Gold => 500,
                    Material => 200,
                    Ration => 10,
                    StarChart => 1,
                    Gramophone => 0,
                    _ => throw new System.ArgumentException(Items[i].Name)
                });
            }
            return datum;
        }

        public void Reset() => _datum = SetUpDatum();
        public string GetDisplayLevel(IItem item) => Datum[(ICurrency)item].ToString();
        public int GetLevel(IItem item) => Datum[(ICurrency)item];

        public void SetLevel(IItem item, int newInventoryLevel)
        {
            throw new System.NotImplementedException("You probably shouldn't be using this");
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not ICurrency) throw new ArgumentException(item.Name);
            Datum[(ICurrency)item] = Datum[(ICurrency)item] + i < 0 ? 0 : Datum[(ICurrency)item] + i;
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
            new StarChart(),
            new Gramophone()
        };

        public bool InventoryIsFull(int Space) => false;

        string IData.GetDescription(IItem item)
        {
            throw new NotImplementedException();
        }

        public IPersistentData PersistentData { get; } = new NotPersistentData();

    }
}
