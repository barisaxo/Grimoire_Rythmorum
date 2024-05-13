using UnityEngine;
using MusicTheory;
using Muscopa;
using System.Collections;

public class GramoScene
{
    public GramoScene()
    {
        _ = Light;

        MuscopaSettings = NewSettings(CadenceDifficulty.ALL, Genre.Stax);
        MuscopaAudio = new(Data.Two.Manager.Io.Volume);
        GetNewSettings(null).StartCoroutine();

        AnswerSheet = MuscopaSettings.Cadence.DiatonicToHarmonicFunctionCadence();
        CurAnswers = new HarmonicFunction[4]{
            AnswerSheet[0],
            HarmonicFunction.Secondary,
            HarmonicFunction.Secondary,
            HarmonicFunction.Secondary};

        CurSelection = Gramo.AnswerMesh1;
        // this.SetAnswer(cadence[0]);
        Gramo.AnswerMesh1.material = this.NewAnswerMat(Gramo.AnswerMesh1);
        CurSelection = Gramo.AnswerMesh2;

        MuscopaAudio.PlayNewMuscopaPuzzleMusic();
    }

    public void SelfDestruct()
    {
        Object.Destroy(Gramo.gameObject);
        Object.Destroy(Light.gameObject);
        _confirmButton?.SelfDestruct();
    }

    MuscopaAudio MuscopaAudio;
    public MuscopaSettings MuscopaSettings;
    public MeshRenderer CurSelection;

    private GramoMB _gramo;
    public GramoMB Gramo => _gramo ? _gramo : _gramo = Assets.GramoPuzzle.GetComponent<GramoMB>();

    public readonly HarmonicFunction[] AnswerSheet;
    public HarmonicFunction[] CurAnswers;

    public bool[] Spinning = new bool[] { false, false, false };

    private Card _confirmButton;
    public Card ConfirmButton => _confirmButton ??= new Card(nameof(ConfirmButton), null)
    .SetTextString("Try Open")
    .SetTMPPosition(Cam.UIOrthoX - 2, -Cam.UIOrthoY + 1)
    .SetImageSprite(Assets.EastButton)
    .SetImagePosition(Cam.UIOrthoX - 2, -Cam.UIOrthoY + 2);

    private Light _light;
    public Light Light => _light ? _light : _light = SetUpLight();

    private Light SetUpLight()
    {
        var l = new GameObject(nameof(Light)).AddComponent<Light>();
        l.type = LightType.Directional;
        return l;
    }

    public MuscopaSettings NewSettings(CadenceDifficulty difficulty, Genre genre)
    {
        return new MuscopaSettings(
            key: Musica.RandomKey(),
            genre: genre,
            scale: MusicalScale.Major,
            cadence: RegionalMode.Aeolian.RandomMode().RandomCadence(difficulty),
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
