using System;
using UnityEngine;

namespace Musica
{
    public static class Cadence
    {
        public static DiatonicRomanNumeral[] RandomCadence(this RegionalMode shipRegion, CadenceDifficulty difficulty)
        {
            // HarmonicFunction f1 = (HarmonicFunction)(UnityEngine.Random.value * FunctionCount());
            HarmonicFunction f1 = shipRegion switch
            {
                RegionalMode.Dorian => HarmonicFunction.Predominant,
                RegionalMode.Lydian => HarmonicFunction.Predominant,
                RegionalMode.MixoLydian => HarmonicFunction.Dominant,
                RegionalMode.Locrian => HarmonicFunction.Dominant,
                _ => HarmonicFunction.Tonic,
            };
            HarmonicFunction f2 = (HarmonicFunction)(UnityEngine.Random.value * FunctionCount());
            HarmonicFunction f3 = ThirdChord();
            HarmonicFunction f4 = FourthChord();

            return NewChordalCadence(new HarmonicFunction[4] { f1, f2, f3, f4 }, shipRegion);

            HarmonicFunction ThirdChord()
            {
                int ton = 0, ant = 0, dom = 0;

                TallyFunctionsUsed(f1);
                TallyFunctionsUsed(f2);

                if (ton == 2) { return UnityEngine.Random.value < (1f / 2f) ? HarmonicFunction.Predominant : HarmonicFunction.Dominant; }
                if (ant == 2) { return UnityEngine.Random.value < (1f / 2f) ? HarmonicFunction.Tonic : HarmonicFunction.Dominant; }
                if (dom == 2) { return UnityEngine.Random.value < (1f / 2f) ? HarmonicFunction.Tonic : HarmonicFunction.Predominant; }
                return (HarmonicFunction)(UnityEngine.Random.value * FunctionCount());

                int TallyFunctionsUsed(HarmonicFunction f) => f switch
                {
                    HarmonicFunction.Tonic => ton++,
                    HarmonicFunction.Predominant => ant++,
                    HarmonicFunction.Dominant => dom++,
                    _ => 0
                };

            }

            HarmonicFunction FourthChord()
            {
                int ton = 0, ant = 0, dom = 0;

                TallyFunctionsUsed(f1);
                TallyFunctionsUsed(f2);
                TallyFunctionsUsed(f3);

                if (ton == 2) { return UnityEngine.Random.value < (1f / 2f) ? HarmonicFunction.Predominant : HarmonicFunction.Dominant; }
                if (ant == 2) { return UnityEngine.Random.value < (1f / 2f) ? HarmonicFunction.Tonic : HarmonicFunction.Dominant; }
                if (dom == 2) { return UnityEngine.Random.value < (1f / 2f) ? HarmonicFunction.Tonic : HarmonicFunction.Predominant; }
                return (HarmonicFunction)(UnityEngine.Random.value * FunctionCount());

                int TallyFunctionsUsed(HarmonicFunction f) => f switch
                {
                    HarmonicFunction.Tonic => ton++,
                    HarmonicFunction.Predominant => ant++,
                    HarmonicFunction.Dominant => dom++,
                    _ => 0
                };
            }

            DiatonicRomanNumeral[] NewChordalCadence(HarmonicFunction[] functionalCadence, RegionalMode level)
            {
                DiatonicRomanNumeral[] ChordalCadence = new DiatonicRomanNumeral[4];

                ChordalCadence[0] = level switch
                {
                    RegionalMode.Dorian => DiatonicRomanNumeral.II,
                    RegionalMode.Phrygian => DiatonicRomanNumeral.III,
                    RegionalMode.Lydian => DiatonicRomanNumeral.IV,
                    RegionalMode.MixoLydian => DiatonicRomanNumeral.V,
                    RegionalMode.Aeolian => DiatonicRomanNumeral.VI,
                    RegionalMode.Locrian => DiatonicRomanNumeral.VII,
                    _ => DiatonicRomanNumeral.I,
                };

                for (int f = 1; f < functionalCadence.Length; f++)
                {
                    ChordalCadence[f] = NewChord(functionalCadence[f]);
                }

                return ChordalCadence;

                DiatonicRomanNumeral NewChord(HarmonicFunction f) => f switch
                {
                    HarmonicFunction.Tonic =>
                    difficulty switch
                    {
                        CadenceDifficulty.I_II_V => DiatonicRomanNumeral.I,
                        CadenceDifficulty.I_IV_V_VI => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.I : DiatonicRomanNumeral.VI,
                        // CadenceDifficulty.LVL3 => UnityEngine.Random.Range(0, 3) switch { 0 => DiatonicRomanNumeral.I, 1 => DiatonicRomanNumeral.VI, _ => DiatonicRomanNumeral.III },
                        // // CadenceDifficulty.LVL4 => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.III : DiatonicRomanNumeral.VI,
                        // CadenceDifficulty.LVL5 => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.I : DiatonicRomanNumeral.VI,
                        _ => UnityEngine.Random.Range(0, 3) switch { 0 => DiatonicRomanNumeral.I, 1 => DiatonicRomanNumeral.VI, _ => DiatonicRomanNumeral.III }
                    },
                    // level switch
                    // {

                    // RegionalMode.Ionic => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.I : DiatonicRomanNumeral.III,
                    // RegionalMode.Doric => DiatonicRomanNumeral.I,
                    // RegionalMode.Phrygic => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.III : DiatonicRomanNumeral.VI,
                    // RegionalMode.Lydic => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.III : DiatonicRomanNumeral.VI,
                    // RegionalMode.MixoLydic => DiatonicRomanNumeral.I,
                    // RegionalMode.Aeolic => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.I : DiatonicRomanNumeral.VI,
                    // RegionalMode.Locric => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.III : DiatonicRomanNumeral.VI,

                    // RegionalMode.Aeolic => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.III : DiatonicRomanNumeral.VI,
                    // RegionalMode.Ionic => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.I : DiatonicRomanNumeral.VI,
                    // RegionalMode.All => UnityEngine.Random.value < (1f / 3f) ? DiatonicRomanNumeral.I :
                    //               UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.III : DiatonicRomanNumeral.VI,
                    // _ => DiatonicRomanNumeral.I,
                    // },

                    HarmonicFunction.Predominant => difficulty switch
                    {
                        CadenceDifficulty.I_II_V => DiatonicRomanNumeral.II,
                        CadenceDifficulty.I_IV_V_VI => DiatonicRomanNumeral.IV,
                        _ => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.II : DiatonicRomanNumeral.IV,
                    },

                    // level switch
                    // {
                    //     RegionalMode.Ionic => DiatonicRomanNumeral.IV,
                    //     RegionalMode.Doric => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.II : DiatonicRomanNumeral.IV,
                    //     RegionalMode.Phrygic => DiatonicRomanNumeral.II,
                    //     RegionalMode.Lydic => DiatonicRomanNumeral.IV,
                    //     RegionalMode.MixoLydic => DiatonicRomanNumeral.IV,
                    //     RegionalMode.Aeolic => DiatonicRomanNumeral.II,
                    //     RegionalMode.Locric => DiatonicRomanNumeral.II,

                    //     // RegionalMode.MixoLydic => DiatonicRomanNumeral.IV,
                    //     // RegionalMode.Ionic => DiatonicRomanNumeral.IV,
                    //     // RegionalMode.All => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.II : DiatonicRomanNumeral.IV,
                    //     _ => DiatonicRomanNumeral.II,
                    // },

                    _ => difficulty switch
                    {
                        CadenceDifficulty.I_II_V => DiatonicRomanNumeral.V,
                        CadenceDifficulty.I_IV_V_VI => DiatonicRomanNumeral.V,
                        _ => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.V : DiatonicRomanNumeral.VII,
                    },
                    // level switch

                    // {
                    //     RegionalMode.Ionic => DiatonicRomanNumeral.VII,
                    //     RegionalMode.Doric => DiatonicRomanNumeral.V,
                    //     RegionalMode.Phrygic => DiatonicRomanNumeral.V,
                    //     RegionalMode.Lydic => DiatonicRomanNumeral.VII,
                    //     RegionalMode.MixoLydic => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.V : DiatonicRomanNumeral.VII,
                    //     RegionalMode.Aeolic => DiatonicRomanNumeral.V,
                    //     RegionalMode.Locric => DiatonicRomanNumeral.VII,

                    //     // RegionalMode.All => UnityEngine.Random.value < (1f / 2f) ? DiatonicRomanNumeral.V : DiatonicRomanNumeral.VII,
                    //     _ => DiatonicRomanNumeral.V,
                    // },
                }; ;
            }

            static int FunctionCount() => (Enum.GetNames(typeof(HarmonicFunction)).Length - 1);//-1 because we aren't using 'secondary'
        }


    }
}