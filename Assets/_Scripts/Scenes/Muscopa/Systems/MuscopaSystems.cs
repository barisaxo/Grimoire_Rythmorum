using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MusicTheory.Notes.Arithmetic;
using MusicTheory.Triads.Arithmetic;

namespace Muscopa
{
    public enum HandSpot { Off = -1, One, Two, Three, Four }
    public enum AnswerSpot { Off = -1, One, Two, Three, Four }

    public static class MuscopaSystems
    {
        public static bool HandIsEmpty(this CardManager cardManager) =>
            cardManager.ActiveHandSpots.Count == 0;

        public static void DrawANewHand(this MuscopaScene scene, Action callback)
        {
            DrawCards().StartCoroutine();

            IEnumerator DrawCards()
            {
                for (HandSpot i = HandSpot.One; i <= HandSpot.Four; i++)
                {
                    yield return null;
                    if (scene.CardManager.CardsInDrawPile.Count == 0) { ShuffleDiscardPileIntoDeck(); }
                    DrawACard(i);
                }
            }

            void DrawACard(HandSpot i)
            {
                MuscopaCard card = scene.CardManager.CardsInDrawPile[0];
                scene.CardManager.CardsInHand[(int)i] = card;
                scene.CardManager.CardsInDrawPile.Remove(card);
                card.HandSlot = i;

                (Vector3, Quaternion) spot = i switch
                {
                    HandSpot.One => MuscopaTableau.HandSpot1,
                    HandSpot.Two => MuscopaTableau.HandSpot2,
                    HandSpot.Three => MuscopaTableau.HandSpot3,
                    HandSpot.Four => MuscopaTableau.HandSpot4,
                    _ => throw new System.Exception(i.ToString())
                };

                CardDrawAnimation(card, spot);
            }

            void ShuffleDiscardPileIntoDeck()
            {
                scene.CardManager.CardsInDrawPile.Clear();

                while (scene.CardManager.CardsInDiscardPile.Count > 0)
                {
                    int index = UnityEngine.Random.Range(0, scene.CardManager.CardsInDiscardPile.Count);
                    scene.CardManager.CardsInDrawPile.Add(scene.CardManager.CardsInDiscardPile[index]);
                    scene.CardManager.CardsInDiscardPile.Remove(scene.CardManager.CardsInDiscardPile[index]);
                }

                for (int card = 0; card < scene.CardManager.CardsInDrawPile.Count; card++)
                {
                    scene.CardManager.CardsInDrawPile[card].GO.transform.SetPositionAndRotation(
                         scene.Tableau.DrawPile.transform.position + (.0375f * (scene.CardManager.CardsInDrawPile.Count - card) * Vector3.back),
                         scene.Tableau.DrawPile.transform.rotation);

                    scene.CardManager.CardsInDrawPile[card].GO.transform.Rotate(Vector3.up * 180);
                }

                scene.CardManager.CardsInDiscardPile.Clear();
            }

            void CardDrawAnimation(MuscopaCard card, (Vector3 pos, Quaternion rot) spot)
            {
                scene.CardManager.CardsInAnimation.Add(true);

                DrawAnimation(
                        card,
                        spot,
                        Time.time,
                        Vector3.Distance(card.GO.transform.position, spot.pos),
                        Vector3.Distance(card.GO.transform.rotation.eulerAngles, spot.rot.eulerAngles))
                    .StartCoroutine();

                IEnumerator DrawAnimation(MuscopaCard card, (Vector3 pos, Quaternion rot) spot, float startTime, float dist, float rot)
                {
                    yield return null;
                    // Debug.Log("Draw animating " + card.HandSlot);
                    bool animating = false;
                    float distDelta = ((Time.time - startTime) * .6f) / dist;
                    float rotDelta = ((Time.time - startTime) * 50f) / rot;

                    if (Mathf.Abs(card.GO.transform.rotation.y - spot.rot.y) > .2f ||
                        Mathf.Abs(card.GO.transform.rotation.z - spot.rot.z) > .2f ||
                        Mathf.Abs(card.GO.transform.rotation.x - spot.rot.x) > .2f)
                    {
                        card.GO.transform.rotation = Quaternion.Slerp(card.GO.transform.rotation, spot.rot, rotDelta);
                    }

                    else
                    {
                        card.GO.transform.localRotation = spot.rot;
                    }

                    if (Vector3.Distance(card.GO.transform.position, spot.pos) > .01f)
                    {
                        card.GO.transform.position = Vector3.Lerp(card.GO.transform.position, spot.pos, distDelta);
                        animating = true;
                    }
                    else
                    {
                        card.GO.transform.position = card.HandPos = spot.pos;
                    }

                    if (animating)
                    {
                        DrawAnimation(card, spot, startTime, dist, rot).StartCoroutine();
                    }

                    else
                    {
                        scene.CardManager.CardsInAnimation.RemoveAt(0);
                        // Debug.Log("Cards In Animation: " + scene.CardManager.CardsInAnimation.Count);
                        card.GO.transform.localRotation = spot.rot;

                        if (scene.CardManager.CardsInAnimation.Count == 0)
                        {
                            //reset active hand piles
                            scene.CardManager.ActiveHandSpots.Clear();

                            for (HandSpot i = HandSpot.One; i <= HandSpot.Four; i++)
                            {
                                scene.CardManager.ActiveHandSpots.Add(i);
                            }

                            // Debug.Log("Finished drawing hand" + scene.CardManager.ActiveHandSpots.Count);
                            callback?.Invoke();
                        }
                    }
                }
            }

        }

