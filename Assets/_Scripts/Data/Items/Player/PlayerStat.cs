using System;
namespace Data.Two
{
    public interface PlayerStat : IItem
    {
        PlayerStatEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable]
    public class PlayerStatEnum : Enumeration
    {
        public PlayerStatEnum() : base(0, null) { }
        public PlayerStatEnum(int id, string name) : base(id, name) { }

        public readonly string Description;

        public readonly static PlayerStatEnum AuralSolved = new(0, "Aural Solved");
        public readonly static PlayerStatEnum AuralFailed = new(1, "Aural Failed");
        public readonly static PlayerStatEnum TheorySolved = new(2, "Theory Solved");
        public readonly static PlayerStatEnum TheoryFailed = new(3, "Theory Failed");
        public readonly static PlayerStatEnum GramoSolved = new(4, "Gramophone Solved");
        public readonly static PlayerStatEnum GramoFailed = new(5, "Gramophone Failed");
        public readonly static PlayerStatEnum FishCaught = new(6, "Fish Caught");
        public readonly static PlayerStatEnum FishLost = new(7, "Fish Lost");
        public readonly static PlayerStatEnum Hit = new(8, "Batterie Hit");
        public readonly static PlayerStatEnum Miss = new(9, "Batterie Miss");
        public readonly static PlayerStatEnum PatternsFound = new(10, "Patterns Found");
        public readonly static PlayerStatEnum PatternsSpent = new(11, "Patterns Spent");
        public readonly static PlayerStatEnum PatternsAvailable = new(12, "Patterns Available");

        public static IItem ToItem(PlayerStatEnum @enum) => @enum switch
        {
            _ when @enum == AuralSolved => new AuralSolved(),
            _ when @enum == AuralFailed => new AuralFailed(),
            _ when @enum == TheorySolved => new TheorySolved(),
            _ when @enum == TheoryFailed => new TheoryFailed(),
            _ when @enum == GramoSolved => new GramoSolved(),
            _ when @enum == GramoFailed => new GramoFailed(),
            _ when @enum == FishCaught => new FishCaught(),
            _ when @enum == FishLost => new FishLost(),
            _ when @enum == Hit => new Hit(),
            _ when @enum == Miss => new Miss(),
            _ when @enum == PatternsFound => new PatternsFound(),
            _ when @enum == PatternsSpent => new PatternsSpent(),
            _ when @enum == PatternsAvailable => new PatternsAvailable(),
            _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
        };
    }

    [Serializable] public readonly struct AuralSolved : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.AuralSolved; }
    [Serializable] public readonly struct AuralFailed : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.AuralFailed; }
    [Serializable] public readonly struct TheorySolved : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.TheorySolved; }
    [Serializable] public readonly struct TheoryFailed : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.TheoryFailed; }
    [Serializable] public readonly struct GramoSolved : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.GramoSolved; }
    [Serializable] public readonly struct GramoFailed : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.GramoFailed; }
    [Serializable] public readonly struct FishCaught : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.FishCaught; }
    [Serializable] public readonly struct FishLost : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.FishLost; }
    [Serializable] public readonly struct Hit : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.Hit; }
    [Serializable] public readonly struct Miss : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.Miss; }
    [Serializable] public readonly struct PatternsFound : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.PatternsFound; }
    [Serializable] public readonly struct PatternsSpent : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.PatternsSpent; }
    [Serializable] public readonly struct PatternsAvailable : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.PatternsAvailable; }
}