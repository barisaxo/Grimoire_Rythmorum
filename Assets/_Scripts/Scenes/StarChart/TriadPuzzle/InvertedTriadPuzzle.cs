using UnityEngine;
using MusicTheory.Notes.Arithmetic;
using MusicTheory.Notes;
using MusicTheory.Triads;
using MusicTheory.Triads.Arithmetic;
using MusicTheory.ScaleDegrees.Arithmetic;

[System.Serializable]
public class InvertedTriadPuzzle : IPuzzle
{
    public int NumOfNotes => 3;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Vertical;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;

    public bool AllowPlayQuestion => true;

    public System.Type GamutType => typeof(ITriad);
    public IMusicalElement Gamut { get; private set; }
    public ITriad Triad => Gamut is ITriad triad ? triad : throw new System.ArgumentOutOfRangeException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;
    readonly Inversion inversion;
    public string Desc => "Build the <b><i>inverted triad";
    public string puzzleGamut => "Inverted Triad";

    private readonly string _question;
    public string Question => _question;
    enum Inversion { first, second }
    public string Clue => GetChordTones(Triad, inversion);

    public InvertedTriadPuzzle()
    {
        Gamut = Random.Range(0, 4) switch
        {
            2 => new Minor(),
            3 => new Major(),
            1 => new Diminished(),
            0 => new Augmented(),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        inversion = Random.value > .5f ? Inversion.first : Inversion.second;

        INote Root = Enumeration.All<NoteEnum>()[Random.Range(0, Enumeration.Length<NoteEnum>())].GetNote();
        INote Third = Gamut switch
        {
            Major or Augmented => Root.GetNoteAbove(new MusicTheory.Intervals.M3()),
            _ => Root.GetNoteAbove(new MusicTheory.Intervals.mi3()),
        };
        INote Fifth = Gamut switch
        {
            Augmented => Root.GetNoteAbove(new MusicTheory.Intervals.A5()),
            Diminished => Root.GetNoteAbove(new MusicTheory.Intervals.d5()),
            _ => Root.GetNoteAbove(new MusicTheory.Intervals.P5()),
        };

        _notes = new KeyboardNoteName[NumOfNotes];

        Notes[0] = (inversion switch
        {
            Inversion.first => Third,
            _ => Fifth
        }).GetKeyboardNoteName();

        Notes[1] = (inversion switch
        {
            Inversion.first => Fifth,
            _ => Root
        }).GetKeyboardNoteName();

        Notes[2] = (inversion switch
        {
            Inversion.first => Root,
            _ => Third
        }).GetKeyboardNoteName();

        for (int i = 1; i < Notes.Length; i++) Notes[i] += Notes[i] < Notes[0] ? 12 : 0;

        _question = Triad.Description + " " + nameof(Triad) + InversionDescription(inversion);
    }

    string InversionDescription(Inversion inversion) => inversion switch
    {
        Inversion.first => " in first inversion",
        _ => " in second inversion"
    };

    string GetChordTones(ITriad chord, Inversion inversion)
    {
        string temp = "";
        for (int i = 0; i < 3; i++)
        {
            int invertedIndex = (i + (int)inversion + 1) % 3;
            if (invertedIndex == 0) temp += "1  ";
            else temp += chord.ChordTonesAsIntervals()[invertedIndex - 1].GetScaleDegree().Name + "  ";
        }
        return temp;
    }
}