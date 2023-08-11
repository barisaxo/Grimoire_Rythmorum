using System;
using UnityEngine;
using System.Threading.Tasks;

public class BootStrap_State : State
{
    private BootStrap_State() { }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        BootStrap_State state = new();
        state.SetStateDirectly(state);
    }

    protected override void PrepareState(Action callback)
    {
        _ = Cam.Io;
        AudioSettings.Reset(AudioSettings.GetConfiguration());
        //Audio.BGMusic.Play(isSerial: false);
        callback();
    }

    protected override void EngageState()
    {
        SetStateDirectly(new MainMenu_State());

        //SetStateDirectly(new TestMusicSheet_State());
    }
}