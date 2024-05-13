// using System.Collections;
// using System.Collections.Generic;

// namespace Data.Inventory
// {
//     [System.Serializable]
//     public class GramophoneData : IData
//     {
//         private Dictionary<DataItem, int> _gramos;
//         private Dictionary<DataItem, int> Gramos => _gramos ??= SetUpGramos();

//         Dictionary<DataItem, int> SetUpGramos()
//         {
//             Dictionary<DataItem, int> gramos = new();
//             foreach (var item in DataItems) gramos.TryAdd((DataItem)item, 0);
//             return gramos;
//         }

//         public void Reset() => _gramos = SetUpGramos();
//         public string GetDisplayLevel(DataEnum item) => Gramos[(DataItem)item].ToString();

//         public int GetLevel(DataEnum item) => Gramos[(DataItem)item];

//         public void IncreaseLevel(DataEnum item)
//         {
//             Gramos[(DataItem)item] += Gramos[(DataItem)item] + 1 > 999 ? 0 : 1;
//             PersistentData.Save(this);
//         }

//         public void DecreaseLevel(DataEnum item)
//         {
//             Gramos[(DataItem)item] -= Gramos[(DataItem)item] - 1 < 0 ? 0 : 1;
//             PersistentData.Save(this);
//         }

//         public void SetLevel(DataEnum item, int newVolumeLevel)
//         {
//             Gramos[(DataItem)item] = newVolumeLevel;
//             PersistentData.Save(this);
//         }

//         public DataEnum[] DataItems => Enumeration.All<DataItem>();

//         public bool InventoryIsFull(int Space)
//         {
//             int i = 0;
//             foreach (var item in DataItems) i += GetLevel(item);
//             return Space < i;
//         }

//         [System.Serializable]
//         public class DataItem : DataEnum
//         {
//             public DataItem() : base(0, "") { }
//             public DataItem(int id, string name) : base(id, name) { }
//             public DataItem(int id, string name, string description) : base(id, name) => Description = description;
//             public static DataItem Lvl1 = new(0, "II- V7 I", "Difficulty: *");
//             public static DataItem Lvl2 = new(1, "I IV V", "Difficulty: *");
//             public static DataItem Lvl3 = new(2, "I VI- II- V", "Difficulty: **");
//             public static DataItem Lvl4 = new(3, "III- VI- II- V", "Difficulty: ***");
//             public static DataItem Lvl5 = new(4, "I II- III- IV V7 VI- VIIø", "Difficulty: ***");
//         }

//         private GramophoneData() { }

//         public static GramophoneData GetData()
//         {
//             GramophoneData data = new();
//             if (data.PersistentData.TryLoadData() is not GramophoneData loadData) return data;
//             for (int i = 0; i < data.DataItems.Length; i++)
//                 try { data.SetLevel(data.DataItems[i], loadData.GetLevel(data.DataItems[i])); }
//                 catch { }
//             return data;
//         }

//         public IPersistentData PersistentData { get; } = new SaveData("Gramophone.Data");
//     }
// }