using UnityEngine;
using MusicTheory;

namespace OLDMuscopa
{
    /// <summary>
    /// HarmonicFunction, GameObject, Transform, SpriteRenderer, Color.
    /// All the good stuff you need for a Card.
    /// </summary>
    public struct MuscopaCard
    {
        public MuscopaCard(HarmonicFunction func, Transform cardTable)
        {
            HarmonicFunction = func;
            GO = Object.Instantiate(HarmonicFunction switch
            {
                HarmonicFunction.Dominant => Assets.DomCard,
                HarmonicFunction.Subdominant => Assets.SubDomCard,
                _ => Assets.TonCard,
            });
            Col = GO.GetComponent<BoxCollider>();
            TF = GO.transform;
            TF.parent = cardTable;
            HandPos = Vector3.zero;
            HandSlot = 0;
        }

        /// <summary>
        /// GameObject.
        /// </summary>
        public GameObject GO;

        /// <summary>
        /// Transform.
        /// </summary>
        public Transform TF;

        /// <summary>
        /// The 'hand position' for cards to return to
        /// if they have been dropped but not placed.
        /// </summary>
        public Vector3 HandPos;

        /// <summary>
        /// The BoxCollider component.
        /// </summary>
        public BoxCollider Col;

        /// <summary>
        /// The musical HarmonicFunction of this card;
        /// </summary>
        public HarmonicFunction HarmonicFunction;

        /// <summary>
        /// Which hand slot this card sits in.
        /// </summary>
        public int HandSlot;
    }
}