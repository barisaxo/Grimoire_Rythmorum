using System;
namespace MusicTheory.ScaleDegrees.DegreeEnums
{
    public interface IDegree : IMusicalElement
    {
        DegreeEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
    }

    [Serializable] public readonly struct _1 : IDegree { public readonly DegreeEnum Enum => DegreeEnum._1; }
    [Serializable] public readonly struct _2 : IDegree { public readonly DegreeEnum Enum => DegreeEnum._2; }
    [Serializable] public readonly struct _3 : IDegree { public readonly DegreeEnum Enum => DegreeEnum._3; }
    [Serializable] public readonly struct _4 : IDegree { public readonly DegreeEnum Enum => DegreeEnum._4; }
    [Serializable] public readonly struct _5 : IDegree { public readonly DegreeEnum Enum => DegreeEnum._5; }
    [Serializable] public readonly struct _6 : IDegree { public readonly DegreeEnum Enum => DegreeEnum._6; }
    [Serializable] public readonly struct _7 : IDegree { public readonly DegreeEnum Enum => DegreeEnum._7; }

    public class DegreeEnum : Enumeration
    {
        public DegreeEnum() : base(0, "") { }
        public DegreeEnum(int snId, string name) : base(snId, name) { }

        public static DegreeEnum _1 = new(0, "1");
        public static DegreeEnum _2 = new(1, "2");
        public static DegreeEnum _3 = new(2, "3");
        public static DegreeEnum _4 = new(3, "4");
        public static DegreeEnum _5 = new(4, "5");
        public static DegreeEnum _6 = new(5, "6");
        public static DegreeEnum _7 = new(6, "7");

        public static explicit operator DegreeEnum(int i) => FindId<DegreeEnum>(i);


        public static explicit operator DegreeEnum(Intervals.QuantityEnum e) => e switch
        {
            _ when e == Intervals.QuantityEnum.Unison => _1,
            _ when e == Intervals.QuantityEnum.Second => _2,
            _ when e == Intervals.QuantityEnum.Third => _3,
            _ when e == Intervals.QuantityEnum.Fourth => _4,
            _ when e == Intervals.QuantityEnum.Fifth => _5,
            _ when e == Intervals.QuantityEnum.Sixth => _6,
            _ when e == Intervals.QuantityEnum.Seventh => _7,
            _ => throw new System.ArgumentOutOfRangeException(e.ToString())
        };
    }

    public static class DegreeSystems
    {
        public static IDegree GetDegree(this DegreeEnum e) => e switch
        {
            _ when e == DegreeEnum._1 => new _1(),
            _ when e == DegreeEnum._2 => new _2(),
            _ when e == DegreeEnum._3 => new _3(),
            _ when e == DegreeEnum._4 => new _4(),
            _ when e == DegreeEnum._5 => new _5(),
            _ when e == DegreeEnum._6 => new _6(),
            _ when e == DegreeEnum._7 => new _7(),
            _ => throw new System.ArgumentOutOfRangeException(e.ToString())
        };
    }

}