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
    public float SwayAmp;
    public float SwayPeriod;
    public NPCShipType ShipType;
    public Vector3Int StartNode;
    private int _patrolIndex = 0;
    public int PatrolIndex { get => _patrolIndex; set => _patrolIndex = value % PatrolPath.Length; }
    public Vector3Int[] PatrolPath;
    public bool PathDirection = true;
    public float MoveSpeed;
    public float MoveDelta = 0;
    public float StuckTimer = Random.Range(1.1f, 5f);
    public float StuckDelta;

    public GameObject GO;

    public NPCShip(Vector3Int start, Vector3Int[] path)
    {
        PosDelta = start;
        Pos = start;
        StartNode = start;

        PatrolPath = path;

        RotY = 90 * Random.Range(0, 4);
        SwayAmp = Random.Range(5f, 10f);
        SwayPeriod = Random.Range(.5f, 2f);
        MoveSpeed = Random.Range(.2f, .5f);
    }
}

public enum NPCShipType { Trade, Pirate, }