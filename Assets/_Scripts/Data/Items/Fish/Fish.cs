namespace Data.Two
{
    public interface Fish : IItem
    {
        public static FishEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [System.Serializable]
    public struct SailFish : Fish { public static FishEnum Enum => FishEnum.SailFish; }

    [System.Serializable]
    public struct Tuna : Fish { public static FishEnum Enum => FishEnum.Tuna; }

    [System.Serializable]
    public struct Carp : Fish { public static FishEnum Enum => FishEnum.Carp; }

    [System.Serializable]
    public struct Halibut : Fish { public static FishEnum Enum => FishEnum.Halibut; }

    [System.Serializable]
    public struct Sturgeon : Fish { public static FishEnum Enum => FishEnum.Sturgeon; }

    [System.Serializable]
    public struct Shark : Fish { public static FishEnum Enum => FishEnum.Shark; }

    [System.Serializable]
    public class FishEnum : Enumeration
    {
        public FishEnum(int id, string name) : base(id, name) { }
        public FishEnum(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }

        public readonly string Description;

        public static FishEnum SailFish = new(0, "Sailfish", "You eat this");
        public static FishEnum Tuna = new(1, "Tuna", "You eat this");
        public static FishEnum Carp = new(2, "Carp", "You eat this");
        public static FishEnum Halibut = new(3, "Halibut", "You eat this");
        public static FishEnum Sturgeon = new(4, "Sturgeon", "You eat this");
        public static FishEnum Shark = new(5, "Shark", "This eat you");
    }
}