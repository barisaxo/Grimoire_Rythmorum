using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using MusicTheory;

namespace OLDMuscopa
{
    public enum CardPile { Off = -1, One, Two, Three, Four }

    public static class CardHandler
    {

        #region INPUT SYSTEMS

        public static void ScrollHand(this MuscopaScene scene, Dir dir)
        {
            if (!scene.ActiveHandPiles.Contains(scene.CurrentHandPile))
            {
                scene.CurrentHandPile = CardPile.Off;
            }

            scene.CurrentHandPile += dir switch
            {
                Dir.Left => PrevHandPile(),
                Dir.Right => NextHandPile(),
                Dir.Reset => PrevHandPile(),
                //TODO Dir.Right_Off or Dir.Left_Off => -(int)scene.CurrentHandPile - 1,
                _ => 0,
            };
            HighlightHandPile(scene, scene.CurrentHandPile);

            #region INTERNAL

            int NextHandPile()
            {
                switch (scene.CurrentHandPile)
                {
                    case CardPile.Off:
                        if (scene.ActiveHandPiles.Contains(CardPile.One)) { return 1; }
                        else if (scene.ActiveHandPiles.Contains(CardPile.Two)) { return 2; }
                        else if (scene.ActiveHandPiles.Contains(CardPile.Three)) { return 3; }
                        else if (scene.ActiveHandPiles.Contains(CardPile.Four)) { return 4; }
                        else { return 0; }

                    case CardPile.One:
                        if (scene.ActiveHandPiles.Contains(CardPile.Two)) { return 1; }
                        else if (scene.ActiveHandPiles.Contains(CardPile.Three)) { return 2; }
                        else if (scene.ActiveHandPiles.Contains(CardPile.Four)) { return 3; }
                        else { return 0; }

                    case CardPile.Two:
                        if (scene.ActiveHandPiles.Contains(CardPile.Three)) { return 1; }
                        else if (scene.ActiveHandPiles.Contains(CardPile.Four)) { return 2; }
                        else { return 0; }

                    case CardPile.Three:
                        if (scene.ActiveHandPiles.Contains(CardPile.Four)) { return 1; }
                        else { return 0; }

                    default: return 0;
                }
            }

            int PrevHandPile()
            {
                switch (scene.CurrentHandPile)
                {
                    case CardPile.Off:
                        if (scene.ActiveHandPiles.Contains(CardPile.One)) { return 1; }
                        else if (scene.ActiveHandPiles.Contains(CardPile.Two)) { return 2; }
                        else if (scene.ActiveHandPiles.Contains(CardPile.Three)) { return 3; }
                        else if (scene.ActiveHandPiles.Contains(CardPile.Four)) { return 4; }
                        else { return 0; }

                    case CardPile.Two:
                        if (scene.ActiveHandPiles.Contains(CardPile.One)) { return -1; }
                        else { return 0; }

                    case CardPile.Three:
                        if (scene.ActiveHandPiles.Contains(CardPile.Two)) { return -1; }
                        else if (scene.ActiveHandPiles.Contains(CardPile.One)) { return -2; }
                        else { return 0; }

                    case CardPile.Four:
                        if (scene.ActiveHandPiles.Contains(CardPile.Three)) { return -1; }
                        else if (scene.ActiveHandPiles.Contains(CardPile.Two)) { return -2; }
                        else if (scene.ActiveHandPiles.Contains(CardPile.One)) { return -3; }
                        else { return 0; }

                    default: return 0;
                }
            }

            #endregion
        }

        public static void ScrollAnswerPile(this MuscopaScene scene, Dir dir)
        {
            scene.CurrentAnswerPile += dir switch
            {
                Dir.Left => PrevAnswerPile(),
                Dir.Right => NextAnswerPile(),
                Dir.Reset => PrevAnswerPile(),
                // TODO Dir.Off => -(int)scene.CurrentAnswerPile - 1,
                _ => 0,
            };

            HighlightAnswerPile(scene, scene.CurrentAnswerPile);

            #region INTERNAL

            int NextAnswerPile()
            {
                switch (scene.CurrentAnswerPile)
                {
                    case CardPile.Off:
                        if (!scene.AnsweredPiles.Contains(CardPile.Two)) { return 2; }
                        else if (!scene.AnsweredPiles.Contains(CardPile.Three)) { return 3; }
                        else if (!scene.AnsweredPiles.Contains(CardPile.Four)) { return 4; }
                        else { return 0; }

                    case CardPile.Two:
                        if (!scene.AnsweredPiles.Contains(CardPile.Three)) { return 1; }
                        else if (!scene.AnsweredPiles.Contains(CardPile.Four)) { return 2; }
                        else { return 0; }

                    case CardPile.Three:
                        if (!scene.AnsweredPiles.Contains(CardPile.Four)) { return 1; }
                        else { return 0; }

                    default: return 0;
                }
            }

            int PrevAnswerPile()
            {
                switch (scene.CurrentAnswerPile)
                {
                    case CardPile.Off:
                        if (!scene.AnsweredPiles.Contains(CardPile.Two)) { return 2; }
                        else if (!scene.AnsweredPiles.Contains(CardPile.Three)) { return 3; }
                        else if (!scene.AnsweredPiles.Contains(CardPile.Four)) { return 4; }
                        else { return 0; }

                    case CardPile.Two: return 0;

                    case CardPile.Three:
                        if (!scene.AnsweredPiles.Contains(CardPile.Two)) { return -1; }
                        else { return 0; }

                    case CardPile.Four:
                        if (!scene.AnsweredPiles.Contains(CardPile.Three)) { return -1; }
                        else if (!scene.AnsweredPiles.Contains(CardPile.Two)) { return -2; }
                        else { return 0; }

                    default: return 0;
                }
            }
            #endregion
        }

