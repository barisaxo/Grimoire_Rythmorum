using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Inventory;

namespace Sea
{
    public class Border : ISceneObject
    {
        public Border(MusicTheory.RegionalMode region)
        {
            GO = new GameObject(nameof(Border));
            SpriteRenderer sr = GO.AddComponent<SpriteRenderer>();
            sr.sprite = Assets.White;
            sr.color = region.GetColor() * new Color(1, 1, 1, .25f);
            TF.SetParent(WorldMapScene.Io.TheSea.transform);
            Collidable = new NotCollidable(GO.AddComponent<CapsuleCollider>());
            Interactable = new NoInteraction();
            Triggerable = new NotTriggerable();
            UpdatePosition = new UpdateBorderPosition();
            Telemeter = new FishTelemetry();
            Instantiator = new ItemInstantiator(
                Assets._gramo.gameObject,
                Vector3.one,
                Vector3.zero);
            Description = new SceneObjectDescription(nameof(Border));

            Inventoriable = new NotInventoriable();

            Questable = new NotQuestable();
        }

        public Transform TF => GO.transform;
        public GameObject GO { get; private set; }
        public CapsuleCollider Col;

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
