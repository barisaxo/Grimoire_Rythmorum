using System;

namespace MusicTheory.Intervals
{
    // [System.Serializable]
    public interface IQuantity : IMusicalElement
    {
        QuantityEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
        string Description => Enum.Description;
    }

    [Serializable] public readonly struct Unison : IQuantity { public readonly QuantityEnum Enum => QuantityEnum.Unison; }
    [Serializable] public readonly struct Second : IQuantity { public readonly QuantityEnum Enum => QuantityEnum.Second; }
    [Serializable] public readonly struct Third : IQuantity { public readonly QuantityEnum Enum => QuantityEnum.Third; }
    [Serializable] public readonly struct Fourth : IQuantity { public readonly QuantityEnum Enum => QuantityEnum.Fourth; }
    [Serializable] public readonly struct Fifth : IQuantity { public readonly QuantityEnum Enum => QuantityEnum.Fifth; }
    [Serializable] public readonly struct Sixth : IQuantity { public readonly QuantityEnum Enum => QuantityEnum.Sixth; }
    [Serializable] public readonly struct Seventh : IQuantity { public readonly QuantityEnum Enum => QuantityEnum.Seventh; }
    [Serializable] public readonly struct Octave : IQuantity { public readonly QuantityEnum Enum => QuantityEnum.Octave; }

    public class QuantityEnum : Enumeration
    {
        public QuantityEnum() : base(0, "") { }
        public QuantityEnum(int snId, string name, string desc) : base(snId, name) { Description = desc; }
        public readonly string Description;

        public static readonly QuantityEnum Unison = new(0, "1", nameof(Unison));
        public static readonly QuantityEnum Second = new(1, "2", nameof(Second));
        public static readonly QuantityEnum Third = new(2, "3", nameof(Third));
        public static readonly QuantityEnum Fourth = new(3, "4", nameof(Fourth));
        public static readonly QuantityEnum Fifth = new(4, "5", nameof(Fifth));
        public static readonly QuantityEnum Sixth = new(5, "6", nameof(Sixth));
        public static readonly QuantityEnum Seventh = new(6, "7", nameof(Seventh));
        public static readonly QuantityEnum Octave = new(7, "8", nameof(Octave));

        public static explicit operator QuantityEnum(int i) => FindId<QuantityEnum>(i);

        public static QuantityEnum GetQuantityEnum(ScaleDegrees.DegreeEnums.IDegree degree) => FindId<QuantityEnum>(degree.Id);
    }
}
