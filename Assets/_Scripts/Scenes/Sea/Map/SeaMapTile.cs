using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMapTile
{
    public SeaMapTileType Type;
    public Vector3 Loc;
    public float ShipRotY;
    public float SwayAmp;
    public float SwayPeriod;

    public SeaMapTile(Vector3 pos)
    {
        Loc = pos;
        ShipRotY = Random.Range(-179f, 179f);
        SwayAmp = Random.Range(6f, 9f);
        SwayPeriod = Random.Range(.25f, .75f);
    }


}

public enum SeaMapTileType { OpenSea, Cave, Trader, CardGame, Rocks }