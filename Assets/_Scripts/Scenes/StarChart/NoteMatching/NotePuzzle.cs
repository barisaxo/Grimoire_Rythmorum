
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Notes;

[System.Serializable]
public class NotePuzzle : IPuzzle
{
    public int NumOfNotes => 1;

    public PlaybackMode QuestionPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode ListenPlaybackMode => PlaybackMode.Horizontal;
    public PlaybackMode AnswerPlaybackMode => PlaybackMode.Horizontal;

    public bool PlayOnEngage => false;
    public bool AllowPlayQuestion => true;

    public IMusicalElement Gamut { get; private set; }
    public INote Note => Gamut is INote note ? note : throw new System.ArgumentNullException();

    private readonly KeyboardNoteName[] _notes;
    public KeyboardNoteName[] Notes => _notes;

    public string Desc => "Find the <b><i>note";
    public string puzzleGamut => "Note";

    private readonly string _question;
    public string Question => _question;

    public string Clue => "\n\nD is always between the group with two black keys." +
        "\nThe notes of the keyboard: C_D_EF_G_A_BC_D_EF_G_A_BC" +
        "\nSharp (#) = +1.   Flat (b) = -1.";

    public NotePuzzle()
    {
        // Gamut = (Key)Enumeration.All<KeyEnum>()[Random.Range(0, Enumeration.Length<KeyEnum>())];
        Gamut = WeightedRandomNote();
        _notes = new KeyboardNoteName[NumOfNotes];
        Notes[0] = Note.GetKeyboardNoteName();

        _question = Note.Name;
    }

    private INote WeightedRandomNote()
    {
        int solved = Datum.Manager.Io.Puzzles.GetLevel(this);

        List<int> ints = new() { 1 };
        if (solved > 4) ints.Add(2);
        if (solved > 8) ints.Add(3);

        List<INote> KeyList;

        switch (ints[Random.Range(0, ints.Count)])
        {
            case 1:
                KeyList = new() { new C(), new D(), new E(), new F(), new G(), new A(), new B() };
                return KeyList[Random.Range(0, KeyList.Count)];

            case 2:
                KeyList = new() { new Ab(), new Bb(), new Db(), new Eb(), new Gb(),
                                  new As(), new Gs(), new Ds(), new Fs(), new Cs() };
                return KeyList[Random.Range(0, KeyList.Count)];

            case 3:
                KeyList = new() { new Bs(), new Cb(), new Es(), new Fb() };
                return KeyList[Random.Range(0, KeyList.Count)];
        }

        throw new System.Exception("?");
    }

}
