using UnityEngine;
using System;
using Ships;

namespace Sea
{
    public interface IShipPrefab
    {
        // public ShipStats.ShipStats ShipStats { get; }
        public Hull _hull { get; }
        public Flag _flag { get; }
        public Rig _rig { get; }
        public Rigging _rigging { get; }
        public Sail[] _sails { get; }
        public Mast[] _masts { get; }
        public GameObject GO { get; }
        // public void RandomRig();
    }

}