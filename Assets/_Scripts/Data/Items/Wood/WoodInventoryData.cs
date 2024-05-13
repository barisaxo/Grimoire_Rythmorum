// using System.Collections.Generic;

// namespace Data.Two
// {
//     public class WoodInventoryData : IData
//     {
//         private Dictionary<Wood, int> _datum;
//         private Dictionary<Wood, int> Datum => _datum ??= SetUpDatum();

//         private Dictionary<Wood, int> SetUpDatum()
//         {
//             Dictionary<Wood, int> datum = new();
//             foreach (IItem item in Items) datum.TryAdd(item as Wood, 10);
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
//                     var enums = Enumeration.All<WoodEnum>();
//                     var items = new IItem[enums.Length];
//                     for (int i = 0; i < enums.Length; i++)
//                         items[i] = WoodEnum.ToItem(enums[i]);

//                     return items;
//                 }
//             }
//         }


//         public string GetDescription(IItem item)
//         {
//             foreach (var i in Datum)
//                 if (i.Key.GetType() == item.GetType())
//                     return item.Description;
//             throw new System.Exception(item.GetType().ToString());
//         }

//         public string GetDisplayLevel(IItem item)
//         {
//             if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//             return Datum[(Wood)item].ToString();
//         }

//         public void DecreaseLevel(IItem item)
//         {
//             if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//             Datum[(Wood)item] = Datum[(Wood)item] - 1 < 0 ? 0 : Datum[(Wood)item] - 1;
//         }
//         public void DecreaseLevel(IItem item, int i)
//         {
//             if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//             Datum[(Wood)item] = Datum[(Wood)item] - i < 0 ? 0 : Datum[(Wood)item] - i;
//         }

//         public int GetLevel(IItem item)
//         {
//             if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//             return Datum[(Wood)item];
//         }

//         public void IncreaseLevel(IItem item)
//         {
//             if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//             Datum[(Wood)item] = Datum[(Wood)item] + 1 > 999 ? 999 : Datum[(Wood)item] + 1;
//         }
//         public void IncreaseLevel(IItem item, int i)
//         {
//             if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//             Datum[(Wood)item] = Datum[(Wood)item] + i > 999 ? 999 : Datum[(Wood)item] + i;
//         }

//         public void SetLevel(IItem item, int level)
//         {
//             if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//             Datum[(Wood)item] = level;
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