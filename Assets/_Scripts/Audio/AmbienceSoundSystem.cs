using Audio;
using UnityEngine;
using System.Collections;
using Data.Two;

public sealed class AmbienceAudioSystem : AudioSystem
{
    public AmbienceAudioSystem(VolumeData volumeData) : base(1, nameof(SoundFXAudioSystem))
    {
        AudioSources[0].volume = volumeData.GetLevel(new SoundFX()) * .01f;
    }

    public void PlayOneShot(AudioClip ac)
    {
        AudioSources[0].loop = false;
        AudioSources[0].clip = ac;
        AudioSources[0].Play();
    }

    public void PlayClip(AudioClip ac)
    {
        AudioSources[0].loop = true;
        AudioSources[0].clip = ac;
        AudioSources[0].time = 0;
        AudioSources[0].Play();
    }

    public void StopClip()
    {
        AudioSources[1].Stop();
    }

    public void Pause()
    {
        foreach (var a in AudioSources) a.Pause();
    }

    public void Resume()
    {
        CurrentVolumeLevel = CurrentVolumeLevel;
        foreach (var a in AudioSources)
            if (a.isPlaying)
                return;

        FadeInAndResume().StartCoroutine();

        IEnumerator FadeInAndResume()
        {
            yield return null;
            if (CurrentVolumeLevel < VolumeLevelSetting)
            {
                CurrentVolumeLevel += Time.deltaTime * 1.75f;
                FadeInAndResume().StartCoroutine();
            }
            else
            {
                CurrentVolumeLevel = VolumeLevelSetting;
                foreach (var a in AudioSources) a.UnPause();
            }
        }
    }
}