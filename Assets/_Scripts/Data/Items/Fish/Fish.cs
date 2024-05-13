using System;

namespace Data.Two
{
    public interface Fish : IItem
    {
        FishEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public struct SailFish : Fish { public readonly FishEnum Enum => FishEnum.SailFish; }
    [Serializable] public struct Tuna : Fish { public readonly FishEnum Enum => FishEnum.Tuna; }
    [Serializable] public struct Carp : Fish { public readonly FishEnum Enum => FishEnum.Carp; }
    [Serializable] public struct Halibut : Fish { public readonly FishEnum Enum => FishEnum.Halibut; }
    [Serializable] public struct Sturgeon : Fish { public readonly FishEnum Enum => FishEnum.Sturgeon; }
    [Serializable] public struct Shark : Fish { public readonly FishEnum Enum => FishEnum.Shark; }

    [Serializable]
    public class FishEnum : Enumeration
    {
        public FishEnum() : base(0, "") { }
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

        internal static IItem ToItem(FishEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == SailFish => new SailFish(),
                _ when @enum == Tuna => new Tuna(),
                _ when @enum == Carp => new Carp(),
                _ when @enum == Halibut => new Halibut(),
                _ when @enum == Sturgeon => new Sturgeon(),
                _ when @enum == Shark => new Shark(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}