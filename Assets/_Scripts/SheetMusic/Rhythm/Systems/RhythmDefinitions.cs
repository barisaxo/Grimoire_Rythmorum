using System.Collections.Generic;

namespace MusicTheory.Rhythms
{
    //Whole note = 240 / BPM
    //Half note = 120 / BPM
    //Dotted quarter note = 90 / BPM
    //Quarter note = 60 / BPM
    //Dotted eighth note = 45 / BPM
    //Eighth note = 30 / BPM
    //Triplet eighth note = 20 / BPM
    //Sixteenth note = 15 / BPM
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

    //    One = 01 + 00, OneE = 04 + 00, OneT = 05 + 00, OneN = 07 + 00, OneL = 09 + 00, OneA = 10 + 00,
    //    Two = 01 + 12, TwoE = 04 + 12, TwoT = 05 + 12, TwoN = 07 + 12, TwoL = 09 + 12, TwoA = 10 + 12,
    //    Thr = 01 + 24, ThrE = 04 + 24, ThrT = 05 + 24, ThrN = 07 + 24, ThrL = 09 + 24, ThrA = 10 + 24,
    //    For = 01 + 36, ForE = 04 + 36, ForT = 05 + 36, ForN = 07 + 36, ForL = 09 + 36, ForA = 10 + 36
    //    //out of 48 (last 2 are space)
    //    //||1 . . e T . + . L a . . 2 . . e T . + . L a . . 3 . . e T . + . L a . . 4 . . e T . + . L a . . |ALL
    //    //||1 . . . 5 . . . . 10. . . . 5 . . . . 20. . . . 5 . . . . 30. . . . 5 . . . . 40. . . . 5 . . 48|spaces
    //    //||1 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . |WHOLE
    //    //||1 . . . . . . . . . . . . . . . . . . . . . . . 3 . . . . . . . . . . . . . . . . . . . . . . . |HALF
    //    //||1 . . . . . . . . . . . . . . . T . . . . . . . . . . . . . . . L . . . . . . . . . . . . . . . |TRIP QUARTER
    //    //||1 . . . . . . . . . . . 2 . . . . . . . . . . . 3 . . . . . . . . . . . 4 . . . . . . . . . . . |QUARTER
    //    //||1 . . . . . . . T . . . . . . . L . . . . . . . 3 . . . . . . . T . . . . . . . L . . . . . . . |TRIP QUARTER
    //    //||1 . . . . . + . . . . . 2 . . . . . + . . . . . 3 . . . . . + . . . . . 4 . . . . . + . . . . . |EIGHTH
    //    //||1 . . . T . . . L . . . 2 . . . T . . . L . . . 3 . . . T . . . L . . . 4 . . . T . . . L . . . |TRIP EIGHTH
    //    //||1 . . e . . + . . a . . 2 . . e . . + . . a . . 3 . . e . . + . . a . . 4 . . e . . + . . a . . |SIXTEENTH



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
        DotHalf = 36, DotQuarter = 18, DotEighth = 9,
        TripHalf = 16, TripQuarter = 8, TripEighth = 4, TripWhole = 32//?? not sure on trip whole
    }
    public enum RhythmOption { Ties, Rests, SomeTrips, TripsOnly }
    public enum SubDivisionTier { BeatOnly, BeatAndD1, D1Only, D1AndD2, D2Only, }
    public enum NoteFunction { Attack, Hold, Rest, Ignore }
    public enum MeasureNumber { One = 1, Two = 2, Thr = 3, For = 4 }

    //unnecessary to define is the 'Multiple Levels' as in the combined beat level eg half & whole notes in 4/4
    //D1 is the fist division level, eg 8th notes in 4/4
    //D1T is triplets at the first division, eg trip 8ths in 4/4
    //D2 is the second division level, eg 16th notes in 4/4.
    //There will be no need to go beyond D2 in this game.
    public enum MetricLevel { Beat, BeatT, D1, D1T, D2, }


    //The elements of Metric Structure
    public enum PulseStress { Duple, Triple, Quadruple }
    //Unnecessary to define is the 'Irregular' as it is a combination of simple & compound and is implied as such.
    public enum BeatDivisor { Simple, Compound }


    public struct BeatLocation
    {
        public MeasureNumber MeasureNumber;
        public Count Count;
        public SubBeatAssignment SubBeatAssignment;
    }
    public enum Count { One = 1, Two = 2, Thr = 3, For = 4, Fiv = 5, Six = 6, Sev = 7, Eht = 8, Nin = 9, Ten = 10, Elv = 11, Tlv = 12 }
    public enum SubCount { One = 1, Two = 2, For = 4, Eht = 8 }
    public enum SubBeatAssignment
    {
        D = 1, E = 4, T = 5, N = 7, L = 9, A = 10
        //Down   e      tup    +      let    a

        //pass    the  god  damn  but-ter
        //|1  .  .  e  T  .  +  .  L  a  .  . 
        //|1  2  3  4  5  6  7  8  9  10 11 12 
    }

    public static class RhythmUtilities
    {

        public static PulseStress GetBeatLevel(this (Meter, MetricLevel) mm) => BeatLevels.GetValueOrDefault(mm);

        public static Dictionary<(Meter, MetricLevel), PulseStress> BeatLevels => new Dictionary<(Meter, MetricLevel), PulseStress>
        {
            // 2/* time
            { (Meter.SimpleDuple, MetricLevel.Beat), PulseStress.Duple },
            { (Meter.SimpleDuple, MetricLevel.D1), PulseStress.Duple },
            { (Meter.SimpleDuple, MetricLevel.D1T), PulseStress.Triple},
            { (Meter.SimpleDuple, MetricLevel.D2), PulseStress.Quadruple},

            // 3/* time
            { (Meter.SimpleTriple, MetricLevel.Beat), PulseStress.Triple },
            { (Meter.SimpleTriple, MetricLevel.D1), PulseStress.Duple },
            { (Meter.SimpleTriple, MetricLevel.D1T), PulseStress.Triple},
            { (Meter.SimpleTriple, MetricLevel.D2), PulseStress.Quadruple},

            // 4/* time
            { (Meter.SimpleQuadruple, MetricLevel.Beat), PulseStress.Quadruple },
            { (Meter.SimpleQuadruple, MetricLevel.D1), PulseStress.Quadruple },
            { (Meter.SimpleQuadruple, MetricLevel.D1T), PulseStress.Triple},
            { (Meter.SimpleQuadruple, MetricLevel.D2), PulseStress.Quadruple},

            // 6/* time
            { (Meter.CompoundDuple, MetricLevel.Beat), PulseStress.Triple },
            { (Meter.CompoundDuple, MetricLevel.D1), PulseStress.Duple },
            { (Meter.CompoundDuple, MetricLevel.D1T), PulseStress.Triple},
            { (Meter.CompoundDuple, MetricLevel.D2), PulseStress.Quadruple},
            
            // 9/* time
            { (Meter.CompoundTriple, MetricLevel.Beat), PulseStress.Triple },
            { (Meter.CompoundTriple, MetricLevel.D1), PulseStress.Duple },
            { (Meter.CompoundTriple, MetricLevel.D1T), PulseStress.Triple},
            { (Meter.CompoundTriple, MetricLevel.D2), PulseStress.Quadruple},
            
            // 12/* time
            { (Meter.CompoundTriple, MetricLevel.Beat), PulseStress.Triple },
            { (Meter.CompoundTriple, MetricLevel.D1), PulseStress.Duple },
            { (Meter.CompoundTriple, MetricLevel.D1T), PulseStress.Triple},
            { (Meter.CompoundTriple, MetricLevel.D2), PulseStress.Quadruple},

        };

        public static Dictionary<(Meter, MetricLevel), int> NumberOfCells => new Dictionary<(Meter, MetricLevel), int>
        {
            // 2/* time
            { (Meter.SimpleDuple, MetricLevel.Beat), 1 },
            { (Meter.SimpleDuple, MetricLevel.D1), 2 },
            { (Meter.SimpleDuple, MetricLevel.D1T), 4},
            { (Meter.SimpleDuple, MetricLevel.D2), 4},
            
            // 3/* time
            { (Meter.SimpleTriple, MetricLevel.Beat),1 },
            { (Meter.SimpleTriple, MetricLevel.D1), 3 },
            { (Meter.SimpleTriple, MetricLevel.D1T), 6},
            { (Meter.SimpleTriple, MetricLevel.D2), 6},

            // 4/* time
            { (Meter.SimpleQuadruple, MetricLevel.Beat), 1 },
            { (Meter.SimpleQuadruple, MetricLevel.D1), 2 },
            { (Meter.SimpleQuadruple, MetricLevel.D1T), 4},
            { (Meter.SimpleQuadruple, MetricLevel.D2), 4},
        };

    }
}





//public enum CellShape { w, dhq, hh, qdh, hqq, qqh, qhq, qqqq, thq, tqh, tqqq, tw, }
//
//public enum CellPosition { One = 1, Two = 2, Thr = 3, For = 4 }

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
