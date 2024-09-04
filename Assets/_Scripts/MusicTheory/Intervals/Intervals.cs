using System;

namespace MusicTheory.Intervals
{

    public interface IInterval : IMusicalElement
    {
        IntervalEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
        string Description => Enum.Description;
        IQuality Quality => Enum.Quality;
        IQuantity Quantity => Enum.Quantity;
    }

    [Serializable] public readonly struct P1 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.P1; }
    [Serializable] public readonly struct mi2 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.mi2; }
    [Serializable] public readonly struct M2 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.M2; }
    [Serializable] public readonly struct A2 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.A2; }
    [Serializable] public readonly struct mi3 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.mi3; }
    [Serializable] public readonly struct M3 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.M3; }
    [Serializable] public readonly struct d4 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.d4; }
    [Serializable] public readonly struct P4 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.P4; }
    [Serializable] public readonly struct A4 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.A4; }
    [Serializable] public readonly struct d5 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.d5; }
    [Serializable] public readonly struct P5 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.P5; }
    [Serializable] public readonly struct A5 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.A5; }
    [Serializable] public readonly struct mi6 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.mi6; }
    [Serializable] public readonly struct M6 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.M6; }
    [Serializable] public readonly struct d7 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.d7; }
    [Serializable] public readonly struct mi7 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.mi7; }
    [Serializable] public readonly struct M7 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.M7; }
    [Serializable] public readonly struct P8 : IInterval { public readonly IntervalEnum Enum => IntervalEnum.P8; }

    public class IntervalEnum : Enumeration
    {
        public IntervalEnum() : base(0, "") { }
        public IntervalEnum(int sn, int id, string name, IQuality quality, IQuantity quantity) : base(sn: sn, id: id, name)
        {
            Quality = quality;
            Quantity = quantity;

            switch (quality)
            {
                case Perfect:
                    switch (quantity)
                    {
                        case Second:
                        case Third:
                        case Sixth:
                        case Seventh:
                            throw new ArgumentOutOfRangeException(quantity.Enum.Name + " can not be Perfect.");
                    }
                    break;

                case Augmented:
                    switch (quantity)
                    {
                        case Unison:
                        case Octave:
                        case Third:
                        case Sixth:
                        case Seventh:
                            throw new ArgumentOutOfRangeException(quantity.Enum.Name + " should not be Augmented.");
                    }
                    break;

                case Diminished:
                    switch (quantity)
                    {
                        case Unison:
                        case Octave:
                        case Second:
                            throw new ArgumentOutOfRangeException(quantity.Enum.Name + " should not be Diminished.");
                    }
                    break;

                case Major:
                case Minor:
                    switch (quantity)
                    {
                        case Fourth:
                        case Fifth:
                        case Unison:
                        case Octave:
                            throw new ArgumentOutOfRangeException(quantity.Enum.Name + " can not be " + quality.Enum.Name + ".");
                    }
                    break;
            }
        }

        public readonly IQuality Quality;
        public readonly IQuantity Quantity;
        public string Description => Quality.Description + " " + Quantity.Description;

        public static readonly IntervalEnum P1 = new(0, 0, nameof(P1), new Perfect(), new Unison());
        public static readonly IntervalEnum mi2 = new(1, 1, nameof(mi2), new Minor(), new Second());
        public static readonly IntervalEnum M2 = new(2, 2, nameof(M2), new Major(), new Second());
        public static readonly IntervalEnum A2 = new(3, 3, nameof(A2), new Augmented(), new Second());
        public static readonly IntervalEnum mi3 = new(4, 3, nameof(mi3), new Minor(), new Third());
        public static readonly IntervalEnum M3 = new(5, 4, nameof(M3), new Major(), new Third());
        public static readonly IntervalEnum d4 = new(6, 4, nameof(d4), new Diminished(), new Fourth());
        public static readonly IntervalEnum P4 = new(7, 5, nameof(P4), new Perfect(), new Fourth());
        public static readonly IntervalEnum A4 = new(8, 6, nameof(A4), new Augmented(), new Fourth());
        public static readonly IntervalEnum d5 = new(9, 6, nameof(d5), new Diminished(), new Fifth());
        public static readonly IntervalEnum P5 = new(10, 7, nameof(P5), new Perfect(), new Fifth());
        public static readonly IntervalEnum A5 = new(11, 8, nameof(A5), new Augmented(), new Fifth());
        public static readonly IntervalEnum mi6 = new(12, 8, nameof(mi6), new Minor(), new Sixth());
        public static readonly IntervalEnum M6 = new(13, 9, nameof(M6), new Major(), new Sixth());
        public static readonly IntervalEnum d7 = new(14, 9, nameof(d7), new Diminished(), new Seventh());
        public static readonly IntervalEnum mi7 = new(15, 10, nameof(mi7), new Minor(), new Seventh());
        public static readonly IntervalEnum M7 = new(16, 11, nameof(M7), new Major(), new Seventh());
        public static readonly IntervalEnum P8 = new(17, 0, nameof(P8), new Perfect(), new Octave());

        public static explicit operator IntervalEnum(int i) => FindId<IntervalEnum>(i);
        public static explicit operator IntervalEnum((IQuality quality, IQuantity quantity) i) => Find(i);

        public static IntervalEnum Find((IQuantity quantity, IQuality quality) i) => Find((i.quality, i.quantity));

        public static IntervalEnum Find((IQuality quality, IQuantity quantity) i)
        {
            foreach (var e in All<IntervalEnum>()) if (e.Quantity.Equals(i.quantity) && e.Quality.Equals(i.quality)) return e;
            throw new ArgumentOutOfRangeException(i.quality.Name + " " + i.quantity.Name);
        }
    }


}
