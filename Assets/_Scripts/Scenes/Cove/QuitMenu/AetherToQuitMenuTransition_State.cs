using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menus;

public class CoveToMenuTransition_State : State
{
    readonly IMenu SubMenu;
    readonly IHeaderMenu HeaderMenu;

    public CoveToMenuTransition_State(IMenu menu)
    {
        SubMenu = menu;
    }
    public CoveToMenuTransition_State(IHeaderMenu menu)
    {
        HeaderMenu = menu;
    }

    protected override void EngageState()
    {
        Audio.BGMusic.Pause();
        Audio.Ambience.Pause();

        CoveScene.Io.RockTheBoat.Rocking = false;

        SetState(new CameraPan_State(HeaderMenu is null ?
            new MenuTest_State(SubMenu) : new MenuTest_State(HeaderMenu as IHeaderMenu),
            new Vector3(-50, Cam.Io.Camera.transform.rotation.eulerAngles.y,
                Cam.Io.Camera.transform.rotation.eulerAngles.z),
                Cam.Io.Camera.transform.position,
                3));
    }
}
//  new CameraPan_State(
//                         new CoveScene_State(),
//                         Cam.StoredCamRot,
//                         Cam.StoredCamPos,
//                         3)),
