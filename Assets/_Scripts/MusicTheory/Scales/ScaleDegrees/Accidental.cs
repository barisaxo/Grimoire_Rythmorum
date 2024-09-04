using System;

namespace MusicTheory.ScaleDegrees.Accidental
{
    public interface IAccidental : IMusicalElement
    {
        AccidentalEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
        string Description => Enum.Description;
    }

    [Serializable] public class Sharp : IAccidental { public AccidentalEnum Enum => AccidentalEnum.Sharp; }
    [Serializable] public class Flat : IAccidental { public AccidentalEnum Enum => AccidentalEnum.Flat; }
    [Serializable] public class Natural : IAccidental { public AccidentalEnum Enum => AccidentalEnum.Natural; }

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