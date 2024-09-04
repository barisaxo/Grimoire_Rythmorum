using UnityEngine;
using MusicTheory.Functions.Harmonic;

namespace Muscopa
{
    /// <summary>
    /// HarmonicFunction, GameObject, SpriteRenderer.
    /// All the good stuff you need for a Card.
    /// </summary>
    public struct MuscopaCard
    {
        public MuscopaCard(IFunction function, Transform tableau)
        {
            HarmonicFunction = function;

            GO = HarmonicFunction switch
            {
                Tonic => Assets.TonCard,
                Dominant => Assets.DomCard,
                Subdominant => Assets.SubDomCard,
                _ => throw new System.Exception(function.Name)
            };

            GO.transform.parent = tableau;

            Col = GO.GetComponent<BoxCollider>();

            HandPos = Vector3.zero;
            HandSlot = HandSpot.Off;
        }

        /// <summary>
        /// GameObject.
        /// </summary>
        public GameObject GO;

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
        public IFunction HarmonicFunction;

        /// <summary>
        /// Which hand slot this card sits in.
        /// </summary>
        public HandSpot HandSlot;
    }
}