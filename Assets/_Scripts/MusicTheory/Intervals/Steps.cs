
using System;

namespace MusicTheory.Steps
{
    public interface IStep : IMusicalElement
    {
        StepEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
    }

    [Serializable] public readonly struct Half : IStep { public StepEnum Enum => StepEnum.Half; }
    [Serializable] public readonly struct Whole : IStep { public StepEnum Enum => StepEnum.Whole; }
    [Serializable] public readonly struct Skip : IStep { public StepEnum Enum => StepEnum.Skip; }

    public class StepEnum : Enumeration
    {
        public StepEnum() : base(0, "") { }
        public StepEnum(int snId, string name) : base(snId, name) { }

        public static readonly StepEnum Half = new(1, nameof(Half));
        public static readonly StepEnum Whole = new(2, nameof(Whole));
        public static readonly StepEnum Skip = new(3, nameof(Skip));

        // public static implicit operator IStep(StepEnum e) => e switch
        // {
        //     _ when e == Half => new Half(),
        //     _ when e == Whole => new Whole(),
        //     _ when e == Skip => new Skip(),
        //     _ => throw new System.ArgumentOutOfRangeException()
        // };

        public static explicit operator StepEnum(int i) => FindId<StepEnum>(i);
    }


}