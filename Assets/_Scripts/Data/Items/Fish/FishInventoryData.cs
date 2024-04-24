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

        public static IItem[] Items => new Fish[] {
            new Carp(), new SailFish(), new Tuna(), new Halibut(),
            new Sturgeon(), new Shark() };

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

        public void DecreaseLevel(IItem item)
        {
            if (item is not Fish) throw new System.Exception(item.GetType().ToString());
            Datum[(Fish)item] = -1 < 0 ? 0 : Datum[(Fish)item] - 1;
        }

        public int GetLevel(IItem item)
        {
            if (item is not Fish) throw new System.Exception(item.GetType().ToString());
            return Datum[(Fish)item];
        }

        public void IncreaseLevel(IItem item)
        {
            if (item is not Fish) throw new System.Exception(item.GetType().ToString());
            Datum[(Fish)item] = +1 > 999 ? 999 : Datum[(Fish)item] + 1;
        }

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