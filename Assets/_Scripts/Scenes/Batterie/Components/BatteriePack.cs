using UnityEngine;
using System;
using System.Collections;
using SheetMusic;
using Batterie;
using MusicTheory.Rhythms;
using Muscopa;
using MusicTheory;

public class BatteriePack
{
    // public Bard2D.BattleHUD.BattleHUD BHUD;
    // public BatteriePack(RhythmSpecs specs)
    // {

    // }

    public BatteriePack(RhythmSpecs rhythmSpecs)
    {
        _rhythmSpecs = rhythmSpecs;
    }

    readonly RhythmSpecs _rhythmSpecs = null;

    public void Initialize(Action<Batterie.Hit> HandleHit, BatterieFeedback BatterieFeedback, Action Tick)
    {
        MusicSheet = new()
        {
            RhythmSpecs = _rhythmSpecs
        };
        MuscopaAudio = new(Data.Two.Manager.Io.Volume);
        MusicSheet.RhythmSpecs.Time.GenerateRhythmCells(MusicSheet);
        MusicSheet.GetNotes();
        MusicSheet.DrawRhythms();
        MusicSheet.BeatMap = MusicSheet.Notes.MapBeats(MusicSheet.RhythmSpecs.Tempo);
        Synchro = new(MusicSheet.RhythmSpecs.Time.GetQuantizement(), MusicSheet.RhythmSpecs.Tempo);
        CountOffNotes = CountOff.GetNotes(MusicSheet.RhythmSpecs.Time);
        CountOffBeatmap = CountOffNotes.MapBeats(MusicSheet.RhythmSpecs.Tempo);
        Analyzer = new(BatterieFeedback.CreateCard, HandleHit, 5, MusicSheet.BeatMap);
        Analyzer.SetUp();

        Synchro.TickEvent += Tick;

        MuscopaSettings = NewSettings(CadenceDifficulty.ALL, MusicTheory.Musica.RandomMode(), Genre.Stax);
        GoodHits = GoodRests = GoodHolds = ErroneousAttacks = MissedHits = MissedHolds = MissedRests = 0;
    }

    public int GoodHits;
    public int GoodHolds;
    public int GoodRests;
    public int ErroneousAttacks;
    public int MissedRests;
    public int MissedHits;
    public int MissedHolds;
    public int TotalErrors => ErroneousAttacks + MissedRests + MissedHits + MissedHolds;
    public bool Spammed => ErroneousAttacks > GoodHits + GoodHolds + GoodRests + MissedRests + MissedHits + MissedHolds;
    public bool HasCritThisBattery = false;
    public bool Crit
    {
        get
        {
            bool c = TotalErrors <= Data.Two.Manager.Io.Skill.GetLevel(new Data.Two.PerfectTiming()) + 1;
            if (c) HasCritThisBattery = true;
            return c;
        }
    }


    public MusicSheet MusicSheet;
    public Synchronizer Synchro;
    public Note[] CountOffNotes;
    public MappedBeat[] CountOffBeatmap;
    public BatterieInputAnalyzer Analyzer;
    public MuscopaSettings MuscopaSettings;
    public MuscopaAudio MuscopaAudio;

    public BatterieResultType ResultType;
    public BatteriePack SetResultType(BatterieResultType type) { ResultType = type; return this; }

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

    public IEnumerator GetNewSettings(Action callback)
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

}

public enum BatterieResultType { Won, Lost, NMEscaped, NMESurrender, Surrender, Fled, Spam }