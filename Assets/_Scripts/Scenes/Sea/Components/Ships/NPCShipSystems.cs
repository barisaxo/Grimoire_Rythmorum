using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;

public static class ShipSystems
{
    // public static bool IsTileOccupied(this Scene scene, Vector2Int value)
    // {
    //     foreach (var ship in scene.NPC.Ships) if (ship.LocalCoords == value) return true;
    //     return false;
    // }


    public static void Rotate(this PlayerShip ship, float x)
    {
        if (!Mathf.Approximately(x, 0)) ship.Parent.transform.Rotate(new Vector3(0, x * Time.fixedDeltaTime * ship.RotateSpeed, 0));
    }



}
