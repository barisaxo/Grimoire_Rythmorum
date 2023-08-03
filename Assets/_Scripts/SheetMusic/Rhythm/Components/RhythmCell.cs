
namespace MusicTheory.Rhythms
{
    public class RhythmCell
    {
        public bool Rest;
        public bool TiedTo;
        public bool TiedFrom;
        public bool LongCell;
        public CellShape Shape;
        public MetricLevel MetricLevel;
        public Quantizement Quantizement;
        public Count Count;

        public RhythmCell SetRest(bool tf) { Rest = tf; return this; }
        public RhythmCell SetTiedTo(bool tf) { TiedTo = tf; return this; }
        public RhythmCell SetTiedFrom(bool tf) { TiedFrom = tf; return this; }
        public RhythmCell SetLongCell(bool tf) { LongCell = tf; return this; }
        public RhythmCell SetRhythmicShape(CellShape shape) { Shape = shape; return this; }
        public RhythmCell SetMetricLevel(MetricLevel level) { MetricLevel = level; return this; }
        public RhythmCell SetQuantizement(Quantizement quantizement) { Quantizement = quantizement; return this; }

        public RhythmCell SetCount(Count c) { Count = c; return this; }
        public RhythmCell SetCount(int c) { Count = (Count)c; return this; }
    }
}