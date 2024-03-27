using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Data.Player
{
    [System.Serializable]
    public class RhythmCellsData : IData
    {
        private Dictionary<DataItem, int> _datum;
        public Dictionary<DataItem, int> RhythmCellsDatum => _datum ??= SetUpRhythmCellsDatum();

        Dictionary<DataItem, int> SetUpRhythmCellsDatum()
        {
            Dictionary<DataItem, int> datum = new();
            foreach (var item in DataItems) datum.TryAdd((DataItem)item, 0);
            return datum;
        }

        public string GetDisplayLevel(DataEnum item)
        {
            var Item = item as DataItem;
            return item.Name + " lvl " + RhythmCellsDatum[(DataItem)item] + " : " + item.Description;
        }

        public int GetLevel(DataEnum item) => RhythmCellsDatum[(DataItem)item];

        public void IncreaseLevel(DataEnum item)
        {
            RhythmCellsDatum[(DataItem)item]++;
            PersistentData.Save(this);
        }

        public void DecreaseLevel(DataEnum item)
        {
            RhythmCellsDatum[(DataItem)item] -= RhythmCellsDatum[(DataItem)item] > 0 ? 1 : 0;
            PersistentData.Save(this);
        }

        public void SetLevel(DataEnum item, int newVolumeLevel)
        {
            RhythmCellsDatum[(DataItem)item] = newVolumeLevel;
            PersistentData.Save(this);
        }

        public DataEnum[] DataItems { get; } = Enumeration.All<DataItem>();

        public bool InventoryIsFull(int Space) => false;

        public Sprite GetSprite(DataEnum item)
        {
            var Item = item as DataItem;
            return Item.Sprite?.Invoke();
        }

        [System.Serializable]
        public class DataItem : DataEnum
        {
            public DataItem() : base(0, "") { }
            public DataItem(int id, string name) : base(id, name) { }
            public DataItem(int id, string name, Func<Sprite> sprite, Func<VideoClip> clip) : base(id, name)
            {
                Sprite = sprite;
                Clip = clip;
            }
            public DataItem(int id, string name, string description) : base(id, name)
            {
                Description = description;
            }
            public readonly Func<Sprite> Sprite;
            public readonly Func<VideoClip> Clip;
            public static DataItem W = new(0, "WHOLE", () => Assets.White, () => Resources.Load<VideoClip>(""));
            public static DataItem HH = new(1, "HALF HALF");
            public static DataItem DHQ = new(2, "DOTTED-HALF QUARTER");
            public static DataItem QDH = new(3, "QUARTER DOTTED-HALF");
            public static DataItem HQQ = new(4, "HALF QUARTER QUARTER");
            public static DataItem QQH = new(5, "QUARTER QUARTER HALF");
            public static DataItem QHQ = new(6, "QUARTER HALF QUARTER");
            public static DataItem QQQQ = new(7, "QUARTER QUARTER QUARTER QUARTER");
            public static DataItem DH = new(8, "DOTTED-HALF");
            public static DataItem HQ = new(9, "HALF QUARTER");
            public static DataItem QH = new(10, "QUARTER HALF");
            public static DataItem QQQ = new(11, "QUARTER QUARTER QUARTER");

            public static DataItem H = new(0 + 12, "HALF");
            public static DataItem QQ = new(1 + 12, "QUARTER QUARTER");
            public static DataItem DQE = new(2 + 12, "DOTTED-QUARTER EIGHTH");
            public static DataItem EDQ = new(3 + 12, "EIGHTH DOTTED-QUARTER");
            public static DataItem QEE = new(4 + 12, "QUARTER EIGHTH EIGHTH");
            public static DataItem EEQ = new(5 + 12, "EIGHTH EIGHTH HALF");
            public static DataItem EQE = new(6 + 12, "EIGHTH QUARTER EIGHTH");
            public static DataItem EEEE = new(7 + 12, "EIGHTH EIGHTH EIGHTH EIGHTH");
            public static DataItem DQ = new(8 + 12, "DOTTED-HALF");
            public static DataItem QE = new(9 + 12, "QUARTER EIGHTH");
            public static DataItem EQ = new(10 + 12, "EIGHTH QUARTER");
            public static DataItem EEE = new(11 + 12, "EIGHTH EIGHTH EIGHTH");

            public static DataItem Q = new(0 + 24, "QUARTER");
            public static DataItem EE = new(1 + 24, "EIGHTH EIGHTH");
            public static DataItem DES = new(2 + 24, "DOTTED-EIGHTH SIXTEENTH");
            public static DataItem SDE = new(3 + 24, "SIXTEENTH DOTTED-EIGHTH");
            public static DataItem ESS = new(4 + 24, "EIGHTH SIXTEENTH SIXTEENTH");
            public static DataItem SSE = new(5 + 24, "SIXTEENTH SIXTEENTH EIGHTH");
            public static DataItem SES = new(6 + 24, "SIXTEENTH QUARTER SIXTEENTH");
            public static DataItem SSSS = new(7 + 24, "SIXTEENTH SIXTEENTH SIXTEENTH SIXTEENTH");
            public static DataItem DE = new(8 + 24, "DOTTED-EIGHTH");
            public static DataItem ES = new(9 + 24, "EIGHTH SIXTEENTH");
            public static DataItem SE = new(10 + 24, "SIXTEENTH EIGHTH");
            public static DataItem SSS = new(11 + 24, "SIXTEENTH SIXTEENTH SIXTEENTH");
        }

        private RhythmCellsData() { }

        public static RhythmCellsData GetData()
        {
            RhythmCellsData data = new();
            if (data.PersistentData.TryLoadData() is not RhythmCellsData loadData) return data;
            for (int i = 0; i < data.DataItems.Length; i++)
                try { data.SetLevel(data.DataItems[i], loadData.GetLevel(data.DataItems[i])); }
                catch { }
            data.PersistentData.Save(data);
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("RhythmCells.Data");

    }
}

