using System.Collections.Generic;
using MusicTheory.Rhythms;

namespace Batterie
{
    public static class CountOff
    {
        public static Note[] GetNotes(this Time t) => t switch
        {
            TwoTwo => new Note[]
            {
                new () { QuantizedRhythmicValue = RhythmicValue.Half },
                new () { QuantizedRhythmicValue = RhythmicValue.Half },
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter, TiedFrom = true},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter, TiedFrom = true},
            },

            TwelveEight or SixEight => new Note[]
            {
                new () { QuantizedRhythmicValue = RhythmicValue.DotQuarter },
                new () { QuantizedRhythmicValue = RhythmicValue.DotQuarter },
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
            },

            SevenFour34 or SevenFour43 => new Note[]
            {
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
            },

            SevenEight34 or SevenEight43 => new Note[]
            {
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
            },

            FiveFour23 or FiveFour32 => new Note[]
            {
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
            },

            FiveEight23 or FiveEight32 => new Note[]
            {
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
            },

            ThreeFour or SixFour or ThreeEight => new Note[]
               {
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new () { QuantizedRhythmicValue = RhythmicValue.Quarter},
               },

            ThreeTwo => new Note[]
                {
                new () { QuantizedRhythmicValue = RhythmicValue.Half},
                new () { QuantizedRhythmicValue = RhythmicValue.Half},
                new () { QuantizedRhythmicValue = RhythmicValue.Half},
                new () { QuantizedRhythmicValue = RhythmicValue.Half},
                new () { QuantizedRhythmicValue = RhythmicValue.Half},
                new () { QuantizedRhythmicValue = RhythmicValue.Half},
                },

            _ => new Note[]
                {
                  new () { QuantizedRhythmicValue = RhythmicValue.Quarter },
                  new () { QuantizedRhythmicValue = RhythmicValue.Quarter },
                  new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
                  new () { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                  new () { QuantizedRhythmicValue = RhythmicValue.Eighth},
                  new () { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                },

        };


        static public (int, string)[] GetCounts(this Time ts)
        {
            return ts switch
            {
                ThreeEight or ThreeFour or ThreeTwo =>
                     new (int, string)[] { (0, "ONE"), (1, "TWO"), (2, "THREE"), (3, "ONE"), (4, "READY"), (5, "GO") },

                NineEight =>
                    new (int, string)[] { (0, "ONE"), (1, "READY"), (2, "GO") },

                FiveEight23 or FiveEight32 or FiveFour23 or FiveFour32 =>
                    new (int, string)[] { (0, "ONE"), (1, "TWO"), (2, "THREE"), (3, "READY"), (4, "GO") },

                SevenEight43 or SevenEight34 or SevenFour43 or SevenFour34 =>
                    new (int, string)[] { (0, "ONE"), (1, "TWO"), (2, "THREE"), (3, "FOUR"), (4, "FIVE"), (5, "READY"), (6, "GO") },

                _ => new (int, string)[] { (0, "ONE"), (1, "TWO"), (2, "READY"), (3, "GO") },
            };
        }
    }
}
