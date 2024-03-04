using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Inventory;

namespace Sea
{
    public class Fish : ISceneObject
    {
        public Fish(State currentState, FishData fishData, ShipData shipData, IMAFish fish)
        {
            IMAFish = fish;
            FishType = IMAFish.FishType;
            TF.SetParent(WorldMapScene.Io.TheSea.transform);
            Collidable = new NotCollidable(IMAFish.Col);
            Interactable = new FishingInteraction(currentState, fishData, shipData, this);
            Triggerable = new NotTriggerable();
            UpdatePosition = new UpdateFishPosition();
            Telemeter = new FishTelemetry();
            Instantiator = new ItemInstantiator(
                IMAFish.ToInstantiate,
                Vector3.one,
                new Vector3(0, Random.Range(0f, 360f), 0));
            Description = new SceneObjectDescription("Sailfish");
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
        public FishData.DataItem FishType;
    }

    public interface IMAFish
    {
        public GameObject GO { get; }
        public GameObject ToInstantiate { get; }
        public CapsuleCollider Col { get; }
        public FishData.DataItem FishType { get; }
    }
}