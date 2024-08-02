using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Arithmetic;
using MusicTheory.Scales;
using MusicTheory.Keys;
using MusicTheory.Steps;
using MusicTheory.ScaleDegrees;

[System.Serializable]
public class ScalePuzzle : IPuzzle
{
    readonly int _numOfNotes;
    public int NumOfNotes => _numOfNotes;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.Horizontal;

    public bool PlayOnEngage => false;
    public bool AllowPlayQuestion => true;

    public IMusicalElement Gamut { get; private set; }
    public Scale Scale => Gamut is Scale scale ? scale : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>scale";

    private readonly string _question;
    public string Question => _question;

    public string Clue => GetSteps() + "\n" + GetScaleDegrees();

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

    private string GetScaleDegrees()
    {
        string temp = string.Empty;
        foreach (ScaleDegree s in Scale.ScaleDegrees) temp += s.Name + ' ';
        return temp;
    }

    private Scale WeightedRandomScale()
    {
        int solved = Datum.Manager.Io.Puzzles.GetLevel(this);

        List<Scale> scaleList = new() { new Major() };
        if (solved > 10) scaleList.Add(new Chromatic());
        if (solved > 15) scaleList.Add(new Pentatonic());
        if (solved > 25) scaleList.Add(new Blues());
        if (solved > 35) scaleList.Add(new JazzMinor());
        if (solved > 45) scaleList.Add(new HarmonicMinor());
        if (solved > 55) scaleList.Add(new WholeTone());
        if (solved > 65) scaleList.Add(new Diminished());
        if (solved > 75) scaleList.Add(new Diminished6th());

        return scaleList[Helpers.WeightedRandomInt(scaleList.Count)];
    }

}
