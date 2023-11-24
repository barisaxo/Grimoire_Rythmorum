
namespace MusicTheory.Rhythms
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
}