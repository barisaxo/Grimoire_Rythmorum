using System;

namespace MusicTheory.Triads
{
    public interface ITriad : IMusicalElement
    {
        TriadEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
        string Description => Enum.Description;
    }

    [Serializable] public readonly struct Major : ITriad { public readonly TriadEnum Enum => TriadEnum.Major; }
    [Serializable] public readonly struct Minor : ITriad { public readonly TriadEnum Enum => TriadEnum.Minor; }
    [Serializable] public readonly struct Augmented : ITriad { public readonly TriadEnum Enum => TriadEnum.Augmented; }
    [Serializable] public readonly struct Diminished : ITriad { public readonly TriadEnum Enum => TriadEnum.Diminished; }

    public class TriadEnum : Enumeration
    {
        public TriadEnum() : base(0, "") { }
        public TriadEnum(int id, string name, string desc) : base(id, name) { Description = desc; }
        public readonly string Description;

        public static TriadEnum Major = new(0, "", nameof(Major)) { };
        public static TriadEnum Minor = new(1, "-", nameof(Minor)) { };
        public static TriadEnum Augmented = new(2, "+", nameof(Augmented)) { };
        public static TriadEnum Diminished = new(3, "º", nameof(Diminished)) { };

        //public static TriadEnum Secundal = new(3, "<size=60%><font-weight=\"100\"><voffset=0.5em>2</voffset></font-weight><size=100%>", nameof(Secundal)) { };
        //public static TriadEnum Quartal = new(3, "<size=60%><font-weight=\"100\"><voffset=0.5em>4</voffset></font-weight><size=100%>", nameof(Quartal)) { };


    }
    //public class Secundal : Triad { public Secundal() : base(TriadEnum.Secundal) { } }
    //public class Quartal : Triad { public Quartal() : base(TriadEnum.Quartal) { } }
}

namespace MusicTheory.Triads.Arithmetic
{
    public static class TriadChordTones
    {
        public static Intervals.IInterval[] ChordTonesAsIntervals(this ITriad triad)
        {
            Intervals.IInterval[] temp = new Intervals.IInterval[2];

            temp[0] = triad switch
            {
                Major or Augmented => new Intervals.M3(),
                Minor or Diminished => new Intervals.mi3(),
                _ => throw new System.ArgumentOutOfRangeException(triad.Description)
            };

            temp[1] = triad switch
            {
                Major or Minor => new Intervals.P5(),
                Augmented => new Intervals.A5(),
                Diminished => new Intervals.d5(),
                _ => throw new System.ArgumentOutOfRangeException(triad.Description)
            };
            return temp;
        }

        public static ITriad GetTriad(this TriadEnum e) => e switch
        {
            _ when e == TriadEnum.Major => new Major(),
            _ when e == TriadEnum.Minor => new Minor(),
            _ when e == TriadEnum.Augmented => new Augmented(),
            _ when e == TriadEnum.Diminished => new Diminished(),
            _ => throw new System.ArgumentOutOfRangeException(e.ToString())
        };

        // public static ITriad GetTriad(this MusicTheory.RomanNumerals.Diatonic.IRomanNumeral rn)
        // {
        //     return rn switch
        //     {
        //         MusicTheory.RomanNumerals.Diatonic.I or
        //         MusicTheory.RomanNumerals.Diatonic.IV or
        //         MusicTheory.RomanNumerals.Diatonic.V => new Major(),

        //         MusicTheory.RomanNumerals.Diatonic.II or
        //         MusicTheory.RomanNumerals.Diatonic.III or
        //         MusicTheory.RomanNumerals.Diatonic.VI => new Minor(),

        //         MusicTheory.RomanNumerals.Diatonic.VII => new Diminished(),
        //         _ => throw new System.Exception(rn.Name),
        //     };
        // }
    }
}