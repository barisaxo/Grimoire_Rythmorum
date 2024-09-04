using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using MusicTheory.Functions.Harmonic;

namespace Muscopa
{
    public class MuscopaTableau
    {
        public MuscopaTableau()
        {
            _ = HandHL;
            _ = AnswerHL;
            _ = AnswerSpot1;
            _ = AnswerSpot2;
            _ = AnswerSpot3;
            _ = AnswerSpot4;
            _ = DrawPile;
            _ = DiscardPile;
            _ = Deck;
        }

        public void SelfDestruct()
        {
            UnityEngine.Object.Destroy(_tableau.gameObject);
        }

        private Transform _tableau;
        public Transform Tableau => _tableau ? _tableau : _tableau = new GameObject(nameof(Tableau)).transform;

        public static readonly (Vector3 pos, Quaternion rot) HandSpot1 = (new(-2.25f, -1f, 100), Quaternion.Euler(0, 0, 15));
        public static readonly (Vector3 pos, Quaternion rot) HandSpot2 = (new(-.75f, -.65f, 100), Quaternion.Euler(0, 0, 5));
        public static readonly (Vector3 pos, Quaternion rot) HandSpot3 = (new(.75f, -.65f, 100), Quaternion.Euler(0, 0, -5));
        public static readonly (Vector3 pos, Quaternion rot) HandSpot4 = (new(2.25f, -1f, 100), Quaternion.Euler(0, 0, -15));

        private List<MuscopaCard> _deck;
        public List<MuscopaCard> Deck
        {
            get
            {
                return _deck ??= SetUpDeck();
                List<MuscopaCard> SetUpDeck()
                {
                    List<MuscopaCard> cards = new();

                    for (int i = 0; i < 3; i++)
                    {
                        cards.Add(new MuscopaCard(new Tonic(), Tableau));
                        cards.Add(new MuscopaCard(new Dominant(), Tableau));
                        cards.Add(new MuscopaCard(new Subdominant(), Tableau));
                    }

                    return cards;
                }
            }
        }

        private GameObject _handHL;
        public GameObject HandHL
        {
            get
            {
                return _handHL ? _handHL : _handHL = SetUp();
                GameObject SetUp()
                {
                    GameObject go = Assets.BackgroundCard;
                    go.name = nameof(HandHL);
                    go.transform.SetParent(Tableau);
                    go.transform.rotation = Quaternion.Euler(15, 0, 0);
                    return go;
                }
            }
        }

        private GameObject _answerPileHL;
        public GameObject AnswerHL
        {
            get
            {
                return _answerPileHL ? _answerPileHL : _answerPileHL = SetUp();
                GameObject SetUp()
                {
                    GameObject go = Assets.BackgroundCard;
                    go.name = nameof(AnswerHL);
                    go.transform.SetParent(Tableau);
                    go.transform.rotation = Quaternion.Euler(15, 0, 0);
                    return go;
                }
            }
        }

        private SpriteRenderer _answerPile1;
        public SpriteRenderer AnswerSpot1
        {
            get
            {
                return _answerPile1 ? _answerPile1 : _answerPile1 = SetUp();
                SpriteRenderer SetUp()
                {
                    SpriteRenderer sr = new GameObject(nameof(AnswerSpot1)).AddComponent<SpriteRenderer>();
                    sr.name = nameof(AnswerSpot1);
                    sr.transform.SetParent(Tableau);
                    sr.transform.SetPositionAndRotation(
                        position: new Vector3(-2.25f, 1.5f, 100),
                        rotation: Quaternion.Euler(15, 0, 0));
                    sr.transform.localScale = new Vector3(1.25f, 1.75f, 1);
                    sr.sprite = Assets.SeaTile;
                    return sr;
                }
            }
        }

        private SpriteRenderer _answerPile2;
        public SpriteRenderer AnswerSpot2
        {
            get
            {
                return _answerPile2 ? _answerPile2 : _answerPile2 = SetUp();
                SpriteRenderer SetUp()
                {
                    SpriteRenderer sr = new GameObject(nameof(AnswerSpot2)).AddComponent<SpriteRenderer>();
                    sr.transform.SetParent(Tableau);
                    sr.transform.SetPositionAndRotation(
                        position: new Vector3(-.75f, 1.5f, 100),
                        rotation: Quaternion.Euler(15, 0, 0));
                    sr.transform.localScale = new Vector3(1.25f, 1.75f, 1);
                    sr.sprite = Assets.SeaTile;
                    return sr;
                }
            }
        }

        private SpriteRenderer _answerPile3;
        public SpriteRenderer AnswerSpot3
        {
            get
            {
                return _answerPile3 ? _answerPile3 : _answerPile3 = SetUp();
                SpriteRenderer SetUp()
                {
                    SpriteRenderer sr = new GameObject(nameof(AnswerSpot3)).AddComponent<SpriteRenderer>();
                    sr.transform.SetParent(Tableau);
                    sr.transform.SetPositionAndRotation(
                        position: new Vector3(.75f, 1.5f, 100),
                        rotation: Quaternion.Euler(15, 0, 0));
                    sr.transform.localScale = new Vector3(1.25f, 1.75f, 1);
                    sr.sprite = Assets.SeaTile;
                    return sr;
                }
            }
        }

        private SpriteRenderer _answerPile4;
        public SpriteRenderer AnswerSpot4
        {
            get
            {
                return _answerPile4 ? _answerPile4 : _answerPile4 = SetUp();
                SpriteRenderer SetUp()
                {
                    SpriteRenderer sr = new GameObject(nameof(AnswerSpot4)).AddComponent<SpriteRenderer>();
                    sr.transform.SetParent(Tableau);
                    sr.transform.SetPositionAndRotation(
                        position: new Vector3(2.25f, 1.5f, 100),
                        rotation: Quaternion.Euler(15, 0, 0));
                    sr.transform.localScale = new Vector3(1.25f, 1.75f, 1);
                    sr.sprite = Assets.SeaTile;
                    return sr;
                }
            }
        }

        private SpriteRenderer _drawPile;
        public SpriteRenderer DrawPile
        {
            get
            {
                return _drawPile ? _drawPile : _drawPile = SetUp();
                SpriteRenderer SetUp()
                {
                    SpriteRenderer sr = new GameObject(nameof(DrawPile)).AddComponent<SpriteRenderer>();
                    sr.transform.SetParent(Tableau);
                    sr.transform.SetPositionAndRotation(
                        position: new Vector3(-3.5f, -1.5f, 100),
                        rotation: Quaternion.Euler(15, 0, 0));
                    sr.transform.localScale = new Vector3(1.25f, 1.75f, 1);
                    sr.sprite = Assets.SeaTile;
                    sr.color = Color.green;
                    return sr;
                }
            }
        }

        private SpriteRenderer _discardPile;
        public SpriteRenderer DiscardPile
        {
            get
            {
                return _discardPile ? _discardPile : _discardPile = SetUp();
                SpriteRenderer SetUp()
                {
                    SpriteRenderer sr = new GameObject(nameof(DiscardPile)).AddComponent<SpriteRenderer>();
                    sr.transform.SetParent(Tableau);
                    sr.transform.SetPositionAndRotation(
                        position: new Vector3(3.5f, -1.5f, 100),
                        rotation: Quaternion.Euler(15, 0, 0));
                    sr.transform.localScale = new Vector3(1.25f, 1.75f, 1);
                    sr.sprite = Assets.SeaTile;
                    sr.color = Color.magenta;
                    return sr;
                }
            }
        }
    }
}