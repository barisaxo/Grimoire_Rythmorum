using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ships
{
    public class SchoonerPrefab : MonoBehaviour, Sea.IShipPrefab
    {
        // void Awake() { NumOfCannons = UnityEngine.Random.Range(4, 17); }

        [Header(nameof(Hull))]
        public Hull Hull;
        [Space(10)]

        [Header(nameof(Cannons))]
        public Cannon[] Cannons;

        // [HideInInspector]
        // public int NumOfCannons = 32;

        [Header(nameof(Rig))]
        public Rig SchoonerRig;
        public Hull _hull { get => Hull; }
        public Flag _flag { get => SchoonerRig.Flag; }
        public Rig _rig { get => SchoonerRig; }
        public Rigging _rigging { get => SchoonerRig.Rigging; }
        public Sail[] _sails { get => SchoonerRig.Sails; }
        public Mast[] _masts { get => SchoonerRig.Masts; }
        public CapsuleCollider Collider { get => Hull.Col; }
        // public string PopupText => "Hail";
        // public event Action Interaction;
        public Transform Transform => _hull.transform;
        public GameObject GO => gameObject;

        public ShipStats.ShipStats ShipStats { get; set; }
        // private ShipStats.ShipStats _shipStats;
        // public ShipStats.ShipStats ShipStats => _shipStats ??= new(
        //     new ShipStats.HullStats(
        //         new Data.Two.Schooner(),
        //         new Data.Two.Fir()),

        //     new ShipStats.CannonStats(
        //        new Data.Two.Saker(),
        //         new Data.Two.CastIron()),
        //         new ShipStats.RiggingStats(
        //         new Data.Two.Hemp()),
        //         numOfCannons: NumOfCannons
        // );


    }
}
