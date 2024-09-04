using UnityEngine;
using MusicTheory.Notes.Arithmetic;
using MusicTheory.Notes;
using MusicTheory.Triads;
using MusicTheory.Triads.Arithmetic;

[System.Serializable]
public class TriadPuzzle : IPuzzle
{
    public int NumOfNotes => 3;

    public PlaybackMode ListenPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;
    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Vertical;

    public bool AllowPlayQuestion => true;

    public System.Type GamutType => typeof(ITriad);
    public IMusicalElement Gamut { get; private set; }
    public ITriad Triad => Gamut is ITriad triad ? triad : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>triad";
    public string puzzleGamut => "Triad";

    private readonly string _question;
    public string Question => _question;
    public string Clue => GetChordTones(Triad);

    public TriadPuzzle()
    {
        Gamut = Enumeration.All<TriadEnum>()[Random.Range(0, Enumeration.Length<TriadEnum>())].GetTriad();

        _notes = new KeyboardNoteName[NumOfNotes];

        INote Root = Enumeration.All<NoteEnum>()[Random.Range(0, Enumeration.Length<NoteEnum>())].GetNote();

        Notes[0] = Root.GetKeyboardNoteName();
        Notes[1] = Root.GetNoteAbove(Triad.ChordTonesAsIntervals()[0]).GetKeyboardNoteName();
        Notes[2] = Root.GetNoteAbove(Triad.ChordTonesAsIntervals()[1]).GetKeyboardNoteName();

        for (int i = 1; i < Notes.Length; i++) Notes[i] += Notes[i] < Notes[0] ? 12 : 0;

        _question = Triad.Description + " " + nameof(Triad);
    }

    private string GetChordTones(ITriad chord)
    {
        string temp = "Root ";
        foreach (MusicTheory.Intervals.IInterval i in chord.ChordTonesAsIntervals())
            temp += i.Name + " ";
        return temp;
    }
}