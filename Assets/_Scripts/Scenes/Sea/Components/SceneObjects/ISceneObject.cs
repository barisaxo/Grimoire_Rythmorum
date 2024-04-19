using UnityEngine;
using Data.Inventory;
using Data;
using System.Collections.Generic;

namespace Sea
{
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
        public DataManager DataManager { get; }
        public DataEnum DifficultyLevel { get; }
    }

    public class NoDifficulty : IDifficulty
    {
        public DataManager DataManager { get; } = null;
        public DataEnum DifficultyLevel { get; } = null;
    }

    public class DifficultySetter : IDifficulty
    {
        public DifficultySetter(DataManager dataManager)
        {
            DataManager = dataManager;
        }
        public DataManager DataManager { get; }
        public DataEnum DifficultyLevel { get; }
    }

    public class FishDifficultySetter : IDifficulty
    {
        public FishDifficultySetter(DataManager dataManager)
        {
            DataManager = dataManager;
        }
        public DataManager DataManager { get; }
        public DataEnum DifficultyLevel
        {
            get
            {
                int caught = DataManager.PlayerData.GetLevel(Data.Player.PlayerData.DataItem.FishCaught);
                int lost = DataManager.PlayerData.GetLevel(Data.Player.PlayerData.DataItem.FishLost);
                int total = caught + lost;
                if (total < 10) return FishData.DataItem.SailFish;
                float percent = (float)((float)caught / (float)total);
                float rand = Random.value;

                List<DataEnum> enums = new() { FishData.DataItem.SailFish };
                if (caught > 9 || total > 19) enums.Add(FishData.DataItem.Carp);
                if (caught > 24 || total > 49) enums.Add(FishData.DataItem.Tuna);
                if (caught > 69 || total > 99) enums.Add(FishData.DataItem.Sturgeon);
                if (caught > 99 || total > 149) enums.Add(FishData.DataItem.Shark);

                int weightedRand = Helpers.WeightedRandomInt(rand, percent, enums.Count);

                // Debug.Log(enums.Count + " " + rand + " " + percent + " " + weightedRand);

                return enums[weightedRand];
            }
        }
    }

    public class StarChartDifficultySetter : IDifficulty
    {
        public StarChartDifficultySetter(DataManager dataManager)
        {
            DataManager = dataManager;
        }

        public DataManager DataManager { get; }
        public DataEnum DifficultyLevel
        {
            get
            {
                int theorySolved = DataManager.PlayerData.GetLevel(Data.Player.PlayerData.DataItem.TheorySolved);
                int theoryFailed = DataManager.PlayerData.GetLevel(Data.Player.PlayerData.DataItem.TheoryFailed);
                int totalTheory = theorySolved + theoryFailed;
                if (totalTheory < 6) return StarChartsData.DataItem.NotesT;

                int auralSolved = DataManager.PlayerData.GetLevel(Data.Player.PlayerData.DataItem.AuralSolved);
                int auralFailed = DataManager.PlayerData.GetLevel(Data.Player.PlayerData.DataItem.AuralFailed);
                int totalAural = auralSolved + auralFailed;
                if (totalAural < 6) return StarChartsData.DataItem.NotesA;

                bool aOrT = Random.value < .666f;
                float percent = aOrT ?
                    (float)((float)theorySolved / (float)totalTheory) :
                    (float)((float)auralSolved / (float)totalAural);

                List<DataEnum> Puzzles = new() { aOrT ? StarChartsData.DataItem.NotesT : StarChartsData.DataItem.NotesA };
                if (aOrT)
                {
                    if (theorySolved > 5) Puzzles.Add(StarChartsData.DataItem.StepsT);
                    if (theorySolved > 10) Puzzles.Add(StarChartsData.DataItem.ScalesT);
                    if (theorySolved > 15) Puzzles.Add(StarChartsData.DataItem.IntervalsT);
                    if (theorySolved > 20) Puzzles.Add(StarChartsData.DataItem.TriadsT);
                    if (theorySolved > 25) Puzzles.Add(StarChartsData.DataItem.InversionsT);
                    if (theorySolved > 30) Puzzles.Add(StarChartsData.DataItem.InvertedTriadsT);
                    if (theorySolved > 35) Puzzles.Add(StarChartsData.DataItem.SeventhChordsT);
                    if (theorySolved > 40) Puzzles.Add(StarChartsData.DataItem.ModesT);
                    if (theorySolved > 45) Puzzles.Add(StarChartsData.DataItem.Inverted7thChordsT);
                }
                else
                {
                    if (auralSolved > 5) Puzzles.Add(StarChartsData.DataItem.StepsA);
                    if (auralSolved > 10) Puzzles.Add(StarChartsData.DataItem.ScalesA);
                    if (auralSolved > 15) Puzzles.Add(StarChartsData.DataItem.IntervalsA);
                    if (auralSolved > 20) Puzzles.Add(StarChartsData.DataItem.TriadsA);
                    if (auralSolved > 25) Puzzles.Add(StarChartsData.DataItem.InversionsA);
                    if (auralSolved > 30) Puzzles.Add(StarChartsData.DataItem.InvertedTriadsA);
                    if (auralSolved > 35) Puzzles.Add(StarChartsData.DataItem.SeventhChordsA);
                    if (auralSolved > 40) Puzzles.Add(StarChartsData.DataItem.ModesA);
                    if (auralSolved > 45) Puzzles.Add(StarChartsData.DataItem.Inverted7thChordsA);
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
        public QuestData.DataItem DataItem { get; }
        public void QuestComplete();
    }

    public class Questable : IQuestable
    {
        public QuestData QuestData { get; }
        public QuestData.DataItem DataItem { get; }
        public void QuestComplete()
        {
            QuestData.SetQuest(DataItem, null);
        }
        public Questable(QuestData data, QuestData.DataItem dataItem)
        {
            QuestData = data;
            DataItem = dataItem;
        }
    }

    public class NotQuestable : IQuestable
    {
        public QuestData QuestData => null;
        public QuestData.DataItem DataItem => null;
        public void QuestComplete() { }
    }

    public interface IInventoriable
    {
        public (Data.IData Data, DataEnum DataItem, int Amount)[] Rewards { get; }
        public void AddRewards()
        {
            if (Rewards is not null) foreach (var r in Rewards)
                    r.Data.SetLevel(r.DataItem, r.Data.GetLevel(r.DataItem) + r.Amount);
        }
    }

    [System.Serializable]
    public class Inventoriable : IInventoriable
    {
        public Inventoriable((Data.IData Data, DataEnum DataItem, int Amount)[] rewards) =>
            Rewards = rewards;
        public Inventoriable((Data.IData Data, DataEnum DataItem, int Amount) rewards) =>
            Rewards = new (Data.IData Data, DataEnum DataItem, int Amount)[] { rewards };
        public (Data.IData Data, DataEnum DataItem, int Amount)[] Rewards { get; private set; }
    }

    [System.Serializable]
    public class NotInventoriable : IInventoriable
    {
        public (Data.IData Data, DataEnum DataItem, int Amount)[] Rewards { get; private set; } = null;
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
