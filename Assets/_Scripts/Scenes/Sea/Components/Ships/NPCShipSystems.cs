using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;

public static class ShipSystems
{

    public static float Rotate(this PlayerShip ship, float x)
    {
        if (Mathf.Approximately(x, 0)) return 0;
        ship.RotY += x * Time.fixedDeltaTime * ship.RotateSpeed;
        ship.Parent.transform.rotation = Quaternion.Euler(new Vector3(ship.Parent.transform.eulerAngles.x, ship.RotY, ship.Parent.transform.eulerAngles.z));
        return ship.RotY;
        // ship.Parent.transform.Rotate(new Vector3(0, x * Time.fixedDeltaTime * ship.RotateSpeed, 0));
    }



}
