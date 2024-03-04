// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace OLD
// {
//     public class MaterialsData
//     {
//         private (DataItem materialsItem, int count)[] _materialsLevels;
//         private (DataItem materialsItem, int count)[] MaterialsLevels => _materialsLevels ??= SetUpMaterialsLevels();
//         private (DataItem materialsItem, int count)[] SetUpMaterialsLevels()
//         {
//             var items = Enumeration.All<DataItem>();
//             var materialsLevels = new (DataItem materialsItem, int count)[items.Length];

//             for (int i = 0; i < items.Length; i++) materialsLevels[i] = (items[i], 0);

//             return materialsLevels;
//         }

//         /// <summary>
//         /// Give this to the menu objects text to display the current materials count.
//         /// </summary>
//         /// <returns>an int 0 to 100</returns>
//         public string GetItemCount(DataItem item) => MaterialsLevels[item].count.ToString();

//         public void IncreaseCount(DataItem item) =>
//             MaterialsLevels[item].count = MaterialsLevels[item].count + 5 > 100 ? 0 : MaterialsLevels[item].count + 5;

//         public void DecreaseCount(DataItem item) =>
//             MaterialsLevels[item].count = MaterialsLevels[item].count - 5 < 0 ? 100 : MaterialsLevels[item].count - 5;

//         public void SetCount(DataItem item, int newMaterialsLevel) => MaterialsLevels[item].count = newMaterialsLevel;

//         public class DataItem : DataEnum
//         {
//             public DataItem() : base(0, "") { }
//             public DataItem(int id, string name) : base(id, name) { }
//             public DataItem(int id, string name, string description) : base(id, name) => Description = description;
//             public static readonly DataItem Pine = new(0, "Pine");
//             public static readonly DataItem Fir = new(1, "Fir");
//             public static readonly DataItem Oak = new(2, "Oak");
//             public static readonly DataItem Teak = new(3, "Teak");

//             public static readonly DataItem Hemp = new(4, "Hemp");
//             public static readonly DataItem Cotton = new(5, "Cotton");
//             public static readonly DataItem Linen = new(6, "Linen");
//             public static readonly DataItem Silk = new(7, "Silk");

//             public static readonly DataItem PigIron = new(8, "Pig Iron");
//             public static readonly DataItem WroughtIron = new(9, "Wrought Iron");
//             public static readonly DataItem Brass = new(10, "Brass");

//         }

//     }
// }