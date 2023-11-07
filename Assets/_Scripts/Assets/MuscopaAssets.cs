using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Musica;

public static class MuscopaAssets
{
    public static AudioClip GetAudioClip(Genre genre, Instrument axe, int tempo)
    {
        return Resources.Load<AudioClip>(FileName());

        string FileName() =>
             "Audio/" + AxeFolder() + genre.ToString() + "_" + axe.ToString() + "_" + tempo.ToString();

        string AxeFolder() => axe switch
        {
            Instrument.Bass => "Bass/",
            Instrument.Chords => "Chords/",
            _ => "Drums/",
        };

        // string GenreFolder() => genre switch
        // {
        //     Genre.Shuffle => "Shuffle/",
        //     _ => "",
        // };


    }


    public static AudioClip GetDrumAC(Genre genre, int tempo, int index)
    {
        return Resources.Load<AudioClip>(FileName());
        string FileName() => "Audio/Drums/" + genre.ToString() + "_Drums_" + tempo + "_" + index;
    }


}