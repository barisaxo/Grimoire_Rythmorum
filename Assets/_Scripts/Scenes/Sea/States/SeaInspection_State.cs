using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaInspection_State : State
{

    GameObject Overlay;
    MeshRenderer MR;
    int Index = 0;
    protected override void PrepareState(Action callback)
    {
        Overlay = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Overlay.transform.position = new Vector3(5.5f, .5f, 5.5f);
        MR = Overlay.GetComponent<MeshRenderer>();
        MR.material = Assets.Overlay_Mat;
        // MR.material.color = Color.green;
        Cam.Io.Camera.transform.SetParent(null);
        Cam.Io.Camera.transform.SetPositionAndRotation(new Vector3(5.5f, 5, 13), Quaternion.Euler(new Vector3(45, 180, 0)));
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        base.EngageState();
    }

    protected override void DisengageState()
    {
        GameObject.Destroy(Overlay);
        Flag.SelfDestruct();
    }

    protected override void DirectionPressed(Dir dir)
    {
        // float x = (int)Overlay.transform.position.x;
        // float z = (int)Overlay.transform.position.z;
        // float y = Overlay.transform.position.y;
        switch (dir)
        {
            case Dir.Left:
                Index = (Index - 1).SignedMod(SeaScene.Io.UsedShips.Count);
                // x = Mathf.Clamp(x + 1, 0.5f, SeaScene.Io.BoardSize - .5f);
                break;
            case Dir.Right:
                Index = (Index + 1).SignedMod(SeaScene.Io.UsedShips.Count);
                // x = Mathf.Clamp(x - 1, 0.5f, SeaScene.Io.BoardSize - .5f);
                break;
            case Dir.Up:
                // z = Mathf.Clamp(z - 1, 0.5f, SeaScene.Io.BoardSize - .5f);
                break;
            case Dir.Down:
                // z = Mathf.Clamp(z + 1, 0.5f, SeaScene.Io.BoardSize - .5f);
                break;
        }
        // Overlay.transform.position = new Vector3(x, .5f, z);

        foreach (NPCShip ship in SeaScene.Io.NPCShips)
        {
            if (ship.GO != null && SeaScene.Io.UsedShips.Count > 0 &&
                ship.GO.transform.GetInstanceID() == SeaScene.Io.UsedShips[Index].transform.GetInstanceID())
            {
                Overlay.transform.position = ship.GO.transform.position;
                Flag.SetImageSprite(ship.Flag)
                    .SetImageColor(ship.FlagColor);
                break;
            }
        }

    }

    protected override void WestPressed()
    {
        SetStateDirectly(
            new CameraPan_State(
                new SeaScene_State(),
                Cam.StoredCamRot,
                Cam.StoredCamPos,
                3.5f
            ));
    }

    Card _flag;
    Card Flag => _flag ??= new Card(nameof(Flag), null)
        .SetImageSize(Vector3.one * 2f)
        .SetImagePosition(new Vector3(-Cam.UIOrthoX + .65f, Cam.UIOrthoY - 1f))
        .SetImageRectPivot(0, .5f)
        ;
}
