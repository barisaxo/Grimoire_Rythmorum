using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;

public class QuitMenuToSeaTransition_State : State
{
    protected override void PrepareState(Action callback)
    {
        Scene.Io.HUD.Hud.GO.SetActive(false);

        Audio.BGMusic.Resume();
        Audio.Ambience.Resume();

        Scene.Io.RockTheBoat.Rocking = true;
        Scene.Io.Swells.EnableSwells();
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        SetStateDirectly(
            new CameraPan_State(
                new SeaScene_State(),
                    pan: Cam.StoredCamRot,
                    strafe: Cam.StoredCamPos,
                    speed: 3));
    }
}