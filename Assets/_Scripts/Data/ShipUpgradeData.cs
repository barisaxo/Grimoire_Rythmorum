using System.Collections.Generic;
using ShipStats;

namespace Data
{
    [System.Serializable]
    public class ShipUpgradeData : IData
    {
        private Dictionary<IShipUpgradeStat, int> _datum;
        private Dictionary<IShipUpgradeStat, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<IShipUpgradeStat, int> SetUpDatum()
        {
            Dictionary<IShipUpgradeStat, int> datum = new();
            foreach (IItem item in Items) datum.TryAdd(item as IShipUpgradeStat, 0);
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
                    var enums = Enumeration.All<ShipUpgradeStatEnum>();
                    var items = new IItem[enums.Length];
                    for (int i = 0; i < items.Length; i++)
                    {
                        items[i] = ShipUpgradeStatEnum.ToItem(enums[i]);
                        UnityEngine.Debug.Log(items[i].Name);
                    }

                    return items;
                }
            }
        }
        public ShipStats.ShipStats ActiveShip;

        public void Reset() { }

        public string GetDisplayLevel(IItem item)
        {
            return "";
        }

        public int GetLevel(IItem item)
        {
            return item switch
            {
                _ => throw new System.NotImplementedException(item.Name),
            };
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IShipUpgradeStat) throw new System.ArgumentException(item.Name);
            Datum[(IShipUpgradeStat)item] += i;


            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int i)
        {
            if (item is not IHull) throw new System.ArgumentException(item.Name);
            Datum[(IShipUpgradeStat)item] = i;
            PersistentData.Save(this);
        }

        public int GetSkillCost(IItem item)
        {
            if (item is not ISkill) throw new System.Exception(item.GetType().ToString());
            return ((ISkill)item).Cost * (Datum[(IShipUpgradeStat)item] + 1);
        }

        public bool InventoryIsFull(int Space) => false;

        public ShipUpgradeData(ShipStats.ShipStats shipStats) { ActiveShip = shipStats; }

        // public static ShipStatsData GetData()
        // {
        //     ShipStatsData data = new();
        //     if (data.PersistentData.TryLoadData() is not ShipStatsData loadData) return data;
        //     for (int i = 0; i < data.Items.Length; i++)
        //         try { data.SetLevel(data.Items[i], loadData.GetLevel(data.Items[i])); }
        //         catch { }
        //     return data;
        // }

        string IData.GetDescription(IItem item)
        {
            return null;
        }

        public IPersistentData PersistentData { get; } = new NotPersistentData();// new SaveData("ShipStats.Data");
    }

    public interface IShipUpgradeStat : IItem
    {
        ShipUpgradeStatEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [System.Serializable] public readonly struct TimberType : IShipUpgradeStat { public readonly ShipUpgradeStatEnum Enum => ShipUpgradeStatEnum.TimberType; }
    [System.Serializable] public readonly struct RiggingType : IShipUpgradeStat { public readonly ShipUpgradeStatEnum Enum => ShipUpgradeStatEnum.RiggingType; }
    [System.Serializable] public readonly struct CannonType : IShipUpgradeStat { public readonly ShipUpgradeStatEnum Enum => ShipUpgradeStatEnum.CannonType; }

    [System.Serializable]
    public class ShipUpgradeStatEnum : Enumeration
    {
        public ShipUpgradeStatEnum() : base(0, null) { }
        public ShipUpgradeStatEnum(int id, string name) : base(id, name) { }
        public ShipUpgradeStatEnum(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }

        public readonly string Description;

        public readonly static ShipUpgradeStatEnum TimberType = new(0, "Timber", "Increase the ships HP.");
        public readonly static ShipUpgradeStatEnum RiggingType = new(1, "Rigging", "Increase the ships tonnage.");
        public readonly static ShipUpgradeStatEnum CannonType = new(2, "Cannons", "Increase damage potential.");

        internal static IItem ToItem(ShipUpgradeStatEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == TimberType => new TimberType(),
                _ when @enum == RiggingType => new RiggingType(),
                _ when @enum == CannonType => new CannonType(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }

}