using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Two
{
    public interface ICannon : IItem
    {
        CannonEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
        float Modifier => Enum.Modifier;
    }

    [Serializable] public readonly struct Mynion : ICannon { public readonly CannonEnum Enum => CannonEnum.Mynion; }
    [Serializable] public readonly struct Saker : ICannon { public readonly CannonEnum Enum => CannonEnum.Saker; }
    [Serializable] public readonly struct Culverin : ICannon { public readonly CannonEnum Enum => CannonEnum.Culverin; }
    [Serializable] public readonly struct DemiCannon : ICannon { public readonly CannonEnum Enum => CannonEnum.DemiCannon; }
    [Serializable] public readonly struct Carronade : ICannon { public readonly CannonEnum Enum => CannonEnum.Carronade; }

    [Serializable]
    public class CannonEnum : Enumeration
    {
        public CannonEnum() : base(0, "") { }
        public CannonEnum(int id, string name) : base(id, name) { }
        public CannonEnum(int id, string name, string description, float modifier) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
        }

        public readonly string Description;
        public readonly float Modifier;

        public readonly static CannonEnum Mynion = new(0, "Mynion", "Inexpensive, weak cannon", 8);
        public readonly static CannonEnum Saker = new(1, "Saker", "Moderately inexpensive, moderately weak cannon", 9);
        public readonly static CannonEnum Culverin = new(2, "Culverin", "Moderately Expensive, moderately powerful cannon", 10);
        public readonly static CannonEnum DemiCannon = new(3, "Demi", "Expensive, powerful cannon", 11);
        public readonly static CannonEnum Carronade = new(4, "Carronade", "Very expensive, very powerful cannon", 12);

        public static ICannon GetRandomCannon() => UnityEngine.Random.Range(0, 5) switch
        {
            0 => new Mynion(),
            1 => new Saker(),
            2 => new Culverin(),
            3 => new DemiCannon(),
            4 => new Carronade(),
            _ => throw new InvalidOperationException(),
        };

        internal static ICannon ToItem(int id) => id switch
        {
            0 => new Mynion(),
            1 => new Saker(),
            2 => new Culverin(),
            3 => new DemiCannon(),
            4 => new Carronade(),
            _ => throw new InvalidOperationException(),
        };
    }

    public interface IHull : IItem
    {
        HullEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
        float Modifier => Enum.Modifier;
    }

    [Serializable] public readonly struct Sloop : IHull { public readonly HullEnum Enum => HullEnum.Sloop; }
    [Serializable] public readonly struct Cutter : IHull { public readonly HullEnum Enum => HullEnum.Cutter; }
    [Serializable] public readonly struct Schooner : IHull { public readonly HullEnum Enum => HullEnum.Schooner; }
    [Serializable] public readonly struct Brig : IHull { public readonly HullEnum Enum => HullEnum.Brig; }
    [Serializable] public readonly struct Frigate : IHull { public readonly HullEnum Enum => HullEnum.Frigate; }
    [Serializable] public readonly struct Barque : IHull { public readonly HullEnum Enum => HullEnum.Barque; }

    [Serializable]
    public class HullEnum : Enumeration
    {
        public HullEnum() : base(0, "") { }
        public HullEnum(int id, string name) : base(id, name) { }
        public HullEnum(int id, string name, string description, float modifier) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
        }

        public readonly string Description;
        public readonly float Modifier;

        public readonly static HullEnum Sloop = new(0, "Sloop", "A small vessel with a minimal armament", 512);
        public readonly static HullEnum Cutter = new(1, "Cutter", "A small yet robust vessel with a minimal armament", 768);
        public readonly static HullEnum Schooner = new(2, "Sloop", "Known for its speed, versatility, decent armament", 1024);
        public readonly static HullEnum Brig = new(3, "Brig", "A powerful small vessel, can take on any but the largest of ships", 1280);
        public readonly static HullEnum Frigate = new(4, "Frigate", "A large vessel boasting superior combination of speed, firepower, and endurance", 1536);
        public readonly static HullEnum Barque = new(5, "Barque", "Unmatched firepower and size. A very difficult ship to captain.", 1792);

        internal static IHull ToItem(HullEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == Sloop => new Sloop(),
                _ when @enum == Brig => new Brig(),
                _ when @enum == Schooner => new Schooner(),
                _ when @enum == Cutter => new Cutter(),
                _ when @enum == Frigate => new Frigate(),
                _ when @enum == Barque => new Barque(),
                _ => throw new System.ArgumentException(@enum.Name),
            };
        }
    }
}