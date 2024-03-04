using System.Collections;
using System;
using UnityEngine;
using SheetMusic;
using Batterie;
using MusicTheory.Rhythms;
using MusicTheory;
using Muscopa;

public class BatterieAndCadence_State : State
{
    public BatterieAndCadence_State(BatteriePack pack)
    {
        Pack = pack;
    }

    readonly BatteriePack Pack;
    RhythmSpecs Specs;
    MusicSheet MusicSheet;
    Synchronizer Synchro;
    CountOffFeedback CountOffFeedBack;
    Note[] CountOffNotes;
    MappedBeat[] CountOffBeatmap;
    BatterieInputAnalyzer Analyzer;
    BatterieFeedback BatterieFeedback;

    MuscopaSettings MuscopaSettings;
    MuscopaAudio MuscopaAudio;
    bool cadenceStarted = false;

    public int Counter { get; private set; }
    bool CountingOff = true;
    bool Playing = false;

    int score, cap;

    protected override void PrepareState(Action callback)
    {
        Specs = new MusicTheory.Rhythms.RhythmSpecs()
        {
            Time = new MusicTheory.Rhythms.FourFour(),
            NumberOfMeasures = 4,
            SubDivisionTier = MusicTheory.Rhythms.SubDivisionTier.D1Only,
            HasTies = UnityEngine.Random.value > .5f,
            HasRests = UnityEngine.Random.value > .5f,
            HasTriplets = false,
            Tempo = 90
        };
        MusicSheet = new MusicSheet()
        {
            RhythmSpecs = Specs
        };
        _ = Background;
        MusicSheet.RhythmSpecs.Time.GenerateRhythmCells(MusicSheet);
        MusicSheet.GetNotes();
        MusicSheet.DrawRhythms();
        MusicSheet.BeatMap = MusicSheet.Notes.MapBeats(Specs.Tempo);
        Synchro = new(Specs.Time.GetQuantizement(), Specs.Tempo);
        CountOffNotes = CountOff.GetNotes(Specs.Time);
        CountOffBeatmap = CountOffNotes.MapBeats(Specs.Tempo);
        CountOffFeedBack = new(Specs.Time.GetCounts());
        BatterieFeedback = new();
        Analyzer = new(BatterieFeedback.CreateCard, HandleHit, 5, MusicSheet.BeatMap);
        Analyzer.SetUp();

        BatterieFeedback.UpdateLoop();
        CountOffFeedBack.UpdateLoop();
        Synchro.TickEvent += Tick;
        Counter = 1;
        MonoHelper.OnUpdate += SpaceBar;

        MuscopaAudio = new(Data.Volume);
        MuscopaSettings = NewSettings(CadenceDifficulty.ALL, MusicTheory.Musica.RandomMode(), Genre.Stax);

        Cam.Io.Camera.transform.SetPositionAndRotation(
            new UnityEngine.Vector3(Cam.Io.Camera.transform.position.x, 15, Cam.Io.Camera.transform.position.z),
            Quaternion.identity);

        if (Pack.Ship == null)
        {
            Pack.Ship = Sea.WorldMapScene.Io.Ship.GO;
            Pack.NME = Sea.WorldMapScene.Io.NearestNPC.SceneObject.GO;

            // Pack.Ship.transform.SetParent(Cam.Io.Camera.transform);
            // Pack.NME.transform.SetParent(Cam.Io.Camera.transform);

            Pack.Ship.transform.position = Cam.Io.Camera.transform.position + ((Cam.Io.Camera.transform.forward * 4) - (Cam.Io.Camera.transform.right * 2) - (Cam.Io.Camera.transform.up * 2));
            Pack.NME.transform.position = Cam.Io.Camera.transform.position + ((Cam.Io.Camera.transform.forward * 4) + (Cam.Io.Camera.transform.right * 2) - (Cam.Io.Camera.transform.up * 2));

            Pack.Ship.transform.LookAt(Cam.Io.Camera.transform);
            Pack.NME.transform.LookAt(Cam.Io.Camera.transform);

            Pack.NMEFire = Assets.CannonFire;
            Pack.NMEFire.transform.position = Pack.NME.transform.position + (Vector3.left * .25f);
            Pack.NMEFire.transform.LookAt(Pack.Ship.transform);

            Pack.ShipFire = Assets.CannonFire;
            Pack.ShipFire.transform.position = Pack.Ship.transform.position + (Vector3.right * .25f);
            Pack.ShipFire.transform.LookAt(Pack.NME.transform);
        }

        GetNewSettings(callback).StartCoroutine();
    }

    protected override void EngageState()
    {
        Synchro.KeepTime();
    }

    protected override void DisengageState()
    {
        Background.SelfDestruct();
        Debug.Log((float)((float)score / (float)cap));
        Synchro.TickEvent -= Tick;
        Synchro.BeatEvent -= Click;
        MonoHelper.OnUpdate -= SpaceBar;

        BatterieFeedback.Running = false;
        Audio.Batterie.Stop();
        MuscopaAudio.StopTheCadence();
        MusicSheet.SelfDestruct();
    }

