using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShip
{
    public Vector3 PosDelta;
    public Vector3 Pos;

    public Vector3Int Coords => new Vector3Int(Mathf.FloorToInt(Pos.x), 0, Mathf.FloorToInt(Pos.z));
    // private float _rotY;
    public float RotY;
    public float YTurn;
    public float NewRotY;
    public (float amp, float period, float offset) Sway;
    public NPCShipType ShipType;
    public Vector3Int StartNode;
    private int _patrolIndex = 0;
    public int PatrolIndex
    {
        get => _patrolIndex;
        set => _patrolIndex = value.SignedMod(PatrolPath.Length);
    }
    public float ThreatRange = Random.Range(1.1f, 4f);
    public Vector3Int[] PatrolPath;
    public bool PathDirection = true;
    public float MoveSpeed;
    public float MoveDelta = 0;
    public float StuckTimer = Random.Range(1.1f, 5f);
    public float StuckDelta;
    public MusicTheory.RegionalMode RegionalMode = (MusicTheory.RegionalMode)Random.Range(0, 7);
    public string Name => "[" + RegionalMode.ToString() + " trade ship]:\n";
    public Color FlagColor = Assets.RandomColor;
    public Sprite Flag => ShipType == NPCShipType.Trade ? RegionalMode switch
    {
        MusicTheory.RegionalMode.Aeolian => Assets.AeolianFlag,
        MusicTheory.RegionalMode.Dorian => Assets.DorianFlag,
        MusicTheory.RegionalMode.Ionian => Assets.IonianFlag,
        MusicTheory.RegionalMode.Lydian => Assets.LydianFlag,
        MusicTheory.RegionalMode.Phrygian => Assets.PhrygianFlag,
        MusicTheory.RegionalMode.MixoLydian => Assets.MixoLydianFlagFlag,
        _ => Assets.LocrianFlag,
    } : Assets.PirateFlag;
    public AudioClip RegionalSound => Assets.GetScaleChordClip(RegionalMode);

    public GameObject GO;

    public NPCShip(Vector3Int start, Vector3Int[] path)
    {
        start.y = 0;
        PosDelta = start;
        Pos = start;
        StartNode = start;
        Debug.Log(Pos);
        PatrolPath = path;

        RotY = 90 * Random.Range(0, 4);
        Sway.amp = Random.Range(.01f, .1f);
        Sway.period = Random.Range(.5f, 2f);
        Sway.offset = Random.Range(-10f, 10f);
        MoveSpeed = Random.Range(.2f, .5f);
    }
}

public enum NPCShipType { Trade, Pirate, }