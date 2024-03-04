using System;
using System.Collections;
using UnityEngine;
using Data.Inventory;
// using MusicTheory.Rhythms;
// using MusicTheory.Scales;
// using MusicTheory.Arithmetic;
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
        // Data.starChartsData.IncreaseLevel(StarChartsData.DataItem.NotesT);
        // SetStateDirectly(new MainMenu_State());
        SetState(new MenuTest_State(
            new Menus.Main.MainMenu(
                Data,
                Audio)
        ));

        // FadeToState(new SeaScene_State());
        // SetStateDirectly(new NewAngling_State());

        // if (UnityEngine.Random.value > .5f) FadeToState(PuzzleSelector.WeightedRandomPuzzleState(Data.TheoryPuzzleData));
        // else
        // {
        //     var RhythmSpecs = new RhythmSpecs()
        //     {
        //         Time = new FourFour(),
        //         NumberOfMeasures = 4,
        //         SubDivisionTier = SubDivisionTier.D1Only,
        //         HasTies = UnityEngine.Random.value > .5f,
        //         HasRests = UnityEngine.Random.value > .5f,
        //         HasTriplets = false,
        //         Tempo = 90
        //     };
        //     FadeToState(new BatteryAndCadenceTestState(RhythmSpecs));
        // }

        // FadeToState(new SeaSceneTest_State());
        // FadeToState(new Puzzle_State(new ModePuzzle(new MusicTheory.Scales.Blues(), new MusicTheory.Modes.MajorBlues()), PuzzleType.Aural));
        // SetStateDirectly(new BatteryAndCadenceTestState(RhythmSpecs));
        //SetStateDirectly(new Batterie_State(RhythmSpecs));
        //SetStateDirectly(new VolumeMenu_State(new MainMenu_State()));
        //SetStateDirectly(new TestMusicSheet_State());
        // SetStateDirectly(new TheoryPuzzleState());
        // SetStateDirectly(new NewMuscopaState());

    }
}


public class ThrowState : State
{
    protected override void PrepareState(Action callback)
    {
        throw new System.NotImplementedException();
        base.PrepareState(callback);
    }
}