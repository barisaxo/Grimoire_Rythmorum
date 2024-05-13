
// namespace Data.Main
// {
//     [System.Serializable]
//     public class MainMenuData : IData
//     {
//         private (DataItem mainMenuItem, int level)[] _mainMenuLevels;
//         public (DataItem mainMenuItem, int level)[] MainMenuLevels => _mainMenuLevels ??= SetUpMainMenuLevels();
//         private (DataItem mainMenuItem, int level)[] SetUpMainMenuLevels()
//         {
//             var items = DataItems;
//             var levels = new (DataItem mainMenuItem, int level)[items.Length];
//             return levels;
//         }

//         public void Reset() { }
//         public string GetDisplayLevel(DataEnum item) => string.Empty;
//         public int GetLevel(DataEnum item) => 0;
//         public void SetLevel(DataEnum item, int newMainMenuLevel) { }
//         public void IncreaseLevel(DataEnum item) { }
//         public void DecreaseLevel(DataEnum item) { }
//         public DataEnum[] DataItems => Enumeration.All<DataItem>();
//         readonly DataManager DataManager;
//         public bool InventoryIsFull(int Space) => false;

//         public MainMenuData(DataManager dataManager)
//         {
//             DataManager = dataManager;
//         }

//         public class DataItem : DataEnum
//         {
//             public static readonly DataItem Continue = new(0, "Instantiate");
//             // public static readonly DataItem LoadGame = new(1, "Load Game");
//             // public static readonly DataItem NewGame = new(1, "New Game");
//             public static readonly DataItem Options = new(1, "Options");
//             // public static readonly DataItem HowToPlay = new(4, "How To Play");
//             // public static readonly DataItem Quit = new(5, "Quit");

//             public DataItem() : base(0, "") { }

//             private DataItem(int id, string name) : base(id, name) { }
//         }
//         public IPersistentData PersistentData { get; } = new NotPersistentData();
//     }
// }
