namespace Data.Two
{
    public interface Wood : IItem
    {
        // public Wood(WoodEnum @enum) { Enum = @enum; }
        public static WoodEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [System.Serializable]
    public class WoodEnum : Enumeration
    {
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
    }

    [System.Serializable]
    public struct Pine : Wood { public static WoodEnum Enum => WoodEnum.Pine; }

    [System.Serializable]
    public class Fir : Wood { public static WoodEnum Enum => WoodEnum.Fir; }

    [System.Serializable]
    public class Oak : Wood { public static WoodEnum Enum => WoodEnum.Oak; }

    [System.Serializable]
    public class Teak : Wood { public static WoodEnum Enum => WoodEnum.Teak; }
}