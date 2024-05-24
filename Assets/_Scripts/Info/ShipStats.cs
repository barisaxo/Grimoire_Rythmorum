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

        public ShipStats(IHullStats hull, ICannonStats cannon, IRiggingStats rigging)
        {
            HullStats = hull;
            CannonStats = cannon;
            RiggingStats = rigging;
            NumOfCannons = hull.Hull switch
            {
                Sloop or Cutter => 16,
                Schooner or Brig => 32,
                Frigate or Barque => 64,
                _ => throw new System.NotImplementedException(hull.Hull.Enum.Name)
            };
            // Debug.Log(cannon.Cannon.Modifier + " " + cannon.Metal.Modifier + " " + numOfCannons);
        }
    }

    public interface ICannonStats
    {
        public ICannon Cannon { get; }
        public IMetal Metal { get; }
    }

    [System.Serializable]
    public class CannonStats : ICannonStats
    {
        public CannonStats(
          ICannon cannon,
          IMetal metal)
        {
            Cannon = cannon;
            Metal = metal;
        }
        public ICannon Cannon { get; }
        public IMetal Metal { get; }
    }

    public interface IHullStats
    {
        public IHull Hull { get; }
        public IWood Timber { get; }
    }

    [System.Serializable]
    public class HullStats : IHullStats
    {
        public HullStats(
            IHull hull,
            IWood timberType)
        {
            Hull = hull;
            Timber = timberType;
        }
        public IHull Hull { get; }
        public IWood Timber { get; }

        // Hull IHullStats.HullData => throw new System.NotImplementedException();
    }
    public interface IRiggingStats
    {
        public ICloth ClothType { get; }
    }
    [System.Serializable]
    public class RiggingStats : IRiggingStats
    {
        public RiggingStats(ICloth clothType)
        {
            ClothType = clothType;
        }
        public ICloth ClothType { get; }
    }

}