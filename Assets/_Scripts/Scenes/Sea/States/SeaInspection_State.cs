using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;

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
                Index = (Index - 1).Smod(WorldMapScene.Io.NPCShips.Count);
                // x = Mathf.Clamp(x + 1, 0.5f, Scene.Io.BoardSize - .5f);
                break;
            case Dir.Right:
                Index = (Index + 1).Smod(WorldMapScene.Io.NPCShips.Count);
                // x = Mathf.Clamp(x - 1, 0.5f, Scene.Io.BoardSize - .5f);
                break;
            case Dir.Up:
                // z = Mathf.Clamp(z - 1, 0.5f, Scene.Io.BoardSize - .5f);
                break;
            case Dir.Down:
                // z = Mathf.Clamp(z + 1, 0.5f, Scene.Io.BoardSize - .5f);
                break;
        }
        // Overlay.transform.position = new Vector3(x, .5f, z);

        // var localRegions = Scene.Io.Map.RegionsAdjacentTo(Scene.Io.Ship);
        // foreach (Region region in localRegions)
        foreach (NPCShip npc in WorldMapScene.Io.NPCShips)
        {
            if (npc.SceneObject.GO != null && WorldMapScene.Io.NPCShips.Count > 0 &&
                npc.SceneObject.GO.transform.GetInstanceID() == WorldMapScene.Io.NPCShips[Index].SceneObject.GO.transform.GetInstanceID())
            {
                Overlay.transform.position = npc.SceneObject.GO.transform.position;
                Flag.SetImageSprite(npc.Flag)
                    .SetImageColor(npc.FlagColor);
                break;
            }
        }

    }

    protected override void WestPressed()
    {
        SetState(
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
