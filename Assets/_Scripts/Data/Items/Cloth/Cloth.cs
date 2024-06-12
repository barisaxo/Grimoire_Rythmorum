using System;
namespace Data
{
    public interface ICloth : IItem
    {
        ClothEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
        float Modifier => Enum.Modifier;
    }

    [Serializable] public readonly struct Hemp : ICloth { public readonly ClothEnum Enum => ClothEnum.Hemp; }
    [Serializable] public readonly struct Cotton : ICloth { public readonly ClothEnum Enum => ClothEnum.Cotton; }
    [Serializable] public readonly struct Linen : ICloth { public readonly ClothEnum Enum => ClothEnum.Linen; }
    [Serializable] public readonly struct Silk : ICloth { public readonly ClothEnum Enum => ClothEnum.Silk; }

    [Serializable]
    public class ClothEnum : Enumeration
    {
        public ClothEnum() : base(0, null) { }
        public ClothEnum(int id, string name) : base(id, name) { }
        public ClothEnum(int id, string name, string description, float modifier) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
        }

        public readonly float Modifier;
        public readonly string Description;

        public readonly static ClothEnum Hemp = new(0, "Hemp", "Inexpensive, heavy sailcloth", 1);
        public readonly static ClothEnum Cotton = new(1, "Cotton", "Moderately inexpensive, moderately light sailcloth", 1.5f);
        public readonly static ClothEnum Linen = new(2, "Linen", "Expensive, light sailcloth", 2.25f);
        public readonly static ClothEnum Silk = new(3, "Silk", "Very expensive, very light sailcloth", 3f);

        internal static ICloth GetRandomCloth() => ToItem(UnityEngine.Random.Range(0, 4));

        internal static ICloth ToItem(ClothEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == Hemp => new Hemp(),
                _ when @enum == Cotton => new Cotton(),
                _ when @enum == Linen => new Linen(),
                _ when @enum == Silk => new Silk(),
                _ => throw new ArgumentOutOfRangeException(@enum.Name)
            };
        }

        internal static ICloth ToItem(int id) => id switch
        {
            0 => new Hemp(),
            1 => new Cotton(),
            2 => new Linen(),
            3 => new Silk(),
            _ => throw new Exception()
        };
    }
}