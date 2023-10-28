using System.Collections;
using System;
using UnityEngine;
using SheetMusic;
using Batterie;
using MusicTheory.Rhythms;
using MusicTheory;
using Muscopa;


public class BatteryAndCadenceTestState : State
{
    public BatteryAndCadenceTestState(RhythmSpecs specs)
    {
        Specs = specs;
    }

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
    bool cadencestarted = false;

    public int Counter { get; private set; }
    bool CountingOff = true;
    bool Playing = false;

    protected override void PrepareState(Action callback)
    {
        MusicSheet = new MusicSheet() { RhythmSpecs = Specs };
        _ = MusicSheet.BackGround;
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
        MuscopaSettings = NewSettings(CadenceDifficulty.ALL, MusicTheory.MusicTheory.RandomMode(), Genre.Stax);

        GetNewSettings(callback).StartCoroutine();
    }

    protected override void EngageState()
    {
        Synchro.KeepTime();
    }

    protected override void DisengageState()
    {
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
            key: MusicTheory.MusicTheory.RandomKey(),
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
                CountingOff = false; Playing = true; Counter = 0;
                MonoHelper.OnUpdate += Analyzer.Tick;
                Synchro.BeatEvent += Click;
                Analyzer.Start();
                Audio.Batterie.Miss();
            }
            return;
        }

        if (!cadencestarted)
        {
            MuscopaAudio.PlayNewMuscopaPuzzleMusic();
            cadencestarted = true;
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetStateDirectly(new Batterie_State(Specs));
        }
    }

    private void HandleHit(Batterie.Hit hit)
    {
        switch (hit)
        {
            case Hit.Hit:
                Audio.Batterie.Hit();
                break;
            case Hit.Miss:
                Audio.Batterie.Miss();
                break;
            case Hit.BadHit:
                Audio.Batterie.MissStick();
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


}