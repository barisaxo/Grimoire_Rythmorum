using System;
using Sea;

public class QuitMenuToSeaTransition_State : State
{
    protected override void PrepareState(Action callback)
    {
        WorldMapScene.Io.HUD.Hud.GO.SetActive(false);

        Audio.BGMusic.Resume();
        Audio.Ambience.Resume();

        WorldMapScene.Io.RockTheBoat.Rocking = true;
        WorldMapScene.Io.Board.Swells.EnableSwells();
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        SetStateDirectly(
            new CameraPan_State(
                new SeaScene_State(),
                    pan: Cam.StoredCamRot,
                    strafe: Cam.StoredCamPos,
                    speed: 3));
    }
}