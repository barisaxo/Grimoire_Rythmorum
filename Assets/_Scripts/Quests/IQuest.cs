using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;

namespace Quests
{
    public interface IQuest
    {
        public IInventoriable Reward { get; }
        public Vector2Int QuestLocation { get; }
        public Vector2Int ReturnLocation { get; }
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

        public string LatLong;
        public string Description => "Navigate to " + LatLong;

        public IInventoriable Reward { get; }

        private (int x, int y) _questLocation = (0, 0);
        public Vector2Int QuestLocation
        {
            get
            {
                Debug.Log(_questLocation);
                return new(_questLocation.x, _questLocation.y);
            }
        }

        public Vector2Int ReturnLocation => Vector2Int.zero;
    }

    public interface ISpoils
    {
        public (Data.IData Data, DataEnum DataItem, int Amount)[] Rewards { get; }
        public void ApplyRewards()
        {
            foreach (var r in Rewards)
                r.Data.SetLevel(r.DataItem, r.Data.GetLevel(r.DataItem) + r.Amount);
        }
        // public Data.IData Data { get; }
        // public DataEnum DataItem { get; }
        // public int Amount { get; }
    }

    [System.Serializable]
    public class Spoils : ISpoils
    {
        public Spoils((Data.IData Data, DataEnum DataItem, int Amount)[] rewards) => Rewards = rewards;
        public (Data.IData Data, DataEnum DataItem, int Amount)[] Rewards { get; private set; }
    }

    public interface IMissionParameters
    {

    }
}