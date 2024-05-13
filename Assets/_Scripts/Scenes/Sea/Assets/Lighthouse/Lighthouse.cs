using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Two;

namespace Sea
{
    public class Lighthouse : ISceneObject
    {
        public Lighthouse(Region region, LighthouseData lighthouseData, State currentState)
        {
            Region = region;
            LighthousePrefab = Assets.Lighthouse;
            LighthousePrefab.Light.SetActive(false);
            TF.SetParent(WorldMapScene.Io.TheSea.transform);
            Collidable = new RockCollision(LighthousePrefab.Col);
            Interactable = new LighthouseInteraction(this, currentState);
            Triggerable = new NotTriggerable();
            UpdatePosition = new UpdateLighthousePosition();
            Telemeter = new RockTelemetry();
            Instantiator = new ItemInstantiator(
                Assets._lighthouse.gameObject,
                Vector3.one * 3,
                Vector3.zero);

            Debug.Log(RegionData.Name + " " + WorldMapScene.Io.Ship.GlobalCoord);

            if (lighthouseData.GetLevel(RegionData) == 1)
            {
                LighthousePrefab.Light.SetActive(true);
                RemoveInteractable();
            }
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
        public IInstantiable Instantiator { get; private set; }
        public IDescription Description { get; private set; }
        public IInventoriable Inventoriable { get; } = new NotInventoriable();//todo technically is inventoriable
        public IQuestable Questable => new NotQuestable();
        public IDifficulty Difficulty { get; } = new NoDifficulty();

        public Data.Two.Lighthouse RegionData => Region.R switch
        {
            Sea.Maps.R.i => new IonianLighthouse(),
            Sea.Maps.R.d => new DorianLighthouse(),
            Sea.Maps.R.p => new PhrygianLighthouse(),
            Sea.Maps.R.l => new LydianLighthouse(),
            Sea.Maps.R.m => new MixolydianLighthouse(),
            Sea.Maps.R.a => new AeolianLighthouse(),
            Sea.Maps.R.s => new LocrianLighthouse(),
            _ => new ChromaticLighthouse(),
        };
    }
}