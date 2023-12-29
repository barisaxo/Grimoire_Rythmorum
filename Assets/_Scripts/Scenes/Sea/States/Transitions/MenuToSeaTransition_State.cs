using System;
using Sea;

public class MenuToSeaTransition_State : State
{
    protected override void PrepareState(Action callback)
    {
        Scene.Io.RockTheBoat.Rocking = true;
        Scene.Io.Swells.EnableSwells();
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        UnityEngine.Debug.Log(nameof(MenuToSeaTransition_State));
        SetStateDirectly(
            new CameraPan_State(
                new SeaScene_State(),
                Cam.StoredCamRot,
                Cam.StoredCamPos,
                3));
    }


}