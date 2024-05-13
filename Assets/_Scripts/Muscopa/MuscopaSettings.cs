using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory;

namespace Muscopa
{
    public class MuscopaSettings
    {
        public KeyOf KeyOf { get; private set; }
        public Genre Genre { get; private set; }
        public MusicalScale Scale { get; private set; }
        public DiatonicRomanNumeral[] Cadence { get; private set; }
        public float[] StartTimes { get; private set; }
        public ChordByte[] Chords { get; private set; }
        public ChordByte[] Basses { get; private set; }
        public AudioClip Chord { get; private set; }
        public AudioClip Bass { get; private set; }
        public AudioClip[] Drums { get; private set; }
        public Extension Extension { get; private set; }
        public float Tempo { get; private set; }

        public MuscopaSettings(
            float tempo,
            KeyOf key,
            Genre genre,
            MusicalScale scale,
            Extension extension,
            DiatonicRomanNumeral[] cadence
            )
        {
            Tempo = tempo;
            KeyOf = key;
            Genre = genre;
            Scale = scale;
            Cadence = cadence;
            Extension = extension;

            Chords = GetChordBytes(Cadence, KeyOf, Genre, Extension, Instrument.Chords, Tempo);
            Basses = GetChordBytes(Cadence, KeyOf, Genre, Extension, Instrument.Bass, Tempo);
            StartTimes = new float[4]{
                Chords[0].StartPos,
                Chords[1].StartPos,
                Chords[2].StartPos,
                Chords[3].StartPos,
             };
        }

        ChordByte[] GetChordBytes(
            DiatonicRomanNumeral[] cadence,
            KeyOf key,
            Genre genre,
            Extension extension,
            Instrument axe,
            float tempo
            )
        {
            ChordByte[] newChordBytes = new ChordByte[cadence.Length];

            for (int i = 0; i < newChordBytes.Length; i++)
            {
                newChordBytes[i] = new ChordByte(
                    diatonicFunction: cadence[i].ToDiatonicFunction(),
                    diatonicRomanNumeral: cadence[i],
                    chordQuality: cadence[i].ToQuality(extension),
                    key: key,
                    genre: genre,
                    axe: axe,
                    tempo: tempo
                );
            }

            return newChordBytes;
        }
    }

    public static class MuscopaHelper
    {
        public static HarmonicFunction[] DiatonicToHarmonicFunctionCadence(this DiatonicRomanNumeral[] cadence)
        {
            var cad = new HarmonicFunction[cadence.Length];
            for (int i = 0; i < cad.Length; i++) cad[i] = cadence[i].ToHarmonicFunction();
            return cad;
        }
        public static HarmonicFunction ToHarmonicFunction(this DiatonicRomanNumeral rn)
        {
            return rn switch
            {
                DiatonicRomanNumeral.I or DiatonicRomanNumeral.III or DiatonicRomanNumeral.VI => HarmonicFunction.Tonic,
                DiatonicRomanNumeral.II or DiatonicRomanNumeral.IV => HarmonicFunction.Predominant,
                _ => HarmonicFunction.Dominant
            };
        }
    }
}





//             //https://docs.unity3d.com/Manual/webgl-audio.html#audioclip
//namespace Muscopa
//{
//    public class MuscopaSettings
//    {
//        public KeyOf KeyOf { get; private set; }
//        public Genre Genre { get; private set; }
//        public MusicalScale Scale { get; private set; }
//        public DiatonicRomanNumeral[] Cadence { get; private set; }
//        public float[] StartTimes { get; private set; }
//        public ChordByte[] Chords { get; private set; }
//        public ChordByte[] Basses { get; private set; }
//        public AudioClip Chord { get; private set; }
//        public AudioClip Bass { get; private set; }
//        public AudioClip[] Drums { get; private set; }
//        public Extension Extension { get; private set; }
//        public float Tempo { get; private set; }

//        public MuscopaSettings(
//            float tempo,
//            KeyOf key,
//            Genre genre,
//            MusicalScale scale,
//            Extension extension,
//            DiatonicRomanNumeral[] cadence
//            )
//        {
//            Tempo = tempo;
//            KeyOf = key;
//            Genre = genre;
//            Scale = scale;
//            Cadence = cadence;
//            Extension = extension;

//            Chords = GetChordBytes(Cadence, KeyOf, Genre, Extension, Instrument.Chords, Tempo);
//            Basses = GetChordBytes(Cadence, KeyOf, Genre, Extension, Instrument.Bass, Tempo);
//            StartTimes = new float[4]{
//                Chords[0].StartPos,
//                Chords[1].StartPos,
//                Chords[2].StartPos,
//                Chords[3].StartPos,
//             };
//            // ChordQuality[] CQs = new ChordQuality[4]{
//            //     Cadence[0].ToQuality(Extension),
//            //     Cadence[1].ToQuality(extension),
//            //     Cadence[2].ToQuality(extension),
//            //     Cadence[3].ToQuality(extension),
//            // };

