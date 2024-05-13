using System;
namespace Data.Two
{
    public interface GameplayOption : IItem
    {
        GameplayEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public struct Latency : GameplayOption { public readonly GameplayEnum Enum => GameplayEnum.Latency; }
    [Serializable] public struct Transpose : GameplayOption { public readonly GameplayEnum Enum => GameplayEnum.Transpose; }
    [Serializable] public struct Tuning : GameplayOption { public readonly GameplayEnum Enum => GameplayEnum.Tuning; }

    [Serializable]
    public class GameplayEnum : Enumeration
    {
        public GameplayEnum() : base(0, "") { }
        public GameplayEnum(int id, string name) : base(id, name) { }
        public GameplayEnum(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }

        public readonly string Description;
        public static readonly GameplayEnum Latency = new(0, "LATENCY",
            "Lag offset for rhythm input. The margin for an accurate hit is +- 15." +
            "\nIf you are missing beats try adjusting this latency. Default setting is 5");

        public static readonly GameplayEnum Transpose = new(1, "KEY TRANSPOSITION",
            "C: Concert pitch: flute, piano, guitar, violin, etc..." +
            "\nEb: Alto & baritone saxophone" +
            "\nF: French horn" +
            "\nBb: Clarinet, trumpet, soprano & tenor saxophone" +
            "\nB: Guitar in Eb standard tuning");

        public static GameplayEnum Tuning = new(2, "TUNING NOTE A 440",
            "If your 'A' note doesn't match this \nyou might be out of tune, or in the wrong key");

        public static IItem ToItem(GameplayEnum @enum) => @enum switch
        {
            _ when @enum == Latency => new Latency(),
            _ when @enum == Transpose => new Transpose(),
            _ when @enum == Tuning => new Tuning(),
            _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
        };
    }
}