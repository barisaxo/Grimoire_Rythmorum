using System;
using Sea;
using Menus;

public class SeaToNewMenuTransition_State : State
{
    readonly IMenu SubMenu;
    readonly IHeaderMenu HeaderMenu;

    public SeaToNewMenuTransition_State(IMenu menu)
    {
        SubMenu = menu;
    }
    public SeaToNewMenuTransition_State(IHeaderMenu menu)
    {
        HeaderMenu = menu;
    }

    protected override void PrepareState(Action callback)
    {
        Audio.Ambience.Pause();
        Audio.BGMusic.Pause();
        Audio.SFX.PlayOneShot(Assets.AlertHalfDim);
        Sea.WorldMapScene.Io.HUD.Disable();
        Sea.WorldMapScene.Io.Ship.ConfirmPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.Ship.AttackPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.MiniMap.Card.GO.SetActive(false);
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        // SetState(new MenuTest_State(Menu));

        SetState(new CameraPan_State(HeaderMenu is null ?
            new MenuState(SubMenu) : new MenuState(HeaderMenu as IHeaderMenu),
            new UnityEngine.Vector3(-50, Cam.Io.Camera.transform.rotation.eulerAngles.y,
                Cam.Io.Camera.transform.rotation.eulerAngles.z),
                Cam.Io.Camera.transform.position,
                4.5f));
    }

    protected override void DisengageState()
    {
        WorldMapScene.Io.RockTheBoat.Rocking = false;
        WorldMapScene.Io.Board.Swells.DisableSwells();
    }
}
