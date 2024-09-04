using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MusicTheory;
using MusicTheory.Chords;

namespace OLDMuscopa
{
    public class MuscopaScene
    {
        #region INSTANCE

        public static MuscopaScene Io => Instance.Entities;
        private MuscopaScene()
        {
            _ = CardTable;

            _ = Hand;
            _ = Hand1;
            _ = Hand2;
            _ = Hand3;
            _ = Hand4;

            _ = Hand1HL;
            _ = Hand2HL;
            _ = Hand3HL;
            _ = Hand4HL;

            _ = AnswerPiles;
            _ = AnswerPile1;
            _ = AnswerPile2;
            _ = AnswerPile3;
            _ = AnswerPile4;
            // _ = Answer1HL;
            _ = Answer2HL;
            _ = Answer3HL;
            _ = Answer4HL;

            _ = DiscardPile;
            _ = DrawPile;
            DiscardPile.color = Color.green;
            DrawPile.color = Color.magenta;

            _ = KeyOf;
            _ = AllChords;
            _ = CurrentLevel;
            _ = Answer1ChordName;
            // Answer1ChordName.fontStyle = FontStyles.Normal;
            _ = Answer2ChordName;
            _ = Answer3ChordName;
            _ = Answer4ChordName;

            Hand1HL.gameObject.SetActive(false);
            Hand2HL.gameObject.SetActive(false);
            Hand3HL.gameObject.SetActive(false);
            Hand4HL.gameObject.SetActive(false);

            //Answer1HL.gameObject.SetActive(false);
            Answer2HL.gameObject.SetActive(false);
            Answer3HL.gameObject.SetActive(false);
            Answer4HL.gameObject.SetActive(false);

            _ = Deck;
        }

        private static class Instance
        {
            static Instance() { }
            private static MuscopaScene entities;
            internal static MuscopaScene Entities => entities ??= new MuscopaScene();
            internal static void Destruct() => entities = null;
        }

        public void SelfDestruct()
        {
            UnityEngine.Object.Destroy(cardTable);
            Instance.Destruct();
            Resources.UnloadUnusedAssets();
        }

        #endregion

        #region MANAGEMENT

        private bool keepEnabled;
        public void KeepCardGameEnabled() { keepEnabled = true; }
        public void DisableCardGame()
        {
            keepEnabled = false;
            MonoHelper.Io.StartCoroutine(Disable());
        }

        IEnumerator Disable()
        {
            yield return new WaitForEndOfFrame();
            Io.CardTable.SetActive(keepEnabled);
        }

        #endregion MANAGEMENT

        #region FIELDS

        private GameObject cardTable;
        public GameObject CardTable => cardTable != null ? cardTable :
            cardTable = new GameObject(nameof(CardTable));

        public List<bool> CardsInAnimation { get; private set; } = new List<bool>();

        public bool anyWrongAnswers = false;
        public bool puzzleActive = true;
        public List<CardPile> AnsweredPiles = new List<CardPile>();
        // private List<HarmonicFunction> puzzleChords = null;
        public List<HarmonicFunction> PuzzleChords; //TODO=> puzzleChords ??= Musica.RandomChordalCadence();

        public MuscopaCard[] CardsInHand { get; private set; } = new MuscopaCard[4];
        public List<MuscopaCard> CardsInDrawPile { get; private set; } = new List<MuscopaCard>();
        public List<MuscopaCard> CardsInDiscardPile { get; private set; } = new List<MuscopaCard>();
        public List<MuscopaCard> CardsInAnswerPiles { get; private set; } = new List<MuscopaCard>();

        private List<MuscopaCard> cardDeck;
        public List<MuscopaCard> Deck => cardDeck ??= Initializer.SetUpDeck(CardTable);

        public CardPile CurrentAnswerPile = 0;
        public CardPile CurrentHandPile = 0;
        public List<CardPile> ActiveHandPiles = new List<CardPile>();

        private GameObject hand;
        internal GameObject Hand => hand != null ? hand :
            hand = Initializer.SetupHandSlot(nameof(Hand), CardTable, Vector3.zero);

        private GameObject hand1;
        internal GameObject Hand1 => hand1 != null ? hand1 :
            hand1 = Initializer.SetupHandSlot(nameof(Hand1), Hand, new Vector3(-2.25f, -1f, 0));

        private GameObject hand2;
        internal GameObject Hand2 => hand2 != null ? hand2 :
            hand2 = Initializer.SetupHandSlot(nameof(Hand2), Hand, new Vector3(-.75f, -1f, 0));

        private GameObject hand3;
        internal GameObject Hand3 => hand3 != null ? hand3 :
            hand3 = Initializer.SetupHandSlot(nameof(Hand3), Hand, new Vector3(.75f, -1f, 0));

