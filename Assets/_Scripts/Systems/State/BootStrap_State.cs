using System;
using System.Collections;
using UnityEngine;
using Musica.Rhythms;
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


        if (UnityEngine.Random.value > .5f) FadeToState(PuzzleSelector.WeightedRandomPuzzleState(Data.TheoryPuzzleData));
        else
        {
            var RhythmSpecs = new RhythmSpecs()
            {
                Time = new FourFour(),
                NumberOfMeasures = 4,
                SubDivisionTier = SubDivisionTier.D1Only,
                HasTies = UnityEngine.Random.value > .5f,
                HasRests = UnityEngine.Random.value > .5f,
                HasTriplets = false,
                Tempo = 90
            };
            FadeToState(new BatteryAndCadenceTestState(RhythmSpecs));
        }

        //FadeToState(new SeaSceneTest_State());
        // FadeToState(new Puzzle_State(new InvertedSeventhChordPuzzle(), PuzzleType.Aural));
        // SetStateDirectly(new BatteryAndCadenceTestState(RhythmSpecs));

        //SetStateDirectly(new Batterie_State(RhythmSpecs));

        //SetStateDirectly(new MainMenu_State());
        //SetStateDirectly(new VolumeMenu_State(new MainMenu_State()));

        //SetStateDirectly(new TestMusicSheet_State());

        //SetStateDirectly(new TheoryPuzzleState());
        //SetStateDirectly(new NewMuscopaState());
    }
}