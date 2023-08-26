using System.Collections.Generic;
using MusicTheory.Rhythms;
using Batterie;

public static class BeatMapper
{
    public static MappedBeat[] MapBeats(this Note[] notes, float tempo)
    {
        double interval = (double)(60d / (double)(tempo * 12d));

        List<MappedBeat> beatMap = new()
        {
            //Adding this zero value note at the beginning seems to fix a problem of priming the first note.
            new MappedBeat(0, NoteFunction.Ignore)
        };

        for (int i = 0; i < notes.Length; i++)
        {
            beatMap.Add(new MappedBeat(interval * (int)notes[i].QuantizedRhythmicValue, notes.GetNoteFunction(i)));

            for (int n = 1; n < (int)notes[i].QuantizedRhythmicValue; n++)
            {
                beatMap.Add(new MappedBeat(0, NoteFunction.Ignore));
            }
        }
        return beatMap.ToArray();
    }

    private static NoteFunction GetNoteFunction(this Note[] notes, int i)
    {
        if (notes[i].Rest) return NoteFunction.Rest;
        if (notes[i].TiedFrom) return NoteFunction.Hold;
        return NoteFunction.Attack;
    }
}