        private GameObject hand4;
        internal GameObject Hand4 => hand4 != null ? hand4 :
            hand4 = Initializer.SetupHandSlot(nameof(Hand4), Hand, new Vector3(2.25f, -1f, 0));

        private SpriteRenderer hand1HL;
        internal SpriteRenderer Hand1HL => hand1HL != null ? hand1HL :
            hand1HL = Initializer.SetUpHL(nameof(Hand1HL), Hand1, Quaternion.Euler(15, 0, 0));

        private SpriteRenderer hand2HL;
        internal SpriteRenderer Hand2HL => hand2HL != null ? hand2HL :
            hand2HL = Initializer.SetUpHL(nameof(Hand2HL), Hand2, Quaternion.Euler(15, 0, 0));

        private SpriteRenderer hand3HL;
        internal SpriteRenderer Hand3HL => hand3HL != null ? hand3HL :
            hand3HL = Initializer.SetUpHL(nameof(Hand3HL), Hand3, Quaternion.Euler(15, 0, 0));

        private SpriteRenderer hand4HL;
        internal SpriteRenderer Hand4HL => hand4HL != null ? hand4HL :
            hand4HL = Initializer.SetUpHL(nameof(Hand4HL), Hand4, Quaternion.Euler(15, 0, 0));

        private GameObject answerPiles;
        internal GameObject AnswerPiles => answerPiles != null ? answerPiles :
            answerPiles = Initializer.SetupHandSlot(nameof(AnswerPiles), CardTable, Vector3.zero);

        private SpriteRenderer answerPile1;
        internal SpriteRenderer AnswerPile1 => answerPile1 != null ? answerPile1 :
            answerPile1 = Initializer.SetUpCardPile(
                name: nameof(AnswerPile1),
                parent: AnswerPiles,
                pos: new Vector3(-2.25f, 1.5f, 0),
                rot: Quaternion.Euler(15, 0, 0),
                size: new Vector3(1.25f, 1.75f, 1),
                sprite: Assets.SeaTile);

        private SpriteRenderer answerPile2;
        internal SpriteRenderer AnswerPile2 => answerPile2 != null ? answerPile2 :
           answerPile2 = Initializer.SetUpCardPile(
                name: nameof(AnswerPile2),
                parent: AnswerPiles,
                pos: new Vector3(-.75f, 1.5f, 0),
                rot: Quaternion.Euler(15, 0, 0),
                size: new Vector3(1.25f, 1.75f, 1),
                sprite: Assets.SeaTile);

        private SpriteRenderer answerPile3;
        internal SpriteRenderer AnswerPile3 => answerPile3 != null ? answerPile3 :
            answerPile3 = Initializer.SetUpCardPile(
                name: nameof(AnswerPile3),
                parent: AnswerPiles,
                pos: new Vector3(.75f, 1.5f, 0),
                rot: Quaternion.Euler(15, 0, 0),
                size: new Vector3(1.25f, 1.75f, 1),
                sprite: Assets.SeaTile);

        private SpriteRenderer answerPile4;
        internal SpriteRenderer AnswerPile4 => answerPile4 != null ? answerPile4 :
            answerPile4 = Initializer.SetUpCardPile(
                name: nameof(AnswerPile4),
                parent: AnswerPiles,
                pos: new Vector3(2.25f, 1.5f, 0),
                rot: Quaternion.Euler(15, 0, 0),
                size: new Vector3(1.25f, 1.75f, 1),
                sprite: Assets.SeaTile);

        // private  SpriteRenderer answer1HL;
        //internal  SpriteRenderer Answer1HL => answer1HL != null ? answer1HL :
        //    answer1HL = SetUpAnswerHL(
        //        name: nameof(Answer1HL),
        //        parent: gameObject,
        //        pos: new Vector3(-2.25f, 1.5f, 0.05f),
        //        rot: Quaternion.Euler(15, 0, 0),
        //        size: Vector3.one * 1.4f);

        private SpriteRenderer answer2HL;
        internal SpriteRenderer Answer2HL => answer2HL != null ? answer2HL :
            answer2HL = Initializer.SetUpAnswerHL(
                name: nameof(Answer2HL),
                parent: CardTable,
                pos: new Vector3(-.75f, 1.5f, 0.05f),
                rot: Quaternion.Euler(15, 0, 0),
                size: Vector3.one * 1.4f);

        private SpriteRenderer answer3HL;
        internal SpriteRenderer Answer3HL => answer3HL != null ? answer3HL :
            answer3HL = Initializer.SetUpAnswerHL(
                name: nameof(Answer3HL),
                parent: CardTable,
                pos: new Vector3(.75f, 1.5f, 0.05f),
                rot: Quaternion.Euler(15, 0, 0),
                size: Vector3.one * 1.4f);

