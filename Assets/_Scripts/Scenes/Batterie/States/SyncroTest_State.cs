using System;
using UnityEngine;
using Batterie;
using Rhythm;

public class SynchroTest_State : State
{
    Synchronizer synchro;

    protected override void PrepareState(Action callback)
    {
        synchro = new(MusicTheory.Rhythms.Quantizement.Quarter, 60);
        synchro.BeatEvent += BeatEvent;
        synchro.TickEvent += TimeEvent;

        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        synchro.KeepTime();
    }

    protected override void DisengageState()
    {
        synchro.BeatEvent -= BeatEvent;
        synchro.TickEvent -= TimeEvent;
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

public interface IAttackVFX
{
    void AttackVFX();
    AttackTypes AttackType { get; }
    enum AttackTypes { type1, type2 };

}
public class SwipeVFX : IAttackVFX
{
    public void AttackVFX() { }
    public IAttackVFX.AttackTypes AttackType { get; } = IAttackVFX.AttackTypes.type1;
}