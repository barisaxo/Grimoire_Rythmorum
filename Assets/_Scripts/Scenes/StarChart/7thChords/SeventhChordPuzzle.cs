
using UnityEngine;
using MusicTheory.Notes.Arithmetic;
using MusicTheory.Notes;
using MusicTheory.SeventhChords;
using MusicTheory.SeventhChords.Arithmetic;

[System.Serializable]
public class SeventhChordPuzzle : IPuzzle
{
    public int NumOfNotes => 4;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;

    public bool AllowPlayQuestion => true;

    // public System.Type GamutType => typeof(ISeventhChord);
    public IMusicalElement Gamut { get; private set; }
    public ISeventhChord SeventhChord => Gamut is ISeventhChord chord ? chord : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>seventh chord";
    public string puzzleGamut => "Seventh Chord";

    private readonly string _question;
    public string Question => _question;
    public string Clue => GetChordTones(SeventhChord);

    public SeventhChordPuzzle()
    {
        Gamut = Enumeration.All<SeventhChordEnum>()[Random.Range(0, Enumeration.Length<SeventhChordEnum>())].GetSeventhChord();

        _notes = new KeyboardNoteName[NumOfNotes];

        INote Root = Enumeration.All<NoteEnum>()[Random.Range(0, Enumeration.Length<NoteEnum>())].GetNote();

        Notes[0] = Root.GetKeyboardNoteName();
        Notes[1] = Root.GetNoteAbove(SeventhChord.ChordTonesAsIntervals()[0]).GetKeyboardNoteName();
        Notes[2] = Root.GetNoteAbove(SeventhChord.ChordTonesAsIntervals()[1]).GetKeyboardNoteName();
        Notes[3] = Root.GetNoteAbove(SeventhChord.ChordTonesAsIntervals()[2]).GetKeyboardNoteName();

        for (int i = 1; i < Notes.Length; i++) Notes[i] += Notes[i] < Notes[0] ? 12 : 0;

        _question = SeventhChord.Description.StartCase() + " " + Gamut.Name;
    }

    private string GetChordTones(ISeventhChord seventhChord)
    {
        string temp = "Root ";
        foreach (MusicTheory.Intervals.IInterval i in seventhChord.ChordTonesAsIntervals())
            temp += i.Name + " ";
        return temp;
    }


}
