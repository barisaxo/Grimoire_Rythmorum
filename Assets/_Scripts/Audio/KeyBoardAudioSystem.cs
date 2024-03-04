using Audio;
using UnityEngine;
using Data.Options;
public sealed class KeyboardAudioSystem : AudioSystem
{
    private int _audioSourceIndex = 0;
    private int AudioSourceIndex => _audioSourceIndex = (_audioSourceIndex + 1) % 25;

    public KeyboardAudioSystem(VolumeData volumeData) : base(25, nameof(KeyboardAudioSystem))
    {
        foreach (var a in AudioSources)
            a.volume = volumeData.GetLevel(VolumeData.DataItem.SoundFX) * .01f;
    }

    public void Play(AudioClip ac) => Play(new AudioClip[] { ac });

    public void Play(AudioClip[] acs)
    {
        for (int i = 0; i < acs.Length; i++)
        {
            int index = AudioSourceIndex;
            AudioSources[index].clip = acs[i];
            AudioSources[index].Play();
        }
    }

    // public void PlayNotes(AudioClip[] acs, Action callback, PlaybackMode mode)
    // {
    //     for (int i = 0; i < acs.Length; i++)
    //     {
    //         AudioSources[AudioSourceIndex].clip = acs[i];
    //         AudioSources[AudioSourceIndex].Play();
    //     }

    // switch (mode)
    // {
    //     case PlaybackMode.Horizontal: PlayHorizontal(acs, callback); break;
    //     case PlaybackMode.Vertical: PlayVertical(acs, callback); break;
    //     case PlaybackMode.HorAndVert: PlayHorAndVert(acs, callback); break;
    // }
    // }

    // public void PlayVertical(AudioClip[] acs, Action callback)
    // {

    //     Timer().StartCoroutine();

    //     IEnumerator Timer()
    //     {
    //         for (int i = 0; i < acs.Length; i++)
    //         {
    //             AudioSources[AudioSourceIndex].clip = acs[i];
    //             AudioSources[AudioSourceIndex].Play();
    //         }
    //         yield return new WaitForSeconds(2);
    //         callback?.Invoke();
    //     }
    // }

    // // public void PlayHorizontal(AudioClip[] acs, Action callback)
    // // {
    // //     Timer().StartCoroutine();

    // //     IEnumerator Timer()
    // //     {
    // //         for (int i = 0; i < acs.Length; i++)
    // //         {
    // //             AudioSources[i].clip = acs[i];
    // //             AudioSources[i].Play();
    // //             float timer = 0;
    // //             while (timer < .35f)
    // //             {
    // //                 yield return null;
    // //                 timer += Time.deltaTime;
    // //             }
    // //         }
    // //         yield return new WaitForSeconds(2);
    // //         callback?.Invoke();
    // //     }
    // // }

    // public void PlayHorAndVert(AudioClip[] acs, Action callback)
    // {
    //     Delay().StartCoroutine();

    //     IEnumerator Delay()
    //     {
    //         for (int i = 0; i < acs.Length; i++)
    //         {
    //             AudioSources[i].clip = acs[i];
    //             AudioSources[i].Play();

    //             float timer = 0;
    //             while (timer < .35f)
    //             {
    //                 yield return null;
    //                 timer += Time.deltaTime;
    //             }
    //         }
    //         yield return new WaitForSeconds(.15f);
    //         PlayVertical(acs, callback);
    //     }
    // }

    // public void PlayNote(AudioClip ac)
    // {
    //     AudioSources[0].clip = ac;
    //     AudioSources[0].Play();
    // }

    // public void PlayInterval(AudioClip ac1, AudioClip ac2)
    // {
    //     AudioSources[0].clip = ac1;
    //     AudioSources[0].Play();
    //     AudioSources[1].clip = ac2;
    //     AudioSources[1].Play();
    // }

    // public void PlayChord(AudioClip ac1, AudioClip ac2, AudioClip ac3)
    // {
    //     AudioSources[0].clip = ac1;
    //     AudioSources[0].Play();
    //     AudioSources[1].clip = ac2;
    //     AudioSources[1].Play();
    //     AudioSources[2].clip = ac3;
    //     AudioSources[2].Play();
    // }

    // public void PlaySeventhChord(AudioClip ac1, AudioClip ac2, AudioClip ac3, AudioClip ac4)
    // {
    //     AudioSources[0].clip = ac1;
    //     AudioSources[0].Play();
    //     AudioSources[1].clip = ac2;
    //     AudioSources[1].Play();
    //     AudioSources[2].clip = ac3;
    //     AudioSources[2].Play();
    //     AudioSources[3].clip = ac4;
    //     AudioSources[3].Play();
    // }

    public override void Stop()
    {
        foreach (var a in AudioSources) a.Stop();
    }
}
