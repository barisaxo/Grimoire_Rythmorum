using MusicTheory;

namespace Data.Options
{
    [System.Serializable]
    public class SettingsData : IData
    {

        public void Reset() { }

        private KeyOf _currentKey = KeyOf.C;

        private int _latency = 5;

        public int Latency
        {
            get => _latency;
            set => _latency = value > 25 ? 0 : value;
        }

        public KeyOf CurrentKey
        {
            get => _currentKey;
            set => _currentKey = value > KeyOf.B ? KeyOf.C : value < KeyOf.C ? KeyOf.B : value;
        }

        public DataEnum[] DataItems => Enumeration.All<DataItem>();

        public string GetDisplayLevel(DataEnum item) => item switch
        {
            _ when item == DataItem.Transpose => CurrentKey.ToString(),
            _ when item == DataItem.Latency => Latency.ToString(),
            _ => ""
        };
        public int GetLevel(DataEnum item) => item switch
        {
            _ when item == DataItem.Transpose => (int)CurrentKey,
            _ when item == DataItem.Latency => Latency,
            _ => -1,
        };
        //=> GameplayLevels[item].level;

        public void IncreaseLevel(DataEnum item)
        {
            if (item == DataItem.Transpose) CurrentKey++;
            else if (item == DataItem.Latency) Latency++;
            PersistentData.Save(this);
        }

        public void DecreaseLevel(DataEnum item)
        {
            if (item == DataItem.Transpose) CurrentKey--;
            else if (item == DataItem.Latency) Latency--;
            PersistentData.Save(this);
        }

        public void SetLevel(DataEnum item, int newGameplayLevel)
        {
            if (item == DataItem.Transpose) CurrentKey = (KeyOf)newGameplayLevel;
            else if (item == DataItem.Latency) Latency = newGameplayLevel;
            PersistentData.Save(this);
        }

        public bool InventoryIsFull(int Space) => false;

        [System.Serializable]
        public class DataItem : DataEnum
        {
            public static readonly DataItem Latency = new(0, "LATENCY",
                "Lag offset for rhythm input. The margin for an accurate hit is +- 15." +
                "\nIf you are missing beats try adjusting this latency. Default setting is 5");

            public static readonly DataItem Transpose = new(1, "KEY TRANSPOSITION",
                "C: Concert pitch: flute, piano, guitar, violin, etc..." +
                "\nEb: Alto & baritone saxophone" +
                "\nF: French horn" +
                "\nBb: Clarinet, trumpet, soprano & tenor saxophone" +
                "\nB: Guitar in Eb standard tuning");

            public static DataItem Tuning = new(2, "TUNING NOTE A 440",
                "If your 'A' note doesn't match this \nyou might be out of tune, or in the wrong key");


            public DataItem() : base(0, "")
            {
            }

            public DataItem(int id, string name) : base(id, name)
            {
            }

            private DataItem(int id, string name, string description) : base(id, name)
            {
                Description = description;
            }
        }

        private SettingsData() { }

        public static SettingsData GetData()
        {
            SettingsData data = new();
            if (data.PersistentData.TryLoadData() is not SettingsData loadData) return data;
            for (int i = 0; i < data.DataItems.Length; i++)
                try { data.SetLevel(data.DataItems[i], loadData.GetLevel(data.DataItems[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Settings.Data");
    }
}