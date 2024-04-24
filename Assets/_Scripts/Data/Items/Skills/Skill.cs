namespace Data.Two
{
    public interface Skill : IItem
    {
        // public Skill(SkillEnum @enum) { Enum = @enum; }
        static SkillEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
        int Cost => Enum.Cost;
        int Per => Enum.Per;
        int MaxLevel => Enum.MaxLevel;
    }

    [System.Serializable]
    public class SkillEnum : Enumeration
    {
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

        public static SkillEnum Apophenia = new(0, "Apophenia", "Find more patterns, even if they aren't really there.", 2, 8, 50);
        public static SkillEnum CosmicConsciousness = new(1, "Cosmic Consciousness", "Retain some unsaved patterns when you lose your ship.", 1, 10, 50);
        public static SkillEnum Preparation = new(2, "Preparation", "Instantiate with more materials, rations, and gold.", 2, 7, 50);
        public static SkillEnum PulsePerception = new(3, "Pulse Perception", "Fishing for beats is quicker.", 2, 2, 50);
        public static SkillEnum CelestialNavigation = new(4, "Celestial Navigation", "Get an extra chance to triangulate Star Charts.", 100, 5000, 2);
        public static SkillEnum LightTouch = new(5, "Light Touch", "Get an extra chance to solve Gramophones.", 100, 7500, 2);
    }

    [System.Serializable]
    public struct Apophenia : Skill { public static SkillEnum Enum => SkillEnum.Apophenia; }

    [System.Serializable]
    public class CosmicConsciousness : Skill { public static SkillEnum Enum => SkillEnum.CosmicConsciousness; }

    [System.Serializable]
    public class Preparation : Skill { public static SkillEnum Enum => SkillEnum.Preparation; }

    [System.Serializable]
    public class PulsePerception : Skill { public static SkillEnum Enum => SkillEnum.PulsePerception; }

    [System.Serializable]
    public class CelestialNavigation : Skill { public static SkillEnum Enum => SkillEnum.CelestialNavigation; }

    [System.Serializable]
    public class LightTouch : Skill { public static SkillEnum Enum => SkillEnum.LightTouch; }
}