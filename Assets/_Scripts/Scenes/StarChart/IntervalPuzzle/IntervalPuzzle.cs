
using MusicTheory.Arithmetic;
using MusicTheory.Keys;
using MusicTheory.Intervals;
using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class IntervalPuzzle : IPuzzle
{
    public int NumOfNotes => 2;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.HorAndVert;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.HorAndVert;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.HorAndVert;

    public bool PlayOnEngage => false;
    public bool AllowPlayQuestion => true;

    public IMusicalElement Gamut { get; private set; }
    public Interval Interval => Gamut is Interval interval ? interval : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Build the <b><i>interval";
    public string puzzleType => "interval";

    private readonly string _question;
    public string Question => _question;
    public string Clue => "1 mi2 M2 mi3 M3 P4 d5 P5 mi6 M6 mi7 M7 P8";
    public IntervalPuzzle()
    {
        Gamut = WeightedRandomInterval();

        _notes = new KeyboardNoteName[NumOfNotes];

        Key Root = Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];

        Notes[0] = Root.GetKeyboardNoteName();

        Notes[1] = Root.GetKeyAbove(Interval).GetKeyboardNoteName();

        Notes[1] += Notes[1] <= Notes[0] ? 12 : 0;

        _question = Gamut.Name + " " + nameof(Interval);
    }

    private Interval WeightedRandomInterval()
    {
        int solved = Datum.Manager.Io.Puzzles.GetLevel(this);

        List<Interval> scaleList = new() { new mi2(), new M2() };
        if (solved > 1) { scaleList.Add(new mi3()); scaleList.Add(new M3()); }
        if (solved > 3) { scaleList.Add(new P4()); scaleList.Add(new P5()); }
        if (solved > 5) { scaleList.Add(new d4()); scaleList.Add(new P8()); }
        if (solved > 7) { scaleList.Add(new mi6()); scaleList.Add(new M6()); }
        if (solved > 9) { scaleList.Add(new mi7()); scaleList.Add(new M7()); }
        return scaleList[Helpers.WeightedRandomInt(scaleList.Count)];
    }
}

[System.Serializable]
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
    public string puzzleType => "inverted interval";

    private readonly string _question;
    public string Question => _question;
    public string Clue => "1:8, 2:7, 3:6, 4:5, M:mi, d:a, P:P";
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

        Notes[1] += Notes[1] <= Notes[0] ? 12 : 0;

        _question = Gamut.Name + " " + nameof(Interval);
    }

}