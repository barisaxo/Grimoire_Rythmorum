using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sea
{
    public class Fish : ISceneObject
    {
        public Fish(State currentState)
        {
            FishPrefab = Assets.SailFish;
            TF.SetParent(WorldMapScene.Io.TheSea.transform);
            Collidable = new NotCollidable(FishPrefab.Col);
            Interactable = new FishingInteraction(currentState);
            Triggerable = new NotTriggerable();
            UpdatePosition = new UpdateFishPosition();
            Telemeter = new FishTelemetry();
        }

        public FishPrefab FishPrefab;
        public Transform TF => FishPrefab.transform;
        public GameObject GO => FishPrefab.gameObject;

        public ITelemeter Telemeter { get; private set; }
        public ICollidable Collidable { get; private set; }
        public IInteractable Interactable { get; private set; }
        public ITriggerable Triggerable { get; private set; }
        public IUpdatePosition UpdatePosition { get; private set; }
        public IInstantiable Instantiable { get; private set; }
    }


}