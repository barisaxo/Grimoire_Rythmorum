using MusicTheory.Rhythms;

namespace Ritmo
{
    public struct MappedBeat
    {
        public double TimeInterval;
        public NoteFunction NoteFunction;

        public MappedBeat(double timeInterval, NoteFunction noteFunction)
        {
            TimeInterval = timeInterval;
            NoteFunction = noteFunction;
        }


    }

    public struct MapBeat
    {
        public RhythmicValue RhythmicSpace;
        public NoteFunction NoteFunction;
        public MapBeat(RhythmicValue space, NoteFunction fun) { RhythmicSpace = space; NoteFunction = fun; }
        public MapBeat(int space, NoteFunction fun) { RhythmicSpace = (RhythmicValue)space; NoteFunction = fun; }
    }
}