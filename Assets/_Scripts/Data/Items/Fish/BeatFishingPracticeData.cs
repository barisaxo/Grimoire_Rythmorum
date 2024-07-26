using System;
using System.Collections.Generic;

namespace Data
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

    [Serializable] public struct LVL1 : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.LVL1; }

    // [Serializable] public struct SSSS : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.SSSS; }
    // [Serializable] public struct Q : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.Q; }
    // [Serializable] public struct EE : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.EE; }
    // [Serializable] public struct ESS : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.ESS; }
    // [Serializable] public struct SSE : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.SSE; }
    // [Serializable] public struct DES : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.DES; }
    // [Serializable] public struct SDE : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.SDE; }
    // [Serializable] public struct SES : IBeatFishingPractice { public readonly BeatFishingPracticeEnum Enum => BeatFishingPracticeEnum.SES; }

    [Serializable]
    public class BeatFishingPracticeEnum : Enumeration
    {
        public BeatFishingPracticeEnum() : base(0, null) { }
        public BeatFishingPracticeEnum(int id, string name) : base(id, name) { }

        public readonly string Description;
        public static BeatFishingPracticeEnum LVL1 = new(0, "Level 1");
        // public static BeatFishingPracticeEnum Q = new(1, "Q");
        // public static BeatFishingPracticeEnum EE = new(2, "EE");
        // public static BeatFishingPracticeEnum ESS = new(3, "ESS");
        // public static BeatFishingPracticeEnum SSE = new(4, "SSE");
        // public static BeatFishingPracticeEnum DES = new(5, "E.S");
        // public static BeatFishingPracticeEnum SDE = new(6, "SE.");
        // public static BeatFishingPracticeEnum SES = new(7, "SES");

        internal static IItem ToItem(BeatFishingPracticeEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == LVL1 => new LVL1(),
                // _ when @enum == SSSS => new SSSS(),
                // _ when @enum == Q => new Q(),
                // _ when @enum == EE => new EE(),
                // _ when @enum == ESS => new ESS(),
                // _ when @enum == SSE => new SSE(),
                // _ when @enum == DES => new DES(),
                // _ when @enum == SDE => new SDE(),
                // _ when @enum == SES => new SES(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}