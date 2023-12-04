using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaToMenuTransition_State : State
{
    protected override void EngageState()
    {
        SeaScene.Io.RockTheBoat.Rocking = false;
        SeaScene.Io.Swells.DisableSwells();
        SetStateDirectly(
            new CameraPan_State(
                new VolumeMenu_State(
                    new CameraPan_State(
                        new SeaScene_State(),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3)),
                new Vector3(-50, Cam.Io.Camera.transform.rotation.eulerAngles.y, Cam.Io.Camera.transform.rotation.eulerAngles.z),
                Cam.Io.Camera.transform.position,
                3));
    }
}
