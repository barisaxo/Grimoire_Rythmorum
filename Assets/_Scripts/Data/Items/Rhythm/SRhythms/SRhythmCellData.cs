using System;
using System.Collections.Generic;

namespace Datum
{
    [System.Serializable]
    public class SRhythmCellData : IData
    {
        private Dictionary<ISRhythmCell, int> _datum;
        private Dictionary<ISRhythmCell, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<ISRhythmCell, int> SetUpDatum()
        {
            Dictionary<ISRhythmCell, int> datum = new();
            for (int i = 0; i < Items.Length; i++)
                datum.TryAdd((ISRhythmCell)Items[i], 0);

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
                    var enums = Enumeration.All<SRhythmCellEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = SRhythmCellEnum.ToItem(enums[i]);
                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not ISRhythmCell)
                throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not ISRhythmCell)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(ISRhythmCell)item].ToString();
        }

        public int GetLevel(IItem item)
        {
            if (item is not ISRhythmCell)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(ISRhythmCell)item];
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not ISRhythmCell || i < 0)
                throw new System.Exception(item.GetType().ToString() + " " + i);
            Datum[(ISRhythmCell)item] += i;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not ISRhythmCell)
                throw new System.Exception(item.GetType().ToString());
            Datum[(ISRhythmCell)item] = level;
            PersistentData.Save(this);
        }

        private void LoadValue(IItem item, int value)
        {
            if (item is not ISRhythmCell)
                throw new System.Exception(item.GetType().ToString());
            Datum[(ISRhythmCell)item] = value;
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
            _datum = SetUpDatum();
            PersistentData.Save(this);
        }

        private SRhythmCellData() { }

        public static SRhythmCellData GetData()
        {
            SRhythmCellData data = new();
            if (data.PersistentData.TryLoadData() is not SRhythmCellData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadValue(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData(nameof(SRhythmCellData));
    }

    public interface ISRhythmCell : IItem
    {
        SRhythmCellEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public struct SSSS : ISRhythmCell { public readonly SRhythmCellEnum Enum => SRhythmCellEnum.SSSS; }
    [Serializable] public struct Q : ISRhythmCell { public readonly SRhythmCellEnum Enum => SRhythmCellEnum.Q; }
    [Serializable] public struct EE : ISRhythmCell { public readonly SRhythmCellEnum Enum => SRhythmCellEnum.EE; }
    [Serializable] public struct ESS : ISRhythmCell { public readonly SRhythmCellEnum Enum => SRhythmCellEnum.ESS; }
    [Serializable] public struct SSE : ISRhythmCell { public readonly SRhythmCellEnum Enum => SRhythmCellEnum.SSE; }
    [Serializable] public struct DES : ISRhythmCell { public readonly SRhythmCellEnum Enum => SRhythmCellEnum.DES; }
    [Serializable] public struct SDE : ISRhythmCell { public readonly SRhythmCellEnum Enum => SRhythmCellEnum.SDE; }
    [Serializable] public struct SES : ISRhythmCell { public readonly SRhythmCellEnum Enum => SRhythmCellEnum.SES; }

    [Serializable]
    public class SRhythmCellEnum : Enumeration
    {
        public SRhythmCellEnum() : base(0, null) { }
        public SRhythmCellEnum(int id, string name) : base(id, name) { }

        public readonly string Description;
        public static SRhythmCellEnum SSSS = new(0, "SSSS");
        public static SRhythmCellEnum Q = new(1, "Q");
        public static SRhythmCellEnum EE = new(2, "EE");
        public static SRhythmCellEnum ESS = new(3, "ESS");
        public static SRhythmCellEnum SSE = new(4, "SSE");
        public static SRhythmCellEnum DES = new(5, "E.S");
        public static SRhythmCellEnum SDE = new(6, "SE.");
        public static SRhythmCellEnum SES = new(7, "SES");

        internal static IItem ToItem(SRhythmCellEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == SSSS => new SSSS(),
                _ when @enum == Q => new Q(),
                _ when @enum == EE => new EE(),
                _ when @enum == ESS => new ESS(),
                _ when @enum == SSE => new SSE(),
                _ when @enum == DES => new DES(),
                _ when @enum == SDE => new SDE(),
                _ when @enum == SES => new SES(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}