using System;
using System.Collections.Generic;

namespace Datum
{
    [System.Serializable]
    public class ERhythmCellData : IData
    {
        private Dictionary<IERhythmCell, int> _datum;
        private Dictionary<IERhythmCell, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<IERhythmCell, int> SetUpDatum()
        {
            Dictionary<IERhythmCell, int> datum = new();
            for (int i = 0; i < Items.Length; i++)
                datum.TryAdd((IERhythmCell)Items[i], 0);

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
                    var enums = Enumeration.All<ERhythmCellEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = ERhythmCellEnum.ToItem(enums[i]);
                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not IERhythmCell)
                throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not IERhythmCell)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(IERhythmCell)item].ToString();
        }

        public int GetLevel(IItem item)
        {
            if (item is not IERhythmCell)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(IERhythmCell)item];
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IERhythmCell || i < 0)
                throw new System.Exception(item.GetType().ToString() + " " + i);
            Datum[(IERhythmCell)item] += i;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not IERhythmCell)
                throw new System.Exception(item.GetType().ToString());
            Datum[(IERhythmCell)item] = level;
            PersistentData.Save(this);
        }

        private void LoadValue(IItem item, int value)
        {
            if (item is not IERhythmCell)
                throw new System.Exception(item.GetType().ToString());
            Datum[(IERhythmCell)item] = value;
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
            _datum = SetUpDatum();
            PersistentData.Save(this);
        }

        private ERhythmCellData() { }

        public static ERhythmCellData GetData()
        {
            ERhythmCellData data = new();
            if (data.PersistentData.TryLoadData() is not ERhythmCellData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadValue(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData(nameof(ERhythmCellData));
    }

    public interface IERhythmCell : IItem
    {
        ERhythmCellEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public struct EEEE : IERhythmCell { public readonly ERhythmCellEnum Enum => ERhythmCellEnum.EEEE; }
    [Serializable] public struct H : IERhythmCell { public readonly ERhythmCellEnum Enum => ERhythmCellEnum.H; }
    [Serializable] public struct QQ : IERhythmCell { public readonly ERhythmCellEnum Enum => ERhythmCellEnum.QQ; }
    [Serializable] public struct QEE : IERhythmCell { public readonly ERhythmCellEnum Enum => ERhythmCellEnum.QEE; }
    [Serializable] public struct EEQ : IERhythmCell { public readonly ERhythmCellEnum Enum => ERhythmCellEnum.EEQ; }
    [Serializable] public struct DQE : IERhythmCell { public readonly ERhythmCellEnum Enum => ERhythmCellEnum.DQE; }
    [Serializable] public struct EDQ : IERhythmCell { public readonly ERhythmCellEnum Enum => ERhythmCellEnum.EDQ; }
    [Serializable] public struct EQE : IERhythmCell { public readonly ERhythmCellEnum Enum => ERhythmCellEnum.EQE; }

    [Serializable]
    public class ERhythmCellEnum : Enumeration
    {
        public ERhythmCellEnum() : base(0, null) { }
        public ERhythmCellEnum(int id, string name) : base(id, name) { }

        public readonly string Description;
        public static ERhythmCellEnum EEEE = new(0, "EEEE");
        public static ERhythmCellEnum H = new(1, "H");
        public static ERhythmCellEnum QQ = new(2, "QQ");
        public static ERhythmCellEnum QEE = new(3, "QEE");
        public static ERhythmCellEnum EEQ = new(4, "EEQ");
        public static ERhythmCellEnum DQE = new(5, "Q.E");
        public static ERhythmCellEnum EDQ = new(6, "EQ.");
        public static ERhythmCellEnum EQE = new(7, "EQE");

        internal static IItem ToItem(ERhythmCellEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == EEEE => new EEEE(),
                _ when @enum == H => new H(),
                _ when @enum == QQ => new QQ(),
                _ when @enum == QEE => new QEE(),
                _ when @enum == EEQ => new EEQ(),
                _ when @enum == DQE => new DQE(),
                _ when @enum == EDQ => new EDQ(),
                _ when @enum == EQE => new EQE(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}