        public static void ScrollHand(this MuscopaScene scene, Dir dir)
        {
            // if (!scene.CardManager.ActiveHandSpots.Contains(scene.CardManager.CurrentHandSpot))
            // {
            //     scene.CardManager.CurrentHandSpot = HandSpot.Off;
            // }

            scene.CardManager.CurrentHandSpot = dir switch
            {
                Dir.Left => PrevHandSlot(),
                Dir.Right => NextHandSlot(),
                Dir.Reset => AdjustSpentHandSpotToLeft(),
                // Dir.Right_Off or Dir.Left_Off => -(int)scene.CardManager.CurrentHandPile - 1,
                _ => HandSpot.Off,
            };

            HandSpot AdjustSpentHandSpotToLeft()
            {
                switch (scene.CardManager.CurrentHandSpot)
                {
                    case HandSpot.Off:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.One)) return HandSpot.One;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Two)) return HandSpot.Two;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Three)) return HandSpot.Three;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Four)) return HandSpot.Four;
                        else return HandSpot.Off;

                    case HandSpot.One:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.One)) return HandSpot.One;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Two)) return HandSpot.Two;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Three)) return HandSpot.Three;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Four)) return HandSpot.Four;
                        else return HandSpot.Off;

                    case HandSpot.Two:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Two)) return HandSpot.Two;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.One)) return HandSpot.One;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Three)) return HandSpot.Three;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Four)) return HandSpot.Four;
                        else return HandSpot.Off;

                    case HandSpot.Three:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Three)) return HandSpot.Three;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Two)) return HandSpot.Two;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.One)) return HandSpot.One;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Four)) return HandSpot.Four;
                        else return HandSpot.Off;

                    case HandSpot.Four:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Four)) return HandSpot.Four;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Three)) return HandSpot.Three;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Two)) return HandSpot.Two;
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.One)) return HandSpot.One;
                        else return HandSpot.Off;

                    default: return HandSpot.Off;
                }
            }

            HandSpot NextHandSlot()
            {
                switch (scene.CardManager.CurrentHandSpot)
                {
                    case HandSpot.Off:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.One)) { return HandSpot.One; }
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Two)) { return HandSpot.Two; }
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Three)) { return HandSpot.Three; }
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Four)) { return HandSpot.Four; }
                        else { return HandSpot.Off; }

                    case HandSpot.One:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Two)) { return HandSpot.Two; }
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Three)) { return HandSpot.Three; }
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Four)) { return HandSpot.Four; }
                        else { return HandSpot.One; }

                    case HandSpot.Two:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Three)) { return HandSpot.Three; }
                        else if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Four)) { return HandSpot.Four; }
                        else { return HandSpot.Two; }

                    case HandSpot.Three:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Four)) { return HandSpot.Four; }
                        else { return HandSpot.Three; }

                    case HandSpot.Four:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Four)) { return HandSpot.Four; }
                        else { return HandSpot.Off; }

                    default: return HandSpot.Off;
                }
            }

            HandSpot PrevHandSlot()
            {
                switch (scene.CardManager.CurrentHandSpot)
                {
                    case HandSpot.Off:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.One)) return HandSpot.One;
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Two)) return HandSpot.Two;
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Three)) return HandSpot.Three;
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Four)) return HandSpot.Four;
                        return HandSpot.Off;

                    case HandSpot.One:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.One)) { return HandSpot.One; }
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Two)) return HandSpot.Two;
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Three)) return HandSpot.Three;
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Four)) return HandSpot.Four;
                        return HandSpot.Off;

                    case HandSpot.Two:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.One)) return HandSpot.One;
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Two)) return HandSpot.Two;
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Three)) return HandSpot.Three;
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Four)) return HandSpot.Four;
                        return HandSpot.Off;

                    case HandSpot.Three:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Two)) return HandSpot.Two;
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.One)) return HandSpot.One;
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Three)) return HandSpot.Three;
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Four)) return HandSpot.Four;
                        return HandSpot.Off;

                    case HandSpot.Four:
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Three)) return HandSpot.Three;
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.Two)) return HandSpot.Two;
                        if (scene.CardManager.ActiveHandSpots.Contains(HandSpot.One)) return HandSpot.One;
                        return HandSpot.Off;

                    default: return HandSpot.Off;
                }
            }

        }


        public static AnswerSpot ScrollAnswerSpot(this MuscopaScene scene, Dir dir)
        {
            return dir switch
            {
                Dir.Left => PrevAnswerPile(),
                Dir.Right => NextAnswerPile(),
                Dir.Reset => PrevAnswerPile(),
                // TODO Dir.Off => -(int)scene.CurrentAnswerPile - 1,
                _ => AnswerSpot.Off,
            };

            AnswerSpot NextAnswerPile()
            {
                switch (scene.CardManager.CurrentAnswerSpot)
                {
                    case AnswerSpot.Off:
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Two)) return AnswerSpot.Two;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Three)) return AnswerSpot.Three;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Four)) return AnswerSpot.Four;
                        return AnswerSpot.Off;

                    case AnswerSpot.Two:
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Three)) return AnswerSpot.Three;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Four)) return AnswerSpot.Four;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Two)) return AnswerSpot.Two;
                        return AnswerSpot.Off;

                    case AnswerSpot.Three:
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Four)) return AnswerSpot.Four;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Three)) return AnswerSpot.Three;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Two)) return AnswerSpot.Two;
                        return AnswerSpot.Off;

                    case AnswerSpot.Four:
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Four)) return AnswerSpot.Four;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Three)) return AnswerSpot.Three;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Two)) return AnswerSpot.Two;
                        return AnswerSpot.Off;

                    default: return AnswerSpot.Off;
                }
            }

            AnswerSpot PrevAnswerPile()
            {
                switch (scene.CardManager.CurrentAnswerSpot)
                {
                    case AnswerSpot.Off:
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Two)) return AnswerSpot.Two;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Three)) return AnswerSpot.Three;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Four)) return AnswerSpot.Four;
                        return AnswerSpot.Off;

                    case AnswerSpot.Two:
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Two)) return AnswerSpot.Two;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Three)) return AnswerSpot.Three;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Four)) return AnswerSpot.Four;
                        return AnswerSpot.Off;

                    case AnswerSpot.Three:
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Two)) return AnswerSpot.Two;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Three)) return AnswerSpot.Three;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Four)) return AnswerSpot.Four;
                        return AnswerSpot.Off;

                    case AnswerSpot.Four:
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Three)) return AnswerSpot.Three;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Two)) return AnswerSpot.Two;
                        if (!scene.CardManager.AnsweredSpots.Contains(AnswerSpot.Four)) return AnswerSpot.Four;
                        return AnswerSpot.Off;

                    default: return AnswerSpot.Off;
                }
            }
        }

        public static void HighlightHandSpot(this MuscopaScene scene)
        {
            scene.Tableau.HandHL.transform.SetPositionAndRotation(scene.CardManager.CurrentHandSpot switch
            {
                HandSpot.One => MuscopaTableau.HandSpot1.pos,
                HandSpot.Two => MuscopaTableau.HandSpot2.pos,
                HandSpot.Three => MuscopaTableau.HandSpot3.pos,
                HandSpot.Four => MuscopaTableau.HandSpot4.pos,
                _ => scene.Tableau.HandHL.transform.position,
            }, scene.CardManager.CurrentHandSpot switch
            {
                HandSpot.One => MuscopaTableau.HandSpot1.rot,
                HandSpot.Two => MuscopaTableau.HandSpot2.rot,
                HandSpot.Three => MuscopaTableau.HandSpot3.rot,
                HandSpot.Four => MuscopaTableau.HandSpot4.rot,
                _ => scene.Tableau.HandHL.transform.rotation,
            });
            scene.Tableau.HandHL.SetActive(scene.CardManager.CurrentHandSpot != HandSpot.Off);
        }

        public static void HighlightAnswerSpot(this MuscopaScene scene)
        {
            scene.Tableau.AnswerHL.transform.position = scene.CardManager.CurrentAnswerSpot switch
            {
                AnswerSpot.One => throw new System.Exception("This shouldn't be a thing right?"),
                AnswerSpot.Two => scene.Tableau.AnswerSpot2.transform.position,
                AnswerSpot.Three => scene.Tableau.AnswerSpot3.transform.position,
                AnswerSpot.Four => scene.Tableau.AnswerSpot4.transform.position,
                _ => scene.Tableau.AnswerHL.transform.position,
            };

            scene.Tableau.AnswerHL.SetActive(scene.CardManager.CurrentAnswerSpot != AnswerSpot.Off);
        }

        public static void Discard(this MuscopaScene scene)
        {
            scene.CardManager.ActiveHandSpots.Remove(scene.CardManager.CurrentHandSpot);
            scene.CardManager.CardsInDiscardPile.Add(SelectedCard(scene));
            SelectedCard(scene).GO.transform.position = DiscardDropPos(scene.Tableau.DiscardPile.gameObject);

            Vector3 DiscardDropPos(GameObject pile) =>
                    pile.transform.position + (Vector3.back * .05f) +
                    (.0375f * scene.CardManager.CardsInDiscardPile.Count * Vector3.back);
        }

        public static void DropCardInAnswerSpot(this MuscopaScene scene, MuscopaCard card, AnswerSpot spot)
        {
            scene.CardManager.CardsInAnswerSpots.Add(card);
            card.GO.transform.SetPositionAndRotation(
                AnswerDropPos(AnswerSpotGO(spot)),
                AnswerSpotGO(spot).transform.rotation
            );

            #region INTERNAL
            Vector3 AnswerDropPos(GameObject spot) =>
                spot.transform.position + (.0375f * Vector3.back);

            GameObject AnswerSpotGO(AnswerSpot a) => a switch
            {
                AnswerSpot.One => scene.Tableau.AnswerSpot1.gameObject,
                AnswerSpot.Two => scene.Tableau.AnswerSpot2.gameObject,
                AnswerSpot.Three => scene.Tableau.AnswerSpot3.gameObject,
                AnswerSpot.Four => scene.Tableau.AnswerSpot4.gameObject,
                _ => null,
            };
            #endregion
        }

        public static MuscopaCard SelectedCard(this MuscopaScene scene) =>
            scene.CardManager.CardsInHand[(int)scene.CardManager.CurrentHandSpot];

        public static AnswerSpot SelectedAnswerSpot(this MuscopaScene scene) => scene.CardManager.CurrentAnswerSpot;

        public static MusicTheory.Functions.Harmonic.IFunction GetFunction(this MuscopaScene scene, AnswerSpot spot) => scene.AnswerSheet[(int)spot];


        public static void ColorAnswersYellow(this MuscopaScene scene)
        {
            scene.Tableau.AnswerSpot1.color = Color.yellow;
            scene.Tableau.AnswerSpot2.color = Color.yellow;
            scene.Tableau.AnswerSpot3.color = Color.yellow;
            scene.Tableau.AnswerSpot4.color = Color.yellow;
        }

        public static string GetChordAndRomanNames(this MuscopaScene scene)
        {
            string quality = scene.MuscopaSettings.Cadence[(int)scene.CardManager.CurrentAnswerSpot].GetTriad().Name;

            return scene.MuscopaSettings.Cadence[(int)scene.CardManager.CurrentAnswerSpot].Name + quality + "\n" +
                scene.MuscopaSettings.KeyCenter.GetNoteAbove(scene.MuscopaSettings.Cadence[(int)scene.CardManager.CurrentAnswerSpot]).Name + quality;
        }
        public static string GetChordAndRomanNames(this MuscopaScene scene, AnswerSpot spot)
        {
            string quality = scene.MuscopaSettings.Cadence[(int)spot].GetTriad().Name;

            return scene.MuscopaSettings.Cadence[(int)spot].Name + quality + "\n" +
                scene.MuscopaSettings.KeyCenter.GetNoteAbove(scene.MuscopaSettings.Cadence[(int)spot]).Name + quality;
        }
    }
}