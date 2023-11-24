using MusicTheory;
using UnityEngine;
using System.Collections;

public class AudioParser
{
    public AudioParser(Genre genre, float bpm, Instrument axe) => GetAC(genre, (int)bpm, axe).StartCoroutine();
    // ParseAC(genre, (int)bpm, cq, key, axe).StartCoroutine();
    public AudioClip AC { get; private set; }

    IEnumerator GetAC(Genre genre, float bpm, Instrument axe)
    {
        var ac = MuscopaAssets.GetAudioClip(genre, axe, (int)bpm);

        while (ac.loadState != AudioDataLoadState.Loaded)
            yield return null;

        AC = ac;
    }

    //IEnumerator ParseAC(Genre genre, int tempo, ChordQuality CQ, KeyOf Root, Instrument Axe)
    //{
    //    var ac = MuscopaAssets.GetAudioClip(genre, Axe, tempo);
    //    while (ac.loadState != AudioDataLoadState.Loaded)
    //    {
    //        yield return null;
    //    }

    //    float[] samples = new float[ac.samples];
    //    float chordNibStart = ((float)CQ / (float)CQ.Count() / 12f) + ((float)Root / 12f);
    //    Debug.Log(chordNibStart + ", " + tempo + " " + Root + CQ);
    //    int offset = Mathf.CeilToInt((float)((ac.samples * ac.channels) * chordNibStart));

    //    AudioClip newAC = AudioClip.Create(
    //        Root.ToString() + CQ.ToString(),
    //        (int)(ac.frequency * (60f / tempo * 4f)),
    //        ac.channels,
    //        ac.frequency,
    //        false);

    //    float[] newSamples = new float[newAC.samples * newAC.channels];

    //    ac.GetData(samples, 0);

    //    for (int i = 0; i < newSamples.Length; i++)
    //    {
    //        //https://docs.unity3d.com/Manual/webgl-audio.html#audioclip
    //        //On WebGL, Unity ignores the offsetSample parameter.
    //        newSamples[i] = samples[i + offset];
    //    }

    //    newAC.SetData(newSamples, 0);

    //    AC = newAC;
    //}
}