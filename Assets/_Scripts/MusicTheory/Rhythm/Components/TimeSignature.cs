namespace MusicTheory.Rhythms
{
    public struct TimeSignature
    {
        public Count Quantity;
        public SubCount Quality;
        public Meter Meter;
        public RhythmicValue BeatLevelValue;

        public static TimeSignature TwoTwo => new() { Quantity = Count.Two, Quality = SubCount.Two, Meter = Meter.SimpleDuple, BeatLevelValue = RhythmicValue.Half };
        public static TimeSignature ThreeTwo => new() { Quantity = Count.Thr, Quality = SubCount.Two, Meter = Meter.SimpleTriple, BeatLevelValue = RhythmicValue.Half };

        public static TimeSignature TwoFour => new() { Quantity = Count.Two, Quality = SubCount.For, Meter = Meter.SimpleDuple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignature ThreeFour => new() { Quantity = Count.Thr, Quality = SubCount.For, Meter = Meter.SimpleTriple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignature FourFour => new() { Quantity = Count.For, Quality = SubCount.For, Meter = Meter.SimpleQuadruple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignature FiveFour23 => new() { Quantity = Count.Fiv, Quality = SubCount.For, Meter = Meter.IrregularDupleTriple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignature FiveFour32 => new() { Quantity = Count.Fiv, Quality = SubCount.For, Meter = Meter.IrregularTripleDuple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignature SixFour => new() { Quantity = Count.Six, Quality = SubCount.For, Meter = Meter.CompoundDuple, BeatLevelValue = RhythmicValue.DotHalf };
        public static TimeSignature SevenFour43 => new() { Quantity = Count.Sev, Quality = SubCount.For, Meter = Meter.IrregularQuadrupleTriple, BeatLevelValue = RhythmicValue.Quarter };
        public static TimeSignature SevenFour34 => new() { Quantity = Count.Sev, Quality = SubCount.For, Meter = Meter.IrregularTripleDuple, BeatLevelValue = RhythmicValue.Quarter };

        public static TimeSignature ThreeEight => new() { Quantity = Count.Thr, Quality = SubCount.Eht, Meter = Meter.SimpleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignature FiveEight23 => new() { Quantity = Count.Fiv, Quality = SubCount.Eht, Meter = Meter.IrregularDupleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignature FiveEight32 => new() { Quantity = Count.Fiv, Quality = SubCount.Eht, Meter = Meter.IrregularTripleDuple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignature SixEight => new() { Quantity = Count.Six, Quality = SubCount.Eht, Meter = Meter.CompoundDuple, BeatLevelValue = RhythmicValue.DotQuarter };
        public static TimeSignature SevenEight43 => new() { Quantity = Count.Sev, Quality = SubCount.Eht, Meter = Meter.IrregularQuadrupleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignature SevenEight34 => new() { Quantity = Count.Sev, Quality = SubCount.Eht, Meter = Meter.IrregularQuadrupleTriple, BeatLevelValue = RhythmicValue.Eighth };
        public static TimeSignature NineEight => new() { Quantity = Count.Nin, Quality = SubCount.Eht, Meter = Meter.CompoundTriple, BeatLevelValue = RhythmicValue.DotHalf };
        public static TimeSignature TwelveEight => new() { Quantity = Count.Tlv, Quality = SubCount.Eht, Meter = Meter.CompoundQuadruple, BeatLevelValue = RhythmicValue.DotHalf };

        public static bool operator ==(TimeSignature a, TimeSignature b) => a.Quality == b.Quality && a.Quantity == b.Quantity;
        public static bool operator !=(TimeSignature a, TimeSignature b) => a.Quality != b.Quality || a.Quantity != b.Quantity;
        public override readonly bool Equals(object obj) => obj is TimeSignature t && Quality == t.Quality && Quantity == t.Quantity;
        public override readonly int GetHashCode() => System.HashCode.Combine(Quality, Quantity);
    }
}

//
//Whole note             = 64 : 240 / BPM
//Half note              = 32 : 120 / BPM
//Dotted quarter note    = 24 : 90 / BPM
//Quarter note           = 16 : 60 / BPM
//Dotted eighth note     = 12 : 45 / BPM
//Triplet quarter note   = 10 : ??
//Eighth note            =  8 : 30 / BPM
//Triplet eighth note    =  6 : 20 / BPM
//Sixteenth note         =  4 : 15 / BPM
//
