using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenuToSeaTransition_State : State
{
    protected override void PrepareState(Action callback)
    {
        SeaScene.Io.SeaHUD.HUD.GO.SetActive(false);

        Audio.BGMusic.Resume();
        Audio.Ambience.Resume();

        SeaScene.Io.RockTheBoat.Rocking = true;
        SeaScene.Io.Swells.EnableSwells();
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