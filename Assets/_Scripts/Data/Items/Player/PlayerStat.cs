using System;
namespace Datum
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
        public readonly static PlayerStatEnum AuralRate = new(2, "Aural Solve Rate");
        public readonly static PlayerStatEnum TheorySolved = new(3, "Theory Solved");
        public readonly static PlayerStatEnum TheoryFailed = new(4, "Theory Failed");
        public readonly static PlayerStatEnum TheoryRate = new(5, "Theory Solve Rate");
        public readonly static PlayerStatEnum GramoSolved = new(6, "Gramophone Solved");
        public readonly static PlayerStatEnum GramoFailed = new(7, "Gramophone Failed");
        public readonly static PlayerStatEnum GramoRate = new(8, "Gramophone Solve Rate");
        public readonly static PlayerStatEnum FishCaught = new(9, "Fish Caught");
        public readonly static PlayerStatEnum FishLost = new(10, "Fish Lost");
        public readonly static PlayerStatEnum FishRate = new(11, "Fish Catch Rate");
        public readonly static PlayerStatEnum Hit = new(12, "Batterie Hit");
        public readonly static PlayerStatEnum Miss = new(13, "Batterie Miss");
        public readonly static PlayerStatEnum HitRate = new(14, "Batterie Hit Rate");
        public readonly static PlayerStatEnum PatternsFound = new(15, "Patterns Found");
        public readonly static PlayerStatEnum PatternsSpent = new(16, "Patterns Spent");
        public readonly static PlayerStatEnum PatternsAvailable = new(17, "Patterns Available");

        public static IItem ToItem(PlayerStatEnum @enum) => @enum switch
        {
            _ when @enum == AuralSolved => new AuralSolved(),
            _ when @enum == AuralFailed => new AuralFailed(),
            _ when @enum == AuralRate => new AuralRate(),
            _ when @enum == TheorySolved => new TheorySolved(),
            _ when @enum == TheoryFailed => new TheoryFailed(),
            _ when @enum == TheoryRate => new TheoryRate(),
            _ when @enum == GramoSolved => new GramoSolved(),
            _ when @enum == GramoFailed => new GramoFailed(),
            _ when @enum == GramoRate => new GramoRate(),
            _ when @enum == FishCaught => new FishCaught(),
            _ when @enum == FishLost => new FishLost(),
            _ when @enum == FishRate => new FishRate(),
            _ when @enum == Hit => new Hit(),
            _ when @enum == Miss => new Miss(),
            _ when @enum == HitRate => new HitRate(),
            _ when @enum == PatternsFound => new PatternsFound(),
            _ when @enum == PatternsSpent => new PatternsSpent(),
            _ when @enum == PatternsAvailable => new PatternsAvailable(),
            _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
        };
    }

    [Serializable] public readonly struct AuralSolved : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.AuralSolved; }
    [Serializable] public readonly struct AuralFailed : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.AuralFailed; }
    [Serializable] public readonly struct AuralRate : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.AuralRate; }
    [Serializable] public readonly struct TheorySolved : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.TheorySolved; }
    [Serializable] public readonly struct TheoryFailed : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.TheoryFailed; }
    [Serializable] public readonly struct TheoryRate : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.TheoryRate; }
    [Serializable] public readonly struct GramoSolved : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.GramoSolved; }
    [Serializable] public readonly struct GramoFailed : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.GramoFailed; }
    [Serializable] public readonly struct GramoRate : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.GramoRate; }
    [Serializable] public readonly struct FishCaught : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.FishCaught; }
    [Serializable] public readonly struct FishLost : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.FishLost; }
    [Serializable] public readonly struct FishRate : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.FishRate; }
    [Serializable] public readonly struct Hit : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.Hit; }
    [Serializable] public readonly struct Miss : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.Miss; }
    [Serializable] public readonly struct HitRate : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.HitRate; }
    [Serializable] public readonly struct PatternsFound : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.PatternsFound; }
    [Serializable] public readonly struct PatternsSpent : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.PatternsSpent; }
    [Serializable] public readonly struct PatternsAvailable : PlayerStat { public readonly PlayerStatEnum Enum => PlayerStatEnum.PatternsAvailable; }


    public interface PlayerRecentStat : IItem
    {
        PlayerRecentStatEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable]
    public class PlayerRecentStatEnum : Enumeration
    {
        public PlayerRecentStatEnum() : base(0, null) { }
        public PlayerRecentStatEnum(int id, string name) : base(id, name) { }

        public readonly string Description;

        public readonly static PlayerRecentStatEnum AuralRecentSolved = new(0, "Aural Recent Solved");
        public readonly static PlayerRecentStatEnum AuralRecentFailed = new(1, "Aural Recent Failed");
        public readonly static PlayerRecentStatEnum TheoryRecentSolved = new(2, "Theory Recent Solve");
        public readonly static PlayerRecentStatEnum TheoryRecentFailed = new(3, "Theory Recent Fail");
        public readonly static PlayerRecentStatEnum GramoRecentSolved = new(4, "Gramophone Recent Solve");
        public readonly static PlayerRecentStatEnum GramoRecentFailed = new(5, "GramophoneRecent Fail");
        public readonly static PlayerRecentStatEnum FishRecentCaught = new(6, "Fish Recent Caught");
        public readonly static PlayerRecentStatEnum FishRecentLost = new(7, "Fish Recent Lost");

        public static IItem ToItem(PlayerRecentStatEnum @enum) => @enum switch
        {
            _ when @enum == AuralRecentSolved => new AuralRecentSolved(),
            _ when @enum == AuralRecentFailed => new AuralRecentFailed(),
            _ when @enum == TheoryRecentSolved => new TheoryRecentSolved(),
            _ when @enum == TheoryRecentFailed => new TheoryRecentFailed(),
            _ when @enum == GramoRecentSolved => new GramoRecentSolved(),
            _ when @enum == GramoRecentFailed => new GramoRecentFailed(),
            _ when @enum == FishRecentCaught => new FishRecentCaught(),
            _ when @enum == FishRecentLost => new FishRecentLost(),
            _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
        };
    }


    [Serializable] public readonly struct AuralRecentSolved : PlayerRecentStat { public readonly PlayerRecentStatEnum Enum => PlayerRecentStatEnum.AuralRecentSolved; }
    [Serializable] public readonly struct AuralRecentFailed : PlayerRecentStat { public readonly PlayerRecentStatEnum Enum => PlayerRecentStatEnum.AuralRecentFailed; }
    [Serializable] public readonly struct TheoryRecentSolved : PlayerRecentStat { public readonly PlayerRecentStatEnum Enum => PlayerRecentStatEnum.TheoryRecentSolved; }
    [Serializable] public readonly struct TheoryRecentFailed : PlayerRecentStat { public readonly PlayerRecentStatEnum Enum => PlayerRecentStatEnum.TheoryRecentFailed; }
    [Serializable] public readonly struct GramoRecentSolved : PlayerRecentStat { public readonly PlayerRecentStatEnum Enum => PlayerRecentStatEnum.GramoRecentSolved; }
    [Serializable] public readonly struct GramoRecentFailed : PlayerRecentStat { public readonly PlayerRecentStatEnum Enum => PlayerRecentStatEnum.GramoRecentFailed; }
    [Serializable] public readonly struct FishRecentCaught : PlayerRecentStat { public readonly PlayerRecentStatEnum Enum => PlayerRecentStatEnum.FishRecentCaught; }
    [Serializable] public readonly struct FishRecentLost : PlayerRecentStat { public readonly PlayerRecentStatEnum Enum => PlayerRecentStatEnum.FishRecentLost; }


}