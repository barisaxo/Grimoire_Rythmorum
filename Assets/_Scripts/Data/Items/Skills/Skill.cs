using System;
namespace Data.Two
{
    public interface Skill : IItem
    {
        SkillEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
        int Cost => Enum.Cost;
        int Per => Enum.Per;
        int MaxLevel => Enum.MaxLevel;
    }

    [Serializable]
    public class SkillEnum : Enumeration
    {
        public SkillEnum() : base(0, null) { }
        public SkillEnum(int id, string name) : base(id, name) { }
        public SkillEnum(int id, string name, string description, int per, int cost, int max) : base(id, name)
        {
            Description = description;
            Per = per;
            Cost = cost;
            MaxLevel = max;
        }

        public readonly string Description;
        public readonly int Per;
        public readonly int Cost;
        public readonly int MaxLevel;

        public static SkillEnum Apophenia = new(0, "Apophenia", "Find more patterns, even if they aren't really there.", 4, 15, 50);
        // public static SkillEnum CosmicConsciousness = new(1, "Cosmic Consciousness", "Retain some unsaved patterns when you lose your ship.", 1, 10, 50);
        public static SkillEnum Preparation = new(1, "Preparation", "Instantiate with more materials, rations, and gold.", 2, 7, 50);
        public static SkillEnum PulsePerception = new(2, "Pulse Perception", "Fishing for beats is quicker.", 2, 2, 50);
        public static SkillEnum CelestialNavigation = new(3, "Celestial Navigation", "Get an extra chance to triangulate Star Charts.", 100, 5000, 2);
        public static SkillEnum LightTouch = new(4, "Light Touch", "Get an extra chance to solve Gramophones.", 100, 7500, 2);

        public static IItem ToItem(SkillEnum @enum) => @enum switch
        {
            _ when @enum == Apophenia => new Apophenia(),
            // _ when @enum == CosmicConsciousness => new CosmicConsciousness(),
            _ when @enum == Preparation => new Preparation(),
            _ when @enum == PulsePerception => new PulsePerception(),
            _ when @enum == CelestialNavigation => new CelestialNavigation(),
            _ when @enum == LightTouch => new LightTouch(),
            _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
        };
    }

    [Serializable] public struct Apophenia : Skill { public readonly SkillEnum Enum => SkillEnum.Apophenia; }
    // [Serializable] public struct CosmicConsciousness : Skill { public readonly SkillEnum Enum => SkillEnum.CosmicConsciousness; }
    [Serializable] public struct Preparation : Skill { public readonly SkillEnum Enum => SkillEnum.Preparation; }
    [Serializable] public struct PulsePerception : Skill { public readonly SkillEnum Enum => SkillEnum.PulsePerception; }
    [Serializable] public struct CelestialNavigation : Skill { public readonly SkillEnum Enum => SkillEnum.CelestialNavigation; }
    [Serializable] public struct LightTouch : Skill { public readonly SkillEnum Enum => SkillEnum.LightTouch; }
}