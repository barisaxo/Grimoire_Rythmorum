using System;
using System.Collections.Generic;
using UnityEngine;
using Ships;

//https://docs.unity3d.com/ScriptReference/HeaderAttribute.html
public class SloopPrefab : MonoBehaviour, Sea.IShipPrefab
{
    [Header(nameof(Hull))]
    public Hull Hull;
    [Space(10)]

    [Header(nameof(Cannons))]
    public Cannon[] Cannons;

    // [HideInInspector]
    // public int NumOfCannons = 6;

    [Header(nameof(Rig))]
    public Rig SloopRig;


    public Hull _hull { get => Hull; }
    public Flag _flag { get => SloopRig.Flag; }
    public Rig _rig { get => SloopRig; }
    public Rigging _rigging { get => SloopRig.Rigging; }
    public Sail[] _sails { get => SloopRig.Sails; }
    public Mast[] _masts { get => SloopRig.Masts; }
    public CapsuleCollider Collider { get => Hull.Col; }
    // public string PopupText => "Hail";
    // public event Action Interaction;
    public Transform Transform => _hull.transform;
    public GameObject GO => gameObject;


    // public ShipStats.ShipStats ShipStats { get; set; }
    // private ShipStats.ShipStats _shipStats;
    // public ShipStats.ShipStats ShipStats => _shipStats ??= new(
    //     new ShipStats.HullStats(
    //         new Data.Two.Sloop(),
    //         new Data.Two.Pine()),

    //     new ShipStats.CannonStats(
    //         new Data.Two.Mynion(),
    //         new Data.Two.CastIron()),
    //         new ShipStats.RiggingStats(new Data.Two.Hemp()),
    //         numOfCannons: NumOfCannons
    // );


}
