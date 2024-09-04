using System;
using UnityEngine;
using System.Collections;
using Audio;

public class AuralSplashState : State
{
    protected override void PrepareState(Action callback)
    {
        _ = Splash;
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
    }

    protected override void DisengageState()
    {
        _splash?.SelfDestruct();
        _splashBG1.SelfDestruct();
        _splashBG2.SelfDestruct();
        _splashBG3.SelfDestruct();


        Audio.BGMusic.SetClip(Assets.BGMus1);
        Audio.BGMusic.Play(false);
        Audio.BGMusic.Loop = true;
    }

    float interval = .23f;
    int step = 1;
    IEnumerator FadeOutSFX()
    {
        while (AudioManager.Io.SFX.VolumeLevelSetting > .01f)
        {
            yield return new WaitForEndOfFrame();
            AudioManager.Io.SFX.VolumeLevelSetting -= Time.deltaTime * .075f;
            if ((interval -= Time.deltaTime) < 0)
                IncreaseBG();
        }
        AudioManager.Io.SFX.ImmediateStop();
        AudioManager.Io.SFX.VolumeLevelSetting = DataManager.Volume.GetLevel(new Datum.SoundFX());
        SetState(new MenuState(new Menus.MainMenu(Datum.Manager.Io, Audio)) { Fade = true });
    }

    Card _splash;
    Card Splash => _splash ??= new Card(nameof(Splash), null)
        .SetImageSprite(Assets.AuralTech)
        .SetImageSize(Cam.UIOrthoX * 2, Cam.UIOrthoY * 2)
        .SetCanvasSortingOrder(4)
        .SetImagePosition(0, 0);


    Card _splashBG1;
    Card SplashBG1 => _splashBG1 ??= new Card(nameof(SplashBG1), null)
        .SetImageSprite(Assets.AT1)
        .SetImageSize(Cam.UIOrthoX * 2, Cam.UIOrthoY * 2)
        .SetCanvasSortingOrder(3)
        .SetImagePosition(0, 0);


    Card _splashBG2;
    Card SplashBG2 => _splashBG2 ??= new Card(nameof(SplashBG2), null)
        .SetImageSprite(Assets.CLEAR)
        .SetImageSize(Cam.UIOrthoX * 2, Cam.UIOrthoY * 2)
        .SetCanvasSortingOrder(2)
        .SetImagePosition(0, 0);

    Card _splashBG3;
    Card SplashBG3 => _splashBG3 ??= new Card(nameof(SplashBG3), null)
        .SetImageSprite(Assets.CLEAR)
        .SetImageSize(Cam.UIOrthoX * 2, Cam.UIOrthoY * 2)
        .SetCanvasSortingOrder(1)
        .SetImagePosition(0, 0);

    void IncreaseBG()
    {
        interval = .24f;
        step++;
        step = step == 28 ? 2 : step;
        Debug.Log(step);

        SplashBG1.SetImageSprite(step switch
        {
            1 or 26 or 27 => Assets.AT1,
            2 or 3 => Assets.AT2,
            4 or 5 => Assets.AT3,
            6 or 7 => Assets.AT4,
            8 or 9 => Assets.AT5,
            10 or 11 => Assets.AT6,
            13 or 12 => Assets.AT7,
            15 or 14 => Assets.AT8,
            17 or 16 => Assets.AT9,
            19 or 18 => Assets.AT10,
            21 or 20 => Assets.AT11,
            23 or 22 => Assets.AT12,
            25 or 24 => Assets.AT13,
            _ => Assets.CLEAR,
        });

        SplashBG2.SetImageSprite(step switch
        {
            2 or 27 => Assets.AT1,
            3 or 4 => Assets.AT2,
            5 or 6 => Assets.AT3,
            7 or 8 => Assets.AT4,
            9 or 10 => Assets.AT5,
            11 or 12 => Assets.AT6,
            13 or 14 => Assets.AT7,
            15 or 16 => Assets.AT8,
            17 or 18 => Assets.AT9,
            19 or 20 => Assets.AT10,
            21 or 22 => Assets.AT11,
            23 or 24 => Assets.AT12,
            25 or 26 => Assets.AT13,

            _ => Assets.CLEAR,
        });

        SplashBG3.SetImageSprite(step switch
        {
            3 => Assets.AT1,
            5 => Assets.AT2,
            7 => Assets.AT3,
            9 => Assets.AT4,
            11 => Assets.AT5,
            13 => Assets.AT6,
            15 => Assets.AT7,
            17 => Assets.AT8,
            19 => Assets.AT9,
            21 => Assets.AT10,
            23 => Assets.AT11,
            25 => Assets.AT12,
            27 => Assets.AT13,

            _ => Assets.CLEAR,
        });
    }

}