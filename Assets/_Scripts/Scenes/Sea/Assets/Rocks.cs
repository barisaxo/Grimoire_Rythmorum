using System.Collections;
using UnityEngine;

namespace Sea
{
    public class Rocks : ISceneObject
    {
        public Rocks()
        {
            RocksPrefab = Assets.Rocks;
            TF.SetParent(WorldMapScene.Io.TheSea.transform);
            Collidable = new RockCollision(RocksPrefab.Col);
            Interactable = new NoInteraction();
            Triggerable = new NotTriggerable();
            UpdatePosition = new UpdateRockPosition();
            Telemeter = new RockTelemetry();
        }

        public RocksPrefab RocksPrefab;
        public Transform TF => RocksPrefab.transform;
        public GameObject GO => RocksPrefab.gameObject;

        public ITelemeter Telemeter { get; private set; }
        public ICollidable Collidable { get; private set; }
        public IInteractable Interactable { get; private set; }
        public ITriggerable Triggerable { get; private set; }
        public IUpdatePosition UpdatePosition { get; private set; }
        public IInstantiable Instantiable { get; private set; }
    }
}