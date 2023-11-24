
namespace MusicTheory.Rhythms
{

    //
    // when you quantize something, it undergoes quantization, and is now quantized.
    // 
    // quantizement is a things current state, or resolution, of quantization.
    // 
    // quanta (plural;  singular: quantum) is the smallest amount something possible can be measured at.
    //
    //     while technically the quantum of rhythmic resolution is infinite,
    //     the use of 64th notes is not unheard of.
    //     16th notes will be the smallest unit used in this game.

    //    Whole note = 240 / BPM
    //    Half note = 120 / BPM
    //    Dotted quarter note = 90 / BPM
    //    Quarter note = 60 / BPM
    //    Dotted eighth note = 45 / BPM
    //    Eighth note = 30 / BPM
    //    Triplet eighth note = 20 / BPM
    //    Sixteenth note = 15 / BPM
    //    Triplet Sixteenth note = 10 / BPM

    //    (out of 48)
    //    One = 01 + 00, OneE = 04 + 00, OneT = 05 + 00, OneN = 07 + 00, OneL = 09 + 00, OneA = 10 + 00,
    //    Two = 01 + 12, TwoE = 04 + 12, TwoT = 05 + 12, TwoN = 07 + 12, TwoL = 09 + 12, TwoA = 10 + 12,
    //    Thr = 01 + 24, ThrE = 04 + 24, ThrT = 05 + 24, ThrN = 07 + 24, ThrL = 09 + 24, ThrA = 10 + 24,
    //    For = 01 + 36, ForE = 04 + 36, ForT = 05 + 36, ForN = 07 + 36, ForL = 09 + 36, ForA = 10 + 36
    //    
    //    ||1 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . |WHOLE
    //    ||1 . . . . . . . . . . . . . . . . . . . . . . . 3 . . . . . . . . . . . . . . . . . . . . . . . |HALF
    //    ||1 . . . . . . . . . . . . . . . T . . . . . . . . . . . . . . . L . . . . . . . . . . . . . . . |TRIP HALF
    //    ||1 . . . . . . . . . . . 2 . . . . . . . . . . . 3 . . . . . . . . . . . 4 . . . . . . . . . . . |QUARTER
    //    ||1 . . . . . . . T . . . . . . . L . . . . . . . 3 . . . . . . . T . . . . . . . L . . . . . . . |TRIP QUARTER
    //    ||1 . . . . . + . . . . . 2 . . . . . + . . . . . 3 . . . . . + . . . . . 4 . . . . . + . . . . . |EIGHTH
    //    ||1 . . . T . . . L . . . 2 . . . T . . . L . . . 3 . . . T . . . L . . . 4 . . . T . . . L . . . |TRIP EIGHTH
    //    ||1 . . e . . + . . a . . 2 . . e . . + . . a . . 3 . . e . . + . . a . . 4 . . e . . + . . a . . |SIXTEENTH
    //    ||1 . T . L . + . T . L . 2 . T . L . + . T . L . 3 . T . L . + . T . L . 4 . T . L . + . T . L . |TRIP SIXTEENTH
    //    ||1 . . e T . + . L a . . 2 . . e T . + . L a . . 3 . . e T . + . L a . . 4 . . e T . + . L a . . |ALL
    //    ||1 . . . 5 . . . . 10. . . . 5 . . . . 20. . . . 5 . . . . 30. . . . 5 . . . . 40. . . . 5 . . 48|spaces


    public enum Quantizement { Half, Quarter, QuarterTrips, Eighth, EighthTrips, Sixteenth }


    // Rhythm Cell Shapes:
    // Duple has 2 shapes: L & LL(or SS relatively)
    // Triple has 4 shapes: TL, TLS, TSL, TSSS
    // Quadruple has 8 shapes: L, LL, SL, LSS, SSL, SLS, SSSS.
    // The first two quadruple cells are not unqiue as they overlap with the duple cells.
    public enum CellShape { L, LL, LS, SL, LSS, SSL, SLS, SSSS, TLS, TSL, TSSS, TL, DLL, DL }


    public enum RhythmicValue
    {
        Whole = 48, Half = 24, Quarter = 12, Eighth = 6, Sixteenth = 3,
        DotWhole = 72, DotHalf = 36, DotQuarter = 18, DotEighth = 9,
        TripHalf = 16, TripQuarter = 8, TripEighth = 4,
    }
    public enum RhythmOption { Ties, Rests, SomeTrips, TripsOnly }
    public enum SubDivisionTier { BeatOnly, BeatAndD1, D1Only, D1AndD2, D2Only, }
    public enum NoteFunction { Attack, Hold, Rest, Ignore }


    /// <summary>
    /// unnecessary to define is the 'Multiple Levels' as in the combined beat level eg half & whole notes in 4/4 D1 is the fist division level, eg 8th notes in 4/4/D1T is triplets at the first division, eg trip 8ths in 4/4D2 is the second division level, eg 16th notes in 4/4.There will be no need to go beyond D2 in this game.
    /// </summary>
    public enum MetricLevel { Beat, BeatT, D1, D1T, D2, }



    /// <summary>
    /// The elements of Metric Structure
    /// </summary>
    public enum PulseStress { Duple, Triple, Quadruple }


    /// <summary>
    /// Unnecessary to define is the 'Irregular' as it is a combination of simple & compound and is implied as such.
    /// </summary>
    public enum BeatDivisor { Simple, Compound }



    /// <summary>
    ///The bottom number of a time signature.
    ///Could potentially continue on in powers of 2, but not going beyond 8th notes in this project. 
    /// </summary>
    public enum SubCount { One = 1, Two = 2, For = 4, Eht = 8 }


    public static class RhythmUtilities
    {
        public static Quantizement GetQuantizement(this Time time)
        {
            return time switch
            {
                TwoTwo => Quantizement.Half,
                ThreeTwo => Quantizement.Half,
                NineEight => Quantizement.EighthTrips,
                SixEight => Quantizement.EighthTrips,
                TwelveEight => Quantizement.EighthTrips,
                ThreeEight => Quantizement.Eighth,
                FiveEight23 => Quantizement.Eighth,
                FiveEight32 => Quantizement.Eighth,
                SevenEight34 => Quantizement.Eighth,
                SevenEight43 => Quantizement.Eighth,
                _ => Quantizement.Quarter,
            };
        }
    }

}



/*
 * unnecessary to define is the 'Multiple Levels' as in the combined beat level eg half & whole notes in 4/4 D1 is the fist division level, eg 8th notes in 4/4/D1T is triplets at the first division, eg trip 8ths in 4/4D2 is the second division level, eg 16th notes in 4/4.There will be no need to go beyond D2 in this game.

*/