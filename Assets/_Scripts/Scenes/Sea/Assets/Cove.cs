using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sea
{
    public class NullCove : ISceneObject
    {
        public NullCove()
        {
            CovePrefab = Assets.NullCove;
            TF.SetParent(WorldMapScene.Io.TheSea.transform);
            Collidable = new RockCollision(CovePrefab.Col);
            Interactable = new CoveInteraction();
            Triggerable = new NotTriggerable();
            UpdatePosition = new UpdateCovePosition();
            Telemeter = new RockTelemetry();
        }

        public CovePrefab CovePrefab;
        public Transform TF => CovePrefab.transform;
        public GameObject GO => CovePrefab.gameObject;
        public ITelemeter Telemeter { get; private set; }
        public ICollidable Collidable { get; private set; }
        public IInteractable Interactable { get; private set; }
        public ITriggerable Triggerable { get; private set; }
        public IUpdatePosition UpdatePosition { get; private set; }
        public IInstantiable Instantiable { get; private set; }
    }
}