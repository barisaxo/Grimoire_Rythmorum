using System;
using System.Collections.Generic;

namespace Datum.ERhythm
{
    [System.Serializable]
    public class EBatterieOptionData : IData
    {
        private Dictionary<IEBatterieOption, int> _datum;
        private Dictionary<IEBatterieOption, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<IEBatterieOption, int> SetUpDatum()
        {
            Dictionary<IEBatterieOption, int> datum = new();
            for (int i = 0; i < Items.Length; i++)
                datum.TryAdd((IEBatterieOption)Items[i], 0);

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
                    var enums = Enumeration.All<EBatterieOptionEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = EBatterieOptionEnum.ToItem(enums[i]);
                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not IEBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not IEBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(IEBatterieOption)item].ToString();
        }

        public int GetLevel(IItem item)
        {
            if (item is not IEBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(IEBatterieOption)item];
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IEBatterieOption || i < 0)
                throw new System.Exception(item.GetType().ToString() + " " + i);
            Datum[(IEBatterieOption)item] += i;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not IEBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            Datum[(IEBatterieOption)item] = level;
            PersistentData.Save(this);
        }

        private void LoadValue(IItem item, int value)
        {
            if (item is not IEBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            Datum[(IEBatterieOption)item] = value;
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
            _datum = SetUpDatum();
            PersistentData.Save(this);
        }

        private EBatterieOptionData() { }

        public static EBatterieOptionData GetData()
        {
            EBatterieOptionData data = new();
            if (data.PersistentData.TryLoadData() is not EBatterieOptionData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadValue(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData(nameof(EBatterieOptionData));
    }

    public interface IEBatterieOption : IItem
    {
        EBatterieOptionEnum Enum { get; }
        int IItem.Id => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    // [Serializable] public struct QQQQ : IEBatterieOption { public readonly EBatterieOptionEnum Enum => EBatterieOptionEnum.QQQQ; }
    // [Serializable] public struct W : IEBatterieOption { public readonly EBatterieOptionEnum Enum => EBatterieOptionEnum.W; }
    // [Serializable] public struct HH : IEBatterieOption { public readonly EBatterieOptionEnum Enum => EBatterieOptionEnum.HH; }
    // [Serializable] public struct HQQ : IEBatterieOption { public readonly EBatterieOptionEnum Enum => EBatterieOptionEnum.HQQ; }
    [Serializable] public struct NoRestsOrTies : IEBatterieOption { public readonly EBatterieOptionEnum Enum => EBatterieOptionEnum.NoRestsOrTies; }
    [Serializable] public struct Rests : IEBatterieOption { public readonly EBatterieOptionEnum Enum => EBatterieOptionEnum.Rests; }
    [Serializable] public struct Ties : IEBatterieOption { public readonly EBatterieOptionEnum Enum => EBatterieOptionEnum.Ties; }
    [Serializable] public struct RestsAndTies : IEBatterieOption { public readonly EBatterieOptionEnum Enum => EBatterieOptionEnum.RestsAndTies; }

    [Serializable]
    public class EBatterieOptionEnum : Enumeration
    {
        public EBatterieOptionEnum() : base(0, null) { }
        public EBatterieOptionEnum(int id, string name) : base(id, name) { }

        public readonly string Description;
        public static EBatterieOptionEnum NoRestsOrTies = new(0, "No Rests Or Ties");
        public static EBatterieOptionEnum Rests = new(1, "With Rests");
        public static EBatterieOptionEnum Ties = new(2, "With Ties");
        public static EBatterieOptionEnum RestsAndTies = new(3, "With Rests and Ties");

        internal static IItem ToItem(EBatterieOptionEnum @enum)
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