using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory;

namespace Data.Options
{
    [System.Serializable]
    public class GameplayData : IData
    {
        // private (DataItem gameplayItem, int level)[] _gameplayLevels;
        // private (DataItem gameplayItem, int level)[] GameplayLevels => _gameplayLevels ??= SetUpGameplayLevels();
        // private (DataItem gameplayItem, int level)[] SetUpGameplayLevels()
        // {
        //     var items = DataItems;
        //     var levels = new (DataItem gameplayItem, int level)[items.Length];

        //     for (int i = 0; i < items.Length; i++)
        //         if (i == DataItem.Latency) levels[i] = ((DataItem)items[i], 5);
        //         else if (i == DataItem.Transpose) levels[i] = ((DataItem)items[i], (int)KeyOf.C);
        //         else levels[i] = ((DataItem)items[i], 0);

        //     return levels;
        // }

        private RegionalMode _currentLevel = RegionalMode.Dorian;
        public RegionalMode CurrentLevel
        {
            get => _currentLevel;
            set
            {
                _currentLevel = value;
                PersistentData.Save(this);
            }
        }

        private BatterieDifficulty _batterie_Difficulty;

        private CadenceDifficulty _cadence_Difficulty;

        // private KeyOf _currentKey = KeyOf.C;

        // private int _latency = 5;

        public bool EasyModeWon = false;

        public bool ExplainBatterie = true;

        public Difficulty GameDifficulty = 0;
        public bool MediumModeWon = false;

        public MusicTheory.Rhythms.CellShape RecentCell;


        public BatterieDifficulty Batterie_Difficulty
        {
            get => _batterie_Difficulty;
            set
            {
                _batterie_Difficulty = _batterie_Difficulty + (int)value < 0 || (int)(_batterie_Difficulty + (int)value) > 5
                ? _batterie_Difficulty : value;
                PersistentData.Save(this);
            }
        }

        public CadenceDifficulty Cadence_Difficulty
        {
            get => _cadence_Difficulty;
            set
            {
                _cadence_Difficulty = _cadence_Difficulty + (int)value < 0 || (int)(_cadence_Difficulty + (int)value) > 3
                ? _cadence_Difficulty : value;
                PersistentData.Save(this);
            }
        }

        // public int Latency
        // {
        //     get => _latency;
        //     set => _latency = value > 25 ? 0 : value;
        // }

        // public KeyOf CurrentKey
        // {
        //     get => _currentKey;
        //     set => _currentKey = value > KeyOf.B ? KeyOf.C : value < KeyOf.C ? KeyOf.B : value;
        // }

        public DataEnum[] DataItems => Enumeration.All<DataItem>();

        public string GetDisplayLevel(DataEnum item) => item switch
        {
            // _ when item == DataItem.Transpose => CurrentKey.ToString(),
            // _ when item == DataItem.Latency => Latency.ToString(),
            _ => ""
        };

        public int GetLevel(DataEnum item) => item switch
        {
            // _ when item == DataItem.Transpose => (int)CurrentKey,
            // _ when item == DataItem.Latency => Latency,
            _ => -1,
        };
        //=> GameplayLevels[item].level;

        public void IncreaseLevel(DataEnum item)
        {
            // if (item == DataItem.Transpose) CurrentKey++;
            // else if (item == DataItem.Latency) Latency++;
        }

        public void DecreaseLevel(DataEnum item)
        {
            // if (item == DataItem.Transpose) CurrentKey--;
            // else if (item == DataItem.Latency) Latency--;
        }

        public void SetLevel(DataEnum item, int newGameplayLevel)
        {
            // if (item == DataItem.Transpose) CurrentKey = (KeyOf)newGameplayLevel;
            // else if (item == DataItem.Latency) Latency = newGameplayLevel;
        }

        [System.Serializable]
        public class DataItem : DataEnum
        {
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


        private GameplayData() { }

        public static GameplayData GetData()
        {
            GameplayData data = new();
            var loadData = data.PersistentData.TryLoadData();
            if (loadData is null) return data;
            data = (GameplayData)loadData;
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Gameplay.Data");
    }
}