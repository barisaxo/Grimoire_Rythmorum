using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Arithmetic;
using MusicTheory.Scales;
using MusicTheory.Keys;
using MusicTheory.Steps;

public class ScalePuzzle : IPuzzle
{
    readonly int _numOfNotes;
    public int NumOfNotes => _numOfNotes;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.Horizontal;

    public bool PlayOnEngage => false;
    public bool AllowPlayQuestion => true;

    public System.Type GamutType => typeof(Scale);
    public IMusicalElement Gamut { get; private set; }
    public Scale Scale => Gamut is Scale scale ? scale : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>scale";

    private readonly string _question;
    public string Question => _question;

    public string Clue => GetSteps();

    public ScalePuzzle()
    {
        Gamut = WeightedRandomScale();
        _numOfNotes = Scale.ScaleDegrees.Length + 1;
        _notes = new KeyboardNoteName[NumOfNotes];

        KeyboardNoteName Root = ((Key)Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())]).GetKeyboardNoteName();

        Notes[0] = Root;
        Notes[^1] = Root + 12;

        for (int i = 1; i < Notes.Length - 1; i++)
        {
            Notes[i] = Root.NoteNameToKey().GetKeyAbove(Scale.ScaleDegrees[i].AsInterval()).GetKeyboardNoteName();
            Notes[i] += Notes[i] < Root ? 12 : 0;
            //Debug.Log(Notes[i].ToString());
        }

        _question = Scale.Description.StartCase() + " " + nameof(MusicTheory.Scales.Scale);
    }

    private string GetSteps()
    {
        string temp = string.Empty;
        foreach (Step s in Scale.Steps) temp += s.Name + ' ';
        return temp;
    }

    private Scale WeightedRandomScale()
    {
        return Random.Range(0, 53) switch
        {
            < 10 => new Major(),
            < 19 => new JazzMinor(),
            < 29 => new HarmonicMinor(),
            < 37 => new Pentatonic(),
            < 41 => new MusicTheory.Scales.Diminished(),
            < 45 => new Diminished6th(),
            < 48 => new MusicTheory.Scales.WholeTone(),
            < 51 => new Blues(),
            _ => new MusicTheory.Scales.Chromatic(),
        };
    }

}
