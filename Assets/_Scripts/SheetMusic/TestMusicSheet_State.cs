using System;
using UnityEngine;
using SheetMusic;
using MusicTheory.Rhythms;

public class TestMusicSheet_State : State
{
    protected override void PrepareState(Action callback)
    {
        MusicSheet ms = new()
        {
            RhythmSpecs = new()
            {
                Time = new SevenEight34(),
                NumberOfMeasures = 4,
                SubDivisionTier = SubDivisionTier.BeatOnly,
                HasTies = true,
                HasRests = true,
                HasTriplets = false,
            },
        };

        ms.RhythmSpecs.Time.GenerateRhythmCells(ms);
        ms.GetNotes();

        for (int m = 0; m < ms.Measures.Length; m++)
        {
            for (int c = 0; c < ms.Measures[m].Cells.Length; c++)
            {
                Debug.Log("measure " + m + ", cell " + c + " " + ms.Measures[m].Cells[c].Shape + ", " + ms.Measures[m].Cells[c].Quantizement +
                    ", has rest " + ms.Measures[m].Cells[c].Rest +
                    ", is tied from previous cell " + ms.Measures[m].Cells[c].TiedFrom +
                    ", ties to next cell " + ms.Measures[m].Cells[c].TiedTo);
            }
        }

        ms.DrawRhythms();
        base.PrepareState(callback);
    }
}
