using System;
using System.Collections.Generic;
using UnityEngine;

//https://docs.unity3d.com/ScriptReference/HeaderAttribute.html
public class Sloop : MonoBehaviour, Sea.IMAShip
{
    void Awake() { NumOfCannons = UnityEngine.Random.Range(1, 7); }

    [Header(nameof(Hull))]
    public Hull Hull;
    [Space(10)]



    [Header(nameof(Cannons))]
    public Cannon[] Cannons;

    [HideInInspector]
    public int NumOfCannons
    {
        get => _numOfCannons < 1 ? _numOfCannons = NumOfCannons = UnityEngine.Random.Range(1, 7) : _numOfCannons;
        set
        {
            if (value < 1 || value > 6) throw new ArgumentOutOfRangeException(value.ToString());
            _numOfCannons = value;
            for (int i = 0; i < Cannons.Length; i++)
                Cannons[i].gameObject.SetActive(i < value);
        }
    }
    private int _numOfCannons = 0;



    [Header(nameof(Rig))]
    public Rig SloopRig;
    public Rig CutterRig;
    public Rig Rig { get; private set; }

    [HideInInspector]
    public SailConfig Config
    {
        get => _config;
        set
        {
            // if (value == _config) return;
            _config = value;
            SloopRig.gameObject.SetActive(value == SailConfig.Sloop);
            CutterRig.gameObject.SetActive(value == SailConfig.Cutter); Rig = value switch
            {
                SailConfig.Sloop => SloopRig,
                _ => CutterRig,
            };
        }
    }
    private SailConfig _config;
    public enum SailConfig { Sloop, Cutter }
    public void RandomRig()
    {
        Config = UnityEngine.Random.value < .5f ? SailConfig.Sloop : SailConfig.Cutter;
    }

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
    public GameObject GO => gameObject;

    private ShipStats.ShipStats _shipStats;
    public ShipStats.ShipStats ShipStats => _shipStats ??= new(
        new ShipStats.HullStats(
            hullData: Data.Equipment.HullData.Sloop,
            timberType: Data.Inventory.MaterialsData.DataItem.Pine),

        new ShipStats.CannonStats(
            cannon: Data.Equipment.CannonData.Mynion,
            metal: Data.Inventory.MaterialsData.DataItem.CastIron),
        new ShipStats.RiggingStats(Data.Inventory.MaterialsData.DataItem.Hemp),
        numOfCannons: NumOfCannons
    );


}
