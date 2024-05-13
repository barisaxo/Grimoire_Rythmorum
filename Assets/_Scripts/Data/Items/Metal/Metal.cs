using System;
namespace Data.Two
{
    public interface Metal : IItem
    {
        MetalEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
        float Modifier => Enum.Modifier;
    }

    [Serializable] public struct WroughtIron : Metal { public readonly MetalEnum Enum => MetalEnum.WroughtIron; }
    [Serializable] public struct CastIron : Metal { public readonly MetalEnum Enum => MetalEnum.CastIron; }
    [Serializable] public struct Bronze : Metal { public readonly MetalEnum Enum => MetalEnum.Bronze; }
    [Serializable] public struct Patina : Metal { public readonly MetalEnum Enum => MetalEnum.Patina; }

    [Serializable]
    public class MetalEnum : Enumeration
    {
        public MetalEnum() : base(0, null) { }
        public MetalEnum(int id, string name) : base(id, name) { }
        public MetalEnum(int id, string name, string description, float modifier) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
        }

        public readonly float Modifier;
        public readonly string Description;

        public static MetalEnum WroughtIron = new(0, "WroughtIron", "Inexpensive but weak metal", 1f);
        public static MetalEnum CastIron = new(1, "CastIron", "Moderately inexpensive, moderately strong metal", 1.5f);
        public static MetalEnum Bronze = new(2, "Bronze", "Expensive, strong metal", 2.25f);
        public static MetalEnum Patina = new(3, "Patina", "Very expensive, very strong metal", 3f);

        internal static IItem ToItem(MetalEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == WroughtIron => new WroughtIron(),
                _ when @enum == CastIron => new CastIron(),
                _ when @enum == Bronze => new Bronze(),
                _ when @enum == Patina => new Patina(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}