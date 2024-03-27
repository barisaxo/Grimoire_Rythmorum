using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipStats
{
    [System.Serializable]
    public class ShipStats
    {
        public IHullStats HullStats;
        public ICannonStats CannonStats;
        public int NumOfCannons;

        public int HullStrength => (int)(HullStats.HullData.Modifier * HullStats.TimberType.Modifier);
        public int HitDamage => (int)(CannonStats.Cannon.Modifier * CannonStats.Metal.Modifier);
        public int VolleyDamage => (int)(CannonStats.Cannon.Modifier * CannonStats.Metal.Modifier * NumOfCannons);

        public ShipStats(IHullStats hull, ICannonStats cannon, int numOfCannons)
        {
            HullStats = hull;
            CannonStats = cannon;
            NumOfCannons = numOfCannons;
            // Debug.Log(cannon.Cannon.Modifier + " " + cannon.Metal.Modifier + " " + numOfCannons);
        }
    }

    public interface ICannonStats
    {
        public Data.Equipment.CannonData Cannon { get; }
        public Data.Inventory.MaterialsData.DataItem Metal { get; }
    }

    [System.Serializable]
    public class CannonStats : ICannonStats
    {
        public CannonStats(
            Data.Equipment.CannonData cannon,
            Data.Inventory.MaterialsData.DataItem metal)
        {
            Cannon = cannon;
            Metal = metal;
        }
        public Data.Equipment.CannonData Cannon { get; }
        public Data.Inventory.MaterialsData.DataItem Metal { get; }
    }

    public interface IHullStats
    {
        public Data.Equipment.HullData HullData { get; }
        public Data.Inventory.MaterialsData.DataItem TimberType { get; }
    }

    [System.Serializable]
    public class HullStats : IHullStats
    {
        public HullStats(
            Data.Equipment.HullData hullData,
            Data.Inventory.MaterialsData.DataItem timberType)
        {
            HullData = hullData;
            TimberType = timberType;
        }
        public Data.Equipment.HullData HullData { get; }
        public Data.Inventory.MaterialsData.DataItem TimberType { get; }
    }
    public interface IRiggingStats
    {
        public Data.Inventory.MaterialsData.DataItem ClothType { get; }
    }


}