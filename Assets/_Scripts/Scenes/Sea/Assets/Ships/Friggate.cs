using System;
using UnityEngine;

public class Frigate : MonoBehaviour, Sea.IMAShip
{
    [Header(nameof(Hull))]
    [SerializeField] private Hull Hull;
    [Space(10)]

    [Header(nameof(Cannons))]
    [SerializeField] private Cannon[] Cannons;

    [HideInInspector]
    public int NumOfCannons
    {
        get => _numOfCannons;
        set
        {
            if (value < 16 || value > 32) return;
            _numOfCannons = value;
            for (int i = 0; i < Cannons.Length; i++)
                Cannons[i].gameObject.SetActive(i < value);
        }
    }
    private int _numOfCannons = 16;

    [Header(nameof(Rig))]
    [SerializeField] private Rig FrigateRig;
    [SerializeField] private Rig BarqueRig;

    public Rig Rig { get; private set; }

    [HideInInspector]
    public SailConfig Config
    {
        get => _config;
        set
        {
            if (value == _config) return;
            _config = value;
            FrigateRig.gameObject.SetActive(value == SailConfig.Frigate);
            BarqueRig.gameObject.SetActive(value == SailConfig.Barque);
            Rig = value switch
            {
                SailConfig.Frigate => FrigateRig,
                SailConfig.Barque => BarqueRig,
                _ => throw new System.ArgumentOutOfRangeException(value.ToString())
            };
        }
    }
    private SailConfig _config;
    public enum SailConfig { Frigate, Barque }

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


}
