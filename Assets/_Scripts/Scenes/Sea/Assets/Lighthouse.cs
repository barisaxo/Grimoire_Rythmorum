using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Inventory;

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

        public LighthouseData.DataItem RegionData => Region.RegionalMode switch
        {
            MusicTheory.RegionalMode.Ionian => LighthouseData.DataItem.Ios,
            MusicTheory.RegionalMode.Dorian => LighthouseData.DataItem.Doria,
            MusicTheory.RegionalMode.Phrygian => LighthouseData.DataItem.Phrygia,
            MusicTheory.RegionalMode.Lydian => LighthouseData.DataItem.Lydia,
            MusicTheory.RegionalMode.MixoLydian => LighthouseData.DataItem.MixoLydia,
            MusicTheory.RegionalMode.Aeolian => LighthouseData.DataItem.Aeolia,
            MusicTheory.RegionalMode.Locrian => LighthouseData.DataItem.Locria,
            _ => LighthouseData.DataItem.Chromatica,
        };
    }
}