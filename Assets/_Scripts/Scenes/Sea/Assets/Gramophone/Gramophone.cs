using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Inventory;

namespace Sea
{
    public class Gramophone : ISceneObject
    {
        public Gramophone(State currentState, GramophoneData gramoData, QuestData questData, ShipData shipData, Sea.Maps.R region)
        {
            Gramo = Assets.Gramo;
            TF.SetParent(WorldMapScene.Io.TheSea.transform);
            Collidable = new NotCollidable(Gramo.Col);
            Interactable = new GramoInteraction(currentState, gramoData, shipData, this);
            Triggerable = new NotTriggerable();
            UpdatePosition = new UpdateFishPosition();
            Telemeter = new FishTelemetry();
            Instantiator = new ItemInstantiator(
                toInstantiate: Assets._gramo.gameObject,
                scale: Vector3.one * .5f,
                rot: new Vector3(0, Random.Range(0f, 360f), 0));
            Description = new SceneObjectDescription("Gramophone");
            Inventoriable = new NotInventoriable();
            // Inventoriable = new Inventoriable(
            //     (gramoData,
            //      region switch
            //      {
            //          MusicTheory.RegionalMode.Ionian or MusicTheory.RegionalMode.Dorian => GramophoneData.DataItem.Lvl1,
            //          MusicTheory.RegionalMode.Lydian or MusicTheory.RegionalMode.MixoLydian => GramophoneData.DataItem.Lvl2,
            //          MusicTheory.RegionalMode.Phrygian or MusicTheory.RegionalMode.Aeolian => GramophoneData.DataItem.Lvl3,
            //          MusicTheory.RegionalMode.Locrian => GramophoneData.DataItem.Lvl4,
            //          _ => GramophoneData.DataItem.Lvl5,
            //      },
            //     1));

            Questable = new Questable(questData, Data.Inventory.QuestData.DataItem.StarChart);
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
        public FishData.DataItem FishType;
        public IQuestable Questable { get; private set; }
        public IDifficulty Difficulty { get; } = new NoDifficulty();
    }
}
