
using SheetMusic;


using SheetMusic;

namespace MusicTheory.Rhythms
{
    public abstract class Time
    {
        public TimeSignature Signature;

        public void GenerateRhythmCells(MusicSheet ms) { GetRhythmCells(ms); ms.GetRestsAndTies(); }
        protected virtual void GetRhythmCells(MusicSheet ms) { }

        public static bool operator ==(Time a, Time b) => a.Signature.Quality == b.Signature.Quality && a.Signature.Quantity == b.Signature.Quantity;
        public static bool operator !=(Time a, Time b) => a.Signature.Quality != b.Signature.Quality || a.Signature.Quantity != b.Signature.Quantity;
        public override bool Equals(object obj) => obj is Time t && Signature.Quality == t.Signature.Quality && Signature.Quantity == t.Signature.Quantity;
        public override int GetHashCode() => System.HashCode.Combine(Signature.Quality, Signature.Quantity);
    }
}
