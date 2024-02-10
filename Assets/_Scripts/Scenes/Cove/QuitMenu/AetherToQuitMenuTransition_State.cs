using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherToQuitMenuTransition_State : State
{
    protected override void EngageState()
    {
        Audio.BGMusic.Pause();
        Audio.Ambience.Pause();

        CoveScene.Io.RockTheBoat.Rocking = false;

        SetStateDirectly(
            new CameraPan_State(
                new QuitAetherMenu_State(
                    new CameraPan_State(
                        new CoveScene_State(),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3)),
                new Vector3(-50, Cam.Io.Camera.transform.rotation.eulerAngles.y, Cam.Io.Camera.transform.rotation.eulerAngles.z),
                Cam.Io.Camera.transform.position,
                3));
    }
}
