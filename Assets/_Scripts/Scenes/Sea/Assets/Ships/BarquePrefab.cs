using System;
using UnityEngine;
using Ships;

public class BarquePrefab : MonoBehaviour, Sea.IShipPrefab
{
    [Header(nameof(Hull))]
    [SerializeField] private Hull Hull;
    [Space(10)]

    [Header(nameof(Cannons))]
    [SerializeField] private Cannon[] Cannons;

    // [HideInInspector]
    // public int NumOfCannons = 64;

    [Header(nameof(Rig))]
    [SerializeField] private Rig BarqueRig;


    public Hull _hull { get => Hull; }
    public Flag _flag { get => BarqueRig.Flag; }
    public Rig _rig { get => BarqueRig; }
    public Rigging _rigging { get => BarqueRig.Rigging; }
    public Sail[] _sails { get => BarqueRig.Sails; }
    public Mast[] _masts { get => BarqueRig.Masts; }
    public CapsuleCollider Collider { get => Hull.Col; }
    // public string PopupText => "Hail";
    // public event Action Interaction;
    public Transform Transform => _hull.transform;
    public GameObject GO => gameObject;

    // public ShipStats.ShipStats ShipStats { get; }
}
