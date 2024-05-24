using System.Collections.Generic;

namespace Data.Two
{
    public class GramophoneInventoryData : IData
    {
        private Dictionary<IGramophone, int> _datum;
        private Dictionary<IGramophone, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<IGramophone, int> SetUpDatum()
        {
            Dictionary<IGramophone, int> datum = new();
            foreach (IItem item in Items) datum.TryAdd(item as IGramophone, 0);
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
                    var enums = Enumeration.All<GramophoneEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < enums.Length; i++)
                        temp[i] = GramophoneEnum.ToItem(enums[i]);
                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not IGramophone) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }
        public int GetLevel(IItem item)
        {
            return 1;
            // if (item is not IGramophone) throw new System.Exception(item.GetType().ToString());
            // return Datum[(IGramophone)item];
        }


        public string GetDisplayLevel(IItem item)
        {
            return "";
            // if (item is not IGramophone) throw new System.Exception(item.GetType().ToString());
            // return Datum[(IGramophone)item].ToString();
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IGramophone) throw new System.Exception(item.GetType().ToString());
            Datum[(IGramophone)item] =
                Datum[(IGramophone)item] + i > 999 ? 999 :
                Datum[(IGramophone)item] + i < 0 ? 0 :
                Datum[(IGramophone)item] + i;
        }

        // public void DecreaseLevel(IItem item)
        // {
        //     if (item is not Gramophone) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Gramophone)item] = Datum[(Gramophone)item] - 1 < 0 ? 0 : Datum[(Gramophone)item] - 1;
        // }
        // public void DecreaseLevel(IItem item, int i)
        // {
        //     if (item is not Gramophone) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Gramophone)item] = Datum[(Gramophone)item] - i < 0 ? 0 : Datum[(Gramophone)item] - i;
        // }
        // public void IncreaseLevel(IItem item)
        // {
        //     if (item is not Gramophone) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Gramophone)item] = Datum[(Gramophone)item] + 1 > 999 ? 999 : Datum[(Gramophone)item] + 1;
        // }
        // public void IncreaseLevel(IItem item, int i)
        // {
        //     if (item is not Gramophone) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Gramophone)item] = Datum[(Gramophone)item] + i > 999 ? 999 : Datum[(Gramophone)item] + i;
        // }

        public void SetLevel(IItem item, int level)
        {
            if (item is not IGramophone) throw new System.Exception(item.GetType().ToString());
            Datum[(IGramophone)item] = level;
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