        private SpriteRenderer answer4HL;
        internal SpriteRenderer Answer4HL => answer4HL != null ? answer4HL :
            answer4HL = Initializer.SetUpAnswerHL(
                name: nameof(Answer4HL),
                parent: CardTable,
                pos: new Vector3(2.25f, 1.5f, 0.05f),
                rot: Quaternion.Euler(15, 0, 0),
                size: Vector3.one * 1.4f);

        private SpriteRenderer drawPile;
        internal SpriteRenderer DrawPile => drawPile != null ? drawPile :
            drawPile = Initializer.SetUpCardPile(
                name: nameof(DrawPile),
                parent: CardTable,
                pos: new Vector3(-3f, -2f, 0),
                rot: Quaternion.Euler(15, 0, 0),
                size: new Vector3(1.25f, 1.75f, 1),
                sprite: Assets.SeaTile);

        private SpriteRenderer discardPile;
        internal SpriteRenderer DiscardPile => discardPile != null ? discardPile :
            discardPile = Initializer.SetUpCardPile(
                name: nameof(DiscardPile),
                parent: CardTable,
                pos: new Vector3(3f, -2f, 0),
                rot: Quaternion.Euler(15, 0, 0),
                size: new Vector3(1.25f, 1.75f, 1),
                sprite: Assets.SeaTile);


        private Card _hud;
        public Card HUD => _hud ??= new(nameof(HUD), null);


        private Card _keyOf;
        internal Card KeyOf => _keyOf ??= HUD.CreateChild(nameof(KeyOf), HUD.Canvas)
            .SetTextString("KEY OF: Gb")
            .SetTMPPosition(-3.25f, -2.25f)
            .SetFontScale(.5f)
            .SetTextAlignment(TextAlignmentOptions.Center);

        private Card _allChords;
        internal Card AllChords => _allChords ??= HUD.CreateChild(nameof(AllChords), HUD.Canvas)
            .SetTextString("I: Gb,   II-: Ab-,   III-: Bb-,   IV: Cb,   V: Db,   VI-: Eb-,   VIIø: Fø")
            .SetTMPPosition(0, -2.5f)
            .SetFontScale(.5f)
            .SetTextAlignment(TextAlignmentOptions.Center)
            .AllowWordWrap(false);

        private Card _currentLevel;
        internal Card CurrentLevel => _currentLevel ??= HUD.CreateChild(nameof(CurrentLevel), HUD.Canvas)
            .SetTextString("I, II-, V")
            .SetTMPPosition(0, -2.25f)
            .SetFontScale(.5f)
            .SetTextAlignment(TextAlignmentOptions.Center);

        private Card _answer1ChordName;
        internal Card Answer1ChordName => _answer1ChordName ??= HUD.CreateChild(nameof(Answer1ChordName), HUD.Canvas)
            .SetTextString("Gb")
            .SetTMPPosition(-2.25f, .5f)
            .SetFontScale(1f)
            .SetTextAlignment(TextAlignmentOptions.Center);

        private Card _answer2ChordName;
        internal Card Answer2ChordName => _answer2ChordName ??= HUD.CreateChild(nameof(Answer2ChordName), HUD.Canvas)
            .SetTextString("?")
            .SetTMPPosition(-.75f, .5f)
            .SetFontScale(1f)
            .SetTextAlignment(TextAlignmentOptions.Center);

        private Card _answer3ChordName;
        internal Card Answer3ChordName => _answer3ChordName ??= HUD.CreateChild(nameof(Answer3ChordName), HUD.Canvas)
            .SetTextString("?")
            .SetTMPPosition(.75f, .5f)
            .SetFontScale(1f)
            .SetTextAlignment(TextAlignmentOptions.Center);

        private Card _answer4ChordName = null;
        internal Card Answer4ChordName => _answer4ChordName ??= HUD.CreateChild(nameof(Answer4ChordName), HUD.Canvas)
            .SetTextString("?")
            .SetTMPPosition(2.25f, .5f)
            .SetFontScale(1f)
            .SetTextAlignment(TextAlignmentOptions.Center);

        //public bool playingAudio = false;
        private AudioPuzzleSettings? audioPuzzleSettings = null;
        public AudioPuzzleSettings AudioPuzzleSettings => audioPuzzleSettings ??= Initializer.GenerateNewSettings(PuzzleChords);
        private AudioSource[] mainASs = null;
        public AudioSource[] MainASs => mainASs ??= Initializer.SetUpMainASs(CardTable);
        private AudioSource[] drumASs = null;
        public AudioSource[] DrumASs => drumASs ??= Initializer.SetUpDrumASs(CardTable);
        public double NextEventTimeMain;
        public double NextEventTimeDrums;
        public int CuedMainAS = 0;
        public int CuedDrumAS = 0;
        public int CuedMainClip = 0;
        public int CuedDrumClip = 0;

        #endregion FIELDS



    }


}