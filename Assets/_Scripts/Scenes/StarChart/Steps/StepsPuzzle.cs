
using MusicTheory.Notes.Arithmetic;
using MusicTheory.Notes;
using MusicTheory.Steps;
using MusicTheory.Intervals.Arithmetic;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class StepsPuzzle : IPuzzle
{
    public int NumOfNotes => 2;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;

    public bool PlayOnEngage => false;
    public bool AllowPlayQuestion => true;

    public IMusicalElement Gamut { get; private set; }
    public IStep Step => Gamut is IStep step ? step : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>step";
    public string puzzleGamut => "step";

    private readonly string _question;
    public string Question => _question;
    public string Clue => "Half = +1, Whole = +2, Skip = +3";
    public StepsPuzzle()
    {
        Gamut = WeightedRandomStep();

        _notes = new KeyboardNoteName[NumOfNotes];

        INote Root = Enumeration.All<NoteEnum>()[Random.Range(0, Enumeration.Length<NoteEnum>())].GetNote();

        Notes[0] = Root.GetKeyboardNoteName();

        Notes[1] = Root.GetNoteAbove(Step.AsInterval()).GetKeyboardNoteName();

        Notes[1] += Notes[1] < Notes[0] ? 12 : 0;

        _question = Gamut.Name + (Gamut.Name == nameof(Skip) ? "" : " " + nameof(Step));
    }

    private IStep WeightedRandomStep()
    {
        int solved = Datum.Manager.Io.Puzzles.GetLevel(this);

        List<int> ints = new() { 1 };
        if (solved > 5) ints.Add(2);

        return ints[Random.Range(0, ints.Count)] switch
        {
            1 => Random.value > .5f ? new Half() : new Whole(),
            2 => Random.value < .333f ? new Half() : Random.value < .5f ? new Whole() : new Skip(),
            _ => throw new System.Exception("?"),
        };
    }

}