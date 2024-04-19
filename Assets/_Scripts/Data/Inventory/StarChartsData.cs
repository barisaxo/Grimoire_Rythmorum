using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Data.Inventory
{
    [System.Serializable]
    public class StarChartsData : IData
    {
        private Dictionary<DataItem, int> _starCharts;
        private Dictionary<DataItem, int> StarCharts => _starCharts ??= SetUpStarCharts();
        Dictionary<DataItem, int> SetUpStarCharts()
        {
            Dictionary<DataItem, int> quests = new();
            foreach (var item in DataItems) quests.TryAdd((DataItem)item, 0);
            return quests;
        }

        public void Reset() => _starCharts = SetUpStarCharts();
        public string GetDisplayLevel(DataEnum item) => StarCharts.GetValueOrDefault((DataItem)item).ToString();
        public int GetLevel(DataEnum item) => StarCharts.GetValueOrDefault((DataItem)item);

        public void IncreaseLevel(DataEnum item)
        {
            Debug.Log(StarCharts[(DataItem)item]);
            StarCharts[(DataItem)item] += StarCharts[(DataItem)item] > 998 ? 0 : 1;
            Debug.Log(StarCharts[(DataItem)item]);
            PersistentData.Save(this);
        }

        public void DecreaseLevel(DataEnum item)
        {
            StarCharts[(DataItem)item] -= StarCharts[(DataItem)item] < 1 ? 0 : 1;
            PersistentData.Save(this);
        }

        public void SetLevel(DataEnum item, int count)
        {
            if (StarCharts.ContainsKey((DataItem)item)) StarCharts[(DataItem)item] = count;
            else StarCharts.TryAdd((DataItem)item, count);
        }

        public bool InventoryIsFull(int Space)
        {
            int i = 0;
            foreach (var item in DataItems) i += GetLevel(item);
            return i >= Space;
        }

        public DataEnum[] DataItems => Enumeration.All<DataItem>();

        [System.Serializable]
        public class DataItem : DataEnum
        {
            public DataItem() : base(0, "") { }
            public DataItem(int id, string name) : base(id, name) { }
            public DataItem(int id, string name, string description) : base(id, name) => Description = description;
            public static DataItem NotesT = new(0, "Notes[Theory]", "Difficulty: *");
            public static DataItem NotesA = new(1, "Notes[Aural]", "Difficulty: !");
            public static DataItem StepsT = new(2, "Steps[Theory]", "Difficulty: **");
            public static DataItem StepsA = new(3, "Steps[Aural]", "Difficulty: !!");
            public static DataItem ScalesT = new(4, "Scales[Theory]", "Difficulty: ***");
            public static DataItem ScalesA = new(5, "Scales[Aural]", "Difficulty: !!!");
            public static DataItem IntervalsT = new(6, "Intervals[Theory]", "Difficulty: ***");
            public static DataItem IntervalsA = new(7, "Intervals[Aural]", "Difficulty: !!!");
            public static DataItem TriadsT = new(8, "Triads[Theory]", "Difficulty: ***");
            public static DataItem TriadsA = new(9, "Triads[Aural]", "Difficulty: !!!");
            public static DataItem InversionsT = new(10, "Inversions[Theory]", "Difficulty: ****");
            public static DataItem InversionsA = new(11, "Inversions[Aural]", "Difficulty: !!!!");
            public static DataItem InvertedTriadsT = new(12, "Inverted Triads[Theory]", "Difficulty: ****");
            public static DataItem InvertedTriadsA = new(13, "Inverted Triads[Aural]", "Difficulty: !!!!");
            public static DataItem SeventhChordsT = new(14, "7th Chords[Theory]", "Difficulty: ****");
            public static DataItem SeventhChordsA = new(15, "7th Chords[Aural]", "Difficulty: !!!!");
            public static DataItem ModesT = new(16, "Modes[Theory]", "Difficulty: *****");
            public static DataItem ModesA = new(17, "Modes[Aural]", "Difficulty: !!!!!");
            public static DataItem Inverted7thChordsT = new(18, "Inverted 7th Chords[Theory]", "Difficulty: *****");
            public static DataItem Inverted7thChordsA = new(19, "Inverted 7th Chords[Aural]", "Difficulty: !!!!!");
        }

        private StarChartsData() { }

        public static StarChartsData GetData()
        {
            StarChartsData data = new();
            if (data.PersistentData.TryLoadData() is not StarChartsData loadData) return data;
            for (int i = 0; i < data.DataItems.Length; i++)
                try { data.SetLevel(data.DataItems[i], loadData.GetLevel(data.DataItems[i])); }
                catch { }
            data.PersistentData.Save(data);
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("StarCharts.Data");
    }
}