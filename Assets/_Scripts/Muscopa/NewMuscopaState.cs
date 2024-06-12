using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Muscopa;
using MusicTheory;

public class NewMuscopaState : State
{
    //Muscopa Muscopa;

    MuscopaSettings MuscopaSettings;
    MuscopaAudio MuscopaAudio;

    protected override void PrepareState(Action callback)
    {
        //Muscopa = new();
        //MHUD = new();
        //MHUD.Parent.gameObject.SetActive(false);
        MuscopaAudio = new(Data.Manager.Io.Volume);
        MuscopaSettings = NewSettings(CadenceDifficulty.ALL, MusicTheory.Musica.RandomMode(), RandomGenre());

        GetNewSettings(callback).StartCoroutine();
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

    protected override void EngageState()
    {
        Audio.BGMusic.Pause();
        MuscopaAudio.PlayNewMuscopaPuzzleMusic();
        //SetStateDirectly(new MuscopaInput_State(Muscopa, MHUD));
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


public enum BatterieDifficulty
{
    LVL1,
    LVL2,
    LVL3,
    LVL4,
    LVL5
}

public enum CadenceDifficulty
{
    I_II_V,
    I_IV_V_VI,
    ALL /* LVL4, LVL5, LVL6 */
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}