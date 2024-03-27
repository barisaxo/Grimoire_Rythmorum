using System;
using System.Collections.Generic;
using UnityEngine;

public class Schooner : MonoBehaviour, Sea.IMAShip
{
    // void Awake() { NumOfCannons = UnityEngine.Random.Range(4, 17); }

    [Header(nameof(Hull))]
    public Hull Hull;
    [Space(10)]

    [Header(nameof(Cannons))]
    public Cannon[] Cannons;

    [HideInInspector]
    public int NumOfCannons
    {
        get => _numOfCannons < 4 ? _numOfCannons = NumOfCannons = UnityEngine.Random.Range(4, 17) : _numOfCannons;
        set
        {
            if (value < 4 || value > 16) throw new ArgumentOutOfRangeException(value.ToString());
            _numOfCannons = value;
            for (int i = 0; i < Cannons.Length; i++)
                Cannons[i].gameObject.SetActive(i < value);
        }
    }
    private int _numOfCannons = 0;


    [Header(nameof(Rig))]
    public Rig SchoonerRig;
    public Rig TopRig;
    public Rig BrigRig;
    public Rig Rig { get; private set; }

    [HideInInspector]
    public SailConfig Config
    {
        get => _config;
        set
        {
            if (value == _config) return;
            _config = value;
            SchoonerRig.gameObject.SetActive(value == SailConfig.Schooner);
            TopRig.gameObject.SetActive(value == SailConfig.Top);
            BrigRig.gameObject.SetActive(value == SailConfig.Brig);
        }
    }
    private SailConfig _config;
    public enum SailConfig { Schooner, Top, Brig }

    public Hull _hull { get => Hull; }
    public Flag _flag { get => Rig.Flag; }
    public Rig _rig { get => Rig; }
    public Rigging _rigging { get => Rig.Rigging; }
    public Sail[] _sails { get => Rig.Sails; }
    public Mast[] _masts { get => Rig.Masts; }
    public CapsuleCollider Collider { get => Hull.Col; }
    public string PopupText => "Hail";
    public event Action Interaction;
    public Transform Transform => _hull.transform;

    private ShipStats.ShipStats _shipStats;
    public ShipStats.ShipStats ShipStats => _shipStats ??= new(
        new ShipStats.HullStats(
            Data.Equipment.HullData.Schooner,
            Data.Inventory.MaterialsData.DataItem.Fir),

        new ShipStats.CannonStats(
            Data.Equipment.CannonData.Saker,
            Data.Inventory.MaterialsData.DataItem.CastIron),
        numOfCannons: NumOfCannons
    );


}
