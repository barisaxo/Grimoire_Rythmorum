using System;
using System.Collections.Generic;
using MusicTheory.Rhythms;

public static class FishingForBeats
{
    public static Note[] BeatsFromTimeSig(MusicTheory.Rhythms.Time signature, int measures) => signature switch
    {
        ThreeFour => GetNotes(RhythmicValue.Quarter, 3 * measures),
        FourFour => GetNotes(RhythmicValue.Quarter, 4 * measures),
        FiveFour23 or FiveFour32 => GetNotes(RhythmicValue.Quarter, 5 * measures),
        SixEight => GetNotes(RhythmicValue.DotEighth, 2 * measures),
        NineEight => GetNotes(RhythmicValue.DotEighth, 3 * measures),
        TwelveEight => GetNotes(RhythmicValue.DotEighth, 4 * measures),
        _ => throw new ArgumentOutOfRangeException(signature.ToString())
    };


    public static Note[] GetNotes(RhythmicValue noteValue, int numOfNotes)
    {
        List<Note> notes = new();

        for (int i = 0; i < numOfNotes; i++)
            notes.Add(new() { QuantizedRhythmicValue = noteValue });

        return notes.ToArray();
    }




}