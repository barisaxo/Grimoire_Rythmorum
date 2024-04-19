using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Equipment
{
    [System.Serializable]
    public class CannonData : DataEnum
    {
        public CannonData() : base(0, "") { }
        public CannonData(int id, string name) : base(id, name) { }
        public CannonData(int id, string name, string description, float modifier) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
        }
        public CannonData(int id, string name, string description, float modifier, Func<Sprite> sprite) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
            Sprite = sprite;
        }

        public readonly float Modifier;
        public readonly Func<Sprite> Sprite = null;

        public static CannonData Mynion = new(0, "Mynion", "Inexpensive, weak cannon", 32);
        public static CannonData Saker = new(1, "Saker", "Moderately inexpensive, moderately weak cannon", 40);
        public static CannonData Culverin = new(2, "Culverin", "Moderately Expensive, moderately powerful cannon", 52);
        public static CannonData DemiCannon = new(3, "DemiCannon", "Expensive, powerful cannon", 64);
        public static CannonData Carronade = new(4, "Carronade", "Very expensive, very powerful cannon", 72);
    }

    [System.Serializable]
    public class HullData : DataEnum
    {
        public HullData() : base(0, "") { }
        public HullData(int id, string name) : base(id, name) { }
        public HullData(int id, string name, string description, float modifier) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
        }
        public HullData(int id, string name, string description, float modifier, Func<Sprite> sprite) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
            Sprite = sprite;
        }

        public readonly float Modifier;
        public readonly Func<Sprite> Sprite = null;

        public static HullData Sloop = new(0, "Sloop", "Small versatile vessel, with minimal armament", 256);
        public static HullData Schooner = new(1, "Schooner", "Known for their speed and versatility, and boast a decent armament", 1024);
        public static HullData Frigate = new(2, "Frigate", "Superior combination of speed, firepower, and endurance", 3056);
    }
}