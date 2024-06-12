using System;
using System.Collections.Generic;
using UnityEngine;
using Ships;

//https://docs.unity3d.com/ScriptReference/HeaderAttribute.html
public class CutterPrefab : MonoBehaviour, Sea.IShipPrefab
{
    [Header(nameof(Hull))]
    public Hull Hull;
    [Space(10)]

    [Header(nameof(Cannons))]
    public Cannon[] Cannons;

    // [HideInInspector]
    // public readonly int NumOfCannons = 16;

    [Header(nameof(Rig))]
    public Rig CutterRig;

    public Hull _hull { get => Hull; }
    public Flag _flag { get => CutterRig.Flag; }
    public Rig _rig { get => CutterRig; }
    public Rigging _rigging { get => CutterRig.Rigging; }
    public Sail[] _sails { get => CutterRig.Sails; }
    public Mast[] _masts { get => CutterRig.Masts; }
    public CapsuleCollider Collider { get => Hull.Col; }
    // public string PopupText => "Hail";
    // public event Action Interaction;
    public Transform Transform => _hull.transform;
    public GameObject GO => gameObject;


    public ShipStats.ShipStats ShipStats { get; set; }
    // private ShipStats.ShipStats _shipStats;
    // public ShipStats.ShipStats ShipStats => _shipStats ??= new(
    //     new ShipStats.HullStats(
    //         new Data.Cutter(),
    //         new Data.Pine()),

    //     new ShipStats.CannonStats(
    //         new Data.Mynion(),
    //         new Data.CastIron()),
    //         new ShipStats.RiggingStats(new Data.Hemp()),
    //         numOfCannons: NumOfCannons
    // );


}
