using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schooner : MonoBehaviour
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
            if (value < 4 || value > 16) return;
            _numOfCannons = value;
            for (int i = 0; i < Cannons.Length; i++)
                Cannons[i].gameObject.SetActive(i < value);
        }
    }
    private int _numOfCannons = 4;


    [Header(nameof(Rig))]
    public Rig SchoonerRig;
    public Rig TopRig;
    public Rig BrigRig;

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


}
