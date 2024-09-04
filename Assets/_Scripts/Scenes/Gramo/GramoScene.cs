using UnityEngine;
using MusicTheory;
using Musica;
using System.Collections;
using MusicTheory.Functions.Harmonic;
using MusicTheory.Functions.Harmonic.Arithmetic;
using MusicTheory.Notes.Arithmetic;
using MusicTheory.RomanNumerals.Diatonic.Arithmetic;
using MusicTheory.Triads.Arithmetic;
using TMPro;


namespace Gramophones
{
    public class GramoScene
    {
        public GramoScene(Datum.IGramophone gramo)
        {
            _ = Light;
            GramoHUD = new();
            GramoHUD.Initialize();

            MuscopaSettings = NewSettings((CadenceDifficulty)gramo.Id, Genre.Stax);
            MuscopaAudio = new(Datum.Manager.Io.Volume);
            GetNewSettings(null).StartCoroutine();

            AnswerSheet = MuscopaSettings.Cadence.DiatonicToHarmonicFunctionCadence();

            foreach (var c in AnswerSheet)
                Debug.Log(c);

            CurAnswers = new IFunction[4];
            CurAnswers[0] = AnswerSheet[0];

            Debug.Log(CurAnswers[0].ToString());
            CurSelection = Gramo.AnswerMesh1;
            // Gramo.AnswerMesh1.material = this.NewAnswerMat(Gramo.AnswerMesh1);
            this.SpinLeft(Gramo.AnswerMesh1).StartCoroutine();

            MuscopaAudio.PlayNewMuscopaPuzzleMusic();

            GramoHUD.KeyOf.TextString = "Key of: " + MuscopaSettings.KeyCenter.Name;
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
            GramoHUD.AllChords.TextString = s;
        }

        public void SelfDestruct()
        {
            Hud.SelfDestruct();
            Object.Destroy(Gramo.gameObject);
            Object.Destroy(Light.gameObject);
            MuscopaAudio.StopTheCadence();
        }

        public GramoHUD GramoHUD;
        public IHud Hud => GramoHUD;
        public MusicaAudio MuscopaAudio;
        public MusicaSettings MuscopaSettings;
        public MeshRenderer CurSelection;

        private GramoMB _gramo;
        public GramoMB Gramo => _gramo ? _gramo : _gramo = Assets.GramoPuzzle.GetComponent<GramoMB>();

        public readonly IFunction[] AnswerSheet;
        public IFunction[] CurAnswers;

        public bool[] Spinning = new bool[] { false, false, false };

        private Light _light;
        public Light Light => _light ? _light : _light = SetUpLight();

        private Light SetUpLight()
        {
            var l = new GameObject(nameof(Light)).AddComponent<Light>();
            l.type = LightType.Directional;
            return l;
        }

        public MusicaSettings NewSettings(CadenceDifficulty difficulty, Genre genre)
        {
            return new MusicaSettings(
                keyCenter: MusicTheory.Notes.NoteEnum.RandomKeyCenter(),
                genre: genre,
                scale: MusicalScale.Major,
                cadence: difficulty.RandomModeByDifficulty().RandomCadence(difficulty),
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

            MuscopaAudio.LoadNewMuscopaSettings(new MuscopaPuzzle_AudioManager_Settings
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

    }
}