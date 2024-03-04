// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class StarChartsData
// {
//     private (DataItem chartItem, int count)[] _chartLevels;
//     private (DataItem chartItem, int count)[] StarChartsLevels => _chartLevels ??= SetUpStarChartsLevels();
//     private (DataItem chartItem, int count)[] SetUpStarChartsLevels()
//     {
//         var items = Enumeration.All<DataItem>();
//         var chartLevels = new (DataItem chartItem, int count)[items.Length];

//         for (int i = 0; i < items.Length; i++) chartLevels[i] = (items[i], 0);

//         return chartLevels;
//     }

//     /// <summary>
//     /// Give this to the menu objects text to display the current chart count.
//     /// </summary>
//     /// <returns>an int 0 to 100</returns>
//     public string GetItemCount(DataItem item) => StarChartsLevels[item].count.ToString();

//     public void IncreaseCount(DataItem item) =>
//         StarChartsLevels[item].count = StarChartsLevels[item].count + 5 > 100 ? 0 : StarChartsLevels[item].count + 5;

//     public void DecreaseCount(DataItem item) =>
//         StarChartsLevels[item].count = StarChartsLevels[item].count - 5 < 0 ? 100 : StarChartsLevels[item].count - 5;

//     public void SetCount(DataItem item, int newStarChartsLevel) => StarChartsLevels[item].count = newStarChartsLevel;

//     public class DataItem : DataEnum
//     {
//         public DataItem() : base(0, "") { }
//         public DataItem(int id, string name) : base(id, name) { }
//         public DataItem(int id, string name, string description) : base(id, name) => Description = description;
//         public static DataItem NotesT = new(0, "Notes: Theory", "Difficulty: *");
//         public static DataItem NotesA = new(1, "Notes: Aural", "Difficulty: !");
//         public static DataItem StepsT = new(2, "Steps: Theory", "Difficulty: **");
//         public static DataItem StepsA = new(3, "Steps: Aural", "Difficulty: !!");
//         public static DataItem ScalesT = new(4, "Scales: Theory", "Difficulty: ***");
//         public static DataItem ScalesA = new(5, "Scales: Aural", "Difficulty: !!!");
//         public static DataItem IntervalsT = new(6, "Intervals: Theory", "Difficulty: ***");
//         public static DataItem IntervalsA = new(7, "Intervals: Aural", "Difficulty: !!!");
//         public static DataItem TriadsT = new(8, "Triads: Theory", "Difficulty: ***");
//         public static DataItem TriadsA = new(9, "Triads: Aural", "Difficulty: !!!");
//         public static DataItem InversionsT = new(10, "Inversions: Theory", "Difficulty: ****");
//         public static DataItem InversionsA = new(11, "Inversions: Aural", "Difficulty: !!!!");
//         public static DataItem InvertedTriadsT = new(12, "Inverted Triads: Theory", "Difficulty: ****");
//         public static DataItem InvertedTriadsA = new(13, "Inverted Triads: Aural", "Difficulty: !!!!");
//         public static DataItem SeventhChordsT = new(14, "7th Chords: Theory", "Difficulty: ****");
//         public static DataItem SeventhChordsA = new(15, "7th Chords: Aural", "Difficulty: !!!!");
//         public static DataItem ModesT = new(16, "Modes: Theory", "Difficulty: *****");
//         public static DataItem ModesA = new(17, "Modes: Aural", "Difficulty: !!!!!");
//         public static DataItem Inverted7thChordsT = new(18, "Inverted 7th Chords: Theory", "Difficulty: *****");
//         public static DataItem Inverted7thChordsA = new(19, "Inverted 7th Chords: Aural", "Difficulty: !!!!!");
//     }
// }
