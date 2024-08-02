using UnityEngine;
using Datum;
using System.Collections.Generic;

namespace Sea
{
    /// <summary>
    /// SceneObjects interact with the SeaSceneSystems
    /// </summary>
    public interface ISceneObject
    {
        public GameObject GO { get; }
        public ITelemeter Telemeter { get; }
        public ICollidable Collidable { get; }
        public IInteractable Interactable { get; }
        public ITriggerable Triggerable { get; }
        public IUpdatePosition UpdatePosition { get; }
        public IDescription Description { get; }
        public IInstantiable Instantiator { get; }
        public IInventoriable Inventoriable { get; }
        public IQuestable Questable { get; }
        public IDifficulty Difficulty { get; }
    }

    public interface IDifficulty
    {
        public Manager Manager { get; }
        public IItem DifficultyLevel { get; }
    }

    public class NoDifficulty : IDifficulty
    {
        public Manager Manager { get; } = null;
        public IItem DifficultyLevel { get; } = null;
    }

    public class DifficultySetter : IDifficulty
    {
        public DifficultySetter(Manager manager)
        {
            Manager = manager;
        }
        public Manager Manager { get; }
        public IItem DifficultyLevel { get; }
    }

    public class FishDifficultySetter : IDifficulty
    {
        public FishDifficultySetter(Manager manager)
        {
            Manager = manager;
        }
        public Manager Manager { get; }
        public IItem DifficultyLevel
        {
            get
            {
                int caught = Manager.Player.GetLevel(new FishCaught());
                int lost = Manager.Player.GetLevel(new FishLost());
                int total = caught + lost;
                if (total < 10) return new SailFish();

                List<IItem> enums = new() { new SailFish() };
                if (caught > 9 || total > 19) enums.Add(new Carp());
                if (caught > 24 || total > 49) enums.Add(new Tuna());
                if (caught > 69 || total > 99) enums.Add(new Sturgeon());
                if (caught > 99 || total > 149) enums.Add(new Shark());

                int weightedRand = Helpers.WeightedRandomInt(enums.Count);

                // Debug.Log(enums.Count + " " + rand + " " + percent + " " + weightedRand);

                return enums[weightedRand];
            }
        }
    }

    public class GramophoneDifficultySetter : IDifficulty
    {
        public GramophoneDifficultySetter(Manager manager)
        {
            Manager = manager;
        }

        public Manager Manager { get; }
        public IItem DifficultyLevel
        {
            get
            {
                int gramoSolved = Manager.Player.GetLevel(new GramoSolved());
                int gramoFailed = Manager.Player.GetLevel(new GramoFailed());
                int totalGramo = gramoSolved + gramoFailed;
                if (totalGramo < 6) return new Gramo1();

                List<IItem> Puzzles = new() { new Gramo1() };

                if (gramoSolved > 5) Puzzles.Add(new Gramo2());
                if (gramoSolved > 10) Puzzles.Add(new Gramo3());
                if (gramoSolved > 15) Puzzles.Add(new Gramo4());
                if (gramoSolved > 20) Puzzles.Add(new Gramo5());

                int weightedRand = Helpers.WeightedRandomInt(Puzzles.Count);

                return Puzzles[weightedRand];
            }
        }
    }

    public interface IQuestable
    {
        public QuestData QuestData { get; }
        public IQuest Quest { get; }
        public void QuestComplete();
    }

    public class Questable : IQuestable
    {
        public QuestData QuestData { get; }
        public IQuest Quest { get; }
        public void QuestComplete()
        {
            QuestData.SetQuest(Quest, null);
        }
        public Questable(QuestData data, IQuest dataItem)
        {
            QuestData = data;
            Quest = dataItem;
        }
    }

    public class NotQuestable : IQuestable
    {
        public QuestData QuestData => null;
        public IQuest Quest => null;
        public void QuestComplete() { }
    }

    public interface IInventoriable
    {
        public (IData Data, IItem DataItem, int Amount)[] Rewards { get; }

        public void AddRewards()
        {
            if (Rewards is not null) foreach (var r in Rewards)
                    r.Data.AdjustLevel(r.DataItem, r.Amount);
        }
    }

    [System.Serializable]
    public class Inventoriable : IInventoriable
    {
        public Inventoriable((IData Data, IItem DataItem, int Amount)[] rewards) =>
            Rewards = rewards;

        public Inventoriable((IData Data, IItem DataItem, int Amount) rewards) =>
            Rewards = new (IData Data, IItem DataItem, int Amount)[] { rewards };

        public (IData Data, IItem DataItem, int Amount)[] Rewards { get; private set; }
    }

    [System.Serializable]
    public class NotInventoriable : IInventoriable
    {
        public (IData Data, IItem DataItem, int Amount)[] Rewards { get; private set; } = null;
    }

    public interface IDescription
    {
        public string Name { get; }
        public string Desc { get; }
    }

    public class SceneObjectDescription : IDescription
    {
        public SceneObjectDescription(string name)
        {
            Name = name;
            // Desc = desc;
        }
        public string Desc { get; private set; }
        public string Name { get; private set; }
    }

    public interface IInstantiable
    {
        public GameObject GetInstantiation();
        public GameObject ToInstantiate { get; }
        public Vector3 Scale { get; set; }
        public Vector3 Rot { get; set; }
    }

    // public class InstantiateFish : IInstantiable
    // {
    //     public InstantiateFish(GameObject toInstantiate)
    //     {
    //         ToInstantiate = toInstantiate;
    //     }

    //     public GameObject Instantiate()
    //     {
    //         return GameObject.Instantiate(ToInstantiate);
    //     }

    //     public GameObject ToInstantiate { get; private set; }
    //     public Vector3 Scale { get; set; }
    //     public Vector3 Rot { get; set; }
    // }

    public class ItemInstantiator : IInstantiable
    {
        public ItemInstantiator(GameObject toInstantiate, Vector3 scale, Vector3 rot)
        {
            _toInstantiate = toInstantiate;
            Scale = scale;
            Rot = rot;
        }

        // public InstantiateItem(GameObject toInstantiate)
        // {
        //     ToInstantiate = toInstantiate;
        // }

        public GameObject GetInstantiation()
        {
            return GameObject.Instantiate(ToInstantiate);
        }

        private GameObject _toInstantiate;
        public GameObject ToInstantiate
        {
            get
            {
                Debug.Log(nameof(GetInstantiation) + " " + _toInstantiate.name);
                return _toInstantiate;
            }
            private set => _toInstantiate = value;
        }
        public Vector3 Scale { get; set; }
        public Vector3 Rot { get; set; }
    }
}
