using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Data.Options;
using UnityEngine;
using Data;
using Data.Player;
using Data.Inventory;

// [System.Serializable]
public class DataManager
{
    #region  INSTANCE
    private DataManager() { }

    public static DataManager Io => Instance.Io;

    [System.Serializable]
    private class Instance
    {
        static Instance() { }
        static DataManager _io;
        internal static DataManager Io => _io ??= new();
        internal static void Destruct() => _io = null;

        // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        // private static DataManager TryLoadData()
        // {
        //     DataManager tryLoadData = null;

        //     if (File.Exists(Application.persistentDataPath + fileName))
        //     {
        //         FileStream stream = new(Application.persistentDataPath + fileName, FileMode.Open)
        //         {
        //             Position = 0
        //         };

        //         try
        //         {
        //             tryLoadData = new BinaryFormatter().Deserialize(stream) as DataManager;
        //             stream.Close();
        //             Debug.Log(tryLoadData);
        //         }

        //         catch
        //         {
        //             stream.Close();
        //             Debug.Log("A loading error has ocurred");
        //         }
        //     }

        //     if (tryLoadData is not null) return tryLoadData;
        //     return new DataManager();
        // }
    }

    public void SelfDestruct()
    {
        Instance.Destruct();
    }
    #endregion INSTANCE

    private CharacterData _characterData;
    public CharacterData CharacterData => _characterData ??= new();



    private Data.Player.CharacterData _charData;
    public Data.Player.CharacterData CharData => _charData ??= Data.Player.CharacterData.GetData();


    //private PlayerData _playerData;
    //public PlayerData PlayerData => _playerData ??= new();

    private GameplayData _gameplayData;
    public GameplayData GamePlay => _gameplayData ??= GameplayData.GetData();

    private VolumeData _volume;
    public VolumeData Volume => _volume ??= VolumeData.GetData();

    private TheoryPuzzleData _theoryPuzzle;
    public TheoryPuzzleData TheoryPuzzleData => _theoryPuzzle ??= new();

    private FishData _fishData;
    public FishData FishData => _fishData ??= FishData.GetData();

    private MaterialsData _materialsData;
    public MaterialsData MaterialsData => _materialsData ??= MaterialsData.GetData();

    // private EpisodeData _episodeData;
    // public EpisodeData EpisodeData => _episodeData ??= new EpisodeData();

    private StarChartsData _starChartsData;
    public StarChartsData starChartsData => _starChartsData ??= StarChartsData.GetData();

    private SettingsData _settings;
    public SettingsData Settings => _settings ??= SettingsData.GetData();

    private QuestData _questsData;
    public QuestData QuestsData => _questsData ??= QuestData.GetData();

    private LighthouseData _lighthousesData;
    public LighthouseData LighthousesData => _lighthousesData ??= LighthouseData.GetData();
    //public void ResetCharacterAndPlayerData() { _characterData = new CharacterData(); _playerData = new PlayerData(); }
    private ShipData _shipData;
    public ShipData ShipData => _shipData ??= ShipData.GetData();

    private GramophoneData _gramophoneData;
    public GramophoneData GramophoneData => _gramophoneData ??= GramophoneData.GetData();

    private Data.Player.PlayerData _playerData;
    public Data.Player.PlayerData PlayerData => _playerData ??= Data.Player.PlayerData.GetData();

    private Data.Player.SkillsData _skillsData;
    public Data.Player.SkillsData SkillsData => _skillsData ??= Data.Player.SkillsData.GetData();

    private Data.Player.RhythmCellsData _rhythmCellData;
    public Data.Player.RhythmCellsData RhythmCellData => _rhythmCellData ??= Data.Player.RhythmCellsData.GetData();
    // const string fileName = "/save.this";
    // public void Save(IData data)
    // {
    //     data?.PersistentData?.Save(data);
    //     // GramophoneData.PersistentData.Save(GramophoneData);
    //     // ShipData.PersistentData.Save(ShipData);
    //     // LighthousesData.PersistentData.Save(LighthousesData);
    //     // QuestsData.PersistentData.Save(QuestsData);
    //     // Settings.PersistentData.Save(Settings);
    //     // starChartsData.PersistentData.Save(starChartsData);
    //     // MaterialsData.PersistentData.Save(MaterialsData);
    //     // FishData.PersistentData.Save(FishData);
    //     // Volume.PersistentData.Save(Volume);
    //     // GamePlay.PersistentData.Save(GamePlay);
    // }


    // public void Save()
    // {
    //     FileStream fileStream = new(Application.persistentDataPath + fileName, FileMode.Create);
    //     new BinaryFormatter().Serialize(fileStream, this);
    //     fileStream.Close();
    // }

    // class DataItem : DataEnum
    // {
    //     public DataItem() : base(0, "") { }
    //     private DataItem(int id, string name) : base(id, name) { }

    //     public static readonly DataItem Volume = new(0, nameof(Volume));
    //     public static readonly DataItem Settings = new(0, nameof(Settings));
    //     public static readonly DataItem StarChart = new(0, nameof(StarChart));
    //     public static readonly DataItem Fish = new(0, nameof(Fish));
    //     public static readonly DataItem Materials = new(0, nameof(Materials));
    //     public static readonly DataItem Character = new(0, nameof(Character));
    // }

}
