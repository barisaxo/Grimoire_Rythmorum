using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menus.Two;

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
            new MenuState(SubMenu) : new MenuState(HeaderMenu as IHeaderMenu),
            new Vector3(-50, Cam.Io.Camera.transform.rotation.eulerAngles.y,
                Cam.Io.Camera.transform.rotation.eulerAngles.z),
                Cam.Io.Camera.transform.position,
                5));
    }
}
public class CoveToAnglingTransition_State : State
{
    readonly State SubsequentState;

    public CoveToAnglingTransition_State(State subsequentState)
    {
        SubsequentState = subsequentState;
    }

    protected override void EngageState()
    {
        Audio.BGMusic.Pause();
        Audio.Ambience.Pause();

        CoveScene.Io.RockTheBoat.Rocking = false;

        SetState(new CameraPan_State(
            subsequentState: SubsequentState,
            pan: new Vector3(-50, Cam.Io.Camera.transform.rotation.eulerAngles.y,
                        Cam.Io.Camera.transform.rotation.eulerAngles.z),
            strafe: Cam.Io.Camera.transform.position,
            speed: 5));
    }
}
