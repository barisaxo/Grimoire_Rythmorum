using System;
using MusicTheory.Notes;
using MusicTheory.Functions;

namespace MusicTheory.RomanNumerals.Diatonic.Arithmetic
{
    public static class RomanNumeralSystems
    {
        public static Functions.Diatonic.IFunction ToDiatonicFunction(this IRomanNumeral drn) =>
            DiatonicRomanToDiatonicFunction(drn);

        public static Functions.Diatonic.IFunction DiatonicRomanToDiatonicFunction(IRomanNumeral drn) => drn switch
        {
            I => new Functions.Diatonic.Tonic(),
            II => new Functions.Diatonic.LateralSubDominant(),
            III => new Functions.Diatonic.MediantTonic(),
            IV => new Functions.Diatonic.Subdominant(),
            V => new Functions.Diatonic.Dominant(),
            VI => new Functions.Diatonic.SubmediantTonic(),
            VII => new Functions.Diatonic.LateralDominant(),
            _ => throw new System.Exception(drn.Name)
        };


        public static IRomanNumeral GetRoman(this RomanNumeralEnum rne)
        {
            return rne switch
            {
                _ when rne == RomanNumeralEnum.I => new I(),
                _ when rne == RomanNumeralEnum.II => new II(),
                _ when rne == RomanNumeralEnum.III => new III(),
                _ when rne == RomanNumeralEnum.IV => new IV(),
                _ when rne == RomanNumeralEnum.V => new V(),
                _ when rne == RomanNumeralEnum.VI => new VI(),
                _ when rne == RomanNumeralEnum.VII => new VII(),
                _ => throw new System.Exception(rne.Name)
            };
        }



    }
}


namespace MusicTheory.RomanNumerals.Chromatic.Arithmetic
{
    public static class RomanNumeralSystems
    {

        public static IRomanNumeral ToChromaticRoman(this Diatonic.IRomanNumeral dc) =>
            DiatonicToChromaticRoman(dc);

        public static IRomanNumeral DiatonicToChromaticRoman(Diatonic.IRomanNumeral dc) => dc switch
        {
            Diatonic.I => new I(),
            Diatonic.II => new II(),
            Diatonic.III => new III(),
            Diatonic.IV => new IV(),
            Diatonic.V => new V(),
            Diatonic.VI => new VI(),
            Diatonic.VII => new VII(),
            _ => throw new Exception(dc.Name)
        };



    }

}