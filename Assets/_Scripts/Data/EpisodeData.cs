// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// namespace Data.Player
// {
//     [System.Serializable]
//     public class EpisodeData : IData
//     {
//         DataEnum[] IData.DataItems => throw new System.NotImplementedException();

//         IPersistentData IData.PersistentData => throw new System.NotImplementedException();

//         void IData.DecreaseLevel(DataEnum item)
//         {
//             throw new System.NotImplementedException();
//         }

//         string IData.GetDisplayLevel(DataEnum item)
//         {
//             throw new System.NotImplementedException();
//         }

//         int IData.GetLevel(DataEnum item)
//         {
//             throw new System.NotImplementedException();
//         }

//         void IData.IncreaseLevel(DataEnum item)
//         {
//             throw new System.NotImplementedException();
//         }

//         bool IData.InventoryIsFull(int i)
//         {
//             throw new System.NotImplementedException();
//         }

//         void IData.Reset()
//         {
//             throw new System.NotImplementedException();
//         }

//         void IData.SetLevel(DataEnum item, int level)
//         {
//             throw new System.NotImplementedException();
//         }

//         [System.Serializable]
//         public class DataItem : DataEnum
//         {
//             public DataItem() : base(0, "") { }
//             public DataItem(int id, string name) : base(id, name) { }

//             public static DataItem Hemp = new(0, "Hemp");
//             public static DataItem Cotton = new(1, "Cotton");
//             public static DataItem Linen = new(2, "Linen");
//             public static DataItem Silk = new(3, "Silk");
//             public static DataItem Pine = new(4, "Pine");
//             public static DataItem Fir = new(5, "Fir");
//             public static DataItem Oak = new(6, "Oak");
//             public static DataItem Teak = new(7, "Teak");
//             public static DataItem WroughtIron = new(8, "WroughtIron");
//             public static DataItem CastIron = new(9, "CastIron");
//             public static DataItem Bronze = new(10, "Bronze");
//             public static DataItem Patina = new(11, "Patina");
//             public static DataItem Patterns = new(12, "Patterns");
//             public static DataItem Gold = new(13, "Gold");
//         }
//     }
// }