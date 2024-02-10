using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sea
{
    public class Bottle : ISceneObject
    {
        public Bottle(State state)
        {
            BottlePrefab = Assets.Bottle;
            TF.SetParent(WorldMapScene.Io.TheSea.transform);
            Collidable = new NotCollidable(BottlePrefab.Col);
            Interactable = new BottleInteraction(state);
            Triggerable = new NotTriggerable();
            UpdatePosition = new UpdateCovePosition();
            Telemeter = new RockTelemetry();
        }

        public BottleWithScrollPrefab BottlePrefab;
        public Transform TF => BottlePrefab.transform;
        public GameObject GO => BottlePrefab.gameObject;
        public ITelemeter Telemeter { get; private set; }
        public ICollidable Collidable { get; private set; }
        public IInteractable Interactable { get; private set; }
        public ITriggerable Triggerable { get; private set; }
        public IUpdatePosition UpdatePosition { get; private set; }
        public IInstantiable Instantiable { get; private set; }
    }
}