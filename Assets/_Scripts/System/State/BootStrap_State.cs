using System;
using UnityEngine;

public class BootStrap_State : State
{
    private BootStrap_State() { Fader.Screen.color = Color.black; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AutoInitialize()
    {
        _ = new FPSDisplay();
        BootStrap_State state = new();
        state.SetState(new BootStrap_State() { Fade = true });
    }

    protected override void PrepareState(Action callback)
    {
        _ = Cam.Io;
        AudioSettings.Reset(AudioSettings.GetConfiguration());
        callback();
    }

    protected override void EngageState()
    {
        SetState(new MenuState(new Menus.Two.MainMenu(Data.Two.Manager.Io, Audio)));
    }

    protected override void DisengageState()
    {
        Fader.SelfDestruct();
    }

    readonly ScreenFader Fader = new();
}

public class ThrowState : State
{
    protected override void PrepareState(Action callback)
    {
        throw new System.NotImplementedException();
    }
}