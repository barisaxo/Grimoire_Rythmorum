using System;

namespace MusicTheory.Intervals.Arithmetic
{
    public static class IntervalArithmetic
    {
        public static IQuality ScaleDegreeQualityToIntervalQuality(this ScaleDegrees.IQuality quality)
             => quality switch
             {
                 ScaleDegrees.Major => new Major(),
                 ScaleDegrees.Minor => new Minor(),
                 ScaleDegrees.Augmented => new Augmented(),
                 ScaleDegrees.Diminished => new Diminished(),
                 ScaleDegrees.Perfect => new Perfect(),
                 _ => throw new Exception(quality.Name)
             };

        public static IQuantity ScaleDegreeQuantityToIntervalQuantity(this ScaleDegrees.DegreeEnums.IDegree sd)
            => sd switch
            {
                ScaleDegrees.DegreeEnums._1 => new Unison(),
                ScaleDegrees.DegreeEnums._2 => new Second(),
                ScaleDegrees.DegreeEnums._3 => new Third(),
                ScaleDegrees.DegreeEnums._4 => new Fourth(),
                ScaleDegrees.DegreeEnums._5 => new Fifth(),
                ScaleDegrees.DegreeEnums._6 => new Sixth(),
                ScaleDegrees.DegreeEnums._7 => new Seventh(),
                _ => throw new Exception(sd.Name)
            };

        public static IInterval AsInterval(this ScaleDegrees.IScaleDegree scaleDegree) =>
         IntervalEnum.Find(
             (scaleDegree.Enum.Quality.ScaleDegreeQualityToIntervalQuality(),
              scaleDegree.Enum.Degree.ScaleDegreeQuantityToIntervalQuantity())).GetInterval();

        public static IInterval AsInterval(this Steps.IStep step) =>
             step switch
             {
                 Steps.Half => new mi2(),
                 Steps.Whole => new M2(),
                 Steps.Skip => new A2(),

                 _ => throw new ArgumentOutOfRangeException()
             };

        public static IQuantity Invert(this IQuantity quantity) => quantity switch
        {
            Unison => new Octave(),
            Second => new Seventh(),
            Third => new Sixth(),
            Fourth => new Fifth(),
            Fifth => new Fourth(),
            Sixth => new Third(),
            Seventh => new Second(),
            Octave => new Unison(),
            _ => throw new ArgumentOutOfRangeException(nameof(IQuantity), quantity.ToString())
        };

        public static IQuality Invert(this IQuality quality) => quality switch
        {
            Major => new Minor(),
            Minor => new Major(),
            Augmented => new Diminished(),
            Diminished => new Augmented(),
            Perfect => quality,
            _ => throw new ArgumentOutOfRangeException(nameof(IQuality), quality.ToString())
        };

        public static IInterval Invert(this IInterval i) =>
            IntervalEnum.Find((i.Quality.Invert(), i.Quantity.Invert())).GetInterval();

        public static RomanNumerals.Chromatic.IRomanNumeral ToChromaticRoman(this IInterval interval) =>
            interval switch
            {
                P1 => new RomanNumerals.Chromatic.I(),
                M2 => new RomanNumerals.Chromatic.II(),
                M3 => new RomanNumerals.Chromatic.III(),
                P4 => new RomanNumerals.Chromatic.IV(),
                P5 => new RomanNumerals.Chromatic.V(),
                M6 => new RomanNumerals.Chromatic.VI(),
                M7 => new RomanNumerals.Chromatic.VII(),
                mi2 => new RomanNumerals.Chromatic.bII(),
                mi3 => new RomanNumerals.Chromatic.bIII(),
                d4 => new RomanNumerals.Chromatic.III(),
                d5 => new RomanNumerals.Chromatic.bV(),
                mi6 => new RomanNumerals.Chromatic.bVI(),
                d7 => new RomanNumerals.Chromatic.VI(),
                mi7 => new RomanNumerals.Chromatic.bVII(),
                A2 => new RomanNumerals.Chromatic.bIII(),
                A4 => new RomanNumerals.Chromatic.bV(),
                A5 => new RomanNumerals.Chromatic.bVI(),
                _ => throw new System.ArgumentOutOfRangeException(interval.Name)
            };

        public static RomanNumerals.Diatonic.IRomanNumeral ToDiatonicRoman(this IInterval interval) =>
            interval switch
            {
                P1 => new RomanNumerals.Diatonic.I(),
                M2 => new RomanNumerals.Diatonic.II(),
                M3 => new RomanNumerals.Diatonic.III(),
                P4 => new RomanNumerals.Diatonic.IV(),
                P5 => new RomanNumerals.Diatonic.V(),
                M6 => new RomanNumerals.Diatonic.VI(),
                M7 => new RomanNumerals.Diatonic.VII(),
                _ => throw new System.ArgumentOutOfRangeException(interval.Name + " is not diatonic")
            };

