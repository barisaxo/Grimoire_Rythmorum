using System;
using System.Collections.Generic;

namespace Datum.BeatFishing
{
    [System.Serializable]
    public class BeatFishingPracticeData : IData
    {
        private Dictionary<IBeatFishingPractice, int> _datum;
        private Dictionary<IBeatFishingPractice, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<IBeatFishingPractice, int> SetUpDatum()
        {
            Dictionary<IBeatFishingPractice, int> datum = new();
            for (int i = 0; i < Items.Length; i++)
                datum.TryAdd((IBeatFishingPractice)Items[i], 0);

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
                    var enums = Enumeration.All<BeatFishingPracticeEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = BeatFishingPracticeEnum.ToItem(enums[i]);
                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not IBeatFishingPractice)
                throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not IBeatFishingPractice)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(IBeatFishingPractice)item].ToString();
        }

        public int GetLevel(IItem item)
        {
            if (item is not IBeatFishingPractice)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(IBeatFishingPractice)item];
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IBeatFishingPractice || i < 0)
                throw new System.Exception(item.GetType().ToString() + " " + i);
            Datum[(IBeatFishingPractice)item] += i;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not IBeatFishingPractice)
                throw new System.Exception(item.GetType().ToString());
            Datum[(IBeatFishingPractice)item] = level;
            PersistentData.Save(this);
        }

        private void LoadValue(IItem item, int value)
        {
            if (item is not IBeatFishingPractice)
                throw new System.Exception(item.GetType().ToString());
            Datum[(IBeatFishingPractice)item] = value;
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
            _datum = SetUpDatum();
            PersistentData.Save(this);
        }

        private BeatFishingPracticeData() { }

        public static BeatFishingPracticeData GetData()
        {
            BeatFishingPracticeData data = new();
            if (data.PersistentData.TryLoadData() is not BeatFishingPracticeData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadValue(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData(nameof(BeatFishingPracticeData));
    }

    public interface IBeatFishingPractice : IItem
    {
        BeatFishingPracticeEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public struct QQQQ : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.QQQQ; }
    [Serializable] public struct W : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.W; }
    [Serializable] public struct HH : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.HH; }
    [Serializable] public struct HQQ : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.HQQ; }
    [Serializable] public struct QQH : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.QQH; }
    [Serializable] public struct DHQ : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.DHQ; }
    [Serializable] public struct QDH : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.QDH; }
    [Serializable] public struct QHQ : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.QHQ; }

    [Serializable]
    public class BeatFishingPracticeEnum : Enumeration
    {
        public BeatFishingPracticeEnum() : base(0, null) { }
        public BeatFishingPracticeEnum(int id, string name) : base(id, name) { }

        public readonly string Description;
        public static BeatFishingPracticeEnum QQQQ = new(0, "QQQQ");
        public static BeatFishingPracticeEnum HH = new(1, "HH");
        public static BeatFishingPracticeEnum W = new(2, "W");
        public static BeatFishingPracticeEnum HQQ = new(3, "HQQ");
        public static BeatFishingPracticeEnum QQH = new(4, "QQH");
        public static BeatFishingPracticeEnum DHQ = new(5, "H.Q");
        public static BeatFishingPracticeEnum QDH = new(6, "QH.");
        public static BeatFishingPracticeEnum QHQ = new(7, "QHQ");

        internal static IItem ToItem(BeatFishingPracticeEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == QQQQ => new QQQQ(),
                _ when @enum == HH => new HH(),
                _ when @enum == W => new W(),
                _ when @enum == HQQ => new HQQ(),
                _ when @enum == QQH => new QQH(),
                _ when @enum == DHQ => new DHQ(),
                _ when @enum == QDH => new QDH(),
                _ when @enum == QHQ => new QHQ(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}