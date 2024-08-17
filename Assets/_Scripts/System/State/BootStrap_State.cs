using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Audio;

public class BootStrap_State : State
{
    private BootStrap_State() { }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AutoInitialize() =>
        new BootStrap_State().SetState(new BootStrap_State() { Fade = true });


    // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    // private static void SplashSound()
    // {

    // }


    protected override void PrepareState(Action callback)
    {
        // Application.targetFrameRate = 30;
        // QualitySettings.vSyncCount = 1;
        // _ = new FPSDisplay();
        _ = Cam.Io;
        AudioSettings.Reset(AudioSettings.GetConfiguration());
        AudioManager.Io.SFX.VolumeLevelSetting = .5f;
        AudioManager.Io.SFX.PlayOneShot(Assets.TuneUp);
        callback();
    }

    protected override void EngageState()
    {
        // _ = Fretboard.Io;
        // _ = Ukulele.Io;
        // SetState(new UkuleleState());

        FadeOutSFX().StartCoroutine();
        SetState(new MenuState(new Menus.MainMenu(Datum.Manager.Io, Audio)));
    }

    IEnumerator FadeOutSFX()
    {
        while (AudioManager.Io.SFX.VolumeLevelSetting > .01f)
        {
            yield return new WaitForEndOfFrame();
            AudioManager.Io.SFX.VolumeLevelSetting -= Time.deltaTime * .1f;
        }
        AudioManager.Io.SFX.ImmediateStop();
        AudioManager.Io.SFX.VolumeLevelSetting = DataManager.Volume.GetLevel(new Datum.SoundFX());
    }
}

public class ThrowState : State
{
    protected override void PrepareState(Action callback)
    {
        throw new System.NotImplementedException();
    }
}
