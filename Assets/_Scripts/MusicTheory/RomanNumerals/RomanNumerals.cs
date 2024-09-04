using System;


namespace MusicTheory.RomanNumerals.Diatonic
{
    public interface IRomanNumeral : IMusicalElement
    {
        RomanNumeralEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
    }

    [Serializable] public readonly struct I : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.I; }
    [Serializable] public readonly struct II : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.II; }
    [Serializable] public readonly struct III : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.III; }
    [Serializable] public readonly struct IV : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.IV; }
    [Serializable] public readonly struct V : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.V; }
    [Serializable] public readonly struct VI : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.VI; }
    [Serializable] public readonly struct VII : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.VII; }

    public class RomanNumeralEnum : Enumeration
    {
        public RomanNumeralEnum() : base(0, "") { }
        public RomanNumeralEnum(int snId, string name) : base(snId, name) { }

        public static RomanNumeralEnum I = new(0, nameof(I));
        public static RomanNumeralEnum II = new(1, nameof(II));
        public static RomanNumeralEnum III = new(2, nameof(III));
        public static RomanNumeralEnum IV = new(3, nameof(IV));
        public static RomanNumeralEnum V = new(4, nameof(V));
        public static RomanNumeralEnum VI = new(5, nameof(VI));
        public static RomanNumeralEnum VII = new(6, nameof(VII));
    }
}


namespace MusicTheory.RomanNumerals.Chromatic
{
    public interface IRomanNumeral : IMusicalElement
    {
        RomanNumeralEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
    }

    [Serializable] public readonly struct I : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.I; }
    [Serializable] public readonly struct bII : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.bII; }
    [Serializable] public readonly struct II : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.II; }
    [Serializable] public readonly struct bIII : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.bIII; }
    [Serializable] public readonly struct III : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.III; }
    [Serializable] public readonly struct IV : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.IV; }
    [Serializable] public readonly struct bV : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.bV; }
    [Serializable] public readonly struct V : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.V; }
    [Serializable] public readonly struct bVI : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.bVI; }
    [Serializable] public readonly struct VI : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.VI; }
    [Serializable] public readonly struct bVII : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.bVII; }
    [Serializable] public readonly struct VII : IRomanNumeral { public readonly RomanNumeralEnum Enum => RomanNumeralEnum.VII; }

    public class RomanNumeralEnum : Enumeration
    {
        public RomanNumeralEnum() : base(0, "") { }
        public RomanNumeralEnum(int snId, string name) : base(snId, name) { }

        public static RomanNumeralEnum I = new(0, nameof(I));
        public static RomanNumeralEnum bII = new(1, nameof(bII));
        public static RomanNumeralEnum II = new(2, nameof(II));
        public static RomanNumeralEnum bIII = new(3, nameof(bIII));
        public static RomanNumeralEnum III = new(4, nameof(III));
        public static RomanNumeralEnum IV = new(5, nameof(IV));
        public static RomanNumeralEnum bV = new(6, nameof(bV));
        public static RomanNumeralEnum V = new(7, nameof(V));
        public static RomanNumeralEnum bVI = new(8, nameof(bV));
        public static RomanNumeralEnum VI = new(9, nameof(VI));
        public static RomanNumeralEnum bVII = new(10, nameof(bVII));
        public static RomanNumeralEnum VII = new(11, nameof(VII));
    }

}