        #endregion INPUT SYSTEMS


        #region COLOR SYSTEMS

        public static void DisablePileHighlights(this MuscopaScene scene)
        {
            ScrollHand(scene, Dir.Down_Off);
            ScrollAnswerPile(scene, Dir.Down_Off);
        }

        public static void ColorAnswersRed(this MuscopaScene scene)
        {
            scene.AnswerPile1.color = Color.red;
            scene.AnswerPile2.color = Color.red;
            scene.AnswerPile3.color = Color.red;
            scene.AnswerPile4.color = Color.red;
        }

        public static void HighlightHandPile(this MuscopaScene scene, CardPile hp)
        {
            scene.Hand1HL.gameObject.SetActive(hp == CardPile.One);
            scene.Hand2HL.gameObject.SetActive(hp == CardPile.Two);
            scene.Hand3HL.gameObject.SetActive(hp == CardPile.Three);
            scene.Hand4HL.gameObject.SetActive(hp == CardPile.Four);
        }

        public static void HighlightAnswerPile(this MuscopaScene scene, CardPile ap)
        {
            //Answer1HL.gameObject.SetActive(ap == AnswerPile.One);
            scene.Answer2HL.gameObject.SetActive(ap == CardPile.Two);
            scene.Answer3HL.gameObject.SetActive(ap == CardPile.Three);
            scene.Answer4HL.gameObject.SetActive(ap == CardPile.Four);
        }

        #endregion COLOR SYSTEMS


        #region DECK SYSTEMS

        public static void SetUpDeck(this MuscopaScene scene)
        {
            CreateDeck();
            AnswerFirstChord();
            DrawANewHand(scene);

            #region INTERNAL
            void CreateDeck()
            {
                List<MuscopaCard> temp = new List<MuscopaCard>();
                foreach (MuscopaCard card in scene.Deck)
                {
                    temp.Add(card);
                }
                while (temp.Count > 0)
                {
                    int i = UnityEngine.Random.Range(0, temp.Count);
                    scene.CardsInDrawPile.Add(temp[i]);
                    temp.Remove(temp[i]);
                }
                for (int card = 0; card < scene.CardsInDrawPile.Count; card++)
                {
                    scene.CardsInDrawPile[card].TF.parent = scene.DrawPile.transform;
                    scene.CardsInDrawPile[card].TF.localPosition = .0375f * (scene.CardsInDrawPile.Count - card) * Vector3.back;
                    scene.CardsInDrawPile[card].TF.localRotation = Quaternion.Euler(Vector3.up * 180);
                }
            }

            void AnswerFirstChord()
            {
                foreach (MuscopaCard c in scene.Deck)
                {
                    //TODO
                    // if (Musica.ChordToFunction(scene.PuzzleChords[0]) == c.Function)
                    // {
                    //     scene.CardsInDrawPile.Remove(c);
                    //     scene.CardsInAnswerPiles.Add(c);
                    //     c.TF.position = AnswerDropPos(scene.AnswerPile1.gameObject);
                    //     c.TF.localRotation = Quaternion.Euler(Vector3.zero);
                    //     return;
                    // }
                }
                //TODO??? static Vector3 AnswerDropPos(GameObject pile) =>
                //      pile.transform.position + (.0375f * Vector3.back);
            }
            #endregion
        }

