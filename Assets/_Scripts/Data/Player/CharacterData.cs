// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Data.Inventory;

// namespace Data.Player
// {
//     [System.Serializable]
//     public class CharacterData : IData
//     {
//         private Dictionary<DataItem, int> _characterDatum;
//         public Dictionary<DataItem, int> CharacterDatum => _characterDatum ??= SetUpCharacterDatum();

//         // private ShipStats.ShipStats _shipStats;
//         // public ShipStats.ShipStats ShipStats => _shipStats ??= new(
//         //     new ShipStats.HullStats(Data.Equipment.HullData.Sloop, MaterialsData.DataItem.Pine),
//         //     new ShipStats.CannonStats(Data.Equipment.CannonData.Carronade, MaterialsData.DataItem.Bronze),
//         //     numOfCannons: 32
//         // );
//         Dictionary<DataItem, int> SetUpCharacterDatum()
//         {
//             Dictionary<DataItem, int> CharacterDatum = new();
//             foreach (var item in DataItems) CharacterDatum.TryAdd((DataItem)item, 0);
//             return CharacterDatum;
//         }

//         public void Reset() => _characterDatum = SetUpCharacterDatum();
//         public string GetDisplayLevel(DataEnum item)
//         {
//             return "";
//         }

//         public int GetLevel(DataEnum item) => CharacterDatum[(DataItem)item];

//         public void IncreaseLevel(DataEnum item)
//         {
//         }

//         public void DecreaseLevel(DataEnum item)
//         {
//             CharacterDatum[(DataItem)item] -= CharacterDatum[(DataItem)item] > 0 ? 1 : 0;
//             PersistentData.Save(this);
//         }

//         public void SetLevel(DataEnum item, int newVolumeLevel)
//         {
//             CharacterDatum[(DataItem)item] = newVolumeLevel;
//             PersistentData.Save(this);
//         }

//         public DataEnum[] DataItems { get; } = Enumeration.All<DataItem>();

//         public bool InventoryIsFull(int Space) => false;

//         [System.Serializable]
//         public class DataItem : DataEnum
//         {
//             public DataItem() : base(0, "") { }
//             public DataItem(int id, string name) : base(id, name) { }
//             // public DataItem(int id, string name, string description) : base(id, name) => Description = description;
//             public static DataItem MaxHP = new(0, "Max HP");
//             public static DataItem CurrentHP = new(1, "Current HP");
//         }

//         private CharacterData() { }

//         public static CharacterData GetData()
//         {
//             CharacterData data = new();
//             if (data.PersistentData.TryLoadData() is not CharacterData loadData) return data;
//             for (int i = 0; i < data.DataItems.Length; i++)
//                 try { data.SetLevel(data.DataItems[i], loadData.GetLevel(data.DataItems[i])); }
//                 catch { }
//             data.PersistentData.Save(data);
//             return data;
//         }

//         public IPersistentData PersistentData { get; } = new SaveData("Character.Data");

//     }
// }