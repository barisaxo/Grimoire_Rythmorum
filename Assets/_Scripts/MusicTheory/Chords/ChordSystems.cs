//using MusicTheory.Scales;
//using MusicTheory.Keys;
//using MusicTheory.Intervals;
//using MusicTheory.Chords;
using MusicTheory.RomanNumerals.Diatonic;
using MusicTheory.Triads;
//using MusicTheory.ScaleDegrees;

namespace MusicTheory.Triads.Arithmetic
{
    public static class TriadSystems
    {
        public static ITriad GetTriad(this IRomanNumeral romanNumeral) => romanNumeral switch
        {
            I or IV or V => new Major(),
            II or III or VI => new Minor(),
            VII => new Diminished(),
            _ => throw new System.Exception(romanNumeral.Name)
        };

        //public static Triad GetTriad(this (Interval third, Interval fifth) notes)
        //{
        //    return notes.fifth switch
        //    {
        //        Intervals.P5 => notes.third switch
        //        {
        //            Intervals.M3 => new Triads.Major(),
        //            Intervals.mi3 => new Triads.Minor(),
        //            _ => throw new System.ArgumentOutOfRangeException()
        //        },
        //        Intervals.TT => notes.third switch
        //        {
        //            Intervals.mi3 => new Triads.Diminished(),
        //            _ => throw new System.ArgumentOutOfRangeException()
        //        },
        //        Intervals.mi6 => notes.third switch
        //        {
        //            Intervals.M3 => new Triads.Augmented(),
        //            _ => throw new System.ArgumentOutOfRangeException()
        //        },
        //        _ => throw new System.ArgumentOutOfRangeException()
        //    };
        //}
    }


}