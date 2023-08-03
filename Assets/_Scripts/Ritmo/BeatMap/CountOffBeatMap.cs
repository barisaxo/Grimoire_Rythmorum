using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Rhythms;


namespace Ritmo
{

    public struct CountOffBeatMap
    {
        public List<MappedBeat> GetCountOffBeatMap(float tempo) => new List<MappedBeat>()
        {
            //one
            new MappedBeat(timeInterval: (30 / tempo) , noteFunction: NoteFunction.Attack),
            new MappedBeat(timeInterval: (30f / tempo) , noteFunction: NoteFunction.Rest),
            //two
            new MappedBeat(timeInterval: (30f / tempo) , noteFunction: NoteFunction.Attack),
            new MappedBeat(timeInterval: (30f / tempo) , noteFunction: NoteFunction.Rest),
            //3
            new MappedBeat(timeInterval: (15f / tempo) , noteFunction: NoteFunction.Attack),
            new MappedBeat(timeInterval: (15f / tempo) , noteFunction: NoteFunction.Rest),
            //3+
            new MappedBeat(timeInterval: (15f / tempo) , noteFunction: NoteFunction.Attack),
            new MappedBeat(timeInterval: (15f / tempo) , noteFunction: NoteFunction.Rest),
            //4e+a
            new MappedBeat(timeInterval: (15f / tempo) , noteFunction: NoteFunction.Attack),
            new MappedBeat(timeInterval: (15f / tempo) , noteFunction: NoteFunction.Rest),
            new MappedBeat(timeInterval: (15f / tempo) , noteFunction: NoteFunction.Attack),
            new MappedBeat(timeInterval: (15f / tempo) , noteFunction: NoteFunction.Rest),
            new MappedBeat(timeInterval: (00f / tempo) , noteFunction: NoteFunction.Rest),
        };
    }

    ///Whole note = 240 / BPM
    ///Half note = 120 / BPM
    ///Dotted quarter note = 90 / BPM
    ///Quarter note = 60 / BPM
    ///Dotted eighth note = 45 / BPM
    ///Eighth note = 30 / BPM
    ///Triplet eighth note = 20 / BPM
    ///Sixteenth note = 15 / BPM
    ///Triplet sixteenth note = 10 / BPM


}