using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ships
{
    public class BrigPrefab : MonoBehaviour, Sea.IShipPrefab
    {
        [Header(nameof(Hull))]
        public Hull Hull;
        [Space(10)]

        [Header(nameof(Cannons))]
        public Cannon[] Cannons;

        // [HideInInspector]
        // public int NumOfCannons = 32;

        [Header(nameof(Rig))]
        public Rig BrigRig;
        public Hull _hull { get => Hull; }
        public Flag _flag { get => BrigRig.Flag; }
        public Rig _rig { get => BrigRig; }
        public Rigging _rigging { get => BrigRig.Rigging; }
        public Sail[] _sails { get => BrigRig.Sails; }
        public Mast[] _masts { get => BrigRig.Masts; }
        public CapsuleCollider Collider { get => Hull.Col; }
        // public string PopupText => "Hail";
        // public event Action Interaction;
        public Transform Transform => _hull.transform;
        public GameObject GO => gameObject;

        // public ShipStats.ShipStats ShipStats { get; set; }
        // private ShipStats.ShipStats _shipStats;
        // public ShipStats.ShipStats ShipStats => _shipStats ??= new(
        //     new ShipStats.HullStats(
        //         new Data.Schooner(),
        //         new Data.Fir()),

        //     new ShipStats.CannonStats(
        //        new Data.Saker(),
        //         new Data.CastIron()),
        //         new ShipStats.RiggingStats(
        //         new Data.Hemp()),
        //         numOfCannons: NumOfCannons
        // );


    }
}
