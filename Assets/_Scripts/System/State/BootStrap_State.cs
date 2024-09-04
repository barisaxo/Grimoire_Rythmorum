using System;
using UnityEngine;
using System.Collections.Generic;

public class BootStrap_State : State
{
    private BootStrap_State() { }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AutoInitialize()
    {
        var bs = new BootStrap_State();
        bs.SetState(bs);
    }

    protected override void PrepareState(Action callback)
    {

#if UNITY_EDITOR
        Application.targetFrameRate = 30;
#endif

        _ = Cam.Io;
        // AudioSettings.Reset(AudioSettings.GetConfiguration());
        callback();
    }

    protected override void EngageState()
    {

        int x = 10;

        for (int i = 0; i < 200; i++)
        {
            x *= 2;
            Debug.Log(x);
        }
        // _ = Fretboard.Io;
        // _ = Ukulele.Io;
        // SetState(new UkuleleState());
        // SetState(new MusicTheoryTest_State());
        // SetState(new Muscopa.NewMuscopaState(new MenuState(new Menus.MainMenu(Datum.Manager.Io, Audio)) { Fade = true }, true));

#if UNITY_EDITOR
        SetState(new MenuState(new Menus.MainMenu(Datum.Manager.Io, Audio)) { Fade = true });
#else
                        SetState(new BlueToothWarningState());
#endif

    }


}

public class ThrowState : State
{
    protected override void PrepareState(Action callback)
    {
        throw new System.NotImplementedException();
    }
}
