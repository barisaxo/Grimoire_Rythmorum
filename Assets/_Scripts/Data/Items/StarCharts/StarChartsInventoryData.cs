using System.Collections.Generic;

namespace Data.Two
{
    public class StarChartInventoryData : IData
    {
        private Dictionary<StarChart, int> _datum;
        private Dictionary<StarChart, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<StarChart, int> SetUpDatum()
        {
            Dictionary<StarChart, int> datum = new();
            foreach (IItem item in Items) datum.TryAdd(item as StarChart, 0);
            return datum;
        }

        public static IItem[] Items => new StarChart[] {
            new NotesT(),
            new NotesA(),
            new StepsT(),
            new StepsA(),
            new ScalesT (),
            new ScalesA (),
            new IntervalsT (),
            new IntervalsA (),
            new TriadsT (),
            new TriadsA (),
            new InversionsT(),
            new InversionsA(),
            new InvertedTriadsT(),
            new InvertedTriadsA(),
            new SeventhChordsT (),
            new SeventhChordsA (),
            new ModesT(),
            new ModesA(),
            new Inverted7thChordsT(),
            new Inverted7thChordsA(),
            };

        public string GetDescription(IItem item)
        {
            if (item is not StarChart) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not StarChart) throw new System.Exception(item.GetType().ToString());
            return Datum[(StarChart)item].ToString();
        }

        public void DecreaseLevel(IItem item)
        {
            if (item is not StarChart) throw new System.Exception(item.GetType().ToString());
            Datum[(StarChart)item] = -1 < 0 ? 0 : Datum[(StarChart)item] - 1;
        }

        public int GetLevel(IItem item)
        {
            if (item is not StarChart) throw new System.Exception(item.GetType().ToString());
            return Datum[(StarChart)item];
        }

        public void IncreaseLevel(IItem item)
        {
            if (item is not StarChart) throw new System.Exception(item.GetType().ToString());
            Datum[(StarChart)item] = +1 > 999 ? 999 : Datum[(StarChart)item] + 1;
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not StarChart) throw new System.Exception(item.GetType().ToString());
            Datum[(StarChart)item] = level;
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