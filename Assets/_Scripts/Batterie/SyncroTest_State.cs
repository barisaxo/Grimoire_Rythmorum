using System;
using UnityEngine;
using Batterie;

public class SyncroTest_State : State
{
    Synchronizer syncro;

    protected override void PrepareState(Action callback)
    {
        syncro = new(Musica.Rhythms.Quantizement.Quarter, 60);
        syncro.BeatEvent += BeatEvent;
        syncro.TickEvent += TimeEvent;

        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        syncro.KeepTime();
    }

    protected override void DisengageState()
    {
        syncro.BeatEvent -= BeatEvent;
        syncro.TickEvent -= TimeEvent;
    }

    void BeatEvent()
    {
        Debug.Log(nameof(BeatEvent) + " " + AudioSettings.dspTime + " " + Time.time + " " + (AudioSettings.dspTime - Time.time));
    }
    void TimeEvent()
    {
        //Debug.Log(nameof(TimeEvent) + " " + AudioSettings.dspTime + " " + Time.time + " " + (AudioSettings.dspTime - Time.time));
    }


}
