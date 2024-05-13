
// namespace Data.Inventory
// {
//     [System.Serializable]
//     public class InventoryData : IData
//     {
//         private (DataItem inventoryItem, int level)[] _inventoryItems;
//         private (DataItem inventoryItem, int level)[] InventoryLevels => _inventoryItems ??= SetUpInventoryItems();
//         private (DataItem inventoryItem, int level)[] SetUpInventoryItems()
//         {
//             var items = DataItems;
//             var levels = new (DataItem inventoryItem, int level)[items.Length];
//             return levels;
//         }

//         public void Reset() => _inventoryItems = SetUpInventoryItems();
//         public string GetDisplayLevel(DataEnum item) => 0.ToString();
//         public int GetLevel(DataEnum item) => 0;
//         public void SetLevel(DataEnum item, int newInventoryLevel) { }
//         public void IncreaseLevel(DataEnum item) { }
//         public void DecreaseLevel(DataEnum item) { }
//         public DataEnum[] DataItems => Enumeration.All<DataItem>();
//         public bool InventoryIsFull(int Space) => false;

//         public class DataItem : DataEnum
//         {
//             public DataItem() : base(0, "") { }
//             public DataItem(int id, string name) : base(id, name) { }
//             public static DataItem Quests = new(0, nameof(Quests).CapsCase());
//             public static DataItem Materials = new(1, nameof(Materials).CapsCase());
//             public static DataItem Fish = new(2, nameof(Fish).CapsCase());
//             public static DataItem StarCharts = new(3, nameof(StarCharts).CapsCase());
//             public static DataItem Gramophones = new(4, nameof(Gramophones).CapsCase());
//             public static DataItem Lighthouses = new(5, nameof(Lighthouses).CapsCase());
//             public static DataItem Ship = new(6, nameof(Ship).CapsCase());
//             public static DataItem Player = new(7, nameof(Player).CapsCase());
//         }
//         public IPersistentData PersistentData { get; } = new NotPersistentData();
//     }
// }