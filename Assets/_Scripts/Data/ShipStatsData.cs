using System.Collections;
using System.Collections.Generic;
using ShipStats;

namespace Data.Two
{
    [System.Serializable]
    public class ShipStatsData : IData
    {
        private Dictionary<IHull, ShipStats.ShipStats> _ship;
        private Dictionary<IHull, ShipStats.ShipStats> Ship => _ship ??= SetUpShip();


        public void Reset() => _ship = SetUpShip();

        Dictionary<IHull, ShipStats.ShipStats> SetUpShip()
        {
            Dictionary<IHull, ShipStats.ShipStats> ship = new();

            ship.TryAdd(new Sloop(), new ShipStats.ShipStats(new HullStats(new Sloop(), new Pine()), new CannonStats(new Mynion(), new WroughtIron()), new RiggingStats(new Hemp())));
            ship.TryAdd(new Cutter(), new ShipStats.ShipStats(new HullStats(new Sloop(), new Pine()), new CannonStats(new Mynion(), new WroughtIron()), new RiggingStats(new Hemp())));
            ship.TryAdd(new Schooner(), new ShipStats.ShipStats(new HullStats(new Sloop(), new Pine()), new CannonStats(new Mynion(), new WroughtIron()), new RiggingStats(new Hemp())));
            ship.TryAdd(new Brig(), new ShipStats.ShipStats(new HullStats(new Sloop(), new Pine()), new CannonStats(new Mynion(), new WroughtIron()), new RiggingStats(new Hemp())));
            ship.TryAdd(new Frigate(), new ShipStats.ShipStats(new HullStats(new Sloop(), new Pine()), new CannonStats(new Mynion(), new WroughtIron()), new RiggingStats(new Hemp())));
            ship.TryAdd(new Barque(), new ShipStats.ShipStats(new HullStats(new Sloop(), new Pine()), new CannonStats(new Mynion(), new WroughtIron()), new RiggingStats(new Hemp())));

            return ship;
        }

        public string GetDisplayLevel(IItem item)
        {
            return "";
        }

        public ShipStats.ShipStats GetItem(IItem item)
        {
            if (item is not IHull) throw new System.ArgumentException(item.Name);
            return Ship[(IHull)item];
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
            throw new System.ArgumentException(item.Name + " " + i);
        }

        public void AdjustLevel(IItem item, ShipStats.ShipStats stats)
        {
            if (item is not IHull) throw new System.ArgumentException(item.Name);
            Ship[(IHull)item] = stats;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int i)
        {
            if (item is not IHull) throw new System.ArgumentException(item.Name);
        }
        public void SetLevel(IItem item, ShipStats.ShipStats stats)
        {
            if (item is not IHull) throw new System.ArgumentException(item.Name);
            Ship[(IHull)item] = stats;
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
                    var enums = Enumeration.All<HullEnum>();
                    var items = new IItem[enums.Length];
                    for (int i = 0; i < enums.Length; i++)
                        items[i] = HullEnum.ToItem(enums[i]);
                    return items;
                }
            }
        }

        public bool InventoryIsFull(int Space) => false;

        private ShipStatsData() { }

        public static ShipStatsData GetData()
        {
            ShipStatsData data = new();
            if (data.PersistentData.TryLoadData() is not ShipStatsData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.SetLevel(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            return data;
        }

        string IData.GetDescription(IItem item)
        {
            return null;
        }

        public IPersistentData PersistentData { get; } = new SaveData("ShipStats.Data");
    }


}