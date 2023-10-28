using MusicTheory;
using UnityEngine;

public struct ChordByte
{
    // public AudioClip AudioClip { get; private set; }
    public KeyOf RootNote { get; private set; }
    public DiatonicFunction DiatonicFunction { get; private set; }
    public DiatonicRomanNumeral DiatonicRomanNumeral { get; private set; }
    public ChordQuality ChordQuality { get; private set; }
    public KeyOf Key { get; private set; }
    public Genre Genre { get; private set; }
    public Instrument Axe { get; private set; }
    public float Tempo { get; private set; }
    public float StartPos { get; private set; }

    public ChordByte(
        float tempo,
        DiatonicFunction diatonicFunction,
        DiatonicRomanNumeral diatonicRomanNumeral,
        ChordQuality chordQuality,
        KeyOf key,
        Genre genre,
        Instrument axe)
    {
        DiatonicFunction = diatonicFunction;
        DiatonicRomanNumeral = diatonicRomanNumeral;
        ChordQuality = chordQuality;
        Key = key;
        Genre = genre;
        Axe = axe;
        Tempo = tempo;

        RootNote = DiatonicRomanNumeral.RootNote(Key);

        // AudioClip = new AudioParser(Genre, Tempo, ChordQuality, RootNote, Axe).AC;
        StartPos = (((float)chordQuality / (float)chordQuality.Count() / 12f) + ((float)RootNote / 12f));// * AudioClip.length;

        // AudioClip = Assets.GetAudioClip(genre, axe, (int)tempo);//new AudioParser(Genre, Tempo, Axe).AC;
        Debug.Log(chordQuality);
    }



}
