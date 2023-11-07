using System.Collections.Generic;
using Musica.Rhythms;

namespace Batterie
{
    public static class CountOff
    {
        public static Note[] GetNotes(this Time t) => t switch
        {
            TwoTwo => new Note[]
            {
                new Note() { QuantizedRhythmicValue = RhythmicValue.Half },
                new Note() { QuantizedRhythmicValue = RhythmicValue.Half },
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter, TiedFrom = true},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter, TiedFrom = true},
            },

            TwelveEight or SixEight => new Note[]
            {
                new Note() { QuantizedRhythmicValue = RhythmicValue.DotQuarter },
                new Note() { QuantizedRhythmicValue = RhythmicValue.DotQuarter },
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
            },

            SevenFour34 or SevenFour43 => new Note[]
            {
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
            },

            SevenEight34 or SevenEight43 => new Note[]
            {
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
            },

            FiveFour23 or FiveFour32 => new Note[]
            {
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
            },

            FiveEight23 or FiveEight32 => new Note[]
            {
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
            },

            ThreeFour or SixFour or ThreeEight => new Note[]
               {
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
               },

            ThreeTwo => new Note[]
                {
                new Note() { QuantizedRhythmicValue = RhythmicValue.Half},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Half},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Half},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Half},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Half},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Half},
                },

            _ => new Note[]
                {
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter },
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter },
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
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


/*
 * 
        static public List<Note> GetNotes(this TimeSignature ts)
        {
            return ts switch
            {
                _ when ts == TimeSignature.TwelveEight || ts == TimeSignature.SixEight => new()
                {
                new Note() { QuantizedRhythmicValue = RhythmicValue.DotQuarter },
                new Note() { QuantizedRhythmicValue = RhythmicValue.DotQuarter },
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                },

                _ when ts == TimeSignature.NineEight => new()
                {
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                },

                //_ when ts == TimeSignature.ThreeEight => new()
                //{
                //    new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                //    new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                //    new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                //    new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                //    new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                //    new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                //},

                _ when ts == TimeSignature.SevenFour34 || ts == TimeSignature.SevenFour43 => new()
                {
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                },

                _ when ts == TimeSignature.SevenEight34 || ts == TimeSignature.SevenEight43 => new()
                {
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                },

                _ when ts == TimeSignature.FiveFour23 || ts == TimeSignature.FiveFour32 ||
                       ts == TimeSignature.FiveEight23 || ts == TimeSignature.FiveEight32 => new()
                {
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                },

                _ when ts == TimeSignature.ThreeFour || ts == TimeSignature.SixFour ||
                       ts == TimeSignature.ThreeEight => new()
                {
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                },


                _ when ts == TimeSignature.TwoTwo => new()
                {
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Half },
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Half },
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter, TiedFrom = true},
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter},
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter, TiedFrom = true},
                },

                _ when ts == TimeSignature.ThreeTwo => new()
                {
                new Note() { QuantizedRhythmicValue = RhythmicValue.Half},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Half},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Half},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Half},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Half},
                new Note() { QuantizedRhythmicValue = RhythmicValue.Half},
                },

                _ => new()
                {
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter },
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Quarter },
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth},
                  new Note() { QuantizedRhythmicValue = RhythmicValue.Eighth, TiedFrom = true},
                },

            };
        }

 * 
 */