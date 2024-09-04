using MusicTheory;
using UnityEngine;
using MusicTheory.RomanNumerals.Diatonic;
using MusicTheory.Functions.Diatonic;
using MusicTheory.Notes;
using MusicTheory.Notes.Arithmetic;
using MusicTheory.Triads;

public struct ChordByte
{
    // public AudioClip AudioClip { get; private set; }
    public INote RootNote { get; private set; }
    public IFunction DiatonicFunction { get; private set; }
    public IRomanNumeral DiatonicRomanNumeral { get; private set; }
    // public ITriad ChordQuality { get; private set; }
    public INote KeyCenter { get; private set; }
    public Genre Genre { get; private set; }
    public Instrument Axe { get; private set; }
    public float Tempo { get; private set; }
    public float StartPos { get; private set; }

    public ChordByte(
        float tempo,
        IFunction diatonicFunction,
        IRomanNumeral diatonicRomanNumeral,
        INote keyCenter,
        Genre genre,
        Instrument axe)
    {
        DiatonicFunction = diatonicFunction;
        DiatonicRomanNumeral = diatonicRomanNumeral;
        KeyCenter = keyCenter;
        Genre = genre;
        Axe = axe;
        Tempo = tempo;

        RootNote = DiatonicRomanNumeral.GetRootNote(KeyCenter);

        int chordSpot = diatonicRomanNumeral switch
        {
            I or IV => 0,
            II or III or VI => 1,
            V => 2,
            VII => 3,
            _ => throw new System.Exception(diatonicRomanNumeral.Name)
        };
        int count = 4;//todo this is how many chord qualities are in the recording, this needs to be better!
        StartPos = ((float)chordSpot / (float)count / 12f) + (float)((float)RootNote.Id / 12f);// * AudioClip.length;

        // AudioClip = Assets.GetAudioClip(genre, axe, (int)tempo);//new AudioParser(Genre, Tempo, Axe).AC;
        // Debug.Log(chordQuality);
    }



}
