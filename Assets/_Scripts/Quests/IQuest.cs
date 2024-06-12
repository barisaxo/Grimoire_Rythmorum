using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;
using Data;

namespace Quests
{
    public interface IQuest
    {
        public bool Complete { get; set; }
        public IInventoriable Reward { get; }
        public Vector2Int QuestLocation { get; }
        public string Description { get; }
    }


    [System.Serializable]
    public class NavigationQuest : IQuest
    {
        public NavigationQuest(IInventoriable reward, Vector2Int globalCoord, string latLong)
        {
            Reward = reward;
            _questLocation = (globalCoord.x, globalCoord.y);
            Debug.Log(nameof(QuestLocation) + _questLocation);
            LatLong = latLong;
        }

        public bool Complete { get; set; }
        public string LatLong;
        public string Description => "Navigate to " + LatLong;
        public IInventoriable Reward { get; }

        private (int x, int y) _questLocation = (0, 0);
        public Vector2Int QuestLocation
        {
            get
            {
                // Debug.Log(_questLocation);
                return new(_questLocation.x, _questLocation.y);
            }
        }

        // public Vector2Int ReturnLocation => Vector2Int.zero;
    }

    [System.Serializable]
    public class BountyQuest : IQuest
    {
        public BountyQuest(IInventoriable reward, Standing standing, Vector2Int globalCoord, string latLong)
        {
            Reward = reward;
            _questLocation = (globalCoord.x, globalCoord.y);
            Debug.Log(nameof(QuestLocation) + _questLocation);
            LatLong = latLong;
            Standing = standing;
        }

        public Standing Standing { get; }
        public bool Complete { get; set; }
        public string LatLong;
        public string Description => "The " + Standing.ToRegionalName() + " are asking you to hunt the bounty at " + LatLong;

        public IInventoriable Reward { get; }

        private (int x, int y) _questLocation = (0, 0);
        public Vector2Int QuestLocation
        {
            get
            {
                // Debug.Log(_questLocation);
                return new(_questLocation.x, _questLocation.y);
            }
        }

        // public Vector2Int ReturnLocation => Vector2Int.zero;
    }

    public interface ISpoils
    {
        public (IData Data, IItem Item, int Amount)[] Rewards { get; }
        public void ApplyRewards()
        {
            foreach (var r in Rewards)
                r.Data.SetLevel(r.Item, r.Data.GetLevel(r.Item) + r.Amount);
        }
        // public Data.IData Data { get; }
        // public DataEnum DataItem { get; }
        // public int Amount { get; }
    }

    [System.Serializable]
    public class Spoils : ISpoils
    {
        public Spoils((IData Data, IItem Item, int Amount)[] rewards) => Rewards = rewards;
        public (IData Data, IItem Item, int Amount)[] Rewards { get; private set; }
    }

    public interface IMissionParameters
    {

    }
}