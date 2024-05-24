using System;
namespace Data.Two
{
    public interface IMetal : IItem
    {
        MetalEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name.StartCase();
        string IItem.Description => Enum.Description;
        float Modifier => Enum.Modifier;
    }

    [Serializable] public readonly struct WroughtIron : IMetal { public readonly MetalEnum Enum => MetalEnum.WroughtIron; }
    [Serializable] public readonly struct CastIron : IMetal { public readonly MetalEnum Enum => MetalEnum.CastIron; }
    [Serializable] public readonly struct Bronze : IMetal { public readonly MetalEnum Enum => MetalEnum.Bronze; }
    [Serializable] public readonly struct Patina : IMetal { public readonly MetalEnum Enum => MetalEnum.Patina; }

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

        public static IMetal GetRandomMetal() => UnityEngine.Random.Range(0, 4) switch
        {
            0 => new WroughtIron(),
            1 => new CastIron(),
            2 => new Bronze(),
            3 => new Patina(),

            _ => throw new Exception()
        };

        public readonly static MetalEnum WroughtIron = new(0, "WroughtIron", "Inexpensive but weak metal", .8f);
        public readonly static MetalEnum CastIron = new(1, "CastIron", "Moderately inexpensive, moderately strong metal", .9f);
        public readonly static MetalEnum Bronze = new(2, "Bronze", "Expensive, strong metal", 1f);
        public readonly static MetalEnum Patina = new(3, "Patina", "Very expensive, very strong metal", 1.1f);

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