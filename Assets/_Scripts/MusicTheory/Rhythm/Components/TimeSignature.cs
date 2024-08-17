using System;
using MusicTheory.ScaleDegrees;
using Datum;
namespace MusicTheory.Rhythms
{
    public class TimeSignatureEnum : Enumeration
    {
        public TimeSignatureEnum() : base(0, "") { }
        public TimeSignatureEnum(int id, string name) : base(id, name) { }

        public Count Quantity;
        public SubCount Quality;
        public Meter Meter;
        public RhythmicValue BeatLevelValue;

        // public readonly static TimeSignatureEnum TwoTwo = new(0, nameof(TwoTwo)) { Quantity = Count.Two, Quality = SubCount.Two, Meter = Meter.SimpleDuple, BeatLevelValue = RhythmicValue.Half };
        // public readonly static TimeSignatureEnum ThreeTwo = new(1, nameof(ThreeTwo)) { Quantity = Count.Thr, Quality = SubCount.Two, Meter = Meter.SimpleTriple, BeatLevelValue = RhythmicValue.Half };
        // public readonly static TimeSignatureEnum TwoFour = new(2, nameof(TwoFour)) { Quantity = Count.Two, Quality = SubCount.For, Meter = Meter.SimpleDuple, BeatLevelValue = RhythmicValue.Quarter };
        // public readonly static TimeSignatureEnum ThreeFour = new(3, nameof(ThreeFour)) { Quantity = Count.Thr, Quality = SubCount.For, Meter = Meter.SimpleTriple, BeatLevelValue = RhythmicValue.Quarter };
        // public readonly static TimeSignatureEnum FourFour = new(4, nameof(FourFour)) { Quantity = Count.For, Quality = SubCount.For, Meter = Meter.SimpleQuadruple, BeatLevelValue = RhythmicValue.Quarter };
        // public readonly static TimeSignatureEnum FiveFour23 = new(5, nameof(FiveFour23)) { Quantity = Count.Fiv, Quality = SubCount.For, Meter = Meter.IrregularDupleTriple, BeatLevelValue = RhythmicValue.Quarter };
        // public readonly static TimeSignatureEnum FiveFour32 = new(6, nameof(FiveFour32)) { Quantity = Count.Fiv, Quality = SubCount.For, Meter = Meter.IrregularTripleDuple, BeatLevelValue = RhythmicValue.Quarter };
        // public readonly static TimeSignatureEnum SixFour = new(7, nameof(SixFour)) { Quantity = Count.Six, Quality = SubCount.For, Meter = Meter.CompoundDuple, BeatLevelValue = RhythmicValue.DotHalf };
        // public readonly static TimeSignatureEnum SevenFour43 = new(8, nameof(SevenFour43)) { Quantity = Count.Sev, Quality = SubCount.For, Meter = Meter.IrregularQuadrupleTriple, BeatLevelValue = RhythmicValue.Quarter };
        // public readonly static TimeSignatureEnum SevenFour34 = new(9, nameof(SevenFour34)) { Quantity = Count.Sev, Quality = SubCount.For, Meter = Meter.IrregularTripleQuadruple, BeatLevelValue = RhythmicValue.Quarter };
        // public readonly static TimeSignatureEnum ThreeEight = new(10, nameof(ThreeEight)) { Quantity = Count.Thr, Quality = SubCount.Eht, Meter = Meter.SimpleTriple, BeatLevelValue = RhythmicValue.Eighth };
        // public readonly static TimeSignatureEnum FiveEight23 = new(11, nameof(FiveEight23)) { Quantity = Count.Fiv, Quality = SubCount.Eht, Meter = Meter.IrregularDupleTriple, BeatLevelValue = RhythmicValue.Eighth };
        // public readonly static TimeSignatureEnum FiveEight32 = new(12, nameof(FiveEight32)) { Quantity = Count.Fiv, Quality = SubCount.Eht, Meter = Meter.IrregularTripleDuple, BeatLevelValue = RhythmicValue.Eighth };
        // public readonly static TimeSignatureEnum SixEight = new(13, nameof(SixEight)) { Quantity = Count.Six, Quality = SubCount.Eht, Meter = Meter.CompoundDuple, BeatLevelValue = RhythmicValue.DotQuarter };
        // public readonly static TimeSignatureEnum SevenEight43 = new(14, nameof(SevenEight43)) { Quantity = Count.Sev, Quality = SubCount.Eht, Meter = Meter.IrregularQuadrupleTriple, BeatLevelValue = RhythmicValue.Eighth };
        // public readonly static TimeSignatureEnum SevenEight34 = new(15, nameof(SevenEight34)) { Quantity = Count.Sev, Quality = SubCount.Eht, Meter = Meter.IrregularTripleQuadruple, BeatLevelValue = RhythmicValue.Eighth };
        // public readonly static TimeSignatureEnum NineEight = new(16, nameof(NineEight)) { Quantity = Count.Nin, Quality = SubCount.Eht, Meter = Meter.CompoundTriple, BeatLevelValue = RhythmicValue.DotHalf };
        // public readonly static TimeSignatureEnum TwelveEight = new(17, nameof(TwelveEight)) { Quantity = Count.Tlv, Quality = SubCount.Eht, Meter = Meter.CompoundQuadruple, BeatLevelValue = RhythmicValue.DotHalf };

