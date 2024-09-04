
using System;
using MusicTheory.Intervals.Arithmetic;

namespace MusicTheory.ScaleDegrees.Arithmetic
{
    public static class ScaleDegreeSystems
    {

        public static IQuality GetQuality(this Intervals.IQuality e) => e switch
        {
            Intervals.Major => new Major(),
            Intervals.Minor => new Minor(),
            Intervals.Augmented => new Augmented(),
            Intervals.Diminished => new Diminished(),
            Intervals.Perfect => new Perfect(),
            _ => throw new ArgumentOutOfRangeException(e.Name)
        };

        public static Intervals.IQuantity GetQuantity(this Notes.INote left, Notes.INote right)
        {
            //UnityEngine.Debug.Log(left.Name + " " + right.Name);
            return ((right.Enum.Letter.Id + 7 - left.Enum.Letter.Id) % 7) switch
            {
                0 => new Intervals.Unison(),
                1 => new Intervals.Second(),
                2 => new Intervals.Third(),
                3 => new Intervals.Fourth(),
                4 => new Intervals.Fifth(),
                5 => new Intervals.Sixth(),
                6 => new Intervals.Seventh(),
                _ => throw new System.ArgumentOutOfRangeException(left.Id.ToString() + " " + right.Id.ToString())
            };
        }

        public static Intervals.IInterval GetInterval(this IScaleDegree left, IScaleDegree right)
        {
            Intervals.IInterval newInterval = new Intervals.P1();
            Intervals.IQuantity quantity = left.GetQuantity(right);
            int id = (right.Id + 12 - left.Id) % 12;

            foreach (var interval in Enumeration.All<Intervals.IntervalEnum>())
            {
                if (interval.Id.Equals(id) &&
                    System.MathF.Abs(interval.Quantity.Id - quantity.Id) <
                    System.MathF.Abs(newInterval.Quantity.Id - quantity.Id))
                    newInterval = interval.GetInterval();
            }

            return newInterval;
        }


        public static ScaleDegreeEnum FindExactMatch(this (QualityEnum quality, DegreeEnums.DegreeEnum degree) i)
        {
            foreach (var e in Enumeration.All<ScaleDegreeEnum>())
                if (e.Degree.Enum == i.degree && e.Quality.Enum == i.quality)
                    return e;
            throw new Exception(i.quality.Name + " " + i.degree.Name);
        }

        public static RomanNumerals.Chromatic.IRomanNumeral ToRoman(this ScaleDegrees.IScaleDegree scaleDegree) =>
            scaleDegree switch
            {
                _1 => new RomanNumerals.Chromatic.I(),
                _2 => new RomanNumerals.Chromatic.II(),
                _3 => new RomanNumerals.Chromatic.III(),
                P4 => new RomanNumerals.Chromatic.IV(),
                P5 => new RomanNumerals.Chromatic.V(),
                _6 => new RomanNumerals.Chromatic.VI(),
                _7 => new RomanNumerals.Chromatic.VII(),
                b2 => new RomanNumerals.Chromatic.bII(),
                b3 => new RomanNumerals.Chromatic.bIII(),
                b4 => new RomanNumerals.Chromatic.III(),
                b5 => new RomanNumerals.Chromatic.bV(),
                b6 => new RomanNumerals.Chromatic.bVI(),
                d7 => new RomanNumerals.Chromatic.VI(),
                b7 => new RomanNumerals.Chromatic.bVII(),
                s2 => new RomanNumerals.Chromatic.bIII(),
                s4 => new RomanNumerals.Chromatic.bV(),
                s5 => new RomanNumerals.Chromatic.bVI(),
                _ => throw new System.ArgumentOutOfRangeException(scaleDegree.Name)
            };

