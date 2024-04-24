using System.Collections.Generic;

namespace Data.Two
{
    public class MetalInventoryData : IData
    {
        private Dictionary<Metal, int> _datum;
        private Dictionary<Metal, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<Metal, int> SetUpDatum()
        {
            Dictionary<Metal, int> datum = new();
            foreach (IItem item in Items) datum.TryAdd(item as Metal, 0);
            return datum;
        }

        public static IItem[] Items => new Metal[] { new WroughtIron(), new CastIron(), new Bronze(), new Patina() };

        public string GetDescription(IItem item)
        {
            if (item is not Metal) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not Metal) throw new System.Exception(item.GetType().ToString());
            return Datum[(Metal)item].ToString();
        }

        public void DecreaseLevel(IItem item)
        {
            if (item is not Metal) throw new System.Exception(item.GetType().ToString());
            Datum[(Metal)item] = -1 < 0 ? 0 : Datum[(Metal)item] - 1;
        }

        public int GetLevel(IItem item)
        {
            if (item is not Metal) throw new System.Exception(item.GetType().ToString());
            return Datum[(Metal)item];
        }

        public void IncreaseLevel(IItem item)
        {
            if (item is not Metal) throw new System.Exception(item.GetType().ToString());
            Datum[(Metal)item] = +1 > 999 ? 999 : Datum[(Metal)item] + 1;
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not Metal) throw new System.Exception(item.GetType().ToString());
            Datum[(Metal)item] = level;
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