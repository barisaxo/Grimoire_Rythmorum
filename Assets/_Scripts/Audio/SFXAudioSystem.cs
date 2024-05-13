using Audio;
using UnityEngine;
using Data.Two;

public sealed class SoundFXAudioSystem : AudioSystem
{
    public SoundFXAudioSystem(VolumeData volumeData) : base(2, nameof(SoundFXAudioSystem))
    {
        AudioSources[0].volume = volumeData.GetLevel(new SoundFX()) * .01f;
        AudioSources[1].volume = volumeData.GetLevel(new SoundFX()) * .01f;
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
        AudioSources[1].loop = false;
    }

    public void StopClip()
    {
        AudioSources[1].Stop();
    }
}