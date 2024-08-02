using System;
using System.Collections.Generic;

namespace Datum.SRhythm
{
    [System.Serializable]
    public class SBatterieOptionData : IData
    {
        private Dictionary<ISBatterieOption, int> _datum;
        private Dictionary<ISBatterieOption, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<ISBatterieOption, int> SetUpDatum()
        {
            Dictionary<ISBatterieOption, int> datum = new();
            for (int i = 0; i < Items.Length; i++)
                datum.TryAdd((ISBatterieOption)Items[i], 0);

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
                    var enums = Enumeration.All<SBatterieOptionEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = SBatterieOptionEnum.ToItem(enums[i]);
                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not ISBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not ISBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(ISBatterieOption)item].ToString();
        }

        public int GetLevel(IItem item)
        {
            if (item is not ISBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(ISBatterieOption)item];
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not ISBatterieOption || i < 0)
                throw new System.Exception(item.GetType().ToString() + " " + i);
            Datum[(ISBatterieOption)item] += i;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not ISBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            Datum[(ISBatterieOption)item] = level;
            PersistentData.Save(this);
        }

        private void LoadValue(IItem item, int value)
        {
            if (item is not ISBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            Datum[(ISBatterieOption)item] = value;
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
            _datum = SetUpDatum();
            PersistentData.Save(this);
        }

        private SBatterieOptionData() { }

        public static SBatterieOptionData GetData()
        {
            SBatterieOptionData data = new();
            if (data.PersistentData.TryLoadData() is not SBatterieOptionData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadValue(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData(nameof(SBatterieOptionData));
    }

    public interface ISBatterieOption : IItem
    {
        SBatterieOptionEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    // [Serializable] public struct QQQQ : ISBatterieOption { public readonly SBatterieOptionEnum Enum => SBatterieOptionEnum.QQQQ; }
    // [Serializable] public struct W : ISBatterieOption { public readonly SBatterieOptionEnum Enum => SBatterieOptionEnum.W; }
    // [Serializable] public struct HH : ISBatterieOption { public readonly SBatterieOptionEnum Enum => SBatterieOptionEnum.HH; }
    // [Serializable] public struct HQQ : ISBatterieOption { public readonly SBatterieOptionEnum Enum => SBatterieOptionEnum.HQQ; }
    [Serializable] public struct NoRestsOrTies : ISBatterieOption { public readonly SBatterieOptionEnum Enum => SBatterieOptionEnum.NoRestsOrTies; }
    [Serializable] public struct Rests : ISBatterieOption { public readonly SBatterieOptionEnum Enum => SBatterieOptionEnum.Rests; }
    [Serializable] public struct Ties : ISBatterieOption { public readonly SBatterieOptionEnum Enum => SBatterieOptionEnum.Ties; }
    [Serializable] public struct RestsAndTies : ISBatterieOption { public readonly SBatterieOptionEnum Enum => SBatterieOptionEnum.RestsAndTies; }

    [Serializable]
    public class SBatterieOptionEnum : Enumeration
    {
        public SBatterieOptionEnum() : base(0, null) { }
        public SBatterieOptionEnum(int id, string name) : base(id, name) { }

        public readonly string Description;
        public static SBatterieOptionEnum NoRestsOrTies = new(0, "No Rests Or Ties");
        public static SBatterieOptionEnum Rests = new(1, "With Rests");
        public static SBatterieOptionEnum Ties = new(2, "With Ties");
        public static SBatterieOptionEnum RestsAndTies = new(3, "With Rests and Ties");

        internal static IItem ToItem(SBatterieOptionEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == NoRestsOrTies => new NoRestsOrTies(),
                _ when @enum == Rests => new Rests(),
                _ when @enum == Ties => new Ties(),
                _ when @enum == RestsAndTies => new RestsAndTies(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}