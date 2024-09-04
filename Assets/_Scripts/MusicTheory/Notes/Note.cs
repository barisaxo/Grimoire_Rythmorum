using System;

namespace MusicTheory.Notes
{
    public interface INote : IMusicalElement
    {
        NoteEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
    }

    [Serializable] public readonly struct C : INote { public readonly NoteEnum Enum => NoteEnum.C; }
    [Serializable] public readonly struct Cs : INote { public readonly NoteEnum Enum => NoteEnum.Cs; }
    [Serializable] public readonly struct Db : INote { public readonly NoteEnum Enum => NoteEnum.Db; }
    [Serializable] public readonly struct D : INote { public readonly NoteEnum Enum => NoteEnum.D; }
    [Serializable] public readonly struct Ds : INote { public readonly NoteEnum Enum => NoteEnum.Ds; }
    [Serializable] public readonly struct Eb : INote { public readonly NoteEnum Enum => NoteEnum.Eb; }
    [Serializable] public readonly struct E : INote { public readonly NoteEnum Enum => NoteEnum.E; }
    [Serializable] public readonly struct Es : INote { public readonly NoteEnum Enum => NoteEnum.Es; }
    [Serializable] public readonly struct Fb : INote { public readonly NoteEnum Enum => NoteEnum.Fb; }
    [Serializable] public readonly struct F : INote { public readonly NoteEnum Enum => NoteEnum.F; }
    [Serializable] public readonly struct Fs : INote { public readonly NoteEnum Enum => NoteEnum.Fs; }
    [Serializable] public readonly struct Gb : INote { public readonly NoteEnum Enum => NoteEnum.Gb; }
    [Serializable] public readonly struct G : INote { public readonly NoteEnum Enum => NoteEnum.G; }
    [Serializable] public readonly struct Gs : INote { public readonly NoteEnum Enum => NoteEnum.Gs; }
    [Serializable] public readonly struct Ab : INote { public readonly NoteEnum Enum => NoteEnum.Ab; }
    [Serializable] public readonly struct A : INote { public readonly NoteEnum Enum => NoteEnum.A; }
    [Serializable] public readonly struct As : INote { public readonly NoteEnum Enum => NoteEnum.As; }
    [Serializable] public readonly struct Bb : INote { public readonly NoteEnum Enum => NoteEnum.Bb; }
    [Serializable] public readonly struct B : INote { public readonly NoteEnum Enum => NoteEnum.B; }
    [Serializable] public readonly struct Bs : INote { public readonly NoteEnum Enum => NoteEnum.Bs; }
    [Serializable] public readonly struct Cb : INote { public readonly NoteEnum Enum => NoteEnum.Cb; }

    public class NoteEnum : Enumeration
    {
        public NoteEnum() : base(0, "") { }
        public NoteEnum(
                int sn,
                int id,
                string name,
                ILetter letter,
                IAccidental accidental) :
            base(sn: sn, id: id, name)
        {
            Letter = letter;
            Accidental = accidental;
        }

        public readonly ILetter Letter;
        public readonly IAccidental Accidental;

        public static NoteEnum C = new(0, 0, nameof(C), new _C(), new Natural());
        public static NoteEnum Cs = new(1, 1, "C♯", new _C(), new Sharp());
        public static NoteEnum Db = new(2, 1, nameof(Db), new _D(), new Flat());
        public static NoteEnum D = new(3, 2, nameof(D), new _D(), new Natural());
        public static NoteEnum Ds = new(4, 3, "D♯", new _D(), new Sharp());
        public static NoteEnum Eb = new(5, 3, nameof(Eb), new _E(), new Flat());
        public static NoteEnum E = new(6, 4, nameof(E), new _E(), new Natural());
        public static NoteEnum Es = new(7, 5, "E♯", new _E(), new Sharp());
        public static NoteEnum Fb = new(8, 4, nameof(Fb), new _F(), new Flat());
        public static NoteEnum F = new(9, 5, nameof(F), new _F(), new Natural());
        public static NoteEnum Fs = new(10, 6, "F♯", new _F(), new Sharp());
        public static NoteEnum Gb = new(11, 6, nameof(Gb), new _G(), new Flat());
        public static NoteEnum G = new(12, 7, nameof(G), new _G(), new Natural());
        public static NoteEnum Gs = new(13, 8, "G♯", new _G(), new Sharp());
        public static NoteEnum Ab = new(14, 8, nameof(Ab), new _A(), new Flat());
        public static NoteEnum A = new(15, 9, nameof(A), new _A(), new Natural());
        public static NoteEnum As = new(16, 10, "A♯", new _A(), new Sharp());
        public static NoteEnum Bb = new(17, 10, nameof(Bb), new _B(), new Flat());
        public static NoteEnum B = new(18, 11, nameof(B), new _B(), new Natural());
        public static NoteEnum Bs = new(19, 0, "B♯", new _B(), new Sharp());
        public static NoteEnum Cb = new(20, 11, nameof(Cb), new _C(), new Flat());

        public static explicit operator NoteEnum(int i)
        {
            foreach (NoteEnum keyEnum in All<NoteEnum>()) if (keyEnum.Id == i) return keyEnum;

            throw new System.Exception(i.ToString());
        }

        public static explicit operator NoteEnum((ILetter letter, IAccidental accidental) la) => Find(la);

        public static NoteEnum Find((ILetter letter, IAccidental accidental) la)
        {
            foreach (var e in All<NoteEnum>())
                if (e.Letter.Equals(la.letter) && e.Accidental.Equals(la.accidental))
                    return e;

            throw new ArgumentOutOfRangeException(la.ToString());
        }

        public static INote RandomKeyCenter() => UnityEngine.Random.Range(0, 15) switch
        {
            0 => new C(),
            1 => new G(),
            2 => new D(),
            3 => new A(),
            4 => new E(),
            5 => new B(),
            6 => new Fs(),
            7 => new Gb(),
            8 => new Db(),
            9 => new Ab(),
            10 => new Eb(),
            11 => new Bb(),
            12 => new F(),
            13 => new Cb(),
            14 => new Cs(),
            _ => throw new System.Exception("How???"),
        };

        public static int Count => 12;
    }

}