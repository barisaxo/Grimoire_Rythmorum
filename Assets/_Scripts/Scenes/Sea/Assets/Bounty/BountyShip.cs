using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Two;

namespace Sea
{
    public class BountyShip : ISceneObject
    {
        public BountyShip(State currentState, QuestData questData, ActiveShipData shipData, Region region, Cell cell)
        {
            Debug.Log("NEW BOUNTY SHIP ");
            Ship = Assets.BountyShip;
            ShipStats = new(
                new ShipStats.HullStats(new Schooner(), new Pine()),
                new ShipStats.CannonStats(new Carronade(), new Bronze()),
                new ShipStats.RiggingStats(new Hemp())
            );

            TF.SetParent(WorldMapScene.Io.TheSea.transform);
            Collidable = new NotCollidable(Ship.Hull.Col);
            Interactable = new BountyInteraction(currentState, ShipStats, Ship.gameObject, region, cell);
            Triggerable = new NotTriggerable();
            UpdatePosition = new UpdateFishPosition();
            Telemeter = new FishTelemetry();
            Instantiator = new ItemInstantiator(
                toInstantiate: Assets._bountyShip.gameObject,
                scale: Vector3.one * .5f,
                rot: new Vector3(0, Random.Range(0f, 360f), 0));
            Description = new SceneObjectDescription("BountyShip");
            Inventoriable = new NotInventoriable();
            // Inventoriable = new Inventoriable(
            //     (gramoData,
            //      region switch
            //      {
            //          MusicTheory.RegionalMode.Ionian or MusicTheory.RegionalMode.Dorian => BountyShipData.DataItem.Lvl1,
            //          MusicTheory.RegionalMode.Lydian or MusicTheory.RegionalMode.MixoLydian => BountyShipData.DataItem.Lvl2,
            //          MusicTheory.RegionalMode.Phrygian or MusicTheory.RegionalMode.Aeolian => BountyShipData.DataItem.Lvl3,
            //          MusicTheory.RegionalMode.Locrian => BountyShipData.DataItem.Lvl4,
            //          _ => BountyShipData.DataItem.Lvl5,
            //      },
            //     1));

            Questable = new Questable(questData, new Navigation());
        }

        public SloopPrefab Ship;
        public ShipStats.ShipStats ShipStats;
        public Transform TF => Ship.transform;
        public GameObject GO => Ship.gameObject;

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
