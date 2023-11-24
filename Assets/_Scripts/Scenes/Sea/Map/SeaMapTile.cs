using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMapTile : ITile
{
    public SeaMapTileType Type;
    public Vector3Int Loc;
    public bool HasGramo;
    public float RotY = Random.Range(0, 360);
    public Vector2Int Coord => new(Loc.x, Loc.z);
    public bool IsOpen => Type == SeaMapTileType.OpenSea;

    public GameObject GO;

    public SeaMapTile(Vector3Int pos)
    {
        Loc = pos;
    }

    public void ClearGOs()
    {
        GO = null;
    }
}

public enum SeaMapTileType { OpenSea, Cave, Rocks }