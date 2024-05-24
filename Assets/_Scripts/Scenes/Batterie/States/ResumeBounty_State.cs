using System;
using MusicTheory;
using Batterie;
using MusicTheory.Rhythms;

public class ResumeBounty_State : State
{
    // RhythmBars RhythmBars;
    // RhythmScriber RhythmScriber;

    readonly BatterieScene Scene;

    public ResumeBounty_State(BatterieScene scene)
    {
        Scene = scene;
    }

    protected override void PrepareState(Action callback)
    {
        // Scene.VolleysFired++;

        // RhythmBars = Data.GamePlay.CurrentLevel switch
        // {
        //     RegionalMode.Ionian => Data.GameplayData.GameDifficulty switch
        //     {
        //         GameDifficulty.Easy => new RhythmBars(1, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(70, 86))
        //         .SetSubDivision(SubDivisionTier.QuartersOnly),

        //         GameDifficulty.Medium => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(70, 91))
        //         .SetSubDivision(SubDivisionTier.QuartersOnly)
        //         .UseRandomRests()
        //         .UseRandomTies(),

        //         _ => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(75, 96))
        //         .SetSubDivision(SubDivisionTier.QuartersAndEighths)
        //         .UseRandomRests()
        //         .UseRandomTies(),
        //     },

        //     RegionalMode.Dorian => Data.GameplayData.GameDifficulty switch
        //     {
        //         GameDifficulty.Easy => new RhythmBars(2, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(70, 86))
        //         .SetSubDivision(SubDivisionTier.QuartersOnly),

        //         GameDifficulty.Medium => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(80, 101))
        //         .SetSubDivision(SubDivisionTier.QuartersOnly),

        //         _ => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(75, 96))
        //         .SetSubDivision(SubDivisionTier.QuartersAndEighths)
        //         .UseRandomRests()
        //         .UseRandomTies(),
        //     },

        //     RegionalMode.Phrygian => Data.GameplayData.GameDifficulty switch
        //     {
        //         GameDifficulty.Easy => new RhythmBars(2, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(80, 111))
        //         .SetSubDivision(SubDivisionTier.QuartersOnly)
        //         .UseRandomRests()
        //         .UseRandomTies(),

        //         GameDifficulty.Medium => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(90, 111))
        //         .SetSubDivision(SubDivisionTier.QuartersOnly)
        //         .UseRandomRests()
        //         .UseRandomTies(),

        //         _ => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(100, 121))
        //         .SetSubDivision(SubDivisionTier.EighthsOnly)
        //         .UseRandomRests()
        //         .UseRandomTies(),
        //     },

        //     RegionalMode.Lydian => Data.GameplayData.GameDifficulty switch
        //     {
        //         GameDifficulty.Easy => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(100, 131))
        //         .SetSubDivision(SubDivisionTier.QuartersOnly)
        //         .UseRandomRests()
        //         .UseRandomTies(),

        //         GameDifficulty.Medium => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(100, 121))
        //         .SetSubDivision(SubDivisionTier.QuartersAndEighths)
        //         .UseRandomRests()
        //         .UseRandomTies(),

        //         _ => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(100, 121))
        //         .SetSubDivision(SubDivisionTier.QuartersAndEighths)
        //         .UseRandomRests()
        //         .UseRandomTies(),
        //     },

        //     RegionalMode.MixoLydian => Data.GameplayData.GameDifficulty switch
        //     {
        //         GameDifficulty.Easy => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(75, 96))
        //         .SetSubDivision(SubDivisionTier.QuartersAndEighths)
        //         .UseRandomRests()
        //         .UseRandomTies(),

        //         GameDifficulty.Medium => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(100, 121))
        //         .SetSubDivision(SubDivisionTier.QuartersAndEighths)
        //         .UseRandomRests()
        //         .UseRandomTies(),

        //         _ => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(100, 121))
        //         .SetSubDivision(SubDivisionTier.QuartersAndEighths)
        //         .UseRandomRests()
        //         .UseRandomTies(),
        //     },

        //     RegionalMode.Aeolian => Data.GameplayData.GameDifficulty switch
        //     {
        //         GameDifficulty.Easy => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(90, 121))
        //         .SetSubDivision(SubDivisionTier.QuartersAndEighths)
        //         .UseRandomRests()
        //         .UseRandomTies(),

        //         GameDifficulty.Medium => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(100, 121))
        //         .SetSubDivision(SubDivisionTier.QuartersAndEighths)
        //         .UseRandomRests()
        //         .UseRandomTies(),

        //         _ => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(100, 121))
        //         .SetSubDivision(SubDivisionTier.QuartersAndEighths)
        //         .UseRandomRests()
        //         .UseRandomTies(),
        //     },

        //     _ => Data.GameplayData.GameDifficulty switch
        //     {//Locrian
        //         GameDifficulty.Easy => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(90, 121))
        //         .SetSubDivision(SubDivisionTier.EighthsOnly)
        //         .UseRandomRests()
        //         .UseRandomTies(),

        //         GameDifficulty.Medium => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(100, 121))
        //         .SetSubDivision(SubDivisionTier.QuartersAndEighths)
        //         .UseRandomRests()
        //         .UseRandomTies(),

        //         _ => new RhythmBars(4, DataManager.Io.GameplayData)
        //         .SetTempo(UnityEngine.Random.Range(100, 121))
        //         .SetSubDivision(SubDivisionTier.QuartersAndEighths)
        //         .UseRandomRests()
        //         .UseRandomTies(),
        //     },
        // };

        // RhythmScriber = new RhythmScriber().DrawRhythms(RhythmBars.ConstructRhythmBars(random: true));

        callback();
    }

    protected override void EngageState()
    {
        // SetState(new NewBatterie_State(Pack));

        SetState(new BatterieAndCadence_State(Scene));
    }

}
