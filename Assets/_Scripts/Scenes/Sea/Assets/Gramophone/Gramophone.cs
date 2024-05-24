using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Two;

namespace Sea
{
    public class Gramophone : ISceneObject
    {
        public Gramophone(State currentState, InventoryData inventoryData, QuestData questData, PlayerShipData shipData, Sea.Maps.R region)
        {
            Gramo = Assets.Gramo;
            TF.SetParent(WorldMapScene.Io.TheSea.transform);
            Collidable = new NotCollidable(Gramo.Col);
            Interactable = new GramoInteraction(currentState, inventoryData, shipData, this);
            Triggerable = new NotTriggerable();
            UpdatePosition = new UpdateFishPosition();
            Telemeter = new FishTelemetry();
            Instantiator = new ItemInstantiator(
                toInstantiate: Assets._gramo.gameObject,
                scale: Vector3.one * .5f,
                rot: new Vector3(0, Random.Range(0f, 360f), 0));
            Description = new SceneObjectDescription("Gramophone");
            Inventoriable = new Inventoriable(
                (inventoryData,
                new Data.Two.Gramophone(),
                1));

            Questable = new Questable(questData, new Navigation());
        }

        public GramophonePrefab Gramo;
        public Transform TF => Gramo.transform;
        public GameObject GO => Gramo.gameObject;

        public ITelemeter Telemeter { get; private set; }
        public ICollidable Collidable { get; private set; }
        public IInteractable Interactable { get; private set; }
        public ITriggerable Triggerable { get; private set; }
        public IUpdatePosition UpdatePosition { get; private set; }
        public IDescription Description { get; private set; }
        public IInstantiable Instantiator { get; private set; }
        public IInventoriable Inventoriable { get; private set; }
        public Fish FishType;
        public IQuestable Questable { get; private set; }
        public IDifficulty Difficulty { get; } = new NoDifficulty();
    }
}
