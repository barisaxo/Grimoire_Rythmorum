using System.Collections;
using System.Collections.Generic;

namespace Datum
{
    [System.Serializable]
    public class PuzzleData : IData
    {
        private Dictionary<object, int> _datum;
        public Dictionary<object, int> Datum => _datum ??= new();

        public void SetDatum(Dictionary<object, int> datum) => _datum = datum;

        public IItem[] Items { get; } = null;

        public void Reset() => _datum = new();

        public string GetDisplayLevel(IItem item)
        {
            throw new System.ArgumentException(item.Name);
        }

        public int GetLevel(IPuzzle item)
        {
            if (item is not IPuzzle) throw new System.Exception(item.GetType().ToString());
            return Datum.ContainsKey(item.GetType()) ? Datum[item.GetType()] : 0;
        }

        public int GetLevel(IItem item)
        {
            throw new System.ArgumentException(item.Name);
        }

        public void AdjustLevel(IItem item, int i)
        {
            throw new System.ArgumentException(item.Name + " " + i.ToString());
        }

        public void AdjustLevel(IPuzzle item, int i)
        {
            if (i < 0 || item is null) throw new System.ArgumentException(item.GetType() + " " + i.ToString());

            UnityEngine.Debug.Log("dict contains System.Type: " + item.GetType() + " ? " + Datum.ContainsKey(item.GetType()));

            if (Datum.ContainsKey(item.GetType())) Datum[item.GetType()] += i;

            else Datum.TryAdd(item.GetType(), i);

            UnityEngine.Debug.Log("dict contains System.Type: " + item.GetType() + "? " + Datum.ContainsKey(item.GetType()) + ", quant: " + Datum[item.GetType()]);

            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            throw new System.ArgumentException(item.Name + " " + level.ToString());
        }

        // public void SetLevel(System.Type item, int level)
        // {
        //     Datum[item] = level;
        //     PersistentData.Save(this);
        // }

        // private void LoadLevel(IItem item, int level)
        // {
        //     throw new System.ArgumentException(item.Name + " " + level.ToString());
        // }

        // private void LoadLevel(System.Type item, int level)
        // {
        //     Datum[item] = level;
        // }

        public bool InventoryIsFull(int _) => false;

        private PuzzleData() { }

        public static PuzzleData GetData()
        {
            PuzzleData data = new();
            if (data.PersistentData.TryLoadData() is not PuzzleData loadData) return data;
            data.SetDatum(loadData.Datum);
            return data;
        }

        string IData.GetDescription(IItem item)
        {
            return "???";
        }

        public IPersistentData PersistentData { get; } = new SaveData(nameof(PuzzleData));

    }
}