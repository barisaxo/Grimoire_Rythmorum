using MusicTheory.Rhythms;

namespace Ritmo
{
    public struct MappedBeat
    {
        public float TimeInterval;
        public NoteFunction NoteFunction;

        public MappedBeat(float timeInterval, NoteFunction noteFunction)
        {
            TimeInterval = timeInterval;
            NoteFunction = noteFunction;
        }
    }
}