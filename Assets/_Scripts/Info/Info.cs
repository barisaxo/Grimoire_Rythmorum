using System.Collections;
using System;
using UnityEngine;
namespace Info
{
    public interface IInfo
    {
        // public int Special { get; }
        // public int Level { get; }
        public int Cost { get; }
        public float Modifier { get; }
        public string Desc { get; }
        public Func<Sprite> Sprite { get; }
    }

    public class ItemInfo : IInfo
    {
        public int Cost { get; }
        public string Desc { get; }
        public float Modifier { get; } = 0;
        public Func<Sprite> Sprite { get; } = null;

        public ItemInfo(int cost, string desc, float modifier)
        {
            Cost = cost;
            Desc = desc;
            Modifier = modifier;
        }
        public ItemInfo(int cost, string desc, float modifier, Func<Sprite> sprite)
        {
            Cost = cost;
            Desc = desc;
            Modifier = modifier;
            Sprite = sprite;
        }
        // public static ItemInfo Pine => new ItemInfo(10, "")
    }
}