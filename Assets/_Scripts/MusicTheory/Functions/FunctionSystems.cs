using System;

namespace MusicTheory.Functions.Harmonic.Arithmetic
{
    public static class FunctionSystems
    {
        public static IFunction[] DiatonicToHarmonicFunctionCadence(this RomanNumerals.Diatonic.IRomanNumeral[] cadence)
        {
            var cad = new IFunction[cadence.Length];
            for (int i = 0; i < cad.Length; i++) cad[i] = cadence[i].ToHarmonicFunction();
            return cad;
        }

        public static IFunction ToHarmonicFunction(this RomanNumerals.Diatonic.IRomanNumeral rn)
        {
            return rn switch
            {
                RomanNumerals.Diatonic.I or RomanNumerals.Diatonic.III or RomanNumerals.Diatonic.VI => new Tonic(),
                RomanNumerals.Diatonic.II or RomanNumerals.Diatonic.IV => new Subdominant(),
                _ => new Dominant()
            };
        }

        public static IFunction ToFunction(this FunctionEnum @enum) => @enum switch
        {
            _ when @enum == FunctionEnum.Tonic => new Tonic(),
            _ when @enum == FunctionEnum.Subdominant => new Subdominant(),
            _ when @enum == FunctionEnum.Dominant => new Dominant(),
            _ => throw new Exception(@enum.Name),
        };
    }
}
