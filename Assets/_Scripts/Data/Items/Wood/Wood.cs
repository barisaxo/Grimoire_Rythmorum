
using System;
namespace Data
{
    public interface IWood : IItem
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

        public readonly static WoodEnum Pine = new(0, "Pine", "Inexpensive, soft timber", 1f);
        public readonly static WoodEnum Fir = new(1, "Fir", "Moderately inexpensive, moderately hard timber", 1.15f);
        public readonly static WoodEnum Oak = new(2, "Oak", "Expensive, hard timber", 1.25f);
        public readonly static WoodEnum Teak = new(3, "Teak", "Very expensive, very hard timber", 1.35f);

        public static IWood GetRandomWood() => UnityEngine.Random.Range(0, 4) switch
        {
            0 => new Pine(),
            1 => new Fir(),
            2 => new Oak(),
            3 => new Teak(),
            _ => throw new ArgumentOutOfRangeException()
        };

        internal static IWood ToItem(WoodEnum @enum)
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

        internal static IWood ToItem(int id) => id switch
        {
            0 => new Pine(),
            1 => new Fir(),
            2 => new Oak(),
            3 => new Teak(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [Serializable] public readonly struct Pine : IWood { public readonly WoodEnum Enum => WoodEnum.Pine; }
    [Serializable] public readonly struct Fir : IWood { public readonly WoodEnum Enum => WoodEnum.Fir; }
    [Serializable] public readonly struct Oak : IWood { public readonly WoodEnum Enum => WoodEnum.Oak; }
    [Serializable] public readonly struct Teak : IWood { public readonly WoodEnum Enum => WoodEnum.Teak; }
}