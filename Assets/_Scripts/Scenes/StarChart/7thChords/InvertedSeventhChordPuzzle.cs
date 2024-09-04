
using UnityEngine;
using MusicTheory.Notes.Arithmetic;
using MusicTheory.Notes;
using MusicTheory.SeventhChords;
using MusicTheory.SeventhChords.Arithmetic;
using MusicTheory.ScaleDegrees.Arithmetic;

[System.Serializable]
public class InvertedSeventhChordPuzzle : IPuzzle
{
    public int NumOfNotes => 4;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;

    public bool AllowPlayQuestion => true;

    // public System.Type GamutType => typeof(ISeventhChord);
    public IMusicalElement Gamut { get; private set; }
    public ISeventhChord ISeventhChord => Gamut is ISeventhChord chord ? chord : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>inverted seventh chord";
    public string puzzleGamut => "Inverted Seventh Chord";
    Inversion inversion;
    private readonly string _question;
    public string Question => _question;
    public string Clue => GetChordTones(ISeventhChord, inversion);

    enum Inversion { First, Second, Third }

    public InvertedSeventhChordPuzzle()
    {
        inversion = (Inversion)Random.Range(0, 3);

        Gamut = Enumeration.All<SeventhChordEnum>()[Random.Range(0, Enumeration.Length<SeventhChordEnum>())].GetSeventhChord();

        _notes = new KeyboardNoteName[NumOfNotes];
        INote Root = Enumeration.All<NoteEnum>()[Random.Range(0, Enumeration.Length<NoteEnum>())].GetNote();
        INote[] keys = new INote[4] {
            Root,
            Root.GetNoteAbove(ISeventhChord.ChordTonesAsIntervals()[0]),
            Root.GetNoteAbove(ISeventhChord.ChordTonesAsIntervals()[1]),
            Root.GetNoteAbove(ISeventhChord.ChordTonesAsIntervals()[2])
        };

        for (int i = 0; i < Notes.Length; i++) Notes[i] = keys[(i + (int)inversion + 1) % 4].GetKeyboardNoteName();

        for (int i = 1; i < Notes.Length; i++) Notes[i] += Notes[i] < Notes[0] ? 12 : 0;

        _question = ISeventhChord.Description.StartCase() + InversionDescription(inversion);
    }

    string InversionDescription(Inversion inversion) => inversion switch
    {
        Inversion.First => " chord in first inversion",
        Inversion.Second => " chord in second inversion",
        _ => " chord in third inversion"
    };

    string GetChordTones(ISeventhChord chord, Inversion inversion)
    {
        string temp = string.Empty;
        for (int i = 0; i < 4; i++)
        {
            int invertedIndex = (i + (int)inversion + 1) % 4;
            if (invertedIndex == 0) temp += "1  ";
            else temp += chord.ChordTonesAsIntervals()[invertedIndex - 1].GetScaleDegree().Name + "  ";
        }
        return temp;
    }
}
