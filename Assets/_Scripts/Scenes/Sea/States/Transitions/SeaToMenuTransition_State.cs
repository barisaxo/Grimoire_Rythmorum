using System;
using Sea;

public class SeaToMenuTransition_State : State
{
    protected override void PrepareState(Action callback)
    {
        WorldMapScene.Io.HUD.Hud.GO.SetActive(false);
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        SetStateDirectly(
            new VolumeMenu_State(
                new MenuToSeaTransition_State()));
    }

    protected override void DisengageState()
    {
        WorldMapScene.Io.RockTheBoat.Rocking = false;
        WorldMapScene.Io.Board.Swells.DisableSwells();
    }
}
