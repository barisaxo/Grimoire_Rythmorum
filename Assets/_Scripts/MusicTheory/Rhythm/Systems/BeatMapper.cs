using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory;
using MusicTheory.Rhythms;
using Ritmo;

public static class BeatMapper
{

    public static void MapBeats(this RhythmSpecs specs, List<Note> notes)
    {
        List<MapBeat> mappedBeats = new() { new MapBeat(0, NoteFunction.Ignore) };

        for (int i = 0; i < notes.Count; i++)
        {
            //mappedBeats.Add(new MapBeat(notes[i].BeatLocation))
        }




        //Adding this zero value rest at the beginning seems to fix a problem of priming the first note.
        List<MappedBeat> beatMap = new()
        {
            new MappedBeat(0, NoteFunction.Ignore)
        };

        for (int i = 0; i < notes.Count; i++)
        {
            beatMap.Add(new MappedBeat((double)(60d / (double)(specs.Tempo * 12d)), notes.GetNoteFunction(i)));

            if (notes.GetNoteFunction(i) == NoteFunction.Rest)
            {
                for (int n = 1; n < (int)notes[i].QuantizedRhythmicValue; n++)
                {
                    beatMap.Add(new MappedBeat((double)(60d / (double)(specs.Tempo * 12d)), NoteFunction.Rest));
                }
            }
            //else if (notes.GetNoteFunction(i) == NoteFunction.Hold)
            //{
            //    for (int n = 1; n < (int)notes[i].QuantizedRhythmicValue; n++)
            //    {
            //        beatMap.Add(new MappedBeat((double)(60d / (double)(specs.Tempo * 12d)), NoteFunction.Hold));
            //    }
            //}
            else
            {
                for (int n = 1; n < (int)notes[i].QuantizedRhythmicValue; n++)
                {
                    beatMap.Add(new MappedBeat((double)(60d / (double)(specs.Tempo * 12d)), NoteFunction.Hold));
                }
            }
        }
    }

    private static NoteFunction GetNoteFunction(this List<Note> notes, int i)
    {
        if (notes[i].Rest) return NoteFunction.Rest;
        if (i != 0 && notes[i - 1].TiedFrom) return NoteFunction.Hold;
        return NoteFunction.Attack;
    }
}