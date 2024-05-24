using System;
using UnityEngine;
using Ships;

public class FrigatePrefab : MonoBehaviour, Sea.IShipPrefab
{
    [Header(nameof(Hull))]
    [SerializeField] private Hull Hull;
    [Space(10)]

    [Header(nameof(Cannons))]
    [SerializeField] private Cannon[] Cannons;

    [HideInInspector]
    public int NumOfCannons = 64;

    [Header(nameof(Rig))]
    [SerializeField] private Rig FrigateRig;

    public Hull _hull { get => Hull; }
    public Flag _flag { get => FrigateRig.Flag; }
    public Rig _rig { get => FrigateRig; }
    public Rigging _rigging { get => FrigateRig.Rigging; }
    public Sail[] _sails { get => FrigateRig.Sails; }
    public Mast[] _masts { get => FrigateRig.Masts; }
    public CapsuleCollider Collider { get => Hull.Col; }
    public string PopupText => "Hail";
    public event Action Interaction;
    public Transform Transform => _hull.transform;
    public GameObject GO => gameObject;

    public ShipStats.ShipStats ShipStats { get; }
    // private ShipStats.ShipStats _shipStats;
    // public ShipStats.ShipStats ShipStats => _shipStats ??= new(
    //     new ShipStats.HullStats(
    //         new Data.Two.Frigate(),
    //         new Data.Two.Oak()),

    //     new ShipStats.CannonStats(
    //         new Data.Two.Culverin(),
    //        new Data.Two.CastIron()),
    //     new ShipStats.RiggingStats(new Data.Two.Hemp()),
    //     minCannons: MinCannons,
    //     maxCannons: MaxCannons,
    //     numOfCannons: NumOfCannons
    // );
}
