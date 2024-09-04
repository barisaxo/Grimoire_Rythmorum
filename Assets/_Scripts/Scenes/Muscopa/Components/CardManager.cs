using System.Collections.Generic;
using UnityEngine;

namespace Muscopa
{
    public class CardManager
    {
        public CardManager(MuscopaTableau tableau)
        {
            var tempIndex = new List<int>();
            for (int i = 0; i < tableau.Deck.Count; i++)
                tempIndex.Add(i);

            while (tempIndex.Count > 0)
            {
                int randIndex = Random.Range(0, tempIndex.Count);
                CardsInDrawPile.Add(tableau.Deck[tempIndex[randIndex]]);
                tempIndex.Remove(tempIndex[randIndex]);
            }

            for (int i = 0; i < CardsInDrawPile.Count; i++)
            {
                CardsInDrawPile[i].GO.transform.SetPositionAndRotation(
                    tableau.DrawPile.transform.position + (.0375f * (CardsInDrawPile.Count - i) * Vector3.back),
                    tableau.DrawPile.transform.rotation);

                CardsInDrawPile[i].GO.transform.Rotate(Vector3.up * 180);

                // Debug.Log(CardsInDrawPile[i].GO.name + ", " + i + ", " + CardsInDrawPile[i].GO.transform.position);
            }
        }

        public MuscopaCard[] CardsInHand { get; private set; } = new MuscopaCard[4];
        public List<MuscopaCard> CardsInDrawPile { get; private set; } = new List<MuscopaCard>();
        public List<MuscopaCard> CardsInDiscardPile { get; private set; } = new List<MuscopaCard>();
        public List<MuscopaCard> CardsInAnswerSpots { get; private set; } = new List<MuscopaCard>();

        public AnswerSpot CurrentAnswerSpot = 0;
        public HandSpot CurrentHandSpot = 0;

        public List<AnswerSpot> AnsweredSpots { get; private set; } = new List<AnswerSpot>();
        public List<HandSpot> ActiveHandSpots { get; private set; } = new List<HandSpot>();
        public List<bool> CardsInAnimation { get; private set; } = new List<bool>();
    }
}