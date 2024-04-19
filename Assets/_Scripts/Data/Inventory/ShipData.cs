using System.Collections;
using System.Collections.Generic;

namespace Data.Inventory
{
    [System.Serializable]
    public class ShipData : IData
    {
        private Dictionary<DataItem, int> _ship;
        private Dictionary<DataItem, int> Ship => _ship ??= SetUpShip();

        private ShipStats.ShipStats _shipStats;
        public ShipStats.ShipStats ShipStats => _shipStats ??= new(
            new ShipStats.HullStats(Data.Equipment.HullData.Schooner, MaterialsData.DataItem.Pine),
            new ShipStats.CannonStats(Data.Equipment.CannonData.Carronade, MaterialsData.DataItem.Bronze),
            new ShipStats.RiggingStats(Data.Inventory.MaterialsData.DataItem.Hemp),
            numOfCannons: 32
        );

        public void Reset() => _ship = SetUpShip();

        Dictionary<DataItem, int> SetUpShip()
        {
            Dictionary<DataItem, int> ship = new();
            foreach (var item in DataItems) ship.Add((DataItem)item, 10);
            return ship;
        }

        public string GetDisplayLevel(DataEnum item)
        {
            return item switch
            {
                _ when item == DataItem.Materials =>
                    (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.HullData.Modifier * .05f) +
                    " Tons of storage.",

                _ when item == DataItem.Fish =>
                    (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.HullData.Modifier * .05f) +
                    " Tons of storage.",

                _ when item == DataItem.Bottle =>
                    (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.HullData.Modifier * .01f) +
                    " storage spaces.",

                _ when item == DataItem.Gramos =>
                    (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.HullData.Modifier * .01f) +
                    " storage spaces.",

                _ when item == DataItem.HitPoints =>
                    ((int)(ShipStats.HullStats.HullData.Modifier * ShipStats.HullStats.TimberType.Modifier)).ToString(),

                _ when item == DataItem.Armament =>
                    ShipStats.NumOfCannons + " " + ShipStats.CannonStats.Metal.Name + " " + ShipStats.CannonStats.Cannon.Name +
                    " cannon" + (ShipStats.NumOfCannons > 1 ? "s" : ""),

                _ when item == DataItem.Damage =>
                    ((int)(ShipStats.CannonStats.Cannon.Modifier * ShipStats.CannonStats.Metal.Modifier * ShipStats.NumOfCannons)).ToString(),

                _ when item == DataItem.Hull => ShipStats.HullStats.HullData.Name,
                _ when item == DataItem.Sails => ShipStats.RiggingStats.ClothType.Name + " sails. ",
                _ => ""
            };
        }

        public int GetLevel(DataEnum item)
        {
            return item switch
            {
                _ when item == DataItem.Materials => (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.HullData.Modifier * .1f),
                _ when item == DataItem.Fish => (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.HullData.Modifier * .1f),
                _ when item == DataItem.Bottle => (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.HullData.Modifier * .1f),
                _ when item == DataItem.Gramos => (int)(ShipStats.RiggingStats.ClothType.Modifier * ShipStats.HullStats.HullData.Modifier * .1f),
                _ when item == DataItem.HitPoints => (int)(ShipStats.HullStats.HullData.Modifier * ShipStats.HullStats.TimberType.Modifier),
                _ when item == DataItem.Armament => ShipStats.NumOfCannons,
                _ when item == DataItem.Damage => (int)(ShipStats.CannonStats.Cannon.Modifier * ShipStats.CannonStats.Metal.Modifier * ShipStats.NumOfCannons),
                _ when item == DataItem.Hull => (int)(ShipStats.HullStats.HullData.Modifier),
                _ when item == DataItem.Sails => (int)(ShipStats.RiggingStats.ClothType.Modifier),
                _ => 0
            };
        }

        public void IncreaseLevel(DataEnum item)
        {
            Ship[(DataItem)item] += Ship[(DataItem)item] > 998 ? 0 : 1;
            PersistentData.Save(this);
        }

        public void DecreaseLevel(DataEnum item)
        {
            Ship[(DataItem)item] -= Ship[(DataItem)item] < 1 ? 0 : 1;
            PersistentData.Save(this);
        }

        public void SetLevel(DataEnum item, int newVolumeLevel)
        {
            Ship[(DataItem)item] = newVolumeLevel;
            PersistentData.Save(this);
        }

        public DataEnum[] DataItems => Enumeration.All<DataItem>();

        public bool InventoryIsFull(int Space) => false;

        [System.Serializable]
        public class DataItem : DataEnum
        {
            public DataItem() : base(0, "") { }
            public DataItem(int id, string name) : base(id, name) { }
            public DataItem(int id, string name, string desc) : base(id, name, desc) { }
            public static DataItem Materials = new(0, "Materials");
            public static DataItem Fish = new(1, "Fish");
            public static DataItem Bottle = new(2, "Star Charts");
            public static DataItem Gramos = new(3, "Gramophones");
            public static DataItem Sails = new(4, "Rigging");
            public static DataItem Hull = new(5, "Hull");
            public static DataItem HitPoints = new(6, "Hit Points");
            // public static DataItem Fishing = new(6, "Fishing");
            public static DataItem Armament = new(7, "Armament");
            // public static DataItem Speed = new(8, "Speed");
            public static DataItem Damage = new(8, "Damage potential");
        }

        private ShipData() { }

        public static ShipData GetData()
        {
            ShipData data = new();
            if (data.PersistentData.TryLoadData() is not ShipData loadData) return data;
            for (int i = 0; i < data.DataItems.Length; i++)
                try { data.SetLevel(data.DataItems[i], loadData.GetLevel(data.DataItems[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Ship.Data");
    }
}