using System.Collections;
using Audio;
using UnityEngine;
using Datum;

public sealed class BGMusic_AudioSystem : AudioSystem
{
    public BGMusic_AudioSystem(VolumeData data) : base(1, nameof(BGMusic_AudioSystem))
    {
        Loop = true;
        VolumeLevelSetting = data.GetScaledLevel(new BGMusic());
        foreach (var a in AudioSources) a.playOnAwake = true;

        foreach (var a in AudioSources)
            a.clip = Random.Range(1, 5) switch
            {
                2 => Assets.BGMus2,
                3 => Assets.BGMus3,
                4 => Assets.BGMus4,
                _ => Assets.BGMus1
            };
    }

    public void NextSong()
    {
        foreach (var a in AudioSources)
            a.clip = a.clip switch
            {
                _ when a.clip == Assets.BGMus1 => Random.value < .5f ? Assets.BGMus2 : Assets.BGMus3,
                _ when a.clip == Assets.BGMus2 => Random.value < .5f ? Assets.BGMus4 : Assets.BGMus3,
                _ when a.clip == Assets.BGMus3 => Random.value < .5f ? Assets.BGMus4 : Assets.BGMus1,
                _ when a.clip == Assets.BGMus4 => Random.value < .5f ? Assets.BGMus2 : Assets.BGMus1,
                _ => Assets.BGMus1
            };
    }

    public AudioClip GetClip => AudioSources[0].clip;

    public void SetClip(AudioClip clip)
    {
        foreach (var a in AudioSources)
            a.clip = clip;
    }

    public void Pause()
    {
        foreach (var a in AudioSources) a.Pause();
        // MonoHelper.Io.StartCoroutine(FadeOutAndPause());
        // IEnumerator FadeOutAndPause()
        // {
        //     yield return new WaitForEndOfFrame();
        //     if (CurrentVolumeLevel > .15f)
        //     {
        //         CurrentVolumeLevel -= Time.deltaTime * .75f;
        //         MonoHelper.Io.StartCoroutine(FadeOutAndPause());
        //     }
        //     else
        //     {
        //         CurrentVolumeLevel = 0;
        //         foreach (AudioSource a in AudioSources) a.Pause();
        //     }
        // }
    }

    public void PlayClip(AudioClip clip)
    {
        foreach (var a in AudioSources)
        {
            a.clip = clip;
            a.loop = true;
            a.Play();
        }
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



public sealed class BeatFishing_AudioSystem : AudioSystem
{
    public BeatFishing_AudioSystem(VolumeData data) : base(1, nameof(BeatFishing_AudioSystem))
    {
        Loop = true;
        VolumeLevelSetting = data.GetScaledLevel(new BatterieVolume());
    }


    public void Pause()
    {
        foreach (var a in AudioSources) a.Pause();
    }

    public void PlayClip(AudioClip clip)
    {
        foreach (var a in AudioSources)
        {
            a.clip = clip;
            a.loop = true;
            a.Play();
        }
    }

}