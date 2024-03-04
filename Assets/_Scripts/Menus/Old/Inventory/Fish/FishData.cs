// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FishData
// {
//     private (DataItem fishItem, int count)[] _fishLevels;
//     private (DataItem fishItem, int count)[] FishLevels => _fishLevels ??= SetUpFishLevels();
//     private (DataItem fishItem, int count)[] SetUpFishLevels()
//     {
//         var items = Enumeration.All<DataItem>();
//         var fishLevels = new (DataItem fishItem, int count)[items.Length];

//         for (int i = 0; i < items.Length; i++) fishLevels[i] = (items[i], 0);

//         return fishLevels;
//     }

//     /// <summary>
//     /// Give this to the menu objects text to display the current fish count.
//     /// </summary>
//     /// <returns>an int 0 to 100</returns>
//     public string GetItemCount(DataItem item) => FishLevels[item].count.ToString();

//     public void IncreaseCount(DataItem item) =>
//         FishLevels[item].count = FishLevels[item].count + 5 > 100 ? 0 : FishLevels[item].count + 5;

//     public void DecreaseCount(DataItem item) =>
//         FishLevels[item].count = FishLevels[item].count - 5 < 0 ? 100 : FishLevels[item].count - 5;

//     public void SetCount(DataItem item, int newFishLevel) => FishLevels[item].count = newFishLevel;

//     public class DataItem : DataEnum
//     {
//         public DataItem() : base(0, "") { }
//         public DataItem(int id, string name) : base(id, name) { }
//         public DataItem(int id, string name, string description) : base(id, name) => Description = description;
//         public static DataItem SailFish = new(0, "Sailfish", "You eat this");
//     }
// }
