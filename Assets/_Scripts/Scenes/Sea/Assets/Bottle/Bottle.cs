using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Inventory;
namespace Sea
{
    public class Bottle : ISceneObject
    {
        public Bottle(State state, DataManager data)
        {
            Difficulty = new StarChartDifficultySetter(data);
            GO = (BottlePrefab = Assets.Bottle).gameObject;
            GO.transform.SetParent(WorldMapScene.Io.TheSea.transform);
            Collidable = new NotCollidable(BottlePrefab.Col);
            Triggerable = new NotTriggerable();
            UpdatePosition = new UpdateCovePosition();
            Telemeter = new RockTelemetry();
            Description = new SceneObjectDescription("Star Chart");

            Instantiator = new ItemInstantiator(
                Assets._bottle.gameObject,
                Vector3.one * .5f,
                new Vector3(35, Random.Range(0, 360), 0));

            Interactable = new BottleInteraction(state, data.starChartsData, data.ShipData, this);

            Inventoriable = new Inventoriable(
                (data.starChartsData, Difficulty.DifficultyLevel, 1));
        }

        public BottleWithScrollPrefab BottlePrefab;
        public GameObject GO { get; }
        public ITelemeter Telemeter { get; private set; }
        public ICollidable Collidable { get; private set; }
        public IInteractable Interactable { get; private set; }
        public ITriggerable Triggerable { get; private set; }
        public IUpdatePosition UpdatePosition { get; private set; }
        public IInstantiable Instantiator { get; private set; }
        public IDescription Description { get; private set; }
        public IInventoriable Inventoriable { get; private set; }
        public IQuestable Questable => new Sea.NotQuestable();
        public IDifficulty Difficulty { get; }
    }
}