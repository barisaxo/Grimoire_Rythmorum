// using System.Collections.Generic;

// namespace Data.Two
// {
//     [System.Serializable]
//     public class WoodStorageData : IData
//     {
//         private Dictionary<Wood, int> _datum;
//         private Dictionary<Wood, int> Datum => _datum ??= SetUpDatum();

//         private Dictionary<Wood, int> SetUpDatum()
//         {
//             Dictionary<Wood, int> datum = new();
//             foreach (IItem item in Items) datum.TryAdd(item as Wood, 0);
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
//             if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//             return item.Description;
//         }

//         public string GetDisplayLevel(IItem item)
//         {
//             if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//             return Datum[(Wood)item].ToString();
//         }

//         public void AdjustLevel(IItem item, int i)
//         {
//             if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//             Datum[(Wood)item] =
//                 Datum[(Wood)item] + i > 999 ? 999 :
//                 Datum[(Wood)item] + i < 0 ? 0 :
//                 Datum[(Wood)item] + i;
//             PersistentData.Save(this);
//         }
//         // public void DecreaseLevel(IItem item, int i)
//         // {
//         //     if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//         //     Datum[(Wood)item] = Datum[(Wood)item] - i < 0 ? 0 : Datum[(Wood)item] - i;
//         //     PersistentData.Save(this);
//         // }

//         public int GetLevel(IItem item)
//         {
//             if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//             return Datum[(Wood)item];
//         }

//         // public void IncreaseLevel(IItem item, int i)
//         // {
//         //     if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//         //     Datum[(Wood)item] = Datum[(Wood)item] + i > 999 ? 999 : Datum[(Wood)item] + i;
//         //     PersistentData.Save(this);
//         // }

//         public void SetLevel(IItem item, int level)
//         {
//             if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//             Datum[(Wood)item] = level;
//             PersistentData.Save(this);
//         }

//         private void LoadLevel(IItem item, int level)
//         {
//             if (item is not Wood) throw new System.Exception(item.GetType().ToString());
//             Datum[(Wood)item] = level;
//         }

//         public bool InventoryIsFull(int space) => false;

//         public void Reset()
//         {
//             _datum = SetUpDatum();
//             PersistentData.Save(this);
//         }

//         private WoodStorageData() { }

//         public static WoodStorageData GetData()
//         {
//             WoodStorageData data = new();
//             if (data.PersistentData.TryLoadData() is not WoodStorageData loadData) return data;
//             for (int i = 0; i < data.Items.Length; i++)
//                 try { data.LoadLevel(data.Items[i], loadData.GetLevel(data.Items[i])); }
//                 catch { }
//             return data;
//         }

//         public IPersistentData PersistentData { get; } = new SaveData("Wood.Data");
//     }

// }