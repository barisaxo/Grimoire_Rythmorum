using System;
using UnityEngine;

public class BootStrap_State : State
{
    private BootStrap_State() { }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        BootStrap_State state = new();
        state.SetState(state);
    }

    protected override void PrepareState(Action callback)
    {
        _ = Cam.Io;
        AudioSettings.Reset(AudioSettings.GetConfiguration());
        callback();
    }

    protected override void EngageState()
    {

        SetState(new Menu_State(
            new Menus.Main.MainMenu(
                DataManager,
                Audio)
        ));
    }
}

public class ThrowState : State
{
    protected override void PrepareState(Action callback)
    {
        throw new System.NotImplementedException();
    }
}