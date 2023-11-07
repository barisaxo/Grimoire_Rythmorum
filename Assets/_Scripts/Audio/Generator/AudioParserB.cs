using MusicTheory;
using UnityEngine;
using System.Collections;
using MusicTheory.Keys;
using Audio;

public class AudioParserB
{
    public AudioClip AC = Resources.Load<AudioClip>("Audio/2_octave_piano_whole_notes_60bpm 1");

    public AudioClip GetAudioClipFromKey(KeyboardNoteName key)
    {
        float chordNibStart = 4f * (int)key;

        int offset = Mathf.CeilToInt((float)(AC.frequency * chordNibStart));
        float[] samples = new float[AC.samples];
        AC.GetData(samples, 0);

        AudioClip newAC = AudioClip.Create(
           name: key.ToString(),
           lengthSamples: (int)(AC.frequency * 4f),
           channels: AC.channels,
           frequency: AC.frequency,
           stream: false);

        float[] newSamples = new float[newAC.samples * newAC.channels];

        for (int i = 0; i < newSamples.Length; i++) newSamples[i] = samples[i + offset];

        newAC.SetData(newSamples, 0);

        return newAC;
    }


}