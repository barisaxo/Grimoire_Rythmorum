
using System;
using MusicTheory.ScaleDegrees.DegreeEnums;

namespace MusicTheory.ScaleDegrees
{
    public interface IScaleDegree : IMusicalElement
    {
        ScaleDegreeEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
        string Description => Enum.Description;
    }

    [Serializable] public readonly struct _1 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum._1; }
    [Serializable] public readonly struct b2 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum.b2; }
    [Serializable] public readonly struct _2 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum._2; }
    [Serializable] public readonly struct s2 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum.s2; }
    [Serializable] public readonly struct b3 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum.b3; }
    [Serializable] public readonly struct _3 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum._3; }
    [Serializable] public readonly struct b4 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum.b4; }
    [Serializable] public readonly struct P4 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum.P4; }
    [Serializable] public readonly struct s4 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum.s4; }
    [Serializable] public readonly struct b5 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum.b5; }
    [Serializable] public readonly struct P5 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum.P5; }
    [Serializable] public readonly struct s5 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum.s5; }
    [Serializable] public readonly struct b6 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum.b6; }
    [Serializable] public readonly struct _6 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum._6; }
    [Serializable] public readonly struct d7 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum.d7; }
    [Serializable] public readonly struct b7 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum.b7; }
    [Serializable] public readonly struct _7 : IScaleDegree { public readonly ScaleDegreeEnum Enum => ScaleDegreeEnum._7; }

    public partial class ScaleDegreeEnum : Enumeration
    {
        public ScaleDegreeEnum() : base(0, "") { }
        private ScaleDegreeEnum(int sn, int id, string name, string desc, IDegree degree, IQuality quality) : base(sn: sn, id: id, name)
        {
            Description = desc;
            Degree = degree;
            Quality = quality;
        }

        public readonly IQuality Quality;
        public readonly IDegree Degree;
        public readonly string Description;

        public static ScaleDegreeEnum _1 = new(0, 0, "1", "Tonic", new DegreeEnums._1(), new Perfect());
        public static ScaleDegreeEnum b2 = new(1, 1, "b2", "Minor Lateral Subdominant", new DegreeEnums._2(), new Minor());
        public static ScaleDegreeEnum _2 = new(2, 2, "2", "Lateral Subdominant", new DegreeEnums._2(), new Major());
        public static ScaleDegreeEnum s2 = new(3, 3, "♯2", "Augmented Lateral Subdominant", new DegreeEnums._2(), new Augmented());
        public static ScaleDegreeEnum b3 = new(4, 3, "b3", "Minor Mediant Tonic", new DegreeEnums._3(), new Minor());
        public static ScaleDegreeEnum _3 = new(5, 4, "3", "Mediant Tonic", new DegreeEnums._3(), new Major());
        public static ScaleDegreeEnum b4 = new(6, 4, "b4", "Diminished Subdominant", new DegreeEnums._4(), new Diminished());
        public static ScaleDegreeEnum P4 = new(7, 5, "4", "Subdominant", new DegreeEnums._4(), new Perfect());
        public static ScaleDegreeEnum s4 = new(8, 6, "♯4", "Augmented Subdominant", new DegreeEnums._4(), new Augmented());
        public static ScaleDegreeEnum b5 = new(9, 6, "b5", "Diminished Dominant", new DegreeEnums._5(), new Diminished());
        public static ScaleDegreeEnum P5 = new(10, 7, "5", "Dominant", new DegreeEnums._5(), new Perfect());
        public static ScaleDegreeEnum s5 = new(11, 8, "♯5", "Augmented Dominant", new DegreeEnums._5(), new Augmented());
        public static ScaleDegreeEnum b6 = new(12, 8, "b6", "Minor Submediant Tonic", new DegreeEnums._6(), new Minor());
        public static ScaleDegreeEnum _6 = new(13, 9, "6", "Submediant Tonic", new DegreeEnums._6(), new Major());
        public static ScaleDegreeEnum d7 = new(14, 9, "º7", "Diminished Lateral Dominant", new DegreeEnums._7(), new Diminished());
        public static ScaleDegreeEnum b7 = new(15, 10, "b7", "Minor Lateral Dominant", new DegreeEnums._7(), new Minor());
        public static ScaleDegreeEnum _7 = new(16, 11, "7", "Lateral Dominant", new DegreeEnums._7(), new Major());

        /// <summary>
        /// 12. The count is twelve.
        /// </summary>
        public static int Count = 12;
    }


}