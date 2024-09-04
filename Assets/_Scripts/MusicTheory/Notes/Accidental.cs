using System;

namespace MusicTheory.Notes
{

    public interface IAccidental : IMusicalElement
    {
        AccidentalEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
    }

    [Serializable] public readonly struct Sharp : IAccidental { public readonly AccidentalEnum Enum => AccidentalEnum.Sharp; }
    [Serializable] public readonly struct Flat : IAccidental { public readonly AccidentalEnum Enum => AccidentalEnum.Sharp; }
    [Serializable] public readonly struct Natural : IAccidental { public readonly AccidentalEnum Enum => AccidentalEnum.Sharp; }

    public class AccidentalEnum : Enumeration
    {
        public AccidentalEnum() : base(0, "") { }
        public AccidentalEnum(int sn, int id, string name, string desc) : base(sn: sn, id: id, name) { Description = desc; }

        public readonly string Description;
        public static AccidentalEnum Sharp = new(0, 1, "♯", nameof(Sharp));
        public static AccidentalEnum Flat = new(1, -1, "b", nameof(Flat));
        public static AccidentalEnum Natural = new(2, 0, "", nameof(Natural));

        public static explicit operator AccidentalEnum(int i) => FindId<AccidentalEnum>(i);

        // public static implicit operator IAccidental(AccidentalEnum e) => e switch
        // {
        //     _ when e == Sharp => new Sharp(),
        //     _ when e == Flat => new Flat(),
        //     _ when e == Natural => new Natural(),
        //     _ => throw new System.ArgumentOutOfRangeException(e.ToString())
        // };
    }

}
namespace MusicTheory.Notes.Arithmetic
{
    public static class AccidentalArithmetic
    {
        //public static Accidental GetAccidental(this Letter letter, Key bottom, Intervals.Interval interval)
        //{


        //}

        public static IAccidental GetAccidental(RomanNumerals.Diatonic.IRomanNumeral rn, INote keyCenter)
        {
            return rn switch
            {
                RomanNumerals.Diatonic.I =>
                    keyCenter is Fs or Cs ? new Sharp() :
                    keyCenter is Bb or Eb or Ab or Db or Gb or Cb ? new Flat() :
                    new Natural(),

                RomanNumerals.Diatonic.II =>
                    keyCenter is Fs or Cs or E or B ? new Sharp() :
                    keyCenter is Ab or Db or Gb or Cb ? new Flat() :
                    new Natural(),

                RomanNumerals.Diatonic.III =>
                    keyCenter is Fs or Cs or E or B or A or D ? new Sharp() :
                    keyCenter is Gb or Cb ? new Flat() :
                    new Natural(),

                RomanNumerals.Diatonic.IV =>
                    keyCenter is Cs ? new Sharp() :
                    keyCenter is Bb or Eb or Ab or Db or Gb or Cb or F ? new Flat() :
                    new Natural(),

                RomanNumerals.Diatonic.V =>
                    keyCenter is Fs or Cs or B ? new Sharp() :
                    keyCenter is Eb or Ab or Db or Gb or Cb ? new Flat() :
                    new Natural(),

                RomanNumerals.Diatonic.VI =>
                    keyCenter is Fs or Cs or B or A or E ? new Sharp() :
                    keyCenter is Cb or Db or Gb ? new Flat() :
                    new Natural(),

                RomanNumerals.Diatonic.VII =>
                    keyCenter is Fs or Cs or G or D or A or E or B ? new Sharp() :
                    keyCenter is Cb ? new Flat() :
                    new Natural(),

                _ => throw new System.Exception(rn.Name + " " + keyCenter.Name),
            };
        }


    }
}