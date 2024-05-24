using System.Collections.Generic;

namespace Data.Two
{
    public class StarChartData : IData
    {
        private Dictionary<IStarChart, int> _datum;
        private Dictionary<IStarChart, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<IStarChart, int> SetUpDatum()
        {
            Dictionary<IStarChart, int> datum = new();
            foreach (IItem item in Items) datum.TryAdd(item as IStarChart, 0);
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
                    var enums = Enumeration.All<StarChartEnum>();
                    var items = new IItem[enums.Length];
                    for (int i = 0; i < enums.Length; i++)
                        items[i] = StarChartEnum.ToItem(enums[i]);
                    return items;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not IStarChart) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not IStarChart) throw new System.Exception(item.GetType().ToString());
            return Datum[(IStarChart)item].ToString();
        }


        public int GetLevel(IItem item)
        {
            if (item is not IStarChart) throw new System.Exception(item.GetType().ToString());
            return Datum[(IStarChart)item];
        }

        // public void DecreaseLevel(IItem item)
        // {
        //     if (item is not StarChart) throw new System.Exception(item.GetType().ToString());
        //     Datum[(StarChart)item] = Datum[(StarChart)item] - 1 < 0 ? 0 : Datum[(StarChart)item] - 1;
        // }
        // public void DecreaseLevel(IItem item, int i)
        // {
        //     if (item is not StarChart) throw new System.Exception(item.GetType().ToString());
        //     Datum[(StarChart)item] = Datum[(StarChart)item] - i < 0 ? 0 : Datum[(StarChart)item] - i;
        // }
        // public void IncreaseLevel(IItem item)
        // {
        //     if (item is not StarChart) throw new System.Exception(item.GetType().ToString());
        //     Datum[(StarChart)item] = Datum[(StarChart)item] + 1 > 999 ? 999 : Datum[(StarChart)item] + 1;
        // }
        // public void IncreaseLevel(IItem item, int i)
        // {
        //     if (item is not StarChart) throw new System.Exception(item.GetType().ToString());
        //     Datum[(StarChart)item] = Datum[(StarChart)item] + i > 999 ? 999 : Datum[(StarChart)item] + i;
        // }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IStarChart) throw new System.Exception(item.GetType().ToString());
            Datum[(IStarChart)item] =
                Datum[(IStarChart)item] + i > 999 ? 999 :
                Datum[(IStarChart)item] + i < 0 ? 0 :
                Datum[(IStarChart)item] + i;
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not IStarChart) throw new System.Exception(item.GetType().ToString());
            Datum[(IStarChart)item] = level;
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