using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Equipment
{
    // public class EquipmentData : IData
    // {
    //     private Dictionary<CannonData, int> _equipment;
    //     private Dictionary<CannonData, int> Equipment => _equipment ??= SetUpFish();

    //     Dictionary<CannonData, int> SetUpFish()
    //     {
    //         Dictionary<CannonData, int> equipment = new();
    //         foreach (var item in CannonDatas) equipment.TryAdd((CannonData)item, 0);
    //         return equipment;
    //     }

    //     public string GetDisplayLevel(DataEnum item) => Equipment[(CannonData)item].ToString();

    //     public int GetLevel(DataEnum item) => Equipment[(CannonData)item];

    //     public void IncreaseLevel(DataEnum item)
    //     {
    //         // if (StorageIsFull()) return;
    //         Equipment[(CannonData)item] = +1 > 999 ? 999 : Equipment[(CannonData)item] + 1;
    //     }

    //     public void DecreaseLevel(DataEnum item) =>
    //         Equipment[(CannonData)item] = -1 < 0 ? 0 : Equipment[(CannonData)item] - 1;

    //     public void SetLevel(DataEnum item, int newVolumeLevel) =>
    //         Equipment[(CannonData)item] = newVolumeLevel;

    //     public DataEnum[] CannonDatas => Enumeration.All<CannonData>();

    //     public bool InventoryIsFull(int Space)
    //     {
    //         int i = 0;
    //         foreach (var item in CannonDatas) i += GetLevel(item);
    //         return i >= Space;
    //     }

    //     public class CannonData : DataEnum
    //     {
    //         public CannonData() : base(0, "") { }
    //         public CannonData(int id, string name) : base(id, name) { }
    //         public CannonData(int id, string name, string description, float modifier) : base(id, name)
    //         {
    //             Description = description;
    //             Modifier = modifier;
    //         }
    //         public CannonData(int id, string name, string description, float modifier, Func<Sprite> sprite) : base(id, name)
    //         {
    //             Description = description;
    //             Modifier = modifier;
    //             Sprite = sprite;
    //         }

    //         public readonly float Modifier;
    //         public readonly Func<Sprite> Sprite = null;

    //         public static CannonData Hemp = new(0, "Hemp", "Inexpensive but heavy sailcloth", 1);
    //         public static CannonData Cotton = new(1, "Cotton", "Moderately inexpensive, moderately light sailcloth", 1.5f);
    //         public static CannonData Linen = new(2, "Linen", "Expensive, light sailcloth", 2.25f);
    //         public static CannonData Silk = new(3, "Silk", "Very expensive, very light sailcloth", 3f);
    //         public static CannonData Pine = new(4, "Pine", "Inexpensive but soft timber", 1f);
    //         public static CannonData Fir = new(5, "Fir", "Moderately inexpensive, moderately hard timber", 1.5f);
    //         public static CannonData Oak = new(6, "Oak", "Expensive, hard timber", 2.25f);
    //         public static CannonData Teak = new(7, "Teak", "Very expensive, very hard timber", 3f);
    //         public static CannonData WroughtIron = new(8, "WroughtIron", "Inexpensive but weak metal", 1f);
    //         public static CannonData CastIron = new(9, "CastIron", "Moderately inexpensive, moderately strong metal", 1.5f);
    //         public static CannonData Bronze = new(10, "Bronze", "Expensive, strong metal", 2.25f);
    //         public static CannonData Patina = new(11, "Patina", "Very expensive, very strong metal", 3f);
    //     }

    //     private EquipmentData() { }

    //     public static EquipmentData GetData()
    //     {
    //         EquipmentData data = new();
    //         if (data.PersistentData.TryLoadData() is not EquipmentData loadData) return data;
    //         for (int i = 0; i < data.CannonDatas.Length; i++)
    //             try { data.SetLevel(data.CannonDatas[i], loadData.GetLevel(data.CannonDatas[i])); }
    //             catch { }
    //         return data;
    //     }

    //     public IPersistentData PersistentData { get; } = new SaveData("Equipment.Data");
    // }

    [System.Serializable]
    public class CannonData : DataEnum
    {
        public CannonData() : base(0, "") { }
        public CannonData(int id, string name) : base(id, name) { }
        public CannonData(int id, string name, string description, float modifier) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
        }
        public CannonData(int id, string name, string description, float modifier, Func<Sprite> sprite) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
            Sprite = sprite;
        }

        public readonly float Modifier;
        public readonly Func<Sprite> Sprite = null;

        public static CannonData Mynion = new(0, "Mynion", "Inexpensive but heavy sailcloth", 32);
        public static CannonData Saker = new(1, "Saker", "Moderately inexpensive, moderately light sailcloth", 40);
        public static CannonData Culverin = new(2, "Culverin", "Expensive, light sailcloth", 52);
        public static CannonData DemiCannon = new(3, "DemiCannon", "Very expensive, very light sailcloth", 64);
        public static CannonData Carronade = new(4, "Carronade", "Very expensive, very light sailcloth", 72);
    }

    [System.Serializable]
    public class HullData : DataEnum
    {
        public HullData() : base(0, "") { }
        public HullData(int id, string name) : base(id, name) { }
        public HullData(int id, string name, string description, float modifier) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
        }
        public HullData(int id, string name, string description, float modifier, Func<Sprite> sprite) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
            Sprite = sprite;
        }

        public readonly float Modifier;
        public readonly Func<Sprite> Sprite = null;

        public static HullData Sloop = new(0, "Sloop", "Small versatile vessel, with minimal armament", 256);
        public static HullData Schooner = new(1, "Schooner", "Known for their speed and versatility, and boast a decent armament", 1024);
        public static HullData Frigate = new(2, "Frigate", "Superior combination of speed, firepower, and endurance", 3056);
    }
}