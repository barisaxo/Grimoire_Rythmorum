using UnityEngine;
using Data;
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
                float percent = (float)((float)caught / (float)total);
                float rand = Random.value;

                List<IItem> enums = new() { new SailFish() };
                if (caught > 9 || total > 19) enums.Add(new Carp());
                if (caught > 24 || total > 49) enums.Add(new Tuna());
                if (caught > 69 || total > 99) enums.Add(new Sturgeon());
                if (caught > 99 || total > 149) enums.Add(new Shark());

                int weightedRand = Helpers.WeightedRandomInt(rand, percent, enums.Count);

                // Debug.Log(enums.Count + " " + rand + " " + percent + " " + weightedRand);

                return enums[weightedRand];
            }
        }
    }

    public class StarChartDifficultySetter : IDifficulty
    {
        public StarChartDifficultySetter(Manager manager)
        {
            Manager = manager;
        }

        public Manager Manager { get; }
        public IItem DifficultyLevel
        {
            get
            {
                int theorySolved = Manager.Player.GetLevel(new TheorySolved());
                int theoryFailed = Manager.Player.GetLevel(new TheoryFailed());
                int totalTheory = theorySolved + theoryFailed;
                if (totalTheory < 6) return new NotesT();

                int auralSolved = Manager.Player.GetLevel(new AuralSolved());
                int auralFailed = Manager.Player.GetLevel(new AuralFailed());
                int totalAural = auralSolved + auralFailed;
                if (totalAural < 6) return new NotesA();

                bool aOrT = Random.value < .666f;
                float percent = aOrT ?
                    (float)((float)theorySolved / (float)totalTheory) :
                    (float)((float)auralSolved / (float)totalAural);

                List<IItem> Puzzles = new() { aOrT ? new NotesT() : new NotesA() };
                if (aOrT)
                {
                    if (theorySolved > 5) Puzzles.Add(new StepsT());
                    if (theorySolved > 10) Puzzles.Add(new ScalesT());
                    if (theorySolved > 15) Puzzles.Add(new IntervalsT());
                    if (theorySolved > 20) Puzzles.Add(new TriadsT());
                    if (theorySolved > 25) Puzzles.Add(new InversionsT());
                    if (theorySolved > 30) Puzzles.Add(new InvertedTriadsT());
                    if (theorySolved > 35) Puzzles.Add(new SeventhChordsT());
                    if (theorySolved > 40) Puzzles.Add(new ModesT());
                    if (theorySolved > 45) Puzzles.Add(new Inverted7thChordsT());
                }
                else
                {
                    if (auralSolved > 5) Puzzles.Add(new StepsA());
                    if (auralSolved > 10) Puzzles.Add(new ScalesA());
                    if (auralSolved > 15) Puzzles.Add(new IntervalsA());
                    if (auralSolved > 20) Puzzles.Add(new TriadsA());
                    if (auralSolved > 25) Puzzles.Add(new InversionsA());
                    if (auralSolved > 30) Puzzles.Add(new InvertedTriadsA());
                    if (auralSolved > 35) Puzzles.Add(new SeventhChordsA());
                    if (auralSolved > 40) Puzzles.Add(new ModesA());
                    if (auralSolved > 45) Puzzles.Add(new Inverted7thChordsA());
                }
                float rand = Random.value;

                int weightedRand = Helpers.WeightedRandomInt(rand, percent, Puzzles.Count);

                // Debug.Log(Puzzles.Count + " " + rand + " " + percent + " " + weightedRand);

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
