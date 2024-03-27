using UnityEngine;
using System;
namespace Sea
{
    public interface IMAShip //: Sea.IInteractable, Sea.ICollidable
    {
        public ShipStats.ShipStats ShipStats { get; }
        public Hull _hull { get; }
        public Flag _flag { get; }
        public Rig _rig { get; }
        public Rigging _rigging { get; }
        public Sail[] _sails { get; }
        public Mast[] _masts { get; }
    }

}