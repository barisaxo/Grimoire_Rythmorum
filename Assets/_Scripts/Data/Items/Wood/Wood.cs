
using System;
namespace Data.Two
{
    public interface Wood : IItem
    {
        WoodEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
        float Modifier => Enum.Modifier;
    }

    [System.Serializable]
    public class WoodEnum : Enumeration
    {
        public WoodEnum() : base(0, "") { }
        public WoodEnum(int id, string name) : base(id, name) { }
        public WoodEnum(int id, string name, string description, float modifier) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
        }

        public readonly float Modifier;
        public readonly string Description;

        public static WoodEnum Pine = new(0, "Pine", "Inexpensive but soft timber", 1f);
        public static WoodEnum Fir = new(1, "Fir", "Moderately inexpensive, moderately hard timber", 1.5f);
        public static WoodEnum Oak = new(2, "Oak", "Expensive, hard timber", 2.25f);
        public static WoodEnum Teak = new(3, "Teak", "Very expensive, very hard timber", 3f);

        internal static Wood ToItem(WoodEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == Pine => new Pine(),
                _ when @enum == Fir => new Fir(),
                _ when @enum == Oak => new Oak(),
                _ when @enum == Teak => new Teak(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }

    [Serializable] public struct Pine : Wood { public readonly WoodEnum Enum => WoodEnum.Pine; }
    [Serializable] public struct Fir : Wood { public readonly WoodEnum Enum => WoodEnum.Fir; }
    [Serializable] public struct Oak : Wood { public readonly WoodEnum Enum => WoodEnum.Oak; }
    [Serializable] public struct Teak : Wood { public readonly WoodEnum Enum => WoodEnum.Teak; }
}