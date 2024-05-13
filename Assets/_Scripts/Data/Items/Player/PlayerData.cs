using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Two
{
    [System.Serializable]
    public class PlayerData : IData
    {
        private Dictionary<PlayerStat, int> _datum;
        public Dictionary<PlayerStat, int> Datum => _datum ??= SetUpPlayerDatum();

        Dictionary<PlayerStat, int> SetUpPlayerDatum()
        {
            Dictionary<PlayerStat, int> datum = new();
            foreach (var item in Items) datum.TryAdd((PlayerStat)item, 0);
            return datum;
        }

        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                static IItem[] SetUp()
                {
                    var enums = Enumeration.All<PlayerStatEnum>();
                    var items = new IItem[enums.Length];
                    for (int i = 0; i < enums.Length; i++)
                        items[i] = PlayerStatEnum.ToItem(enums[i]);

                    return items;
                }
            }
        }

        public void Reset() => _datum = SetUpPlayerDatum();

        public string GetDisplayLevel(IItem item)
        {
            if (item is AuralFailed or AuralSolved)
            {
                int total = GetLevel(new AuralFailed()) + GetLevel(new AuralSolved());
                if (total == 0) return "n/a";
                return ((int)((float)(GetLevel(item) / (float)total) * 100)).ToString() + "%";
            }
            else if (item is Hit or Miss)
            {
                int total = GetLevel(new Hit()) + GetLevel(new Miss());
                if (total == 0) return "n/a";
                return ((int)((float)(GetLevel(item) / (float)total) * 100)).ToString() + "%";
            }
            else if (item is TheoryFailed or TheorySolved)
            {
                int total = GetLevel(new TheoryFailed()) + GetLevel(new TheorySolved());
                if (total == 0) return "n/a";
                return ((int)((float)(GetLevel(item) / (float)total) * 100)).ToString() + "%";
            }
            else if (item is GramoFailed or GramoSolved)
            {
                int total = GetLevel(new GramoSolved()) + GetLevel(new GramoFailed());
                if (total == 0) return "n/a";
                return ((int)((float)(GetLevel(item) / (float)total) * 100)).ToString() + "%";
            }
            else if (item is FishCaught or FishLost)
            {
                int total = GetLevel(new FishCaught()) + GetLevel(new FishLost());
                if (total == 0) return "n/a";
                return ((int)((float)(GetLevel(item) / (float)total) * 100)).ToString() + "%";
            }
            else if (item is PatternsFound or PatternsAvailable)
            {
                return "";
            }
            return "?";
        }

        public int GetLevel(IItem item) => Datum[(PlayerStat)item];

        // public void IncreaseLevel(IItem item) => IncreaseLevel(item, 1);

        public void AdjustLevel(IItem item, int i)
        {
            if (i < 0 || item is not PlayerStat) throw new System.ArgumentException(item.Name + " " + i.ToString());

            if (item is AuralFailed && GetLevel(item) > 999) DecreaseLevel(new AuralSolved(), i / 2);
            else if (item is AuralSolved && GetLevel(item) > 999) DecreaseLevel(new AuralFailed(), i / 2);
            else if (item is TheoryFailed && GetLevel(item) > 999) DecreaseLevel(new TheorySolved(), i / 2);
            else if (item is TheorySolved && GetLevel(item) > 999) DecreaseLevel(new TheoryFailed(), i / 2);
            else if (item is FishLost && GetLevel(item) > 999) DecreaseLevel(new FishCaught(), i / 2);
            else if (item is FishCaught && GetLevel(item) > 999) DecreaseLevel(new FishLost(), i / 2);
            else if (item is GramoSolved && GetLevel(item) > 999) DecreaseLevel(new GramoFailed(), i / 2);
            else if (item is GramoFailed && GetLevel(item) > 999) DecreaseLevel(new GramoSolved(), i / 2);
            else if (item is Hit && GetLevel(item) > 9999) DecreaseLevel(new Miss(), i / 2);
            else if (item is Miss && GetLevel(item) > 9999) DecreaseLevel(new Hit(), i / 2);
            else Datum[(PlayerStat)item] += i;
            PersistentData.Save(this);
        }

        // public void DecreaseLevel(IItem item)
        // {
        //     Datum[(PlayerStat)item] -= Datum[(PlayerStat)item] > 0 ? 1 : 0;
        //     PersistentData.Save(this);
        // }

        public void DecreaseLevel(IItem item, int i)
        {
            Datum[(PlayerStat)item] = Datum[(PlayerStat)item] - i < 0 ? 0 : Datum[(PlayerStat)item] - i;
            // PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            Datum[(PlayerStat)item] = level;
            PersistentData.Save(this);
        }

        private void LoadLevel(IItem item, int level)
        {
            Datum[(PlayerStat)item] = level;
        }

        public bool InventoryIsFull(int Space) => false;

        private PlayerData() { }

        public static PlayerData GetData()
        {
            PlayerData data = new();
            data.LoadLevel(new PatternsFound(), 1500);
            data.LoadLevel(new PatternsAvailable(), 1500);
            if (data.PersistentData.TryLoadData() is not PlayerData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadLevel(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            data.PersistentData.Save(data);
            data.LoadLevel(new PatternsFound(), 1500);
            data.LoadLevel(new PatternsAvailable(), 1500);
            return data;
        }

        string IData.GetDescription(IItem item)
        {
            return "???";
        }

        public IPersistentData PersistentData { get; } = new SaveData("Player.Data");

    }
}