//            // KeyOf[] RootNotes = new KeyOf[4]{
//            //    Cadence[0].RootNote(KeyOf),
//            //    Cadence[1].RootNote(KeyOf),
//            //    Cadence[2].RootNote(KeyOf),
//            //    Cadence[3].RootNote(KeyOf),
//            // };

//            // Chord = new AudioParser(Genre, Tempo, CQs, RootNotes, Instrument.Chords).AC;
//            // Bass = new AudioParser(Genre, Tempo, CQs, RootNotes, Instrument.Bass).AC;
//        }

//        ChordByte[] GetChordBytes(
//            DiatonicRomanNumeral[] cadence,
//            KeyOf key,
//            Genre genre,
//            Extension extension,
//            Instrument axe,
//            float tempo
//            )
//        {
//            ChordByte[] newChordBytes = new ChordByte[cadence.Length];

//            for (int i = 0; i < newChordBytes.Length; i++)
//            {
//                newChordBytes[i] = new ChordByte(
//                    diatonicFunction: cadence[i].ToDiatonicFunction(),
//                    diatonicRomanNumeral: cadence[i],
//                    chordQuality: cadence[i].ToQuality(extension),
//                    key: key,
//                    genre: genre,
//                    axe: axe,
//                    tempo: tempo
//                );
//            }

//            // GetBytes(i).StartCoroutine();

//            return newChordBytes;

//            // IEnumerator GetBytes(int f)
//            // {
//            //     for (int i = 0; i < newChordBytes.Length; i++)
//            //     {
//            //         newChordBytes[i] = new ChordByte(
//            //             diatonicFunction: cadence[i].ToDiatonicFunction(),
//            //             diatonicRomanNumeral: cadence[i],
//            //             chordQuality: cadence[i].ToQuality(extension),
//            //             key: key,
//            //             genre: genre,
//            //             axe: axe,
//            //             tempo: tempo
//            //         );
//            //         // while (newChordBytes[i].AudioClip.loadState != AudioDataLoadState.Loaded)
//            //         // {
//            //         //     Debug.Log("waiting" + f); yield return null;
//            //         // }
//            //     }

//            // }
//        }



//        // public class AudioParser
//        // {
//        //     public AudioParser(Genre genre, float bpm, ChordQuality[] cqs, KeyOf[] roots, Instrument axe) =>
//        //         ParseAC(genre, (int)bpm, cqs, roots, axe).StartCoroutine();

//        //     public AudioClip AC { get; private set; }

//        //     IEnumerator ParseAC(Genre genre, int tempo, ChordQuality[] CQs, KeyOf[] Roots, Instrument Axe)
//        //     {
//        //         var sourceAC = Assets.GetAudioClip(genre, Axe, tempo);
//        //         while (sourceAC.loadState != AudioDataLoadState.Loaded) { yield return null; }

//        //         float[] sourceSamples = new float[sourceAC.samples];
//        //         sourceAC.GetData(sourceSamples, 0);

//        //         AudioClip newAC = AudioClip.Create(nameof(newAC), (int)(sourceAC.frequency * (60f / tempo * 16f)), sourceAC.channels, sourceAC.frequency, false);

//        //         float[] chordNibs = new float[4];
//        //         for (int i = 0; i < 4; i++)
//        //         {
//        //             chordNibs[i] = ((float)CQs[i] / (float)CQs[i].Count() / 12f) + ((float)Roots[i] / 12f);
//        //         }

//        //         int[] offsets = new int[4];
//        //         for (int i = 0; i < 4; i++)
//        //         {
//        //             offsets[i] = Mathf.CeilToInt((float)((sourceAC.samples * sourceAC.channels) * chordNibs[i]));
//        //         }

//        //         int newSampleQuarterLength = (newAC.samples / 4) * newAC.channels;

//        //         float[] newSample = new float[newSampleQuarterLength * 4];

//        //         int sampleIndex = 0;
//        //         for (int i = 0; i < offsets.Length; i++)
//        //         {
//        //             //https://docs.unity3d.com/Manual/webgl-audio.html#audioclip
//        //             //On WebGL, Unity ignores the offsetSample parameter.
//        //             for (int ii = 0; ii < newSampleQuarterLength; ii++)
//        //             {
//        //                 newSample[sampleIndex] = sourceSamples[ii + offsets[i]];
//        //                 sampleIndex++;
//        //             }
//        //         }

//        //         newAC.SetData(newSample, 0);

//        //         AC = newAC;
//        //     }

//        // }
//    }
//}


