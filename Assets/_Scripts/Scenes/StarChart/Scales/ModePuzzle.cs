using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Notes.Arithmetic;
using MusicTheory.Intervals.Arithmetic;
using MusicTheory.Scales;
using MusicTheory.Notes;
using MusicTheory.Modes;

[System.Serializable]
public class ModePuzzle : IPuzzle
{
    private int _numOfNotes;
    public int NumOfNotes => _numOfNotes;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.Horizontal;

    public bool PlayOnEngage => false;
    public bool AllowPlayQuestion => true;

    public IMusicalElement Gamut { get; private set; }
    public IScale Scale => Gamut is IScale scale ? scale : throw new System.ArgumentNullException();

    private KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>mode";
    public string puzzleGamut => "Mode";

    private string _question;
    public string Question => _question;

    public string Clue => GetSteps(Mode);
    readonly IMode Mode;

    public ModePuzzle(IScale gamut, IMode mode)
    {
        Gamut = gamut;
        Mode = mode;
        Initialize();
    }

    public ModePuzzle()
    {
        Gamut = WeightedRandomScale();
        Mode = Scale.Modes[Random.Range(0, Scale.Modes.Length)];
        Initialize();
    }

    void Initialize()
    {
        _numOfNotes = Scale.ScaleDegrees.Length + 1;
        _notes = new KeyboardNoteName[NumOfNotes];

        KeyboardNoteName Root = Enumeration.All<NoteEnum>()[Random.Range(0, Enumeration.Length<NoteEnum>())].GetNote().GetKeyboardNoteName();

        Notes[0] = Root;
        Notes[^1] = Root + 12;

        for (int i = 1; i < Notes.Length - 1; i++)
        {
            Notes[i] = Root.KeyboardKeyToNote().GetNoteAbove(Scale.ScaleDegrees[i].AsInterval()).GetKeyboardNoteName();
            Notes[i] += Notes[i] < Root ? 12 : 0;
        }

        for (int i = 0; i < Notes.Length - 1; i++)
        {
            int modalIndex = (Mode.Enum.Id + i) % Scale.ScaleDegrees.Length;
            Notes[i] = Root.KeyboardKeyToNote().GetNoteAbove(Scale.ScaleDegrees[modalIndex].AsInterval()).GetKeyboardNoteName();
        }

        for (int i = 0; i < Notes.Length - 1; i++)
        {
            Notes[i] += Notes[i] < Notes[0] ? 12 : 0;
        }

        Notes[^1] = Notes[0] + 12;

        _question = GetMajorModeName(Mode) + Mode.Enum.Name + ' ' + nameof(Mode) + " of the " +
            Scale.Description.StartCase() + ' ' + nameof(Scale);
    }

    private string GetMajorModeName(IMode mode)
    {
        return Scale is Major ? mode.Name + ": " : string.Empty;
    }

    private string GetSteps(IMode mode)
    {
        string temp = string.Empty;
        for (int i = 0; i < Scale.Steps.Length; i++) temp += Scale.Steps[(mode.Enum.Id + i) % Scale.ScaleDegrees.Length].Name + ' ';
        return temp;
    }

    private IScale WeightedRandomScale()
    {
        return Random.Range(0, 47) switch
        {
            < 10 => new Major(),
            < 19 => new JazzMinor(),
            < 29 => new HarmonicMinor(),
            < 37 => new Pentatonic(),
            < 42 => new MusicTheory.Scales.Blues(),
            _ => new MusicTheory.Scales.Diminished(),
        };
    }

}
