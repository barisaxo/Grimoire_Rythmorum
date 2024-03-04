using System.Collections;
using System.Collections.Generic;

namespace Data.Inventory
{
    [System.Serializable]
    public class ShipData : IData
    {
        private Dictionary<DataItem, int> _ship;
        private Dictionary<DataItem, int> Ship => _ship ??= SetUpShip();

        Dictionary<DataItem, int> SetUpShip()
        {
            Dictionary<DataItem, int> ship = new();
            foreach (var item in DataItems) ship.Add((DataItem)item, 10);
            return ship;
        }

        public string GetDisplayLevel(DataEnum item) => Ship[(DataItem)item].ToString();

        public int GetLevel(DataEnum item) => Ship[(DataItem)item];

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


        [System.Serializable]
        public class DataItem : DataEnum
        {
            public DataItem() : base(0, "") { }
            public DataItem(int id, string name) : base(id, name) { }
            public static DataItem Materials = new(0, "Materials tonnage");
            public static DataItem Fish = new(1, "Fish tonnage");
            public static DataItem Bottle = new(2, "Star Chart storage");
            public static DataItem Gramos = new(3, "Ship storage");
            public static DataItem Sails = new(4, "Sails");
            public static DataItem Hull = new(5, "Hull");
            public static DataItem Fishing = new(6, "Fishing");
            public static DataItem Cannons = new(7, "Cannons");
            public static DataItem Speed = new(8, "Speed");
            public static DataItem Damage = new(9, "Damage potential");
        }

        private ShipData() { }

        public static ShipData GetData()
        {
            ShipData data = new();
            var loadData = data.PersistentData.TryLoadData();
            if (loadData is null) return data;
            data = (ShipData)loadData;
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Ship.Data");
    }
}