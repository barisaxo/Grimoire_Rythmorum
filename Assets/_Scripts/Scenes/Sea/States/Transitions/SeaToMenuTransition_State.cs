using System;
using Sea;
using Menus;
using Menus.Options;
using Menus.Inventory;

public class SeaToMenuTransition_State : State
{
    readonly IMenu SubMenu;
    readonly IHeaderMenu HeaderMenu;

    public SeaToMenuTransition_State(IMenu menu)
    {
        SubMenu = menu;
    }
    public SeaToMenuTransition_State(IHeaderMenu menu)
    {
        HeaderMenu = menu;
    }

    protected override void PrepareState(Action callback)
    {
        WorldMapScene.Io.HUD.Hud.GO.SetActive(false);
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        // SetState(new MenuTest_State(Menu));

        SetState(new CameraPan_State(HeaderMenu is null ?
            new MenuTest_State(SubMenu) : new MenuTest_State(HeaderMenu as IHeaderMenu),
            new UnityEngine.Vector3(-50, Cam.Io.Camera.transform.rotation.eulerAngles.y,
                Cam.Io.Camera.transform.rotation.eulerAngles.z),
                Cam.Io.Camera.transform.position,
                3));
    }

    protected override void DisengageState()
    {
        WorldMapScene.Io.RockTheBoat.Rocking = false;
        WorldMapScene.Io.Board.Swells.DisableSwells();
    }
}
