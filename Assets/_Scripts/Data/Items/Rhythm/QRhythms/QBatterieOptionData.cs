using System;
using System.Collections.Generic;

namespace Datum.QRhythm
{
    [System.Serializable]
    public class QBatterieOptionData : IData
    {
        private Dictionary<IQBatterieOption, int> _datum;
        private Dictionary<IQBatterieOption, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<IQBatterieOption, int> SetUpDatum()
        {
            Dictionary<IQBatterieOption, int> datum = new();
            for (int i = 0; i < Items.Length; i++)
                datum.TryAdd((IQBatterieOption)Items[i], 0);

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
                    var enums = Enumeration.All<QBatterieOptionEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = QBatterieOptionEnum.ToItem(enums[i]);
                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not IQBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not IQBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(IQBatterieOption)item].ToString();
        }

        public int GetLevel(IItem item)
        {
            if (item is not IQBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(IQBatterieOption)item];
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IQBatterieOption || i < 0)
                throw new System.Exception(item.GetType().ToString() + " " + i);
            Datum[(IQBatterieOption)item] += i;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not IQBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            Datum[(IQBatterieOption)item] = level;
            PersistentData.Save(this);
        }

        private void LoadValue(IItem item, int value)
        {
            if (item is not IQBatterieOption)
                throw new System.Exception(item.GetType().ToString());
            Datum[(IQBatterieOption)item] = value;
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
            _datum = SetUpDatum();
            PersistentData.Save(this);
        }

        private QBatterieOptionData() { }

        public static QBatterieOptionData GetData()
        {
            QBatterieOptionData data = new();
            if (data.PersistentData.TryLoadData() is not QBatterieOptionData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadValue(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData(nameof(QBatterieOptionData));
    }

    public interface IQBatterieOption : IItem
    {
        QBatterieOptionEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    // [Serializable] public struct QQQQ : IQBatterieOption { public readonly QBatterieOptionEnum Enum => QBatterieOptionEnum.QQQQ; }
    // [Serializable] public struct W : IQBatterieOption { public readonly QBatterieOptionEnum Enum => QBatterieOptionEnum.W; }
    // [Serializable] public struct HH : IQBatterieOption { public readonly QBatterieOptionEnum Enum => QBatterieOptionEnum.HH; }
    // [Serializable] public struct HQQ : IQBatterieOption { public readonly QBatterieOptionEnum Enum => QBatterieOptionEnum.HQQ; }
    [Serializable] public struct NoRestsOrTies : IQBatterieOption { public readonly QBatterieOptionEnum Enum => QBatterieOptionEnum.NoRestsOrTies; }
    [Serializable] public struct Rests : IQBatterieOption { public readonly QBatterieOptionEnum Enum => QBatterieOptionEnum.Rests; }
    [Serializable] public struct Ties : IQBatterieOption { public readonly QBatterieOptionEnum Enum => QBatterieOptionEnum.Ties; }
    [Serializable] public struct RestsAndTies : IQBatterieOption { public readonly QBatterieOptionEnum Enum => QBatterieOptionEnum.RestsAndTies; }

    [Serializable]
    public class QBatterieOptionEnum : Enumeration
    {
        public QBatterieOptionEnum() : base(0, null) { }
        public QBatterieOptionEnum(int id, string name) : base(id, name) { }

        public readonly string Description;
        public static QBatterieOptionEnum NoRestsOrTies = new(0, "No Rests Or Ties");
        public static QBatterieOptionEnum Rests = new(1, "With Rests");
        public static QBatterieOptionEnum Ties = new(2, "With Ties");
        public static QBatterieOptionEnum RestsAndTies = new(3, "With Rests and Ties");

        internal static IItem ToItem(QBatterieOptionEnum @enum)
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