    public MuscopaSettings NewSettings(CadenceDifficulty difficulty, RegionalMode shipsRegion, Genre genre)
    {
        return new MuscopaSettings(
            key: MusicTheory.Musica.RandomKey(),
            genre: genre,
            scale: MusicalScale.Major,
            cadence: RegionalMode.Aeolian.RandomMode().RandomCadence(difficulty),
            extension: Extension.Triad,
            tempo: genre.GetTempo()
        );
    }

    void Tick()
    {
        if (CountingOff)
        {
            CountOffTimeEvent();
            if (++Counter == CountOffBeatmap.Length - 1)
            {
                MonoHelper.OnUpdate += Analyzer.Tick;
                Analyzer.Start();
                Synchro.BeatEvent += Click;
                CountingOff = false; Playing = true; Counter = 0;
                // Audio.Batterie.Miss();
            }
            return;
        }

        if (!cadenceStarted)
        {
            MuscopaAudio.PlayNewMuscopaPuzzleMusic();
            cadenceStarted = true;
        }

        if (Playing)
        {
            BatterieTimeEvent();
            Playing = !(++Counter >= MusicSheet.BeatMap.Length);
        }

        if (!Playing && !CountingOff)
        {
            Audio.Batterie.Stop();
            Synchro.Stop();
            MonoHelper.OnUpdate -= Analyzer.Tick;
            MuscopaAudio.StopTheCadence();

            // FadeToState(PuzzleSelector.WeightedRandomPuzzleState(Data.TheoryPuzzleData));
            Pack.NMEHealth.cur -= (int)(10 * (float)((float)score / (float)cap));
            SetState(new DialogStart_State(new BatterieIntermission_Dialogue(Pack)));
        }
    }

    void CountOffTimeEvent()
    {
        switch (CountOffBeatmap[Counter].NoteFunction)
        {
            case NoteFunction.Attack:
                CountOffFeedBack.ReadCountOff();
                Audio.Batterie.PlayClick();
                break;

            case NoteFunction.Hold:
                Audio.Batterie.PlayClick();
                break;
        }
    }

    void BatterieTimeEvent()
    {
        switch (MusicSheet.BeatMap[Counter].NoteFunction)
        {
            case NoteFunction.Attack:
                Audio.Batterie.PlaySnareRoll();
                break;

            case NoteFunction.Rest:
                Audio.Batterie.RestSnareRoll();
                break;

            case NoteFunction.Hold:
                break;
        }
        if (UnityEngine.Random.value < .04f) Pack.NMEFire.Play();
    }

    void Click()
    {
        //Audio.Batterie.PlayClick();
    }

    protected override void Clicked(MouseAction action, Vector3 mousePos)
    {
        switch (action)
        {
            case MouseAction.LUp:
                Analyzer.InputUpAction();
                break;

            case MouseAction.LDown:
                Analyzer.InputDownAction();
                break;
        }
    }

    protected void SpaceBar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Analyzer.InputDownAction();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Analyzer.InputUpAction();

        }

        // if (Input.GetKeyDown(KeyCode.Tab))
        // {
        //     SetState(new Batterie_State(Specs));
        // }
    }

    protected override void GPInput(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.Up_Release:
            case GamePadButton.Down_Release:
            case GamePadButton.Left_Release:
            case GamePadButton.Right_Release:
            case GamePadButton.North_Release:
            case GamePadButton.South_Release:
            case GamePadButton.West_Release:
            case GamePadButton.East_Release:
            case GamePadButton.L1_Release:
            case GamePadButton.R1_Release:
            case GamePadButton.L2_Release:
            case GamePadButton.R2_Release:
                Analyzer.InputUpAction(); break;

            case GamePadButton.Up_Press:
            case GamePadButton.Down_Press:
            case GamePadButton.Left_Press:
            case GamePadButton.Right_Press:
            case GamePadButton.North_Press:
            case GamePadButton.South_Press:
            case GamePadButton.West_Press:
            case GamePadButton.East_Press:
            case GamePadButton.L1_Press:
            case GamePadButton.R1_Press:
            case GamePadButton.L2_Press:
            case GamePadButton.R2_Press:
                Analyzer.InputDownAction(); break;
        }
    }

    private void HandleHit(Batterie.Hit hit)
    {
        cap++;
        switch (hit)
        {
            case Hit.Hit:
                score++;
                Audio.Batterie.Hit();
                Pack.ShipFire.Play();
                break;
            case Hit.Miss:
                score--;
                Audio.Batterie.Miss();
                break;
            case Hit.BadHit:
                score++;
                Audio.Batterie.MissStick();
                Pack.NMEFire.Play();
                break;
            case Hit.Break:
                break;
        }
    }

    IEnumerator GetNewSettings(Action callback)
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


        callback();
    }

    //RegionalMode GetShipRegion()
    //{
    //    return Board.TargetTile.ShipType.ToRegion();
    //}

    public static Genre RandomGenre() => (Genre)UnityEngine.Random.Range(0, Count());
    public static int Count() => Enum.GetNames(typeof(Genre)).Length;

    Card _background;
    Card Background => _background ??= new Card(nameof(Background), null)
        .SetImagePosition(Vector2.zero)
        .SetImageSize(new Vector2(Cam.Io.UICamera.scaledPixelWidth * .8f, Cam.Io.UICamera.scaledPixelHeight * .8f))
        .SetCanvasSortingOrder(0)
        .SetImageColor(new Color(0, 0, 0, .25f));

}