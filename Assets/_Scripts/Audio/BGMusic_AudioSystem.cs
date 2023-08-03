using System.Collections;
using Audio;
using UnityEngine;

public sealed class BGMusic_AudioSystem : AudioSystem
{
    public BGMusic_AudioSystem(VolumeData data) : base(1, nameof(BGMusic_AudioSystem))
    {
        Loop = true;
        VolumeLevelSetting = data.GetScaledLevel(VolumeData.DataItem.BGMusic);
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