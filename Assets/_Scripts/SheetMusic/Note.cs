
using UnityEngine;

namespace MusicTheory.Rhythms
{
    public class Note
    {
        public bool Rest;
        public bool TiesTo;
        public bool Trip;

        public RhythmCell ParentCell;
        public BeatLocation BeatLocation;
        public RhythmicValue QuantizedRhythmicValue;

        public Transform TF;
        public SpriteRenderer SR;

        //public Note(
        //    bool rest,
        //    bool tied,
        //    bool trip,
        //    RhythmCell parentCell,
        //    BeatLocation beatLocation,
        //    RhythmicValue rhythmicNoteValue)
        //{
        //    Rest = rest;
        //    TiesTo = tied;
        //    Trip = trip;
        //    ParentCell = parentCell;
        //    BeatLocation = beatLocation;
        //    QuantizedRhythmicValue = rhythmicNoteValue;
        //}
    }
}

