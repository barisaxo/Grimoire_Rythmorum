using System;

namespace MusicTheory.ScaleDegrees
{
    public interface IQuality : IMusicalElement
    {
        QualityEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
        string Description => Enum.Description;
    }

    [Serializable] public readonly struct Major : IQuality { public readonly QualityEnum Enum => QualityEnum.Major; }
    [Serializable] public readonly struct Minor : IQuality { public readonly QualityEnum Enum => QualityEnum.Minor; }
    [Serializable] public readonly struct Augmented : IQuality { public readonly QualityEnum Enum => QualityEnum.Augmented; }
    [Serializable] public readonly struct Diminished : IQuality { public readonly QualityEnum Enum => QualityEnum.Diminished; }
    [Serializable] public readonly struct Perfect : IQuality { public readonly QualityEnum Enum => QualityEnum.Perfect; }

    public class QualityEnum : Enumeration
    {
        public QualityEnum() : base(0, "") { }
        public QualityEnum(int snId, string name, string desc) : base(snId, name) { Description = desc; }
        public readonly string Description;

        public static readonly QualityEnum Major = new(0, "M", nameof(Major));
        public static readonly QualityEnum Minor = new(1, "mi", nameof(Minor));
        public static readonly QualityEnum Augmented = new(2, "+", nameof(Augmented));
        public static readonly QualityEnum Diminished = new(3, "º", nameof(Diminished));
        public static readonly QualityEnum Perfect = new(4, "P", nameof(Perfect));

        public static explicit operator QualityEnum(Intervals.QualityEnum e) => e switch
        {
            _ when e == Intervals.QualityEnum.Major => Major,
            _ when e == Intervals.QualityEnum.Minor => Minor,
            _ when e == Intervals.QualityEnum.Augmented => Augmented,
            _ when e == Intervals.QualityEnum.Diminished => Diminished,
            _ when e == Intervals.QualityEnum.Perfect => Perfect,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}