using System.Collections;
using System.Collections.Generic;

namespace Data.Two
{
    [System.Serializable]
    public class PlayerShipData : IData
    {
        private Dictionary<PlayerShipStat, int> _ship;
        private Dictionary<PlayerShipStat, int> Ship => _ship ??= SetUpShip();

        private ShipStats.ShipStats _shipStats;
        public ShipStats.ShipStats ShipStats => _shipStats ??= new(
            new ShipStats.HullStats(new Schooner(), new Pine()),
            new ShipStats.CannonStats(new Carronade(), new Bronze()),
            new ShipStats.RiggingStats(new Hemp()),
            numOfCannons: 32
        );

        public void Reset() => _ship = SetUpShip();

        Dictionary<PlayerShipStat, int> SetUpShip()
        {
            Dictionary<PlayerShipStat, int> ship = new();
            foreach (var item in Items) ship.Add((PlayerShipStat)item, 10);
            return ship;
        }

        public string GetDisplayLevel(IItem item)
        {
            return item switch
            {
                Material =>
                    (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .05f) +
                    " Tons of storage.",

                Fish =>
                    (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .05f) +
                    " Tons of storage.",

                StarChart =>
                    (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .01f) +
                    " storage spaces.",

                Gramophone =>
                    (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .01f) +
                    " storage spaces.",

                MaxHitPoints =>
                    ((int)(ShipStats.HullStats.Hull.Modifier * ShipStats.HullStats.Timber.Modifier)).ToString(),

                Armament =>
                    ShipStats.NumOfCannons + " " + ShipStats.CannonStats.Metal.Name + " " + ShipStats.CannonStats.Cannon.Name +
                    " cannon" + (ShipStats.NumOfCannons > 1 ? "s" : ""),

                Damage =>
                    ((int)(ShipStats.CannonStats.Cannon.Modifier * ShipStats.CannonStats.Metal.Modifier * ShipStats.NumOfCannons)).ToString(),

                BoatHull => ShipStats.HullStats.Hull.Name,
                Sails => ShipStats.RiggingStats.ClothType.Name + " sails. ",
                _ => ""
            };
        }

        public int GetLevel(IItem item)
        {
            return item switch
            {
                Material => (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .1f),
                Fish => (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .1f),
                StarChart => (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .1f),
                Gramophone => (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.Hull.Modifier * .1f),
                MaxHitPoints => (int)(ShipStats.HullStats.Hull.Modifier * ShipStats.HullStats.Timber.Modifier),
                CurrentHitPoints => (int)(ShipStats.HullStats.Hull.Modifier * ShipStats.HullStats.Timber.Modifier),
                Armament => ShipStats.NumOfCannons,
                Damage => (int)(ShipStats.CannonStats.Cannon.Modifier * ShipStats.CannonStats.Metal.Modifier * ShipStats.NumOfCannons),
                BoatHull => (int)(ShipStats.HullStats.Hull.Modifier),
                Sails => (int)(ShipStats.RiggingStats.ClothType.Modifier),
                _ => 0
            };
        }

        public void AdjustLevel(IItem item, int i)
        {
            Ship[(PlayerShipStat)item] =
                Ship[(PlayerShipStat)item] + i < 0 ? 0 :
                Ship[(PlayerShipStat)item] + i;
            PersistentData.Save(this);
        }

        // public void IncreaseLevel(IItem item)
        // {
        //     Ship[(PlayerShipStat)item]++; ;
        //     PersistentData.Save(this);
        // }
        // public void IncreaseLevel(IItem item, int i)
        // {
        //     Ship[(PlayerShipStat)item] += i;
        //     PersistentData.Save(this);
        // }

        // public void DecreaseLevel(IItem item, int i)
        // {
        //     Ship[(PlayerShipStat)item] -= Ship[(PlayerShipStat)item] < 1 ? 0 : 1;
        //     PersistentData.Save(this);
        // }
        // public void DecreaseLevel(IItem item)
        // {
        //     Ship[(PlayerShipStat)item] -= Ship[(PlayerShipStat)item] < 1 ? 0 : 1;
        //     PersistentData.Save(this);
        // }

        public void SetLevel(IItem item, int i)
        {
            if (item is not PlayerShipStat) throw new System.ArgumentException(item.Name);
            Ship[(PlayerShipStat)item] = i;
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
            // throw new System.NotImplementedException();
        }

        public IPersistentData PersistentData { get; } = new SaveData("PlayerShip.Data");
    }

    public interface PlayerShipStat : IItem
    {
        PlayerShipStatEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [System.Serializable] public readonly struct Materials : PlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.Materials; }
    [System.Serializable] public readonly struct Fishing : PlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.Fish; }
    [System.Serializable] public readonly struct Bottle : PlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.Bottle; }
    [System.Serializable] public readonly struct Gramos : PlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.Gramos; }
    [System.Serializable] public readonly struct Sails : PlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.Sails; }
    [System.Serializable] public readonly struct Hulls : PlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.Hull; }
    [System.Serializable] public readonly struct MaxHitPoints : PlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.MaxHitPoints; }
    [System.Serializable] public readonly struct CurrentHitPoints : PlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.CurrentHitPoints; }
    [System.Serializable] public readonly struct Armament : PlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.Armament; }
    [System.Serializable] public readonly struct Damage : PlayerShipStat { public readonly PlayerShipStatEnum Enum => PlayerShipStatEnum.Damage; }

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

        public readonly static PlayerShipStatEnum Materials = new(0, "Materials");
        public readonly static PlayerShipStatEnum Fish = new(1, "Fish");
        public readonly static PlayerShipStatEnum Bottle = new(2, "Star Charts");
        public readonly static PlayerShipStatEnum Gramos = new(3, "Gramophones");
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
                _ when @enum == Materials => new Materials(),
                _ when @enum == Fish => new Fishing(),
                _ when @enum == Bottle => new Bottle(),
                _ when @enum == Gramos => new Gramos(),
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