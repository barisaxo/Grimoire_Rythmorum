using UnityEngine;
using Audio;
using Data.Options;

namespace Muscopa
{
    public class MuscopaAudio
    {
        public MuscopaAudio(VolumeData vd)
        {
            VolumeData = vd;

            MuscopaChords = new();
            MuscopaBass = new();
            MuscopaDrums = new();
        }

        readonly VolumeData VolumeData;

        public readonly ChordsAudio MuscopaChords;
        public readonly BassAudio MuscopaBass;
        public readonly DrumsAudio MuscopaDrums;

        public void LoadNewMuscopaSettings(MuscopaPuzzle_AudioManager_Settings newSettings)
        {
            MuscopaChords.VolumeLevelSetting = VolumeData.GetLevel(VolumeData.DataItem.Chords) * .01f;
            MuscopaChords.AudioClipSettings = new AudioClipSettings()
            {
                StartTimes = newSettings.StartTimes,
                AudioClips = newSettings.ChordClips,
                BeatsPerAudioClip = newSettings.CountsPerClipChords,
                BPM = newSettings.BPM,
            };

            MuscopaBass.VolumeLevelSetting = VolumeData.GetScaledLevel(VolumeData.DataItem.Bass);
            MuscopaBass.AudioClipSettings = new AudioClipSettings()
            {
                StartTimes = newSettings.StartTimes,
                AudioClips = newSettings.BassClips,
                BeatsPerAudioClip = newSettings.CountsPerClipBass,
                BPM = newSettings.BPM,
            };
            MuscopaDrums.VolumeLevelSetting = VolumeData.GetScaledLevel(VolumeData.DataItem.Drums);
            MuscopaDrums.AudioClipSettings = new AudioClipSettings()
            {
                AudioClips = newSettings.DrumClips,
                BeatsPerAudioClip = newSettings.CountsPerClipDrums,
                BPM = newSettings.BPM,
                StartTimes = new float[2] { 0, 0 },
            };
        }

        public void PlayNewMuscopaPuzzleMusic()
        {
            Debug.Log("Playing");
            MuscopaChords.Play(true);
            MuscopaBass.Play(true);
            MuscopaDrums.Play(true);
        }

        public void StopTheCadence()
        {
            MuscopaChords.Stop();
            MuscopaBass.Stop();
            MuscopaDrums.Stop();
        }

    }

    public struct MuscopaPuzzle_AudioManager_Settings
    {
        public int BPM;
        public int CountsPerClipChords;
        public int CountsPerClipBass;
        public int CountsPerClipDrums;
        public float[] StartTimes;
        public AudioClip[] ChordClips;
        public AudioClip[] BassClips;
        public AudioClip[] DrumClips;
    }
}