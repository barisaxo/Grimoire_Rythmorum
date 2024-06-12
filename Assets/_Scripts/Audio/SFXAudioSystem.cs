using Audio;
using UnityEngine;
using Data;

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

    public void PlayClip(AudioClip ac, bool loop)
    {
        AudioSources[1].clip = ac;
        AudioSources[1].Play();
        AudioSources[1].loop = loop;
    }

    public void StopClip()
    {
        AudioSources[1].Stop();
    }

    public void RandomizeClipPlaybackPosition()
    {
        AudioSources[1].time = Random.Range(0f, AudioSources[1].clip.length);
    }
}