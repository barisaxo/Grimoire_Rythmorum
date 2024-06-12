using System.Collections.Generic;
using MusicTheory;

namespace Data
{
    [System.Serializable]
    public class GameplayData : IData
    {
        private int _lag = 5;
        public int Lag
        {
            get => _lag;
            set => _lag = value > 25 ? 0 : value;
        }

        private KeyOf _currentKey = KeyOf.C;
        public KeyOf CurrentKey
        {
            get => _currentKey;
            set => _currentKey = value > KeyOf.B ? KeyOf.C : value < KeyOf.C ? KeyOf.B : value;
        }

        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                static IItem[] SetUp()
                {
                    var enums = Enumeration.All<GameplayEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = GameplayEnum.ToItem(enums[i]);
                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not IGameplayOption) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not IGameplayOption) throw new System.Exception(item.GetType().ToString());
            return item switch
            {
                Transpose => CurrentKey.ToString(),
                Latency => Lag.ToString(),
                _ => ""
            };
        }

        public int GetLevel(IItem item)
        {
            if (item is not IGameplayOption) throw new System.Exception(item.GetType().ToString());
            return item switch
            {
                Transpose => (int)CurrentKey,
                Latency => Lag,
                _ => -1,
            };
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IGameplayOption) throw new System.Exception(item.GetType().ToString());
            switch (item)
            {
                case Transpose: CurrentKey += i; break;
                case Latency: Lag += i; break;
            }
            PersistentData.Save(this);
        }

        // public void IncreaseLevel(IItem item, int i) { throw new System.Exception(); }
        // public void IncreaseLevel(IItem item)
        // {
        //     if (item is not GameplayOption) throw new System.Exception(item.GetType().ToString());
        //     switch (item)
        //     {
        //         case Transpose: CurrentKey++; break;
        //         case Latency: Lag++; break;
        //     }
        //     PersistentData.Save(this);
        // }

        // public void DecreaseLevel(IItem item, int i) { throw new System.Exception(); }
        // public void DecreaseLevel(IItem item)
        // {
        //     if (item is not GameplayOption) throw new System.Exception(item.GetType().ToString());
        //     switch (item)
        //     {
        //         case Transpose: CurrentKey--; break;
        //         case Latency: Lag--; break;
        //     }
        //     PersistentData.Save(this);
        // }

        public void SetLevel(IItem item, int level)
        {
            if (item is not IGameplayOption) throw new System.Exception(item.GetType().ToString());
            switch (item)
            {
                case Transpose: CurrentKey = (KeyOf)level; break;
                case Latency: Lag = level; ; break;
            }
            PersistentData.Save(this);
        }

        private void LoadLevel(IItem item, int level)
        {
            if (item is Transpose) CurrentKey = (KeyOf)level;
            else if (item is Latency) Lag = level;
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
            CurrentKey = 0;
            Lag = 5;
            PersistentData.Save(this);
        }

        private GameplayData() { }

        public static GameplayData GetData()
        {
            GameplayData data = new();
            if (data.PersistentData.TryLoadData() is not GameplayData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadLevel(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Gameplay.Data");
    }

}