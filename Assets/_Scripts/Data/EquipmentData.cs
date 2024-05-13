using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Two
{
    public interface Cannon : IItem
    {
        CannonEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
        float Modifier => Enum.Modifier;
    }

    [Serializable] public readonly struct Mynion : Cannon { public readonly CannonEnum Enum => CannonEnum.Mynion; }
    [Serializable] public readonly struct Saker : Cannon { public readonly CannonEnum Enum => CannonEnum.Saker; }
    [Serializable] public readonly struct Culverin : Cannon { public readonly CannonEnum Enum => CannonEnum.Culverin; }
    [Serializable] public readonly struct DemiCannon : Cannon { public readonly CannonEnum Enum => CannonEnum.DemiCannon; }
    [Serializable] public readonly struct Carronade : Cannon { public readonly CannonEnum Enum => CannonEnum.Carronade; }


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
        // public CannonEnum(int id, string name, string description, float modifier, Func<Sprite> sprite) : base(id, name)
        // {
        //     Description = description;
        //     Modifier = modifier;
        //     Sprite = sprite;
        // }

        public readonly string Description;
        public readonly float Modifier;
        // public readonly Func<Sprite> Sprite = null;

        public readonly static CannonEnum Mynion = new(0, "Mynion", "Inexpensive, weak cannon", 32);
        public readonly static CannonEnum Saker = new(1, "Saker", "Moderately inexpensive, moderately weak cannon", 40);
        public readonly static CannonEnum Culverin = new(2, "Culverin", "Moderately Expensive, moderately powerful cannon", 52);
        public readonly static CannonEnum DemiCannon = new(3, "DemiCannon", "Expensive, powerful cannon", 64);
        public readonly static CannonEnum Carronade = new(4, "Carronade", "Very expensive, very powerful cannon", 72);
    }

    public interface BoatHull : IItem
    {
        HullEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
        float Modifier => Enum.Modifier;
    }

    [Serializable] public readonly struct Sloop : BoatHull { public readonly HullEnum Enum => HullEnum.Sloop; }
    [Serializable] public readonly struct Schooner : BoatHull { public readonly HullEnum Enum => HullEnum.Schooner; }
    [Serializable] public readonly struct Frigate : BoatHull { public readonly HullEnum Enum => HullEnum.Frigate; }

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
        // public HullEnum(int id, string name, string description, float modifier, Func<Sprite> sprite) : base(id, name)
        // {
        //     Description = description;
        //     Modifier = modifier;
        //     Sprite = sprite;
        // }

        public readonly string Description;
        public readonly float Modifier;
        // public readonly Func<Sprite> Sprite = null;

        public readonly static HullEnum Sloop = new(0, "Sloop", "Small versatile vessel, with minimal armament", 256);
        public readonly static HullEnum Schooner = new(1, "Schooner", "Known for their speed and versatility, and boast a decent armament", 1024);
        public readonly static HullEnum Frigate = new(2, "Frigate", "Superior combination of speed, firepower, and endurance", 3056);

        internal static IItem ToItem(HullEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == Sloop => new Sloop(),
                _ when @enum == Schooner => new Schooner(),
                _ when @enum == Frigate => new Frigate(),
                _ => throw new System.ArgumentException(@enum.Name),
            };
        }
    }
}