        private static void DrawANewHand(this MuscopaScene scene)
        {
            for (int i = 0; i < 4; i++)
            {
                if (scene.CardsInDrawPile.Count == 0) { ShuffleDeck(); }
                DrawACard(i);
            }

            #region INTERNAL
            void DrawACard(int i)
            {
                MuscopaCard card = scene.CardsInDrawPile[0];
                scene.CardsInHand[i] = card;
                scene.CardsInDrawPile.Remove(card);
                card.HandSlot = i;

                GameObject spot = i switch
                {
                    0 => scene.Hand1,
                    1 => scene.Hand2,
                    2 => scene.Hand3,
                    3 => scene.Hand4,
                    _ => null,
                };

                CardDrawAnimation(card, spot);
            }

            void ShuffleDeck()
            {
                scene.CardsInDrawPile.Clear();

                while (scene.CardsInDiscardPile.Count > 0)
                {
                    int index = UnityEngine.Random.Range(0, scene.CardsInDiscardPile.Count);
                    scene.CardsInDrawPile.Add(scene.CardsInDiscardPile[index]);
                    scene.CardsInDiscardPile.Remove(scene.CardsInDiscardPile[index]);
                }

                for (int card = 0; card < scene.CardsInDrawPile.Count; card++)
                {
                    scene.CardsInDrawPile[card].TF.parent = scene.DrawPile.transform;
                    scene.CardsInDrawPile[card].TF.localPosition = .0375f * (scene.CardsInDrawPile.Count - card) * Vector3.back;
                    scene.CardsInDrawPile[card].TF.localRotation = Quaternion.Euler(Vector3.up * 180);
                }

                scene.CardsInDiscardPile.Clear();
            }

            void CardDrawAnimation(MuscopaCard card, GameObject spot)
            {
                DisablePileHighlights(scene);
                bool animating = true;
                scene.CardsInAnimation.Add(animating);
                DrawAnimation(card, spot, animating).StartCoroutine();

                #region INTERNAL
                IEnumerator DrawAnimation(MuscopaCard card, GameObject spot, bool animating)
                {
                    yield return new WaitForEndOfFrame();

                    bool finished = true;
                    if (card.TF.rotation.y > spot.transform.rotation.y)
                    {//flip the card over (rotate Y axis)
                        card.TF.Rotate(180f * Time.deltaTime * Vector3.down);
                        finished = false;
                    }
                    else
                    {//align the cards Y axis
                        card.TF.rotation = spot.transform.rotation;
                    }

                    if (card.TF.position != spot.transform.position)
                    {//move into position
                        card.TF.position = Vector3.MoveTowards(
                            card.TF.position,
                            spot.transform.position,
                            Time.deltaTime * 3);
                        finished = false;
                    }

                    if (!finished)
                    {//keep going
                        DrawAnimation(card, spot, animating).StartCoroutine();
                    }
                    else
                    {//this card is done animating
                        scene.CardsInAnimation.Remove(animating);
                        card.TF.localRotation = Quaternion.Euler(Vector3.zero);
                        card.TF.position = card.HandPos = spot.transform.position;

                        //when animation is finished
                        if (scene.CardsInAnimation.Count == 0)
                        {
                            //reset active hand piles
                            scene.ActiveHandPiles.Clear();
                            for (int i = 0; i < 4; i++) { scene.ActiveHandPiles.Add((CardPile)i); }

                            //reset hand highlights and position
                            ScrollHand(scene, Dir.Reset);
                        }
                    }
                }
                #endregion
            }

            #endregion
        }

        public static void Discard(this MuscopaScene scene)
        {
            scene.ActiveHandPiles.Remove(scene.CurrentHandPile);
            scene.CardsInDiscardPile.Add(SelectedCard(scene));
            SelectedCard(scene).TF.position = DiscardDropPos(scene.DiscardPile.gameObject);
            CheckForEmptyHand(scene);

            #region INTERNAL
            Vector3 DiscardDropPos(GameObject pile) =>
                    pile.transform.position + (Vector3.back * .05f) +
                    (.0375f * scene.CardsInDiscardPile.Count * Vector3.back);
            #endregion
        }

        public static void DropCardInAnswerPile(this MuscopaScene scene, MuscopaCard card, CardPile pile)
        {
            scene.CardsInAnswerPiles.Add(card);
            card.TF.position = AnswerDropPos(AnswerPileGO(pile));

            #region INTERNAL
            Vector3 AnswerDropPos(GameObject pile) =>
                pile.transform.position + (.0375f * Vector3.back);

            GameObject AnswerPileGO(CardPile p) => p switch
            {
                CardPile.Two => scene.AnswerPile2.gameObject,
                CardPile.Three => scene.AnswerPile3.gameObject,
                CardPile.Four => scene.AnswerPile4.gameObject,
                _ => null,
            };
            #endregion
        }

        #endregion DECK SYSTEMS

        public static MuscopaCard SelectedCard(this MuscopaScene scene) => scene.CardsInHand[(int)scene.CurrentHandPile];

        internal static void CheckForEmptyHand(this MuscopaScene scene)
        {
            if (scene.ActiveHandPiles.Count == 0) { DrawANewHand(scene); }
        }


    }


}