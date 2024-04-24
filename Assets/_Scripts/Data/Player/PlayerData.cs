using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Player
{
    [System.Serializable]
    public class PlayerData : IData
    {
        private Dictionary<DataItem, int> _playerDatum;
        public Dictionary<DataItem, int> PlayerDatum => _playerDatum ??= SetUpPlayerDatum();

        Dictionary<DataItem, int> SetUpPlayerDatum()
        {
            Dictionary<DataItem, int> playerDatum = new();
            foreach (var item in DataItems) playerDatum.TryAdd((DataItem)item, 0);
            return playerDatum;
        }
        public void Reset() => _playerDatum = SetUpPlayerDatum();

        public string GetDisplayLevel(DataEnum item)
        {
            if (item == DataItem.AuralFailed || item == DataItem.AuralSolved)
            {
                int total = this.GetLevel(DataItem.AuralFailed) + this.GetLevel(DataItem.AuralSolved);
                if (total == 0) return "n/a";
                return ((int)((float)(this.GetLevel(item) / (float)total) * 100)).ToString() + "%";
            }
            else if (item == DataItem.Hit || item == DataItem.Miss)
            {
                int total = this.GetLevel(DataItem.Hit) + this.GetLevel(DataItem.Miss);
                if (total == 0) return "n/a";
                return ((int)((float)(this.GetLevel(item) / (float)total) * 100)).ToString() + "%";
            }
            else if (item == DataItem.TheoryFailed || item == DataItem.TheorySolved)
            {
                int total = this.GetLevel(DataItem.TheoryFailed) + this.GetLevel(DataItem.TheorySolved);
                if (total == 0) return "n/a";
                return ((int)((float)(this.GetLevel(item) / (float)total) * 100)).ToString() + "%";
            }
            else if (item == DataItem.GramoFailed || item == DataItem.GramoSolved)
            {
                int total = this.GetLevel(DataItem.GramoSolved) + this.GetLevel(DataItem.GramoFailed);
                if (total == 0) return "n/a";
                return ((int)((float)(this.GetLevel(item) / (float)total) * 100)).ToString() + "%";
            }
            else if (item == DataItem.FishCaught || item == DataItem.FishLost)
            {
                int total = this.GetLevel(DataItem.FishCaught) + this.GetLevel(DataItem.FishLost);
                if (total == 0) return "n/a";
                return ((int)((float)(this.GetLevel(item) / (float)total) * 100)).ToString() + "%";
            }
            else if (item == DataItem.Patterns)
            {
                return "";
            }
            return "?";
        }

        public int GetLevel(DataEnum item) => PlayerDatum[(DataItem)item];

        public void IncreaseLevel(DataEnum item)
        {
            if (item == DataItem.AuralFailed && GetLevel(item) > 99) DecreaseLevel(DataItem.AuralSolved);
            else if (item == DataItem.AuralSolved && GetLevel(item) > 99) DecreaseLevel(DataItem.AuralFailed);
            else if (item == DataItem.TheoryFailed && GetLevel(item) > 99) DecreaseLevel(DataItem.TheorySolved);
            else if (item == DataItem.TheorySolved && GetLevel(item) > 99) DecreaseLevel(DataItem.TheoryFailed);
            else if (item == DataItem.FishLost && GetLevel(item) > 99) DecreaseLevel(DataItem.FishCaught);
            else if (item == DataItem.FishCaught && GetLevel(item) > 99) DecreaseLevel(DataItem.FishLost);
            else if (item == DataItem.GramoSolved && GetLevel(item) > 99) DecreaseLevel(DataItem.GramoFailed);
            else if (item == DataItem.GramoFailed && GetLevel(item) > 99) DecreaseLevel(DataItem.GramoSolved);
            else if (item == DataItem.Hit && GetLevel(item) > 999) DecreaseLevel(DataItem.Miss);
            else if (item == DataItem.Miss && GetLevel(item) > 999) DecreaseLevel(DataItem.Hit);
            else PlayerDatum[(DataItem)item]++;
            PersistentData.Save(this);
        }

        public void DecreaseLevel(DataEnum item)
        {
            PlayerDatum[(DataItem)item] -= PlayerDatum[(DataItem)item] > 0 ? 1 : 0;
            PersistentData.Save(this);
        }

        public void SetLevel(DataEnum item, int newVolumeLevel)
        {
            PlayerDatum[(DataItem)item] = newVolumeLevel;
            PersistentData.Save(this);
        }

        public DataEnum[] DataItems { get; } = Enumeration.All<DataItem>();

        public bool InventoryIsFull(int Space) => false;

        [System.Serializable]
        public class DataItem : DataEnum
        {
            public DataItem() : base(0, "") { }
            public DataItem(int id, string name) : base(id, name) { }
            // public DataItem(int id, string name, string description) : base(id, name) => Description = description;
            public static DataItem AuralSolved = new(0, "Aural Solved");
            public static DataItem AuralFailed = new(1, "Aural Failed");
            public static DataItem TheorySolved = new(2, "Theory Solved");
            public static DataItem TheoryFailed = new(3, "Theory Failed");
            public static DataItem GramoSolved = new(4, "Gramophone Solved");
            public static DataItem GramoFailed = new(5, "Gramophone Failed");
            public static DataItem FishCaught = new(6, "Fish Caught");
            public static DataItem FishLost = new(7, "Fish Lost");
            public static DataItem Hit = new(8, "Batterie Hit");
            public static DataItem Miss = new(9, "Batterie Miss");
            public static DataItem Patterns = new(10, "Pattern");
        }

        private PlayerData() { }

        public static PlayerData GetData()
        {
            PlayerData data = new();
            if (data.PersistentData.TryLoadData() is not PlayerData loadData) return data;
            for (int i = 0; i < data.DataItems.Length; i++)
                try { data.SetLevel(data.DataItems[i], loadData.GetLevel(data.DataItems[i])); }
                catch { }
            data.PersistentData.Save(data);
            data.SetLevel(DataItem.Patterns, 1500);
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Player.Data");

    }
}