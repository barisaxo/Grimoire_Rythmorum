using System;
using MusicTheory.ScaleDegrees;

namespace Musica.Rhythms
{
    public class TimeSignatureEnum : Enumeration
    {
        public TimeSignatureEnum() : base(0, "") { }
        public TimeSignatureEnum(int id, string name) : base(id, name) { }

        public Count Quantity;
        public SubCount Quality;
        public Meter Meter;
        public RhythmicValue BeatLevelValue;

        public static TimeSignatureEnum TwoTwo => new(0, nameof(TwoTwo)) { Quantity = Count.Two, Quality = SubCount.Two, Meter = Meter.SimpleDuple, BeatLevelValue = RhythmicValue.Half };
        public static TimeSignatureEnum ThreeTwo => new(1, nameof(ThreeTwo)) { Quantity = Count.Thr, Quality = SubCount.Two, Meter = Meter.SimpleTriple, BeatLevelValue = RhythmicValue.Half };

        public static TimeSignatureEnum TwoFour => new(2, nameof(TwoFour)) { Quantity = Count.Two, Quality = SubCount.For, Meter = Meter.SimpleDuple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignatureEnum ThreeFour => new(3, nameof(ThreeFour)) { Quantity = Count.Thr, Quality = SubCount.For, Meter = Meter.SimpleTriple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignatureEnum FourFour => new(4, nameof(FourFour)) { Quantity = Count.For, Quality = SubCount.For, Meter = Meter.SimpleQuadruple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignatureEnum FiveFour23 => new(5, nameof(FiveFour23)) { Quantity = Count.Fiv, Quality = SubCount.For, Meter = Meter.IrregularDupleTriple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignatureEnum FiveFour32 => new(6, nameof(FiveFour32)) { Quantity = Count.Fiv, Quality = SubCount.For, Meter = Meter.IrregularTripleDuple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignatureEnum SixFour => new(7, nameof(SixFour)) { Quantity = Count.Six, Quality = SubCount.For, Meter = Meter.CompoundDuple, BeatLevelValue = RhythmicValue.DotHalf };
        public static TimeSignatureEnum SevenFour43 => new(8, nameof(SevenFour43)) { Quantity = Count.Sev, Quality = SubCount.For, Meter = Meter.IrregularQuadrupleTriple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignatureEnum SevenFour34 => new(9, nameof(SevenEight34)) { Quantity = Count.Sev, Quality = SubCount.For, Meter = Meter.IrregularTripleDuple, BeatLevelValue = RhythmicValue.Quarter };

        public static TimeSignatureEnum ThreeEight => new(10, nameof(ThreeEight)) { Quantity = Count.Thr, Quality = SubCount.Eht, Meter = Meter.SimpleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignatureEnum FiveEight23 => new(11, nameof(FiveEight23)) { Quantity = Count.Fiv, Quality = SubCount.Eht, Meter = Meter.IrregularDupleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignatureEnum FiveEight32 => new(12, nameof(FiveEight32)) { Quantity = Count.Fiv, Quality = SubCount.Eht, Meter = Meter.IrregularTripleDuple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignatureEnum SixEight => new(13, nameof(SixEight)) { Quantity = Count.Six, Quality = SubCount.Eht, Meter = Meter.CompoundDuple, BeatLevelValue = RhythmicValue.DotQuarter };
        public static TimeSignatureEnum SevenEight43 => new(14, nameof(SevenEight43)) { Quantity = Count.Sev, Quality = SubCount.Eht, Meter = Meter.IrregularQuadrupleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignatureEnum SevenEight34 => new(15, nameof(SevenEight34)) { Quantity = Count.Sev, Quality = SubCount.Eht, Meter = Meter.IrregularQuadrupleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignatureEnum NineEight => new(16, nameof(NineEight)) { Quantity = Count.Nin, Quality = SubCount.Eht, Meter = Meter.CompoundTriple, BeatLevelValue = RhythmicValue.DotHalf };
        public static TimeSignatureEnum TwelveEight => new(17, nameof(TwelveEight)) { Quantity = Count.Tlv, Quality = SubCount.Eht, Meter = Meter.CompoundQuadruple, BeatLevelValue = RhythmicValue.DotHalf };

        public static bool operator ==(TimeSignatureEnum a, TimeSignatureEnum b) => a.Quality == b.Quality && a.Quantity == b.Quantity;
        public static bool operator !=(TimeSignatureEnum a, TimeSignatureEnum b) => a.Quality != b.Quality || a.Quantity != b.Quantity;
        public override bool Equals(object obj) => obj is TimeSignatureEnum t && Quality == t.Quality && Quantity == t.Quantity;
        public override int GetHashCode() => System.HashCode.Combine(Quality, Quantity);

        public static implicit operator Time(TimeSignatureEnum t) => t switch
        {
            _ when t == TwoTwo => new TwoTwo(),
            _ when t == ThreeTwo => new ThreeTwo(),
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
    }

    public static class RandomTimeSignature
    {
        public static Time Get() => UnityEngine.Random.value < .45f ? new FourFour() :
             Enumeration.All<TimeSignatureEnum>()[UnityEngine.Random.Range(0, Enumeration.All<TimeSignatureEnum>().Length)];

    }
}
