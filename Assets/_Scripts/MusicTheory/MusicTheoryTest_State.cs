using System;
using UnityEngine;
using MusicTheory.Notes;
using MusicTheory.Notes.Arithmetic;
using MusicTheory.Intervals;
using MusicTheory.ScaleDegrees;
using MusicTheory.ScaleDegrees.Arithmetic;
using MusicTheory.Intervals.Arithmetic;

public class MusicTheoryTest_State : State
{
    protected override void PrepareState(Action callback)
    {

        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        _ = new MusicTheory.Scales.Major();
        TestAllIntervals();
        TestScaleDegreeToInterval();
    }

    private void TestScaleDegreeToInterval()
    {
        Debug.Log(IntervalEnum.Find((new MusicTheory.Intervals.Perfect(), new Unison())));

        for (int i = 0; i < ScaleDegreeEnum.Count; i++)
        {
            IScaleDegree degree = Enumeration.All<ScaleDegreeEnum>()[i].GetScaleDegree();
            Debug.Log(degree.Enum.Quality.Name + " " + degree.Enum.Degree.Name);
            Debug.Log(degree.AsInterval());
        }
    }

    private void TestAllIntervals()
    {
        int numOfKeys = Enumeration.Length<NoteEnum>();
        int numOfIntervals = Enumeration.Length<IntervalEnum>();

        for (int i = 0; i < numOfKeys; i++)
        {
            INote key = Enumeration.All<NoteEnum>()[i].GetNote();

            for (int ii = 0; ii < numOfIntervals; ii++)
            {
                IInterval interval = Enumeration.All<IntervalEnum>()[ii].GetInterval();

                INote newKey = key.GetNoteAbove(interval);

                Debug.Log("RESULT: " + key.Name + " +  " + interval.Name + " = " + newKey.Name);
            }
        }
    }

}
