using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sea
{
    public class NPCShip
    {
        public ISceneObject SceneObject;
        public IMAShip Ship;

        public void InstantiateNewSceneObject(State state)
        {
            switch (ShipType)
            {
                case NPCShipType.Trade:
                    TradeShip tradeShip = new(state, Hull);
                    SceneObject = tradeShip;
                    Ship = tradeShip.Ship;
                    break;

                case NPCShipType.Pirate:
                    PirateShip pirateShip = new(state, this, Hull);
                    SceneObject = pirateShip;
                    Ship = pirateShip.Ship;
                    break;

                default: throw new System.NotImplementedException();
            }
            Ship.RandomRig();
        }

        public void DestroySceneObject()
        {
            Object.Destroy(SceneObject.GO);
            SceneObject = null;
        }

        public Vector2 Pos;
        public Vector2Int LocalCoords => new(Mathf.FloorToInt(Pos.x), Mathf.FloorToInt(Pos.y));
        public Vector2Int GlobalCoords => LocalCoords + RegionalCoordOffset;
        public Vector2 GlobalPos => Pos + RegionalCoordOffset;
        public Vector2Int RegionalCoordOffset;

        public float RotY;
        public float YTurn;
        public float NewRotY;

        public (float amp, float period, float offset) Sway;
        // public IMAShip ImAShip;
        public ShipStats.ShipStats ShipStats => Ship.ShipStats;
        // new(
        //     new ShipStats.HullStats(
        //         Data.Equipment.HullData.Frigate,
        //         Data.Inventory.MaterialsData.DataItem.Oak),
        //     new ShipStats.CannonStats(
        //         Data.Equipment.CannonData.Culverin,
        //         Data.Inventory.MaterialsData.DataItem.CastIron),
        //     numOfCannons: 32
        // );

        public NPCShipType ShipType;
        public Vector2Int StartNode;

        public Vector2Int[] PatrolPath;
        public bool PathDirection;
        private int _patrolIndex = 0;
        public int PatrolIndex
        {
            get => _patrolIndex;
            set => _patrolIndex = value.Smod(PatrolPath.Length);
        }

        public float ThreatRange = Random.Range(1.1f, 4f);

        public float MoveSpeed;
        public float MoveDelta = 0;

        public float StuckTimer = Random.Range(1.1f, 5f);
        public float StuckDelta;


        public readonly int HideTime = Random.Range(10, 12);
        // public bool Hiding => HideTimer > 0;
        public float HideTimer;

        public Data.Equipment.HullData Hull;
        public MusicTheory.RegionalMode RegionalMode = (MusicTheory.RegionalMode)Random.Range(0, 7);
        public AudioClip RegionalSound => Assets.GetScaleChordClip(RegionalMode);
        public string Name => "[" + RegionalMode.ToString() + " " + ShipType + " ship]:\n";
        public Color FlagColor = Assets.RandomColor;
        public Sprite Flag => ShipType == NPCShipType.Trade ? RegionalMode switch
        {
            MusicTheory.RegionalMode.Aeolian => Assets.AeolianFlag,
            MusicTheory.RegionalMode.Dorian => Assets.DorianFlag,
            MusicTheory.RegionalMode.Ionian => Assets.IonianFlag,
            MusicTheory.RegionalMode.Lydian => Assets.LydianFlag,
            MusicTheory.RegionalMode.Phrygian => Assets.PhrygianFlag,
            MusicTheory.RegionalMode.MixoLydian => Assets.MixoLydianFlagFlag,
            _ => Assets.LocrianFlag,
        } : Assets.PirateFlag;

        public NPCShip(Vector2Int[] path, NPCShipType shipType, Data.Equipment.HullData hull, Vector2Int regionalCoordOffset)
        {
            ShipType = shipType;
            Hull = hull;
            PathDirection = Random.value < .5f;
            // PosDelta = start;
            int i = Random.Range(0, path.Length);
            Pos = path[i];
            StartNode = path[i];
            PatrolPath = path;
            PatrolIndex = i;

            RotY = 90 * Random.Range(0, 4);
            Sway.amp = Random.Range(.01f, .1f);
            Sway.period = Random.Range(.5f, 2f);
            Sway.offset = Random.Range(-10f, 10f);
            MoveSpeed = Random.Range(.1f, .15f);

            RegionalCoordOffset = regionalCoordOffset;
        }



    }

    public class TradeShip : ISceneObject
    {
        public TradeShip(State state, Data.Equipment.HullData hull)
        {
            IMAShip obj = hull switch
            {
                _ when hull == Data.Equipment.HullData.Sloop => Assets.Sloop,
                _ when hull == Data.Equipment.HullData.Schooner => Assets.Schooner2,
                _ => Assets.Frigate,
            };
            Ship = obj;
            GO = obj.GO;
            Collidable = new ShipCollision(obj._hull.Col);
            Interactable = new HailShipInteraction(state);
            Triggerable = new NotTriggerable();
            Telemeter = new ShipTelemetry();
            UpdatePosition = new UpdateNPCShipPosition();
        }

        public IMAShip Ship { get; private set; }

        public GameObject GO { get; private set; }
        public Transform Tf => GO.transform;

        public ITelemeter Telemeter { get; private set; }
        public ICollidable Collidable { get; private set; }
        public IInteractable Interactable { get; private set; }
        public ITriggerable Triggerable { get; private set; }
        public IUpdatePosition UpdatePosition { get; private set; }
        public IInstantiable Instantiator { get; private set; }
        public IDescription Description { get; private set; }
        public IInventoriable Inventoriable { get; } = new NotInventoriable();
        public IQuestable Questable => new NotQuestable();
        public IDifficulty Difficulty { get; } = new NoDifficulty();
    }

    public class PirateShip : ISceneObject
    {
        public PirateShip(State currentState, NPCShip ship, Data.Equipment.HullData hull)
        {
            IMAShip obj = hull switch
            {
                _ when hull == Data.Equipment.HullData.Sloop => Assets.Sloop,
                _ when hull == Data.Equipment.HullData.Schooner => Assets.Schooner2,
                _ => Assets.Frigate,
            };

            Ship = obj;
            GO = obj.GO;

            Collidable = new NotCollidable(obj._hull.Col);
            Interactable = new NoInteraction();
            Triggerable = new PirateTrigger(currentState, ship);
            UpdatePosition = new UpdateFishPosition();
            Telemeter = new ShipTelemetry();
        }

        public IMAShip Ship { get; private set; }

        public GameObject GO { get; private set; }

        public ITelemeter Telemeter { get; private set; }
        public ICollidable Collidable { get; private set; }
        public IInteractable Interactable { get; private set; }
        public ITriggerable Triggerable { get; private set; }
        public IUpdatePosition UpdatePosition { get; private set; }
        public IInstantiable Instantiator { get; private set; }
        public IDescription Description { get; private set; }
        public IInventoriable Inventoriable { get; } = new NotInventoriable();
        public IQuestable Questable => new NotQuestable();
        public IDifficulty Difficulty { get; } = new NoDifficulty();
    }

    public enum NPCShipType { Trade, Pirate, }
    //todo privateer, exploration vessel, Whaling Ship, Fishing boat, Packet Ship

}
