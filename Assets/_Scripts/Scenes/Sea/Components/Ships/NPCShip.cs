using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShip
{
    // public Vector2 PosDelta;
    public Vector2 Pos;
    public Vector2Int LocalCoords => new(Mathf.FloorToInt(Pos.x), Mathf.FloorToInt(Pos.y));
    public Vector2Int GlobalCoords => LocalCoords + RegionalCoordOffset;
    public Vector2 GlobalPos => Pos + RegionalCoordOffset;
    public Vector2Int RegionalCoordOffset;

    public float RotY;
    public float YTurn;
    public float NewRotY;

    public (float amp, float period, float offset) Sway;

    public NPCShipType ShipType;
    public Vector2Int StartNode;

    public Vector2Int[] PatrolPath;
    public bool PathDirection;
    private int _patrolIndex = 0;
    public int PatrolIndex
    {
        get => _patrolIndex;
        set => _patrolIndex = value.Smod(PatrolPath.Length);
    }

    public float ThreatRange = Random.Range(1.1f, 4f);

    public float MoveSpeed;
    public float MoveDelta = 0;

    public float StuckTimer = Random.Range(1.1f, 5f);
    public float StuckDelta;

    public const int HideTime = 300;
    public bool Hiding;
    public float HideTimer;

    public GameObject GO;

    public MusicTheory.RegionalMode RegionalMode = (MusicTheory.RegionalMode)Random.Range(0, 7);
    public AudioClip RegionalSound => Assets.GetScaleChordClip(RegionalMode);
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

    public NPCShip(Vector2Int[] path, Vector2Int regionalCoordOffset)
    {
        PathDirection = Random.value < .5f;
        // PosDelta = start;
        int i = Random.Range(0, path.Length);
        Pos = path[i];
        StartNode = path[i];
        PatrolPath = path;
        PatrolIndex = i;

        RotY = 90 * Random.Range(0, 4);
        Sway.amp = Random.Range(.01f, .1f);
        Sway.period = Random.Range(.5f, 2f);
        Sway.offset = Random.Range(-10f, 10f);
        MoveSpeed = Random.Range(.1f, .15f);

        RegionalCoordOffset = regionalCoordOffset;
    }
}

public enum NPCShipType { Trade, Pirate, }
//todo privateer, exploration vessel, Whaling Ship, Fishing boat, Packet Ship