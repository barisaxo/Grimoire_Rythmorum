using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterieToGameOverTransition_State : State
{

    public BatterieToGameOverTransition_State(BatterieScene scene)
    {
        Fade = true;
        Scene = scene;
    }

    readonly BatterieScene Scene;
    protected override void PrepareState(Action callback)
    {
        // Data.Manager.Io.Player.AdjustLevel()
        Audio.Ambience.FadeAndStop();
        Audio.BGMusic.FadeAndStop();
        // GameObject.Destroy(Scene.NMEGO);
        GameObject.Destroy(Scene.PlayerShip.GO);
        Scene.SelfDestruct();
        Sea.WorldMapScene.Io.SelfDestruct();
        base.PrepareState(callback);
    }
    protected override void EngageState()
    {
        Cam.Io.SelfDestruct();
        _ = Cam.Io;
        SetState(new CoveScene_State());
    }

}

public class SeaToGameOverTransition_State : State
{

    protected override void PrepareState(Action callback)
    {
        Audio.Ambience.FadeAndStop();
        Audio.BGMusic.FadeAndStop();
        Sea.WorldMapScene.Io.SelfDestruct();
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        Cam.Io.SelfDestruct();
        _ = Cam.Io;
        SetState(new MenuState(new Menus.MainMenu(DataManager, Audio)) { Fade = true });
    }

}
