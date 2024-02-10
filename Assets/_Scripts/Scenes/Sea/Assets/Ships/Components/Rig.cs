using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Rig : MonoBehaviour
{
    [Header(nameof(MainMast))]
    [SerializeField] private Mast _mainMast;
    public Mast MainMast => _mainMast;

    [SerializeField] private Sail _mainSail;
    public Sail MainSail => _mainSail;

    [SerializeField] private Sail _mainMastSquareSail;
    public Sail SquareSail => _mainMastSquareSail;

    [SerializeField] private Sail _mainMastTopSail;
    public Sail TopSail => _mainMastTopSail;


    [Header(nameof(ForeMast))]
    [SerializeField] private Mast _foreMast;
    public Mast ForeMast => _foreMast;

    [SerializeField] private Sail _foreSail;
    public Sail ForeSail => _foreSail;

    [SerializeField] private Sail _foreTopSail;
    public Sail ForeTopSail => _foreTopSail;


    [Header(nameof(MizzenMast))]
    [SerializeField] private Mast _mizzenMast;
    public Mast MizzenMast => _mizzenMast;

    [SerializeField] private Sail _mizzenSail;
    public Sail MizzenSail => _mizzenSail;

    [SerializeField] private Sail _mizzenSquareSail;
    public Sail MizzenSquareSail => _mizzenSquareSail;

    [SerializeField] private Sail _mizzenTopSail;
    public Sail MizzenTopSail => _mizzenTopSail;



    [Header(nameof(HeadSail))]
    [SerializeField] private Sail _headSail;
    public Sail HeadSail => _headSail;

    [SerializeField] private Sail _staySail;
    public Sail StaySail => _staySail;


    [Header(nameof(Rigging))]
    [SerializeField] private Rigging _rigging;
    public Rigging Rigging => _rigging;

    [SerializeField] private Flag _flag;
    public Flag Flag => _flag;

    private Sail[] _sails;
    public Sail[] Sails => _sails ??= new Sail[]{
        MainSail, SquareSail, TopSail,
        ForeSail,ForeTopSail,
        MizzenSail, MizzenTopSail, MizzenSquareSail,
        HeadSail, StaySail
    };

    private Mast[] _masts;
    public Mast[] Masts => _masts ??= new Mast[]{
        MizzenMast, MainMast, ForeMast,
    };
}
