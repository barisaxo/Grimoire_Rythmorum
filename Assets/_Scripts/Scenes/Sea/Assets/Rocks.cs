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
            Instantiator = new ItemInstantiator(
                Assets._rocks.gameObject,
                new Vector3(Random.Range(.85f, 1.1f), Random.Range(.85f, 1.1f), Random.Range(.85f, 1.1f)),
                new Vector3(0, Random.Range(0, 360), 0));
        }

        public RocksPrefab RocksPrefab;
        public Transform TF => RocksPrefab.transform;
        public GameObject GO => RocksPrefab.gameObject;

        public ITelemeter Telemeter { get; private set; }
        public ICollidable Collidable { get; private set; }
        public IInteractable Interactable { get; private set; }
        public ITriggerable Triggerable { get; private set; }
        public IUpdatePosition UpdatePosition { get; private set; }
        public IInstantiable Instantiator { get; private set; }
        public IDescription Description { get; private set; }
    }
}