
using System;
namespace Data
{
    [System.Serializable]
    public class SeaMenuData : IData
    {
        // private (IItem SeaMenuItem, int level)[] _SeaMenuItems;
        // private (IItem SeaMenuItem, int level)[] SeaMenuLevels => _SeaMenuItems ??= SetUpSeaMenuItems();
        // private (IItem SeaMenuItem, int level)[] SetUpSeaMenuItems()
        // {
        //     var items = Items;
        //     var levels = new (IItem SeaMenuItem, int level)[items.Length];
        //     return levels;
        // }

        public void Reset() { return; }// _SeaMenuItems = SetUpSeaMenuItems();
        public string GetDisplayLevel(IItem item) => 0.ToString();
        public int GetLevel(IItem item) => 0;
        public void SetLevel(IItem item, int newSeaMenuLevel) { }
        // public void IncreaseLevel(IItem item) { }
        // public void DecreaseLevel(IItem item) { }
        public void AdjustLevel(IItem item, int i) { }
        // public void DecreaseLevel(IItem item, int i) { }
        private IItem[] _items;
        public IItem[] Items => _items ??= new IItem[]{
            new ActiveQuests(),
            new Inventory(),
            // new StarChartInventory(),
            // new GramophoneInventory(),
            new Standings(),
            new LighthouseInventory(),
            new ShipStat(),
            new PlayerStats()
        };
        public bool SeaMenuIsFull(int Space) => false;

        string IData.GetDescription(IItem item)
        {
            throw new NotImplementedException();
        }

        bool IData.InventoryIsFull(int space)
        {
            throw new NotImplementedException();
        }

        public IPersistentData PersistentData { get; } = new NotPersistentData();

    }

    public interface SeaMenuOption : IItem
    {
        SeaMenuEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public struct ActiveQuests : SeaMenuOption { public readonly SeaMenuEnum Enum => SeaMenuEnum.Quests; }
    [Serializable] public struct Inventory : SeaMenuOption { public readonly SeaMenuEnum Enum => SeaMenuEnum.Inventory; }
    // [Serializable] public struct StarChartInventory : SeaMenuOption { public readonly SeaMenuEnum Enum => SeaMenuEnum.StarChartInventory; }
    // [Serializable] public struct GramophoneInventory : SeaMenuOption { public readonly SeaMenuEnum Enum => SeaMenuEnum.GramophoneInventory; }
    [Serializable] public struct Standings : SeaMenuOption { public readonly SeaMenuEnum Enum => SeaMenuEnum.Standings; }
    [Serializable] public struct LighthouseInventory : SeaMenuOption { public readonly SeaMenuEnum Enum => SeaMenuEnum.LighthouseInventory; }
    [Serializable] public struct ShipStat : SeaMenuOption { public readonly SeaMenuEnum Enum => SeaMenuEnum.ShipStat; }
    [Serializable] public struct PlayerStats : SeaMenuOption { public readonly SeaMenuEnum Enum => SeaMenuEnum.PlayerStats; }

    [Serializable]
    public class SeaMenuEnum : Enumeration
    {
        public SeaMenuEnum() : base(0, "") { }
        public SeaMenuEnum(int id, string name) : base(id, name) { }

        public readonly string Description;

        public static readonly SeaMenuEnum Quests = new(0, "QUESTS");
        public static readonly SeaMenuEnum Inventory = new(1, "INVENTORY");
        // public static readonly SeaMenuEnum StarChartInventory = new(2, "STAR CHARTS");
        // public static readonly SeaMenuEnum GramophoneInventory = new(3, "GRAMOPHONES");
        public static readonly SeaMenuEnum Standings = new(2, "STANDINGS");
        public static readonly SeaMenuEnum LighthouseInventory = new(3, "LIGHTHOUSES");
        public static readonly SeaMenuEnum ShipStat = new(4, "SHIP");
        public static readonly SeaMenuEnum PlayerStats = new(5, "PLAYER");

        public static IItem ToItem(SeaMenuEnum @enum) => @enum switch
        {
            _ when @enum == Quests => new ActiveQuests(),
            _ when @enum == Inventory => new Inventory(),
            // _ when @enum == StarChartInventory => new StarChartInventory(),
            // _ when @enum == GramophoneInventory => new GramophoneInventory(),
            _ when @enum == Standings => new Standings(),
            _ when @enum == LighthouseInventory => new LighthouseInventory(),
            _ when @enum == ShipStat => new ShipStat(),
            _ when @enum == PlayerStats => new PlayerStats(),
            _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
        };
    }
}

// public class IItem : IItem
// {
//     public IItem() : base(0, "") { }
//     public IItem(int id, string name) : base(id, name) { }
//     public static IItem Quests = new(0, nameof(Quests).CapsCase());
//     public static IItem Materials = new(1, nameof(Materials).CapsCase());
//     public static IItem Fish = new(2, nameof(Fish).CapsCase());
//     public static IItem StarCharts = new(3, nameof(StarCharts).CapsCase());
//     public static IItem Gramophones = new(4, nameof(Gramophones).CapsCase());
//     public static IItem Lighthouses = new(5, nameof(Lighthouses).CapsCase());
//     public static IItem Ship = new(6, nameof(Ship).CapsCase());
//     public static IItem Player = new(7, nameof(Player).CapsCase());
// }