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
    public static Vector3 CameraPos = new(WorldMapScene.Io.Board.Center(), 16, 16);
    public static Quaternion CameraRotQuat = Quaternion.Euler(new Vector3(65, 180, 0));
    public static Vector3 CameraRot = new(65, 180, 0);
    readonly State SubsequentState;

    public SeaInspection_State(State subsequentState)
    {
        SubsequentState = subsequentState;
    }

    protected override void PrepareState(Action callback)
    {
        Overlay = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Overlay.transform.position = Vector3.one * WorldMapScene.Io.Board.Center();
        MR = Overlay.GetComponent<MeshRenderer>();
        MR.material = Assets.Overlay_Mat;
        // MR.material.color = Color.green;
        Cam.Io.Camera.transform.SetParent(null);
        Cam.Io.Camera.transform.SetPositionAndRotation(CameraPos, CameraRotQuat);
        DirectionPressed(Dir.Right);
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
        Info.SelfDestruct();
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
        if (WorldMapScene.Io.NPCShips.Count > 0)
            foreach (NPCShip npc in WorldMapScene.Io.NPCShips)
            {
                if (npc.SceneObject.GO != null &&
                    npc.SceneObject.GO.transform.GetInstanceID() == WorldMapScene.Io.NPCShips[Index].SceneObject.GO.transform.GetInstanceID())
                {
                    Overlay.transform.position = npc.SceneObject.GO.transform.position;
                    Flag.SetImageSprite(npc.Flag)
                        .SetImageColor(npc.FlagColor);
                    Info.SetTextString(ShipInfo(npc));
                    break;
                }
            }
        else Overlay.SetActive(false);

    }

    protected override void WestPressed()
    {
        SetState(
            new CameraPan_State(
                SubsequentState,
                Cam.StoredCamRot,
                Cam.StoredCamPos,
                9.5f
            ));
    }

    Card _flag;
    Card Flag => _flag ??= new Card(nameof(Flag), null)
        .SetImageSize(Vector3.one * 2f)
        .SetImagePosition(new Vector3(-Cam.UIOrthoX + 1.65f, Cam.UIOrthoY - 1f))
        .SetImageRectPivot(0, .5f)
        ;

    Card _info;
    Card Info => _info ??= new Card(nameof(Info), null)
        .SetTMPPosition(new Vector3(-Cam.UIOrthoX + 1.65f, Cam.UIOrthoY - 2f))
        // .SetTMPSize(new Vector2(4, 3))
        .AllowWordWrap(false)
        ;

    string ShipInfo(NPCShip npc)
    {
        string s = string.Empty;
        s += npc.RegionalMode + " " + npc.ShipType + " Ship";
        s += "\n" + DataManager.StandingData.GetDisplayLevel(npc.Standing) + " Standings";
        s += "\nRigging: " + npc.ShipPrefab._rig.name.StartCase();
        s += "\nHull Strength: " + npc.ShipStats.HullStrength;
        s += "\nArmament: " + npc.ShipStats.NumOfCannons + " " + npc.ShipStats.CannonStats.Metal.Name.StartCase() + " " + npc.ShipStats.CannonStats.Cannon.Name +
                " Cannon" + (npc.ShipStats.NumOfCannons > 1 ? "s" : "");//+ npc.ShipStats.NumOfCannons;
        s += "\nDamage Potential: " + npc.ShipStats.VolleyDamage;
        s += "\nThreat Level: " + ThreatLevel(npc);
        return s;
    }

    string ThreatLevel(NPCShip npc)
    {
        float threat = (float)((float)npc.ShipStats.VolleyDamage / (float)DataManager.ActiveShip.GetLevel(new Data.Damage()));
        threat += (float)((float)npc.ShipStats.HullStrength / (float)DataManager.ActiveShip.GetLevel(new Data.CurrentHitPoints()));
        int t = (int)(threat * 50f);
        return threat switch
        {
            < .75f => t + ", Very Low",
            < 1f => t + ", Low",
            < 1.5f => t + ", Moderate",
            < 2f => t + ", High",
            < 2.5f => t + ", Very High",
            >= 2.5f => t + ", Extreme",
            _ => throw new Exception()
        };


    }

}
