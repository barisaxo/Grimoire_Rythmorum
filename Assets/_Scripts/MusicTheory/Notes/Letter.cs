using System;
namespace MusicTheory.Notes
{
    public interface ILetter : IMusicalElement
    {
        LetterEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
    }

    [Serializable] public readonly struct _C : ILetter { public readonly LetterEnum Enum => LetterEnum.C; }
    [Serializable] public readonly struct _D : ILetter { public readonly LetterEnum Enum => LetterEnum.D; }
    [Serializable] public readonly struct _E : ILetter { public readonly LetterEnum Enum => LetterEnum.E; }
    [Serializable] public readonly struct _F : ILetter { public readonly LetterEnum Enum => LetterEnum.F; }
    [Serializable] public readonly struct _G : ILetter { public readonly LetterEnum Enum => LetterEnum.G; }
    [Serializable] public readonly struct _A : ILetter { public readonly LetterEnum Enum => LetterEnum.A; }
    [Serializable] public readonly struct _B : ILetter { public readonly LetterEnum Enum => LetterEnum.B; }

    public class LetterEnum : Enumeration
    {
        public LetterEnum() : base(0, "") { }
        public LetterEnum(int snId, string name) : base(snId, name) { }

        public static LetterEnum A = new(0, nameof(A));
        public static LetterEnum B = new(1, nameof(B));
        public static LetterEnum C = new(2, nameof(C));
        public static LetterEnum D = new(3, nameof(D));
        public static LetterEnum E = new(4, nameof(E));
        public static LetterEnum F = new(5, nameof(F));
        public static LetterEnum G = new(6, nameof(G));

        // public static explicit operator LetterEnum(int i) => FindId<LetterEnum>(i);

        /// <summary>
        /// 7. The letter count is seven.
        /// </summary>
        public static int Count => Length<LetterEnum>();
    }
}