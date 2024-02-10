using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://docs.unity3d.com/ScriptReference/HeaderAttribute.html
public class Sloop : MonoBehaviour
{
    [Header(nameof(Hull))]
    public Hull Hull;
    [Space(10)]



    [Header(nameof(Cannons))]
    public Cannon[] Cannons;

    [HideInInspector]
    public int NumOfCannons
    {
        get => _numOfCannons;
        set
        {
            if (value < 1 || value > 6) return;
            _numOfCannons = value;
            for (int i = 0; i < Cannons.Length; i++)
                Cannons[i].gameObject.SetActive(i < value);
        }
    }
    private int _numOfCannons = 1;



    [Header(nameof(Rig))]
    public Rig SloopRig;
    public Rig CutterRig;

    [HideInInspector]
    public SailConfig Config
    {
        get => _config;
        set
        {
            if (value == _config) return;
            _config = value;
            SloopRig.gameObject.SetActive(value == SailConfig.Sloop);
            CutterRig.gameObject.SetActive(value == SailConfig.Cutter);
        }
    }
    private SailConfig _config;
    public enum SailConfig { Sloop, Cutter }




}
