using Audio;
using UnityEngine;
using System.Collections;

public sealed class AmbienceAudioSystem : AudioSystem
{
    public AmbienceAudioSystem(VolumeData volumeData) : base(2, nameof(SoundFXAudioSystem))
    {
        AudioSources[0].volume = volumeData.GetScaledLevel(VolumeData.DataItem.SoundFX);
        AudioSources[1].volume = volumeData.GetScaledLevel(VolumeData.DataItem.SoundFX);
    }

    public void PlayOneShot(AudioClip ac)
    {
        AudioSources[0].clip = ac;
        AudioSources[0].Play();
    }

    public void PlayClip(AudioClip ac)
    {
        AudioSources[1].clip = ac;
        AudioSources[1].Play();
        AudioSources[1].loop = true;
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