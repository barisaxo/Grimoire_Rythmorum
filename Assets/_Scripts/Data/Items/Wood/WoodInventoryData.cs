using System.Collections.Generic;

namespace Data.Two
{
    public class WoodInventoryData : IData
    {
        private Dictionary<Wood, int> _datum;
        private Dictionary<Wood, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<Wood, int> SetUpDatum()
        {
            Dictionary<Wood, int> datum = new();
            foreach (IItem item in Items) datum.TryAdd(item as Wood, 0);
            return datum;
        }

        public static IItem[] Items => new Wood[] { new Pine(), new Fir(), new Oak(), new Teak() };

        public string GetDescription(IItem item)
        {
            if (item is not Wood) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not Wood) throw new System.Exception(item.GetType().ToString());
            return Datum[(Wood)item].ToString();
        }

        public void DecreaseLevel(IItem item)
        {
            if (item is not Wood) throw new System.Exception(item.GetType().ToString());
            Datum[(Wood)item] = -1 < 0 ? 0 : Datum[(Wood)item] - 1;
        }

        public int GetLevel(IItem item)
        {
            if (item is not Wood) throw new System.Exception(item.GetType().ToString());
            return Datum[(Wood)item];
        }

        public void IncreaseLevel(IItem item)
        {
            if (item is not Wood) throw new System.Exception(item.GetType().ToString());
            Datum[(Wood)item] = +1 > 999 ? 999 : Datum[(Wood)item] + 1;
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not Wood) throw new System.Exception(item.GetType().ToString());
            Datum[(Wood)item] = level;
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