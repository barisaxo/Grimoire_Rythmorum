
using MusicTheory.Arithmetic;
using MusicTheory.Keys;
using MusicTheory.Intervals;
using UnityEngine;


public class IntervalPuzzle : IPuzzle
{
    public int NumOfNotes => 2;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.HorAndVert;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.HorAndVert;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;

    public bool PlayOnEngage => false;
    public bool AllowPlayQuestion => true;

    public System.Type GamutType => typeof(Interval);
    public IMusicalElement Gamut { get; private set; }
    public Interval Interval => Gamut is Interval interval ? interval : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>interval";

    private readonly string _question;
    public string Question => _question;
    public string Clue => "1 m2 2 m3 3 p4 d5 p5 m6 6 m7 7 p8";
    public IntervalPuzzle()
    {
        Gamut = Random.Range(0, 12) switch
        {
            0 => new mi2(),
            1 => new M2(),
            2 => new mi3(),
            3 => new M3(),
            4 => new P4(),
            5 => new d5(),
            6 => new P5(),
            7 => new mi6(),
            8 => new M6(),
            9 => new mi7(),
            10 => new M7(),
            11 => new P8(),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        _notes = new KeyboardNoteName[NumOfNotes];

        Key Root = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];

        Notes[0] = Root.GetKeyboardNoteName();

        Notes[1] = Root.GetKeyAbove(Interval).GetKeyboardNoteName();

        Notes[1] += Notes[1] < Notes[0] ? 12 : 0;

        _question = Gamut.Name + " " + nameof(Interval);
    }

}

public class InvertedIntervalPuzzle : IPuzzle
{
    public int NumOfNotes => 2;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.HorAndVert;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.HorAndVert;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;

    public bool PlayOnEngage => false;
    public bool AllowPlayQuestion => true;

    public System.Type GamutType => typeof(Interval);
    public IMusicalElement Gamut { get; private set; }
    public Interval Interval => Gamut is Interval interval ? interval : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Invert the <b><i>interval";

    private readonly string _question;
    public string Question => _question;
    public string Clue => "1:8, 2:7, 3:6, 4:5, M:m, d:a, p:p";
    public InvertedIntervalPuzzle()
    {
        Gamut = Random.Range(0, 12) switch
        {
            0 => new mi2(),
            1 => new M2(),
            2 => new mi3(),
            3 => new M3(),
            4 => new P4(),
            5 => new d5(),
            6 => new P5(),
            7 => new mi6(),
            8 => new M6(),
            9 => new mi7(),
            10 => new M7(),
            11 => new P8(),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        _notes = new KeyboardNoteName[NumOfNotes];

        Key Root = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];

        Notes[0] = Root.GetKeyboardNoteName();

        Notes[1] = Root.GetKeyAbove(Interval.Invert()).GetKeyboardNoteName();

        Notes[1] += Notes[1] < Notes[0] ? 12 : 0;

        _question = Gamut.Name + " " + nameof(Interval);
    }

}