        public readonly static TimeSignatureEnum TwoFour = new(0, nameof(TwoFour)) { Quantity = Count.Two, Quality = SubCount.For, Meter = Meter.SimpleDuple, BeatLevelValue = RhythmicValue.Quarter };
        public readonly static TimeSignatureEnum ThreeFour = new(1, nameof(ThreeFour)) { Quantity = Count.Thr, Quality = SubCount.For, Meter = Meter.SimpleTriple, BeatLevelValue = RhythmicValue.Quarter };
        public readonly static TimeSignatureEnum FourFour = new(2, nameof(FourFour)) { Quantity = Count.For, Quality = SubCount.For, Meter = Meter.SimpleQuadruple, BeatLevelValue = RhythmicValue.Quarter };
        public readonly static TimeSignatureEnum FiveFour23 = new(3, nameof(FiveFour23)) { Quantity = Count.Fiv, Quality = SubCount.For, Meter = Meter.IrregularDupleTriple, BeatLevelValue = RhythmicValue.Quarter };
        public readonly static TimeSignatureEnum FiveFour32 = new(4, nameof(FiveFour32)) { Quantity = Count.Fiv, Quality = SubCount.For, Meter = Meter.IrregularTripleDuple, BeatLevelValue = RhythmicValue.Quarter };
        public readonly static TimeSignatureEnum SixFour = new(5, nameof(SixFour)) { Quantity = Count.Six, Quality = SubCount.For, Meter = Meter.CompoundDuple, BeatLevelValue = RhythmicValue.DotHalf };
        public readonly static TimeSignatureEnum SevenFour43 = new(6, nameof(SevenFour43)) { Quantity = Count.Sev, Quality = SubCount.For, Meter = Meter.IrregularQuadrupleTriple, BeatLevelValue = RhythmicValue.Quarter };
        public readonly static TimeSignatureEnum SevenFour34 = new(7, nameof(SevenFour34)) { Quantity = Count.Sev, Quality = SubCount.For, Meter = Meter.IrregularTripleQuadruple, BeatLevelValue = RhythmicValue.Quarter };
        public readonly static TimeSignatureEnum ThreeEight = new(8, nameof(ThreeEight)) { Quantity = Count.Thr, Quality = SubCount.Eht, Meter = Meter.SimpleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public readonly static TimeSignatureEnum FiveEight23 = new(9, nameof(FiveEight23)) { Quantity = Count.Fiv, Quality = SubCount.Eht, Meter = Meter.IrregularDupleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public readonly static TimeSignatureEnum FiveEight32 = new(10, nameof(FiveEight32)) { Quantity = Count.Fiv, Quality = SubCount.Eht, Meter = Meter.IrregularTripleDuple, BeatLevelValue = RhythmicValue.Eighth };
        public readonly static TimeSignatureEnum SixEight = new(11, nameof(SixEight)) { Quantity = Count.Six, Quality = SubCount.Eht, Meter = Meter.CompoundDuple, BeatLevelValue = RhythmicValue.DotQuarter };
        public readonly static TimeSignatureEnum SevenEight43 = new(12, nameof(SevenEight43)) { Quantity = Count.Sev, Quality = SubCount.Eht, Meter = Meter.IrregularQuadrupleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public readonly static TimeSignatureEnum SevenEight34 = new(13, nameof(SevenEight34)) { Quantity = Count.Sev, Quality = SubCount.Eht, Meter = Meter.IrregularTripleQuadruple, BeatLevelValue = RhythmicValue.Eighth };
        public readonly static TimeSignatureEnum NineEight = new(14, nameof(NineEight)) { Quantity = Count.Nin, Quality = SubCount.Eht, Meter = Meter.CompoundTriple, BeatLevelValue = RhythmicValue.DotHalf };
        public readonly static TimeSignatureEnum TwelveEight = new(15, nameof(TwelveEight)) { Quantity = Count.Tlv, Quality = SubCount.Eht, Meter = Meter.CompoundQuadruple, BeatLevelValue = RhythmicValue.DotHalf };


        public static bool operator ==(TimeSignatureEnum a, TimeSignatureEnum b) => a.BeatLevelValue == b.BeatLevelValue && a.Meter == b.Meter && a.Quality == b.Quality && a.Quantity == b.Quantity;
        public static bool operator !=(TimeSignatureEnum a, TimeSignatureEnum b) => a.BeatLevelValue != b.BeatLevelValue || a.Meter != b.Meter || a.Quality != b.Quality || a.Quantity != b.Quantity;
        public override bool Equals(object obj) => obj is TimeSignatureEnum tse && Quality == tse.Quality && Quantity == tse.Quantity && tse.Meter == Meter && tse.BeatLevelValue == BeatLevelValue;
        public override int GetHashCode() => System.HashCode.Combine(Quality, Quantity, Meter, BeatLevelValue);

        public static explicit operator Time(TimeSignatureEnum t) => t switch
        {
            // _ when t == TwoTwo => new TwoTwo(),
            // _ when t == ThreeTwo => new ThreeTwo(),
            _ when t == TwoFour => new TwoFour(),
            _ when t == ThreeFour => new ThreeFour(),
            _ when t == FourFour => new FourFour(),
            _ when t == FiveFour23 => new FiveFour23(),
            _ when t == FiveFour32 => new FiveFour32(),
            _ when t == SixFour => new SixFour(),
            _ when t == SevenFour43 => new SevenFour43(),
            _ when t == SevenFour34 => new SevenFour34(),
            _ when t == ThreeEight => new ThreeEight(),
            _ when t == FiveEight23 => new FiveEight23(),
            _ when t == FiveEight32 => new FiveEight32(),
            _ when t == SixEight => new SixEight(),
            _ when t == SevenEight43 => new SevenEight43(),
            _ when t == SevenEight34 => new SevenEight34(),
            _ when t == NineEight => new NineEight(),
            _ when t == TwelveEight => new TwelveEight(),
            _ => throw new System.ArgumentOutOfRangeException(t.ToString()),
        };

        internal static IItem ToItem(TimeSignatureEnum @enum)
        {
            return @enum switch
            {
                // _ when @enum == TwoTwo => new Datum.Rhythm.TwoTwo(),
                // _ when @enum == ThreeTwo => new Datum.Rhythm.ThreeTwo(),
                _ when @enum == TwoFour => new Datum.Rhythm.TwoFour(),
                _ when @enum == ThreeFour => new Datum.Rhythm.ThreeFour(),
                _ when @enum == FourFour => new Datum.Rhythm.FourFour(),
                _ when @enum == FiveFour23 => new Datum.Rhythm.FiveFour23(),
                _ when @enum == FiveFour32 => new Datum.Rhythm.FiveFour32(),
                _ when @enum == SixFour => new Datum.Rhythm.SixFour(),
                _ when @enum == SevenFour43 => new Datum.Rhythm.SevenFour43(),
                _ when @enum == SevenFour34 => new Datum.Rhythm.SevenFour34(),
                _ when @enum == ThreeEight => new Datum.Rhythm.ThreeEight(),
                _ when @enum == FiveEight23 => new Datum.Rhythm.FiveEight23(),
                _ when @enum == FiveEight32 => new Datum.Rhythm.FiveEight32(),
                _ when @enum == SixEight => new Datum.Rhythm.SixEight(),
                _ when @enum == SevenEight43 => new Datum.Rhythm.SevenEight43(),
                _ when @enum == SevenEight34 => new Datum.Rhythm.SevenEight34(),
                _ when @enum == NineEight => new Datum.Rhythm.NineEight(),
                _ when @enum == TwelveEight => new Datum.Rhythm.TwelveEight(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }

    }

    public static class RandomTimeSignature
    {
        public static Time Get()
        {
            int t = UnityEngine.Random.Range(0, Enumeration.All<TimeSignatureEnum>().Length);
            UnityEngine.Debug.Log(t + " " + Enumeration.All<TimeSignatureEnum>().Length);

            return (Time)Enumeration.All<TimeSignatureEnum>()[t];
        }
    }

    public static class RandomTimeSignatureSans44
    {
        public static Time Get()
        {
            int t = UnityEngine.Random.Range(0, Enumeration.All<TimeSignatureEnum>().Length);
            if ((Time)Enumeration.All<TimeSignatureEnum>()[t] is FourFour) return new NineEight();
            return (Time)Enumeration.All<TimeSignatureEnum>()[t];
        }
    }
}
