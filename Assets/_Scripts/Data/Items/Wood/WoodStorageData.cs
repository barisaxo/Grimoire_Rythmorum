using System.Collections.Generic;

namespace Data.Two
{
    [System.Serializable]
    public class WoodStorageData : IData
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
            PersistentData.Save(this);
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
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not Wood) throw new System.Exception(item.GetType().ToString());
            Datum[(Wood)item] = level;
            PersistentData.Save(this);
        }

        private void LoadLevel(IItem item, int level)
        {
            if (item is not Wood) throw new System.Exception(item.GetType().ToString());
            Datum[(Wood)item] = level;
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
            _datum = SetUpDatum();
            PersistentData.Save(this);
        }

        private WoodStorageData() { }

        public static WoodStorageData GetData()
        {
            WoodStorageData data = new();
            if (data.PersistentData.TryLoadData() is not WoodStorageData loadData) return data;
            for (int i = 0; i < Items.Length; i++)
                try { data.LoadLevel(Items[i], loadData.GetLevel(Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Materials.Data");
    }

}