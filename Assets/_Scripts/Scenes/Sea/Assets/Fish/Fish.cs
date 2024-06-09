using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Two;

namespace Sea
{
    public class Fish : ISceneObject
    {
        public Fish(State currentState, Manager data)
        {
            Difficulty = new FishDifficultySetter(data);
            IMAFish = Assets.SailFishPrefab;//todo difficultylevel switch
            FishType = IMAFish.FishType;
            TF.SetParent(WorldMapScene.Io.TheSea.transform);
            Collidable = new NotCollidable(IMAFish.Col);
            Interactable = new FishingInteraction(currentState, data.Fish, data.ActiveShip, this);
            Triggerable = new NotTriggerable();
            UpdatePosition = new UpdateFishPosition();
            Telemeter = new FishTelemetry();
            Instantiator = new ItemInstantiator(
                IMAFish.ToInstantiate,
                Vector3.one,
                new Vector3(0, Random.Range(0f, 360f), 0));
            Description = new SceneObjectDescription("Sailfish");

            int patterns = (int)(10 * (Difficulty.DifficultyLevel.ID + 1) * data.Skill.GetBonusRatio(new Apophenia()));

            Inventoriable = new Inventoriable(new (IData IData, IItem DataItem, int Amount)[]{
                // (data.Fish, Difficulty.DifficultyLevel, 1),
                (data.Inventory, new Ration(), Difficulty.DifficultyLevel.ID + 1),
                // (data.FishData,region switch
                //  {
                //      MusicTheory.RegionalMode.Ionian or MusicTheory.RegionalMode.Dorian => FishData.DataItem.SailFish,
                //      MusicTheory.RegionalMode.Lydian or MusicTheory.RegionalMode.MixoLydian => FishData.DataItem.Carp,
                //      MusicTheory.RegionalMode.Phrygian or MusicTheory.RegionalMode.Aeolian => FishData.DataItem.Sturgeon,
                //      MusicTheory.RegionalMode.Locrian => FishData.DataItem.Tuna,
                //      _ => FishData.DataItem.Shark,
                //  }, 1),
                (data.Player, new PatternsFound(), patterns),
                 });//(data.Player, new PatternsAvailable(), patterns)

        }

        public IMAFish IMAFish;
        public Transform TF => IMAFish.GO.transform;
        public GameObject GO => IMAFish.GO;

        public ITelemeter Telemeter { get; private set; }
        public ICollidable Collidable { get; private set; }
        public IInteractable Interactable { get; private set; }
        public ITriggerable Triggerable { get; private set; }
        public IUpdatePosition UpdatePosition { get; private set; }
        public IDescription Description { get; private set; }
        public IInstantiable Instantiator { get; private set; }
        public Data.Two.IFish FishType;
        public IInventoriable Inventoriable { get; private set; }
        public IQuestable Questable => new NotQuestable();
        public IDifficulty Difficulty { get; }
    }

    public interface IMAFish
    {
        public GameObject GO { get; }
        public GameObject ToInstantiate { get; }
        public CapsuleCollider Col { get; }
        public Data.Two.IFish FishType { get; }
    }
}