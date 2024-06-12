using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryToGameOverTransition_State : State
{

    public BatteryToGameOverTransition_State(BatterieScene scene)
    {
        Fade = true;
        Scene = scene;
    }

    readonly BatterieScene Scene;
    protected override void PrepareState(Action callback)
    {
        // Data.Manager.Io.Player.AdjustLevel()
        Audio.Ambience.Stop();
        Audio.BGMusic.Stop();
        GameObject.Destroy(Scene.NMEGO);
        GameObject.Destroy(Scene.PlayerShip.GO);
        Scene.SelfDestruct();
        Sea.WorldMapScene.Io.SelfDestruct();
        base.PrepareState(callback);
    }
    protected override void EngageState()
    {
        SetState(new MenuState(new Menus.MainMenu(DataManager, Audio)));
    }

}

public class SeaToGameOverTransition_State : State
{

    protected override void PrepareState(Action callback)
    {
        Audio.Ambience.Stop();
        Audio.BGMusic.Stop();
        Sea.WorldMapScene.Io.SelfDestruct();
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        SetState(new MenuState(new Menus.MainMenu(DataManager, Audio)));
    }

}
