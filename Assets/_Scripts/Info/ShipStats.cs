using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Two;

namespace ShipStats
{
    [System.Serializable]
    public class ShipStats
    {
        public IHullStats HullStats;
        public ICannonStats CannonStats;
        public IRiggingStats RiggingStats;
        public int NumOfCannons;

        public int Tonnage => (int)(HullStats.Hull.Modifier * RiggingStats.ClothType.Modifier * .1f);
        public int HullStrength => (int)(HullStats.Hull.Modifier * HullStats.Timber.Modifier);
        public int HitDamage => (int)(CannonStats.Cannon.Modifier * CannonStats.Metal.Modifier);
        public int VolleyDamage => (int)(CannonStats.Cannon.Modifier * CannonStats.Metal.Modifier * NumOfCannons);

        public ShipStats(IHullStats hull, ICannonStats cannon, IRiggingStats rigging, int numOfCannons)
        {
            HullStats = hull;
            CannonStats = cannon;
            NumOfCannons = numOfCannons;
            RiggingStats = rigging;
            // Debug.Log(cannon.Cannon.Modifier + " " + cannon.Metal.Modifier + " " + numOfCannons);
        }
    }

    public interface ICannonStats
    {
        public Data.Two.Cannon Cannon { get; }
        public Data.Two.Metal Metal { get; }
    }

    [System.Serializable]
    public class CannonStats : ICannonStats
    {
        public CannonStats(
            Data.Two.Cannon cannon,
            Data.Two.Metal metal)
        {
            Cannon = cannon;
            Metal = metal;
        }
        public Data.Two.Cannon Cannon { get; }
        public Data.Two.Metal Metal { get; }
    }

    public interface IHullStats
    {
        public Data.Two.BoatHull Hull { get; }
        public Data.Two.Wood Timber { get; }
    }

    [System.Serializable]
    public class HullStats : IHullStats
    {
        public HullStats(
            Data.Two.BoatHull hull,
            Wood timberType)
        {
            Hull = hull;
            Timber = timberType;
        }
        public Data.Two.BoatHull Hull { get; }
        public Wood Timber { get; }

        // Hull IHullStats.HullData => throw new System.NotImplementedException();
    }
    public interface IRiggingStats
    {
        public Data.Two.Cloth ClothType { get; }
    }
    [System.Serializable]
    public class RiggingStats : IRiggingStats
    {
        public RiggingStats(Data.Two.Cloth clothType)
        {
            ClothType = clothType;
        }
        public Data.Two.Cloth ClothType { get; }
    }

}