namespace Data.Two
{
    public interface Metal : IItem
    {
        static MetalEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [System.Serializable]
    public struct WroughtIron : Metal { public static MetalEnum Enum => MetalEnum.WroughtIron; }

    [System.Serializable]
    public struct CastIron : Metal { public static MetalEnum Enum => MetalEnum.CastIron; }

    [System.Serializable]
    public struct Bronze : Metal { public static MetalEnum Enum => MetalEnum.Bronze; }

    [System.Serializable]
    public struct Patina : Metal { public static MetalEnum Enum => MetalEnum.Patina; }

    [System.Serializable]
    public class MetalEnum : Enumeration
    {
        public MetalEnum(int id, string name) : base(id, name) { }
        public MetalEnum(int id, string name, string description, float modifier) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
        }

        public readonly float Modifier;
        public readonly string Description;

        public static MetalEnum WroughtIron = new(8, "WroughtIron", "Inexpensive but weak metal", 1f);
        public static MetalEnum CastIron = new(9, "CastIron", "Moderately inexpensive, moderately strong metal", 1.5f);
        public static MetalEnum Bronze = new(10, "Bronze", "Expensive, strong metal", 2.25f);
        public static MetalEnum Patina = new(11, "Patina", "Very expensive, very strong metal", 3f);
    }
}