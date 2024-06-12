using System;
namespace Data
{
    public interface ISkill : IItem
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
        public static SkillEnum Preparation = new(1, "Preparation", "Instantiate with more materials, rations, and gold.", 2, 7, 50);
        public static SkillEnum CriticalVolley = new(2, "Critical Volley", "Increase damage after firing a perfectly timed volley.", 2, 10, 65);
        public static SkillEnum PerfectTiming = new(3, "Perfect Timing", "Increase the margin of error for a successful Critical Volley.", 1, 25, 15);
        public static SkillEnum PulsePerception = new(4, "Pulse Perception", "Fishing for beats is quicker.", 2, 2, 50);
        public static SkillEnum CelestialNavigation = new(5, "Celestial Navigation", "Get an extra chance to triangulate Star Charts.", 100, 5000, 2);
        public static SkillEnum LightTouch = new(6, "Light Touch", "Get an extra chance to solve Gramophones.", 100, 7500, 2);

        public static IItem ToItem(SkillEnum @enum) => @enum switch
        {
            _ when @enum == Apophenia => new Apophenia(),
            _ when @enum == Preparation => new Preparation(),
            _ when @enum == CriticalVolley => new CriticalVolley(),
            _ when @enum == PerfectTiming => new PerfectTiming(),
            _ when @enum == PulsePerception => new PulsePerception(),
            _ when @enum == CelestialNavigation => new CelestialNavigation(),
            _ when @enum == LightTouch => new LightTouch(),
            _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
        };
    }

    [Serializable] public struct Apophenia : ISkill { public readonly SkillEnum Enum => SkillEnum.Apophenia; }
    [Serializable] public struct Preparation : ISkill { public readonly SkillEnum Enum => SkillEnum.Preparation; }
    [Serializable] public struct CriticalVolley : ISkill { public readonly SkillEnum Enum => SkillEnum.CriticalVolley; }
    [Serializable] public struct PerfectTiming : ISkill { public readonly SkillEnum Enum => SkillEnum.PerfectTiming; }
    [Serializable] public struct PulsePerception : ISkill { public readonly SkillEnum Enum => SkillEnum.PulsePerception; }
    [Serializable] public struct CelestialNavigation : ISkill { public readonly SkillEnum Enum => SkillEnum.CelestialNavigation; }
    [Serializable] public struct LightTouch : ISkill { public readonly SkillEnum Enum => SkillEnum.LightTouch; }
}