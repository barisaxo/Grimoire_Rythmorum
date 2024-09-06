
using System.Collections;
using UnityEngine;

using Musica;
using MusicTheory;
using MusicTheory.Functions.Harmonic;
using MusicTheory.Functions.Harmonic.Arithmetic;
using MusicTheory.Notes.Arithmetic;
using MusicTheory.RomanNumerals.Diatonic.Arithmetic;
using MusicTheory.Triads.Arithmetic;
using MusicTheory.RomanNumerals.Diatonic;

namespace Muscopa
{
    public class MuscopaScene : IScene
    {
        public MuscopaScene(State subsequentState, RegionalMode mode, bool isPractice)
        {
            SubsequentState = subsequentState;
            Mode = mode;
            IsPractice = isPractice;
        }

        readonly RegionalMode Mode;
        public readonly State SubsequentState;
        public readonly bool IsPractice;
        public bool AnyWrongAnswers;
        public MusicaAudio MusicaAudio;
        public MusicaSettings MuscopaSettings;
        public MuscopaTableau Tableau;
        public CardManager CardManager;

        public IFunction[] AnswerSheet;
        public IFunction[] CurAnswers;

        public MuscopaHUD MuscopaHud = new();
        public IHud Hud => MuscopaHud;

        MusicaSettings NewSettings(CadenceDifficulty difficulty, Genre genre)
        {
            return new MusicaSettings(
                keyCenter: MusicTheory.Notes.NoteEnum.RandomKeyCenter(),
                genre: genre,
                scale: MusicalScale.Major,
                cadence: Mode.RandomCadence(difficulty),
                extension: Extension.Triad,
                tempo: genre.GetTempo()
            );
        }

        public IEnumerator GetNewSettings(System.Action callback)
        {
            AudioClip[] chords = new AudioClip[1];
            AudioClip[] basses = new AudioClip[1];

            for (int i = 0; i < 1; i++)
            {
                chords[i] = MuscopaAssets.GetAudioClip(MuscopaSettings.Chords[i].Genre, MuscopaSettings.Chords[i].Axe, (int)MuscopaSettings.Chords[i].Tempo);
                basses[i] = MuscopaAssets.GetAudioClip(MuscopaSettings.Basses[i].Genre, MuscopaSettings.Basses[i].Axe, (int)MuscopaSettings.Basses[i].Tempo);
            }

            AudioClip[] drums = new AudioClip[2]
            {
                MuscopaAssets.GetDrumAC(MuscopaSettings.Genre, (int)MuscopaSettings.Tempo, 1),
                MuscopaAssets.GetDrumAC(MuscopaSettings.Genre, (int)MuscopaSettings.Tempo, 2)
            };

            while (chords[0].loadState != AudioDataLoadState.Loaded)
            {
                Debug.Log("Loading");
                yield return null;
            }
            while (basses[0].loadState != AudioDataLoadState.Loaded)
            {
                yield return null;
            }

            MusicaAudio.LoadNewMuscopaSettings(new MuscopaPuzzle_AudioManager_Settings
            {
                StartTimes = MuscopaSettings.StartTimes,

                BPM = (int)MuscopaSettings.Tempo,

                CountsPerClipChords = 4,
                ChordClips = chords,

                CountsPerClipBass = 4,
                BassClips = basses,

                CountsPerClipDrums = 16,
                DrumClips = drums,
            });

            callback?.Invoke();
        }

        void IScene.SetUp()
        {
            Tableau = new();
            MonoHelper.OnUpdate += Pulsate;
            CardManager = new(Tableau);
            MuscopaSettings = NewSettings(CadenceDifficulty.ALL, Genre.Stax);

            MusicaAudio = new(Datum.Manager.Io.Volume);

            GetNewSettings(null).StartCoroutine();

            MusicaAudio.PlayNewMuscopaPuzzleMusic();

            AnswerSheet = MuscopaSettings.Cadence.DiatonicToHarmonicFunctionCadence();
            foreach (var a in AnswerSheet) UnityEngine.Debug.Log(a);

            CardManager.AnsweredSpots.Add(AnswerSpot.One);

            foreach (MuscopaCard card in Tableau.Deck)
                if (card.HarmonicFunction.Equals(AnswerSheet[0]))
                {
                    this.DropCardInAnswerSpot(card, AnswerSpot.One);
                    CardManager.CardsInAnswerSpots.Add(card);
                    CardManager.CardsInDrawPile.Remove(card);
                    break;
                }


            ((MuscopaHUD)Hud).Answer1ChordName.TextString = this.GetChordAndRomanNames();
            ((MuscopaHUD)Hud).KeyOf.TextString = "Key of: " + MuscopaSettings.KeyCenter.Name;
            string s = "";
            foreach (var i in Enumeration.All<MusicTheory.RomanNumerals.Diatonic.RomanNumeralEnum>())
            {
                s += i.Name +
                    i.GetRoman().GetTriad().Name +
                    " : " +
                    MuscopaSettings.KeyCenter.GetNoteAbove(i.GetRoman()).Name +
                    i.GetRoman().GetTriad().Name +
                    "    ";
            }
            ((MuscopaHUD)Hud).AllChords.TextString = s;
        }

        void IScene.Destroy()
        {
            Tableau.SelfDestruct();
            MonoHelper.OnUpdate -= Pulsate;
        }

        bool sign;
        float alpha = 1f;

        void Pulsate()
        {
            float min = 0.35f;
            float max = 1.0f;
            float frequency = 1f;
            alpha += Time.deltaTime * frequency * (sign ? 1 : -1);
            sign = alpha <= max && (alpha < min || sign);
            Debug.Log(alpha);

            Color c = Tableau.AnswerHL.color;
            c.a = alpha;

            Tableau.AnswerHL.color = c;
            Tableau.HandHL.color = c;
        }
    }
}