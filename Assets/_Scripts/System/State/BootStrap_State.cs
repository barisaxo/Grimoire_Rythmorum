using System;
using UnityEngine;

public class BootStrap_State : State
{
    private BootStrap_State() { }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AutoInitialize() =>
        new BootStrap_State().SetState(new BootStrap_State() { Fade = true });

    protected override void PrepareState(Action callback)
    {
        // _ = new FPSDisplay();
        _ = Cam.Io;
        AudioSettings.Reset(AudioSettings.GetConfiguration());
        callback();
    }

    protected override void EngageState()
    {
        SetState(new MenuState(new Menus.MainMenu(Data.Manager.Io, Audio)));
    }
}

public class ThrowState : State
{
    protected override void PrepareState(Action callback)
    {
        throw new System.NotImplementedException();
    }
}

