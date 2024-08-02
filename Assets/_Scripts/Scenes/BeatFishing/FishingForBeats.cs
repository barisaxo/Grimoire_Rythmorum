using System;
using System.Collections.Generic;
using MusicTheory.Rhythms;
using Datum.BeatFishing;

public static class FishingForBeats
{
    public static Note[] BeatsFromTimeSig(MusicTheory.Rhythms.Time signature, int measures) => signature switch
    {
        ThreeFour => GetNotes(RhythmicValue.Quarter, 3 * measures),
        FourFour => GetNotes(RhythmicValue.Quarter, 4 * measures),
        FiveFour23 or FiveFour32 => GetNotes(RhythmicValue.Quarter, 5 * measures),
        SixEight => GetNotes(RhythmicValue.DotEighth, 2 * measures),
        NineEight => GetNotes(RhythmicValue.DotEighth, 3 * measures),
        TwelveEight => GetNotes(RhythmicValue.DotEighth, 4 * measures),
        _ => throw new ArgumentOutOfRangeException(signature.ToString())
    };

    public static Note[] GetBeats(IBeatFishingPractice item, int measures)
    {
        List<Note> notes = new();
        switch (item)
        {
            case QQQQ:
                for (int i = 0; i < measures; i++)
                    for (int ii = 0; ii < 4; ii++)
                        notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.Quarter });
                break;

            case HH:
                for (int i = 0; i < measures; i++)
                    for (int ii = 0; ii < 2; ii++)
                        notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.Half });
                break;

            case W:
                for (int i = 0; i < measures; i++)
                    notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.Whole });
                break;

            case HQQ:
                for (int i = 0; i < measures; i++)
                {
                    notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.Half });
                    notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.Quarter });
                    notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.Quarter });
                }
                break;

            case QQH:
                for (int i = 0; i < measures; i++)
                {
                    notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.Quarter });
                    notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.Quarter });
                    notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.Half });
                }
                break;

            case QHQ:
                for (int i = 0; i < measures; i++)
                {
                    notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.Quarter });
                    notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.Half });
                    notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.Quarter });
                }
                break;

            case DHQ:
                for (int i = 0; i < measures; i++)
                {
                    notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.DotHalf });
                    notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.Quarter });
                }
                break;

            case QDH:
                for (int i = 0; i < measures; i++)
                {
                    notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.Quarter });
                    notes.Add(new() { QuantizedRhythmicValue = RhythmicValue.DotHalf });
                }
                break;
        };

        return notes.ToArray();
    }

    public static Measure[] GetMeasures(IBeatFishingPractice item)
    {
        Measure[] rhythm = new Measure[1] {
            new (){Cells = new RhythmCell[1]{
                new RhythmCell()
                    .SetMetricLevel(MetricLevel.Beat)
                    .SetQuantizement(Quantizement.Quarter)
                    .SetRhythmicShape(ItemToCellShape(item))
                    .SetCount(1)
        }}};
        return rhythm;
    }

    public static CellShape ItemToCellShape(IBeatFishingPractice item) => item switch
    {
        QQQQ => CellShape.SSSS,
        HH => CellShape.LL,
        W => CellShape.L,
        HQQ => CellShape.LSS,
        QQH => CellShape.SSL,
        QHQ => CellShape.SLS,
        DHQ => CellShape.LS,
        QDH => CellShape.SL,
        _ => throw new System.Exception(item.Name)
    };

    public static Note[] GetNotes(RhythmicValue noteValue, int numOfNotes)
    {
        List<Note> notes = new();

        for (int i = 0; i < numOfNotes; i++)
            notes.Add(new() { QuantizedRhythmicValue = noteValue });

        return notes.ToArray();
    }




}

/*

 QQQQ
 HH  
 W   
 HQQ 
 QQH 
 DHQ 
 QDH 
 QHQ 
 
 */