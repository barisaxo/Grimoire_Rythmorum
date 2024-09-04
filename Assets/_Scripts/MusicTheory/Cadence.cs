using System;
using UnityEngine;
// using OLD;
using MusicTheory.Functions.Harmonic;
using MusicTheory.RomanNumerals.Diatonic;
using Musica;

namespace MusicTheory
{
    public static class Cadence
    {
        public static IRomanNumeral[] RandomCadence(this RegionalMode shipRegion, CadenceDifficulty difficulty)
        {
            IFunction f1 = shipRegion switch
            {
                RegionalMode.Dorian or RegionalMode.Lydian => new Subdominant(),
                RegionalMode.MixoLydian or RegionalMode.Locrian => new Dominant(),
                _ => new Tonic(),
            };
            IFunction f2 = FunctionEnum.GetRandomFunction;
            IFunction f3 = ThirdChord();
            IFunction f4 = FourthChord();

            return NewChordalCadence(new IFunction[4] { f1, f2, f3, f4 }, shipRegion);

            IFunction ThirdChord()
            {
                int ton = 0, ant = 0, dom = 0;

                TallyFunctionsUsed(f1);
                TallyFunctionsUsed(f2);

                if (ton == 2) { return UnityEngine.Random.value < (1f / 2f) ? new Subdominant() : new Dominant(); }
                if (ant == 2) { return UnityEngine.Random.value < (1f / 2f) ? new Tonic() : new Dominant(); }
                if (dom == 2) { return UnityEngine.Random.value < (1f / 2f) ? new Tonic() : new Subdominant(); }
                return FunctionEnum.GetRandomFunction;

                int TallyFunctionsUsed(IFunction f) => f switch
                {
                    Tonic => ton++,
                    Subdominant => ant++,
                    Dominant => dom++,
                    _ => 0
                };

            }

            IFunction FourthChord()
            {
                int ton = 0, ant = 0, dom = 0;

                TallyFunctionsUsed(f1);
                TallyFunctionsUsed(f2);
                TallyFunctionsUsed(f3);

                if (ton == 2) { return UnityEngine.Random.value < (1f / 2f) ? new Subdominant() : new Dominant(); }
                if (ant == 2) { return UnityEngine.Random.value < (1f / 2f) ? new Tonic() : new Dominant(); }
                if (dom == 2) { return UnityEngine.Random.value < (1f / 2f) ? new Tonic() : new Subdominant(); }
                return FunctionEnum.GetRandomFunction;

                int TallyFunctionsUsed(IFunction f) => f switch
                {
                    Tonic => ton++,
                    Subdominant => ant++,
                    Dominant => dom++,
                    _ => 0
                };
            }

            IRomanNumeral[] NewChordalCadence(IFunction[] functionalCadence, RegionalMode level)
            {
                IRomanNumeral[] ChordalCadence = new IRomanNumeral[4];

                ChordalCadence[0] = level switch
                {
                    RegionalMode.Ionian => new I(),
                    RegionalMode.Dorian => new II(),
                    RegionalMode.Phrygian => new III(),
                    RegionalMode.Lydian => new IV(),
                    RegionalMode.MixoLydian => new V(),
                    RegionalMode.Aeolian => new VI(),
                    RegionalMode.Locrian => new VII(),
                    _ => throw new Exception(level.ToString())
                };

                for (int f = 1; f < functionalCadence.Length; f++)
                {
                    ChordalCadence[f] = NewChord(functionalCadence[f]);
                }

                return ChordalCadence;

                IRomanNumeral NewChord(IFunction f) => f switch
                {
                    Tonic =>
                    difficulty switch
                    {
                        CadenceDifficulty.I_II_V or CadenceDifficulty.I_IV_V => new I(),
                        CadenceDifficulty.I_VI_II_V => UnityEngine.Random.value < (1f / 2f) ? new I() : new VI(),
                        CadenceDifficulty.III_VI_II_V => UnityEngine.Random.value < (1f / 2f) ? new III() : new VI(),
                        _ => UnityEngine.Random.Range(0, 3) switch { 0 => new I(), 1 => new VI(), _ => new III() }
                    },


                    Subdominant => difficulty switch
                    {
                        CadenceDifficulty.I_II_V or CadenceDifficulty.III_VI_II_V or CadenceDifficulty.I_VI_II_V => new II(),
                        CadenceDifficulty.I_IV_V => new IV(),
                        _ => UnityEngine.Random.value < (1f / 2f) ? new II() : new IV(),
                    },


                    _ => difficulty switch
                    {
                        CadenceDifficulty.ALL => UnityEngine.Random.value < (1f / 2f) ? new V() : new VII(),
                        _ => new V()
                    },

                };
            }
        }


    }
}