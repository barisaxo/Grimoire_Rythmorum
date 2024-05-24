using System.Collections;
using System.Collections.Generic;

namespace Data.Two
{
    [System.Serializable]
    public class PlayerShipData : IData
    {
        private Dictionary<IPlayerShipStat, int> _ship;
        private Dictionary<IPlayerShipStat, int> Ship => _ship ??= SetUpShip();

        private ShipStats.ShipStats _shipStats;
        public ShipStats.ShipStats ShipStats => _shipStats ??= new(
            new ShipStats.HullStats(new Sloop(), new Pine()),
            new ShipStats.CannonStats(new Mynion(), new WroughtIron()),
            new ShipStats.RiggingStats(new Hemp())
        );

        public void Reset() => _ship = SetUpShip();

        Dictionary<IPlayerShipStat, int> SetUpShip()
        {
            Dictionary<IPlayerShipStat, int> ship = new();
            foreach (var item in Items) ship.Add((IPlayerShipStat)item, 1);
            return ship;
        }

        public string GetDisplayLevel(IItem item)
        {
            return item switch
            {
                MaterialStorage =>
                    (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .05f) +
                    " Tons Of Storage.",

                FishStorage =>
                    (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .05f) +
                    " Tons Of Storage.",

                StarChartStorage =>
                    (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .01f) +
                    " Storage Spaces.",

                GramophoneStorage =>
                    (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .01f) +
                    " Storage Spaces.",

                CurrentHitPoints => GetLevel(item).ToString(),

                MaxHitPoints =>
                    ((int)(ShipStats.HullStats.Hull.Modifier * ShipStats.HullStats.Timber.Modifier)).ToString(),

                Armament =>
                    ShipStats.NumOfCannons + " " + ShipStats.CannonStats.Metal.Name.StartCase() + " " + ShipStats.CannonStats.Cannon.Name +
                    " Cannon" + (ShipStats.NumOfCannons > 1 ? "s" : ""),

                Damage =>
                    ((int)(ShipStats.CannonStats.Cannon.Modifier * ShipStats.CannonStats.Metal.Modifier * ShipStats.NumOfCannons)).ToString(),

                Hulls => ShipStats.HullStats.Hull.Name,

                Sails => ShipStats.RiggingStats.ClothType.Name + " Sails. ",

                _ => item.Name + " this is an error"
            };
        }

        public int GetLevel(IItem item)
        {
            return item switch
            {
                MaterialStorage => (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .05f),
                FishStorage => (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .05f),
                StarChart => (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .01f),
                GramophoneStorage => (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .01f),
                MaxHitPoints => (int)(ShipStats.HullStats.Hull.Modifier * ShipStats.HullStats.Timber.Modifier),
                CurrentHitPoints => (int)(ShipStats.HullStats.Hull.Modifier * ShipStats.HullStats.Timber.Modifier),
                Armament => ShipStats.NumOfCannons,
                Damage => (int)(ShipStats.CannonStats.Cannon.Modifier * ShipStats.CannonStats.Metal.Modifier * ShipStats.NumOfCannons),
                Hulls => (int)(ShipStats.HullStats.Hull.Modifier),
                Sails => (int)(ShipStats.RiggingStats.ClothType.Modifier),
                _ => throw new System.NotImplementedException(item.Name),
            };
        }

        public void AdjustLevel(IItem item, int i)
        {
            Ship[(IPlayerShipStat)item] =
                Ship[(IPlayerShipStat)item] + i < 0 ? 0 :
                Ship[(IPlayerShipStat)item] + i;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int i)
        {
            if (item is not IPlayerShipStat) throw new System.ArgumentException(item.Name);
            Ship[(IPlayerShipStat)item] = i;
            PersistentData.Save(this);
        }

        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                static IItem[] SetUp()
                {
                    var enums = Enumeration.All<PlayerShipStatEnum>();
                    var items = new IItem[enums.Length];
                    for (int i = 0; i < enums.Length; i++)
                        items[i] = PlayerShipStatEnum.ToItem(enums[i]);
                    return items;
                }
            }
        }
        public bool InventoryIsFull(int Space) => false;

        private PlayerShipData() { }

        public static PlayerShipData GetData()
        {
            PlayerShipData data = new();
            if (data.PersistentData.TryLoadData() is not PlayerShipData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.SetLevel(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            return data;
        }

        string IData.GetDescription(IItem item)
        {
            return null;
        }

        public IPersistentData PersistentData { get; } = new SaveData("PlayerShip.Data");
    }

    public interface IPlayerShipStat : IItem
    {
        PlayerShipStatEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [System.Serializable] public readonly struct MaterialStorage : IPlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.MaterialStorage; }
    [System.Serializable] public readonly struct FishStorage : IPlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.FishStorage; }
    [System.Serializable] public readonly struct StarChartStorage : IPlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.StarChartStorage; }
    [System.Serializable] public readonly struct GramophoneStorage : IPlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.GramophoneStorage; }
    [System.Serializable] public readonly struct Sails : IPlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.Sails; }
    [System.Serializable] public readonly struct Hulls : IPlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.Hull; }
    [System.Serializable] public readonly struct MaxHitPoints : IPlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.MaxHitPoints; }
    [System.Serializable] public readonly struct CurrentHitPoints : IPlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.CurrentHitPoints; }
    [System.Serializable] public readonly struct Armament : IPlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.Armament; }
    [System.Serializable] public readonly struct Damage : IPlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.Damage; }

    [System.Serializable]
    public class PlayerShipStatEnum : Enumeration
    {
        public PlayerShipStatEnum() : base(0, null) { }
        public PlayerShipStatEnum(int id, string name) : base(id, name) { }
        public PlayerShipStatEnum(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }

        public readonly string Description;

        public readonly static PlayerShipStatEnum MaterialStorage = new(0, "Materials");
        public readonly static PlayerShipStatEnum FishStorage = new(1, "Fish");
        public readonly static PlayerShipStatEnum StarChartStorage = new(2, "Star Charts");
        public readonly static PlayerShipStatEnum GramophoneStorage = new(3, "Gramophones");
        public readonly static PlayerShipStatEnum Sails = new(4, "Rigging");
        public readonly static PlayerShipStatEnum Hull = new(5, "Hull");
        public readonly static PlayerShipStatEnum MaxHitPoints = new(6, "Max Hit Points");
        public readonly static PlayerShipStatEnum CurrentHitPoints = new(7, "Current Hit Points");
        public readonly static PlayerShipStatEnum Armament = new(8, "Armament");
        public readonly static PlayerShipStatEnum Damage = new(9, "Damage potential");

        internal static IItem ToItem(PlayerShipStatEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == MaterialStorage => new MaterialStorage(),
                _ when @enum == FishStorage => new FishStorage(),
                _ when @enum == StarChartStorage => new StarChartStorage(),
                _ when @enum == GramophoneStorage => new GramophoneStorage(),
                _ when @enum == Sails => new Sails(),
                _ when @enum == Hull => new Hulls(),
                _ when @enum == MaxHitPoints => new MaxHitPoints(),
                _ when @enum == CurrentHitPoints => new CurrentHitPoints(),
                _ when @enum == Armament => new Armament(),
                _ when @enum == Damage => new Damage(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}