        public static Triads.ITriad GetTriadQuality(this ScaleDegrees.IScaleDegree root, ScaleDegrees.IScaleDegree third, ScaleDegrees.IScaleDegree fifth)
        {
            return (root.GetInterval(third), root.GetInterval(fifth)) switch
            {
                (Intervals.M3, Intervals.P5) => new Triads.Major(),
                (Intervals.mi3, Intervals.P5) => new Triads.Minor(),

                (Intervals.M3, Intervals.A5) => new Triads.Augmented(),
                (Intervals.M3, Intervals.mi6) => new Triads.Augmented(),
                (Intervals.d4, Intervals.mi6) => new Triads.Augmented(),
                (Intervals.d4, Intervals.A5) => new Triads.Augmented(),

                (Intervals.mi3, Intervals.d5) => new Triads.Diminished(),
                (Intervals.mi3, Intervals.A4) => new Triads.Diminished(),
                (Intervals.A2, Intervals.d5) => new Triads.Diminished(),
                (Intervals.A2, Intervals.A4) => new Triads.Diminished(),

                //(Intervals.M2, Intervals.M3) => new Triads.Secundal(),
                //(Intervals.M2, Intervals.d4) => new Triads.Secundal(),

                //(Intervals.M3, Intervals.M6) => new Triads.Quartal(),
                //(Intervals.P4, Intervals.mi7) => new Triads.Quartal(),
                //(Intervals.P4, Intervals.M6) => new Triads.Quartal(),
                //(Intervals.mi3, Intervals.P4) => new Triads.Quartal(),
                //(Intervals.M2, Intervals.P4) => new Triads.Quartal(),
                _ => throw new System.ArgumentOutOfRangeException(root.Name + ", " + root.GetInterval(third) + ", " + third.Name + ", " + root.GetInterval(fifth) + ", " + fifth.Name)
            };
        }

        public static IScaleDegree GetScaleDegree(this ScaleDegreeEnum s) => s switch
        {
            _ when s == ScaleDegreeEnum._1 => new _1(),
            _ when s == ScaleDegreeEnum.b2 => new b2(),
            _ when s == ScaleDegreeEnum._2 => new _2(),
            _ when s == ScaleDegreeEnum.s2 => new s2(),
            _ when s == ScaleDegreeEnum.b3 => new b3(),
            _ when s == ScaleDegreeEnum._3 => new _3(),
            _ when s == ScaleDegreeEnum.b4 => new b4(),
            _ when s == ScaleDegreeEnum.P4 => new P4(),
            _ when s == ScaleDegreeEnum.s4 => new s4(),
            _ when s == ScaleDegreeEnum.b5 => new b5(),
            _ when s == ScaleDegreeEnum.P5 => new P5(),
            _ when s == ScaleDegreeEnum.s5 => new s5(),
            _ when s == ScaleDegreeEnum.b6 => new b6(),
            _ when s == ScaleDegreeEnum._6 => new _6(),
            _ when s == ScaleDegreeEnum.d7 => new d7(),
            _ when s == ScaleDegreeEnum.b7 => new b7(),
            _ when s == ScaleDegreeEnum._7 => new _7(),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        public static IScaleDegree GetScaleDegree(this Intervals.IInterval s) => s switch
        {
            Intervals.P1 or Intervals.P8 => new _1(),
            Intervals.mi2 => new b2(),
            Intervals.M2 => new _2(),
            Intervals.A2 => new s2(),
            Intervals.mi3 => new b3(),
            Intervals.M3 => new _3(),
            Intervals.d4 => new b4(),
            Intervals.P4 => new P4(),
            Intervals.A4 => new s4(),
            Intervals.d5 => new b5(),
            Intervals.P5 => new P5(),
            Intervals.A5 => new s5(),
            Intervals.mi6 => new b6(),
            Intervals.M6 => new _6(),
            Intervals.d7 => new d7(),
            Intervals.mi7 => new b7(),
            Intervals.M7 => new _7(),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        public static IQuality GetQuality(this QualityEnum e) => e switch
        {
            _ when e == QualityEnum.Major => new Major(),
            _ when e == QualityEnum.Minor => new Minor(),
            _ when e == QualityEnum.Augmented => new Augmented(),
            _ when e == QualityEnum.Diminished => new Diminished(),
            _ when e == QualityEnum.Perfect => new Perfect(),
            _ => throw new System.ArgumentOutOfRangeException()
        };

    }
}