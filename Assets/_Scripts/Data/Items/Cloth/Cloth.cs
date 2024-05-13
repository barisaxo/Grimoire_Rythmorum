using System;
namespace Data.Two
{
    public interface Cloth : IItem
    {
        ClothEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
        float Modifier => Enum.Modifier;
    }

    [Serializable] public struct Hemp : Cloth { public readonly ClothEnum Enum => ClothEnum.Hemp; }
    [Serializable] public struct Cotton : Cloth { public readonly ClothEnum Enum => ClothEnum.Cotton; }
    [Serializable] public struct Linen : Cloth { public readonly ClothEnum Enum => ClothEnum.Linen; }
    [Serializable] public struct Silk : Cloth { public readonly ClothEnum Enum => ClothEnum.Silk; }

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

        public static ClothEnum Hemp = new(0, "Hemp", "Inexpensive but heavy sailcloth", 1);
        public static ClothEnum Cotton = new(1, "Cotton", "Moderately inexpensive, moderately light sailcloth", 1.5f);
        public static ClothEnum Linen = new(2, "Linen", "Expensive, light sailcloth", 2.25f);
        public static ClothEnum Silk = new(3, "Silk", "Very expensive, very light sailcloth", 3f);

        internal static IItem ToItem(ClothEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == Hemp => new Hemp(),
                _ when @enum == Cotton => new Cotton(),
                _ when @enum == Linen => new Linen(),
                _ when @enum == Silk => new Silk(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}