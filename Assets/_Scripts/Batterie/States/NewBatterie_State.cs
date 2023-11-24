using System;
using SheetMusic;
using MusicTheory.Rhythms;
using Batterie;

public class NewBatterie_State : State
{
    public NewBatterie_State(BatteriePack pack)
    {
        Pack = pack;
    }

    BatteriePack Pack;

    protected override void PrepareState(Action callback)
    {

        base.PrepareState(callback);
    }

    protected override void EngageState()
    {

        var RhythmSpecs = new MusicTheory.Rhythms.RhythmSpecs()
        {
            Time = new MusicTheory.Rhythms.FourFour(),
            NumberOfMeasures = 4,
            SubDivisionTier = MusicTheory.Rhythms.SubDivisionTier.D1Only,
            HasTies = UnityEngine.Random.value > .5f,
            HasRests = UnityEngine.Random.value > .5f,
            HasTriplets = false,
            Tempo = 90
        };
        SetStateDirectly(new BatteryAndCadenceTestState(RhythmSpecs, Pack));
    }


}
