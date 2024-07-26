using System.Collections.Generic;

namespace Data
{
    [System.Serializable]
    public class PlayerRecentData : IData
    {
        private Dictionary<PlayerRecentStat, int> _datum;
        public Dictionary<PlayerRecentStat, int> Datum => _datum ??= SetUpPlayerDatum();

        Dictionary<PlayerRecentStat, int> SetUpPlayerDatum()
        {
            Dictionary<PlayerRecentStat, int> datum = new();
            foreach (var item in Items) datum.TryAdd((PlayerRecentStat)item, 0);
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
                    var enums = Enumeration.All<PlayerRecentStatEnum>();
                    var items = new IItem[enums.Length];
                    for (int i = 0; i < enums.Length; i++)
                        items[i] = PlayerRecentStatEnum.ToItem(enums[i]);

                    return items;
                }
            }
        }

        public void Reset() => _datum = SetUpPlayerDatum();

        public string GetDisplayLevel(IItem item)
        {
            if (item is not PlayerRecentStat) throw new System.ArgumentException(item.Name);

            if (item is AuralRecentFailed or AuralRecentSolved)
            {
                int total = GetLevel(new AuralRecentFailed()) + GetLevel(new AuralRecentSolved());
                if (total == 0) return "n/a";
                return ((int)((float)(GetLevel(item) / (float)total) * 100)).ToString() + "%";
            }
            else if (item is TheoryRecentFailed or TheoryRecentSolved)
            {
                int total = GetLevel(new TheoryRecentFailed()) + GetLevel(new TheoryRecentSolved());
                if (total == 0) return "n/a";
                return ((int)((float)(GetLevel(item) / (float)total) * 100)).ToString() + "%";
            }
            else if (item is GramoRecentFailed or GramoRecentSolved)
            {
                int total = GetLevel(new GramoRecentSolved()) + GetLevel(new GramoRecentFailed());
                if (total == 0) return "n/a";
                return ((int)((float)(GetLevel(item) / (float)total) * 100)).ToString() + "%";
            }
            else if (item is FishRecentCaught or FishRecentLost)
            {
                int total = GetLevel(new FishRecentCaught()) + GetLevel(new FishRecentLost());
                if (total == 0) return "n/a";
                return ((int)((float)(GetLevel(item) / (float)total) * 100)).ToString() + "%";
            }
            return "?";
        }

        public int GetLevel(IItem item)
        {
            if (item is not PlayerRecentStat) throw new System.ArgumentException(item.Name);

            else return Datum[(PlayerRecentStat)item];
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (i < 0 || item is not PlayerRecentStat) throw new System.ArgumentException(item.Name + " " + i.ToString());

            if (item is AuralRecentFailed && GetLevel(item) > 10) DecreaseLevel(new AuralRecentSolved(), i);
            else if (item is AuralRecentSolved && GetLevel(item) > 10) DecreaseLevel(new AuralRecentFailed(), i);
            else if (item is TheoryRecentFailed && GetLevel(item) > 10) DecreaseLevel(new TheoryRecentSolved(), i);
            else if (item is TheoryRecentSolved && GetLevel(item) > 10) DecreaseLevel(new TheoryRecentFailed(), i);
            else if (item is FishRecentLost && GetLevel(item) > 10) DecreaseLevel(new FishRecentCaught(), i);
            else if (item is FishRecentCaught && GetLevel(item) > 10) DecreaseLevel(new FishRecentLost(), i);
            else if (item is GramoRecentSolved && GetLevel(item) > 10) DecreaseLevel(new GramoRecentFailed(), i);
            else if (item is GramoRecentFailed && GetLevel(item) > 10) DecreaseLevel(new GramoRecentSolved(), i);

            PersistentData.Save(this);
        }

        public void DecreaseLevel(IItem item, int i)
        {
            Datum[(PlayerRecentStat)item] = Datum[(PlayerRecentStat)item] - i < 0 ? 0 : Datum[(PlayerRecentStat)item] - i;
            // PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            Datum[(PlayerRecentStat)item] = level;
            PersistentData.Save(this);
        }

        private void LoadLevel(IItem item, int level)
        {
            Datum[(PlayerRecentStat)item] = level;
        }

        public bool InventoryIsFull(int Space) => false;

        private PlayerRecentData() { }

        public static PlayerRecentData GetData()
        {
            PlayerRecentData data = new();
            // data.LoadLevel(new PatternsFound(), 1500);
            // data.LoadLevel(new PatternsAvailable(), 1500);
            if (data.PersistentData.TryLoadData() is not PlayerRecentData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadLevel(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            data.PersistentData.Save(data);
            // data.LoadLevel(new PatternsFound(), 1500);
            // data.LoadLevel(new PatternsAvailable(), 1500);
            return data;
        }

        string IData.GetDescription(IItem item)
        {
            return "???";
        }

        public IPersistentData PersistentData { get; } = new SaveData(nameof(PlayerRecentData));
    }
}
