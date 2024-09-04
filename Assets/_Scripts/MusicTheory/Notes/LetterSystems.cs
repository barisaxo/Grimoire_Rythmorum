

namespace MusicTheory.Notes.Arithmetic
{
    public static class LetterArithmetic
    {
        public static ILetter GetLetterAbove(this INote key, ScaleDegrees.IScaleDegree degree) =>
            Enumeration.FindId<LetterEnum>((key.Enum.Letter.Id + degree.Enum.Degree.Id).Smod(LetterEnum.Count)).GetLetter();

        public static ILetter GetLetterAbove(this INote key, Intervals.IInterval interval) =>
            Enumeration.FindId<LetterEnum>((key.Enum.Letter.Id + interval.Enum.Quantity.Id).Smod(LetterEnum.Count)).GetLetter();

        public static ILetter GetLetterAbove(this INote key, RomanNumerals.Diatonic.IRomanNumeral rn) =>
            Enumeration.FindId<LetterEnum>((key.Enum.Letter.Id + rn.Id).Smod(LetterEnum.Count)).GetLetter();

        public static ILetter GetLetter(this LetterEnum e) => e switch
        {
            _ when e == LetterEnum.A => new _A(),
            _ when e == LetterEnum.B => new _B(),
            _ when e == LetterEnum.C => new _C(),
            _ when e == LetterEnum.D => new _D(),
            _ when e == LetterEnum.E => new _E(),
            _ when e == LetterEnum.F => new _F(),
            _ when e == LetterEnum.G => new _G(),
            _ => throw new System.ArgumentOutOfRangeException(e.ToString())
        };

    }
}
