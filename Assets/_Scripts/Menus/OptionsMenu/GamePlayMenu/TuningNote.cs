using UnityEngine;
using Musica;
using Audio;

public class TuningNote
{
    public TuningNote(KeyOf note)
    {
        WaveGenerator.CreateSineWave(NoteToHertz(note.Transposed(KeyOf.Eb)), 5);

        AS.clip = WaveGenerator.CreateSineWave(NoteToHertz(note.Transposed(KeyOf.Eb)), 5);
        AS.loop = true;
        AS.Play();
    }

    private AudioSource _as;
    private AudioSource AS => _as != null ? _as : _as = SetUpAS();
    private AudioSource SetUpAS()
    {
        GameObject go = new(nameof(AudioSource));
        AudioSource a = go.AddComponent<AudioSource>();
        return a;
    }

    public void Stop()
    {
        AS.Stop();
        GameObject.Destroy(_as.gameObject);
    }

    private int NoteToHertz(KeyOf note) => note switch
    {
        KeyOf.Bb => 233,
        KeyOf.B => 247,
        KeyOf.C => 262,
        KeyOf.Db => 277,
        KeyOf.D => 294,
        KeyOf.Eb => 311,
        KeyOf.E => 330,
        KeyOf.F => 349,
        KeyOf.Gb => 370,
        KeyOf.G => 392,
        KeyOf.Ab => 415,
        _ => 440,
    };

}
