
using System;

public class SeaToMenuTransition_State : State
{
    protected override void PrepareState(Action callback)
    {
        SeaScene.Io.SeaHUD.HUD.GO.SetActive(false);
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        UnityEngine.Debug.Log(nameof(SeaToMenuTransition_State));
        SetStateDirectly(
            new VolumeMenu_State(
                new MenuToSeaTransition_State()));
    }

    protected override void DisengageState()
    {
        SeaScene.Io.RockTheBoat.Rocking = false;
        SeaScene.Io.Swells.DisableSwells();
    }
}
