using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaToQuitMenuTransition_State : State
{
    protected override void EngageState()
    {
        Audio.BGMusic.Pause();
        Audio.Ambience.Pause();

        SeaScene.Io.RockTheBoat.Rocking = false;
        SeaScene.Io.Swells.DisableSwells();

        SetStateDirectly(
            new CameraPan_State(
                new QuitSeaMenu_State(
                    new CameraPan_State(
                        new SeaScene_State(),
                        pan: Cam.StoredCamRot,
                        strafe: Cam.StoredCamPos,
                        speed: 3)),
                new Vector3(-50, Cam.Io.Camera.transform.rotation.eulerAngles.y, Cam.Io.Camera.transform.rotation.eulerAngles.z),
                Cam.Io.Camera.transform.position,
                3));
    }
}
