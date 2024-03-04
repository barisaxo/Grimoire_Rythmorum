using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public interface IQuest
    {

        public IQuestReward Reward { get; }
        public Vector2Int QuestLocation { get; }
        public Vector2Int ReturnLocation { get; }

    }


    public class NavigationQuest : IQuest
    {
        public NavigationQuest(IQuestReward reward, Vector2Int loc)
        {
            Reward = reward;
            QuestLocation = loc;
        }

        public IQuestReward Reward { get; }

        public Vector2Int QuestLocation { get; }

        public Vector2Int ReturnLocation => Vector2Int.zero;
    }

    public interface IQuestReward
    {
        public (Data.Inventory.MaterialsData.DataItem, int)[] Mats { get; }
        // public int Hemp { get; }
        // public int Cotton { get; }
        // public int Linen { get; }
        // public int Silk { get; }
        // public int Pine { get; }
        // public int Fir { get; }
        // public int Oak { get; }
        // public int Teak { get; }
        // public int Iron { get; }
        // public int CastIron { get; }
        // public int Brass { get; }
        // public int Patina { get; }


        public int Gramophones { get; }
        public int Gold { get; }

    }

    public class SmallGramoReward : IQuestReward
    {
        public (Data.Inventory.MaterialsData.DataItem, int)[] Mats { get; }
        // public int Hemp { get; }
        // public int Cotton { get; }
        // public int Linen { get; }
        // public int Silk { get; }
        // public int Pine { get; }
        // public int Fir { get; }
        // public int Oak { get; }
        // public int Teak { get; }
        // public int Iron { get; }
        // public int CastIron { get; }
        // public int Brass { get; }
        // public int Patina { get; }


        public int Gramophones { get; }
        public int Gold { get; }
    }

    public interface IMissionParameters
    {

    }
}