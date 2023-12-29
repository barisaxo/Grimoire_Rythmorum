
using System;
using Sea;
public class SeaToMenuTransition_State : State
{
    protected override void PrepareState(Action callback)
    {
        Scene.Io.HUD.Hud.GO.SetActive(false);
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
        Scene.Io.RockTheBoat.Rocking = false;
        Scene.Io.Swells.DisableSwells();
    }
}