        public static IInterval GetInterval(this IntervalEnum e) => e switch
        {
            _ when e == IntervalEnum.P1 => new P1(),
            _ when e == IntervalEnum.mi2 => new mi2(),
            _ when e == IntervalEnum.M2 => new M2(),
            _ when e == IntervalEnum.A2 => new A2(),
            _ when e == IntervalEnum.mi3 => new mi3(),
            _ when e == IntervalEnum.M3 => new M3(),
            _ when e == IntervalEnum.d4 => new d4(),
            _ when e == IntervalEnum.P4 => new P4(),
            _ when e == IntervalEnum.A4 => new A4(),
            _ when e == IntervalEnum.d5 => new d5(),
            _ when e == IntervalEnum.P5 => new P5(),
            _ when e == IntervalEnum.A5 => new A5(),
            _ when e == IntervalEnum.mi6 => new mi6(),
            _ when e == IntervalEnum.M6 => new M6(),
            _ when e == IntervalEnum.d7 => new d7(),
            _ when e == IntervalEnum.mi7 => new mi7(),
            _ when e == IntervalEnum.M7 => new M7(),
            _ when e == IntervalEnum.P8 => new P8(),
            _ => throw new ArgumentOutOfRangeException(e.Name)
        };

        public static IQuantity GetQuantity(this QuantityEnum e) => e switch
        {
            _ when e == QuantityEnum.Unison => new Unison(),
            _ when e == QuantityEnum.Second => new Second(),
            _ when e == QuantityEnum.Third => new Third(),
            _ when e == QuantityEnum.Fourth => new Fourth(),
            _ when e == QuantityEnum.Fifth => new Fifth(),
            _ when e == QuantityEnum.Sixth => new Sixth(),
            _ when e == QuantityEnum.Seventh => new Seventh(),
            _ when e == QuantityEnum.Octave => new Octave(),
            _ => throw new ArgumentOutOfRangeException(e.Name)
        };

        public static IQuantity GetQuantity(this Notes.INote left, Notes.INote right)
        {
            return (right.Id + 7 - left.Id).Smod(ScaleDegrees.ScaleDegreeEnum.Count) switch
            {
                0 => QuantityEnum.Unison.GetQuantity(),
                1 => QuantityEnum.Second.GetQuantity(),
                2 => QuantityEnum.Third.GetQuantity(),
                3 => QuantityEnum.Fourth.GetQuantity(),
                4 => QuantityEnum.Fifth.GetQuantity(),
                5 => QuantityEnum.Sixth.GetQuantity(),
                6 => QuantityEnum.Seventh.GetQuantity(),
                _ => throw new System.ArgumentOutOfRangeException()
            };
        }

        public static IQuality GetQuality(this QualityEnum e) => e switch
        {
            _ when e == QualityEnum.Major => new Major(),
            _ when e == QualityEnum.Minor => new Minor(),
            _ when e == QualityEnum.Augmented => new Augmented(),
            _ when e == QualityEnum.Diminished => new Diminished(),
            _ when e == QualityEnum.Perfect => new Perfect(),
            _ => throw new ArgumentOutOfRangeException(e.Name)
        };

        public static IQuality GetQuality(this ScaleDegrees.IQuality e) => e switch
        {
            ScaleDegrees.Major => new Major(),
            ScaleDegrees.Minor => new Minor(),
            ScaleDegrees.Augmented => new Augmented(),
            ScaleDegrees.Diminished => new Diminished(),
            ScaleDegrees.Perfect => new Perfect(),
            _ => throw new ArgumentOutOfRangeException(e.Name)
        };


        public static QualityEnum GetQualityEnum(this ScaleDegrees.IQuality quality) => quality switch
        {
            ScaleDegrees.Major => QualityEnum.Major,
            ScaleDegrees.Minor => QualityEnum.Minor,
            ScaleDegrees.Augmented => QualityEnum.Augmented,
            ScaleDegrees.Diminished => QualityEnum.Diminished,
            ScaleDegrees.Perfect => QualityEnum.Perfect,
            _ => throw new ArgumentOutOfRangeException()
        };

        public static IQuantity GetQuantity(this ScaleDegrees.IScaleDegree left, ScaleDegrees.IScaleDegree right)
        {
            return (right.Enum.Degree.Id + 7 - left.Enum.Degree.Id).Smod(ScaleDegrees.ScaleDegreeEnum.Count) switch
            {
                0 => QuantityEnum.Unison.GetQuantity(),
                1 => QuantityEnum.Second.GetQuantity(),
                2 => QuantityEnum.Third.GetQuantity(),
                3 => QuantityEnum.Fourth.GetQuantity(),
                4 => QuantityEnum.Fifth.GetQuantity(),
                5 => QuantityEnum.Sixth.GetQuantity(),
                6 => QuantityEnum.Seventh.GetQuantity(),
                _ => throw new System.ArgumentOutOfRangeException()
            };
        }

    }
}



//public static Interval GetInterval(int i) => ((IntervalEnum)i).ToClass();

//public static Interval ToClass(this IntervalEnum ie) =>
//    (int)ie > 12 ? throw new ArgumentOutOfRangeException(nameof(IntervalEnum), "Interval must not be greater than 12") : new Interval(ie);

//public static Interval Invert(this Interval i) => i switch
//{
//    P1 => new P8(),
//    mi2 => new M7(),
//    M2 => new mi7(),
//    mi3 => new M6(),
//    M3 => new mi6(),
//    P4 => new P5(),
//    TT => i,
//    P5 => new P4(),
//    mi6 => new M3(),
//    M6 => new mi3(),
//    mi7 => new M2(),
//    M7 => new mi2(),
//    P8 => new P1(),
//    _ => throw new ArgumentOutOfRangeException(nameof(IntervalEnum), "Interval must not be greater than 12")
//};