using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    public class Manager
    {
        #region  INSTANCE
        private Manager() { }

        public static Manager Io => Instance.Io;
        private class Instance
        {
            static Instance() { }
            static Manager _io;
            internal static Manager Io => _io ??= new();
        }
        #endregion INSTANCE

        // private WoodInventoryData _woodInventoryData;
        // public WoodInventoryData WoodInventoryData => _woodInventoryData ??= new();

        // private WoodStorageData _woodStorageData;
        // public WoodStorageData WoodStorage => _woodStorageData ??= WoodStorageData.GetData();

        // private FishInventoryData _fishInventoryData;
        // public FishInventoryData FishInventoryData => _fishInventoryData ??= new();

        private InventoryData _inventory;
        public InventoryData Inventory => _inventory ??= new();

        private StarChartData _starChartInventoryData;
        public StarChartData StarChart => _starChartInventoryData ??= new();

        private GramophoneInventoryData _gramophone;
        public GramophoneInventoryData Gramophones => _gramophone ??= new();

        private LighthouseData _lighthouse;
        public LighthouseData Lighthouse => _lighthouse ??= new();

        private ActiveShipData _activeShip;
        public ActiveShipData ActiveShip => _activeShip ??= new();

        private FishInventoryData _fish;
        public FishInventoryData Fish => _fish ??= new();

        private VolumeData _volumeData;
        public VolumeData Volume => _volumeData ??= VolumeData.GetData();

        private GameplayData _gameplayData;
        public GameplayData Gameplay => _gameplayData ??= GameplayData.GetData();

        private SkillData _skillData;
        public SkillData Skill => _skillData ??= SkillData.GetData();

        private QuestData _questData;
        public QuestData Quests => _questData ??= new();

        private PlayerData _playerData;
        public PlayerData Player => _playerData ??= PlayerData.GetData();

        private StandingData _standingData;
        public StandingData StandingData => _standingData ??= new StandingData();

        private ShipStatsData _shipStatsData;
        public ShipStatsData ShipStats => _shipStatsData ??= ShipStatsData.GetData();

        private ShipUpgradeData _shipUpgradeData;
        public ShipUpgradeData ShipUpgradeData => _shipUpgradeData ??= new ShipUpgradeData(ShipStats.ActiveShip);

        private MiscData _miscData;
        public MiscData MiscData => _miscData ??= MiscData.GetData();
    }
}