// using System.Collections.Generic;

// namespace Data.Two
// {
//     public class MetalInventoryData : IData
//     {
//         private Dictionary<Metal, int> _datum;
//         private Dictionary<Metal, int> Datum => _datum ??= SetUpDatum();

//         private Dictionary<Metal, int> SetUpDatum()
//         {
//             Dictionary<Metal, int> datum = new();
//             foreach (IItem item in Items) datum.TryAdd(item as Metal, 0);
//             return datum;
//         }

//         private IItem[] _items;
//         public IItem[] Items
//         {
//             get
//             {
//                 return _items ??= SetUp();
//                 static IItem[] SetUp()
//                 {
//                     var enums = Enumeration.All<MetalEnum>();
//                     var temp = new IItem[enums.Length];
//                     for (int i = 0; i < enums.Length; i++)
//                         temp[i] = MetalEnum.ToItem(enums[i]);

//                     return temp;
//                 }
//             }
//         }
//         public string GetDescription(IItem item)
//         {
//             if (item is not Metal) throw new System.Exception(item.GetType().ToString());
//             return item.Description;
//         }

//         public string GetDisplayLevel(IItem item)
//         {
//             if (item is not Metal) throw new System.Exception(item.GetType().ToString());
//             return Datum[(Metal)item].ToString();
//         }

//         public void DecreaseLevel(IItem item)
//         {
//             if (item is not Metal) throw new System.Exception(item.GetType().ToString());
//             Datum[(Metal)item] = Datum[(Metal)item] - 1 < 0 ? 0 : Datum[(Metal)item] - 1;
//         }
//         public void DecreaseLevel(IItem item, int i)
//         {
//             if (item is not Metal) throw new System.Exception(item.GetType().ToString());
//             Datum[(Metal)item] = Datum[(Metal)item] - i < 0 ? 0 : Datum[(Metal)item] - i;
//         }

//         public int GetLevel(IItem item)
//         {
//             if (item is not Metal) throw new System.Exception(item.GetType().ToString());
//             return Datum[(Metal)item];
//         }

//         public void IncreaseLevel(IItem item)
//         {
//             if (item is not Metal) throw new System.Exception(item.GetType().ToString());
//             Datum[(Metal)item] = Datum[(Metal)item] + 1 > 999 ? 999 : Datum[(Metal)item] + 1;
//         }

//         public void IncreaseLevel(IItem item, int i)
//         {
//             if (item is not Metal) throw new System.Exception(item.GetType().ToString());
//             Datum[(Metal)item] = Datum[(Metal)item] + i > 999 ? 999 : Datum[(Metal)item] + i;
//         }

//         public void SetLevel(IItem item, int level)
//         {
//             if (item is not Metal) throw new System.Exception(item.GetType().ToString());
//             Datum[(Metal)item] = level;
//         }

//         public bool InventoryIsFull(int i)
//         {
//             int space = 0;
//             foreach (var item in Items) i += GetLevel(item);
//             return space >= i;
//         }

//         public void Reset()
//         {
//             _datum = SetUpDatum();
//         }

//         public IPersistentData PersistentData { get; } = new NotPersistentData();
//     }

// }