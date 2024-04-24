using System.Collections.Generic;

namespace Data.Two
{
    public class GramophoneInventoryData : IData
    {
        private Dictionary<Gramophone, int> _datum;
        private Dictionary<Gramophone, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<Gramophone, int> SetUpDatum()
        {
            Dictionary<Gramophone, int> datum = new();
            foreach (IItem item in Items) datum.TryAdd(item as Gramophone, 0);
            return datum;
        }

        public static IItem[] Items => new Gramophone[] {
            new Gramo1(),new Gramo2(), new Gramo3(),new Gramo4(),new Gramo5() };

        public string GetDescription(IItem item)
        {
            if (item is not Gramophone) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not Gramophone) throw new System.Exception(item.GetType().ToString());
            return Datum[(Gramophone)item].ToString();
        }

        public void DecreaseLevel(IItem item)
        {
            if (item is not Gramophone) throw new System.Exception(item.GetType().ToString());
            Datum[(Gramophone)item] = -1 < 0 ? 0 : Datum[(Gramophone)item] - 1;
        }

        public int GetLevel(IItem item)
        {
            if (item is not Gramophone) throw new System.Exception(item.GetType().ToString());
            return Datum[(Gramophone)item];
        }

        public void IncreaseLevel(IItem item)
        {
            if (item is not Gramophone) throw new System.Exception(item.GetType().ToString());
            Datum[(Gramophone)item] = +1 > 999 ? 999 : Datum[(Gramophone)item] + 1;
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not Gramophone) throw new System.Exception(item.GetType().ToString());
            Datum[(Gramophone)item] = level;
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