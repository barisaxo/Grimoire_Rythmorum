using System;
namespace MusicTheory.Chords
{
    public interface IChordTone : IMusicalElement
    {
        ChordToneEnum Enum { get; }
        string IMusicalElement.Name => Enum.Name;
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
    }

    [Serializable] public readonly struct Root : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.Root; }
    [Serializable] public readonly struct mi3 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.mi3; }
    [Serializable] public readonly struct M3 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.M3; }
    [Serializable] public readonly struct Sus4 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.Sus4; }
    [Serializable] public readonly struct TT : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.TT; }
    [Serializable] public readonly struct b5 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.b5; }
    [Serializable] public readonly struct P5 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.P5; }
    [Serializable] public readonly struct S5 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.S5; }
    [Serializable] public readonly struct mi6 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.mi6; }
    [Serializable] public readonly struct M6 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.M6; }
    [Serializable] public readonly struct dim7 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.dim7; }
    [Serializable] public readonly struct mi7 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.mi7; }
    [Serializable] public readonly struct M7 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.M7; }
    [Serializable] public readonly struct b9 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.b9; }
    [Serializable] public readonly struct _9 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.Nine; }
    [Serializable] public readonly struct S9 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.S9; }
    [Serializable] public readonly struct _11 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.Eleven; }
    [Serializable] public readonly struct S11 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.S11; }
    [Serializable] public readonly struct b13 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.b13; }
    [Serializable] public readonly struct _13 : IChordTone { public readonly ChordToneEnum Enum => ChordToneEnum.Thirteen; }

    public class ChordToneEnum : Enumeration
    {
        public ChordToneEnum() : base(0, "") { }
        public ChordToneEnum(int sn, int id, string name) : base(sn: sn, id: id, name) { }

        public static ChordToneEnum Root = new(0, 0, nameof(Root));
        public static ChordToneEnum mi3 = new(1, 3, nameof(mi3));
        public static ChordToneEnum M3 = new(2, 4, nameof(M3));
        public static ChordToneEnum Sus4 = new(3, 5, nameof(Sus4));
        public static ChordToneEnum TT = new(4, 6, nameof(TT));
        public static ChordToneEnum b5 = new(5, 6, nameof(b5));
        public static ChordToneEnum P5 = new(6, 7, nameof(P5));
        public static ChordToneEnum S5 = new(7, 8, nameof(S5));
        public static ChordToneEnum mi6 = new(8, 8, nameof(mi6));
        public static ChordToneEnum M6 = new(9, 9, nameof(M6));
        public static ChordToneEnum dim7 = new(10, 9, nameof(dim7));
        public static ChordToneEnum mi7 = new(11, 10, nameof(mi7));
        public static ChordToneEnum M7 = new(12, 11, nameof(M7));
        public static ChordToneEnum b9 = new(13, 1, nameof(b9));
        public static ChordToneEnum Nine = new(14, 2, nameof(Nine));
        public static ChordToneEnum S9 = new(15, 3, nameof(S9));
        public static ChordToneEnum Eleven = new(16, 5, nameof(Eleven));
        public static ChordToneEnum S11 = new(17, 6, nameof(S11));
        public static ChordToneEnum b13 = new(18, 8, nameof(b13));
        public static ChordToneEnum Thirteen = new(19, 9, nameof(Thirteen));
    }
}
