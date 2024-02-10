using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sea
{
    public class Lighthouse : ISceneObject
    {
        public Lighthouse(Region region, State currentState)
        {
            Region = region;
            LighthousePrefab = Assets.Lighthouse;
            LighthousePrefab.Light.SetActive(DataManager.Io.CharacterData.ActivatedLighthouses.Contains(Region));
            TF.SetParent(WorldMapScene.Io.TheSea.transform);
            Collidable = new RockCollision(LighthousePrefab.Col);
            Interactable = new LighthouseInteraction(this, currentState);
            Triggerable = new NotTriggerable();
            UpdatePosition = new UpdateLighthousePosition();
            Telemeter = new RockTelemetry();
        }

        public void RemoveInteractable() { Interactable = new NoInteraction(); }
        public Region Region;
        public LighthousePrefab LighthousePrefab;
        public Transform TF => LighthousePrefab.transform;
        public GameObject GO => LighthousePrefab.gameObject;
        public ITelemeter Telemeter { get; private set; }
        public ICollidable Collidable { get; private set; }
        public IInteractable Interactable { get; private set; }
        public ITriggerable Triggerable { get; private set; }
        public IUpdatePosition UpdatePosition { get; private set; }
        public IInstantiable Instantiable { get; private set; }
    }
}