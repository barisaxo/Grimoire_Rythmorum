using System;
using System.Collections;
using UnityEngine;
using MusicTheory.Rhythms;
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
        //var RhythmSpecs = new RhythmSpecs()
        //{
        //    Time = new FourFour(),
        //    NumberOfMeasures = 4,
        //    SubDivisionTier = SubDivisionTier.D1Only,
        //    HasTies = true,
        //    HasRests = false,
        //    HasTriplets = false,
        //    Tempo = 90
        //};

        //SetStateDirectly(new Batterie_State(RhythmSpecs));

        //SetStateDirectly(new MainMenu_State());
        SetStateDirectly(new VolumeMenu_State(new MainMenu_State()));

        //SetStateDirectly(new TestMusicSheet_State());
    }
}