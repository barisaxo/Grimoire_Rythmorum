using System;
using UnityEngine;
using MusicTheory.Rhythms;

public class Puzzle_State : State
{
    private Keyboard Keyboard;
    private readonly IPuzzle Puzzle;
    private readonly AudioParserB AudioParser = new();
    private readonly PuzzleType PuzzleType;
    private bool ReadyForEndPuzzle = false;

    private int WrongAnswers;
    private int HintsUsed;
    private int Skipped;
    private int Failed;
    private int Solved;
    private int Errors;

    private KeyboardKey CaretKey;
    private readonly CaretBlink CaretBlink = new CaretBlink().Start();

    public Puzzle_State(IPuzzle puzzle, PuzzleType puzzleType)
    {
        Puzzle = puzzle;
        PuzzleType = puzzleType;
    }

    protected override void PrepareState(Action callback)
    {
        Keyboard = Puzzle.NumOfNotes == 1 ? new(Puzzle.NumOfNotes) :
            new(Puzzle.NumOfNotes, Puzzle.Notes[0]);

        CaretKey = Keyboard.SelectedKeys[0] ?? Keyboard.Keys[12];
        CaretBlink.SetCaret(CaretKey.SR);

        //Data.TheoryPuzzleData.ResetHints();
        _ = Question;
        _ = Desc;
        _ = Hint;
        _ = Listen;
        _ = SubmitAnswer;
        _ = Skip;
        base.PrepareState(callback);
    }

    protected override void PreEngageState(Action callback)
    {
        Listen.GO.SetActive(false);
        SubmitAnswer.GO.SetActive(false);
        Skip.GO.SetActive(false);
        Hint.GO.SetActive(false);
        base.PreEngageState(callback);
    }

    protected override void EngageState()
    {
        if (PuzzleType == PuzzleType.Aural)
            Keyboard.PlayNotes(Puzzle.Notes, EnableButtons, Puzzle.QuestionPlaybackMode, KeyboardInteractionType.Puzzle);
        else EnableButtons();
    }

    void EnableButtons()
    {
        Skip.GO.SetActive(true);
        Hint.GO.SetActive(true);
    }

    protected override void DisengageState()
    {
        Data.TheoryPuzzleData.AddStat(
            new PuzzleStat(
                specs: new PuzzleSpec(PuzzleType, Puzzle),
                hints: HintsUsed,
                wrong: WrongAnswers,
                fail: Failed,
                solve: Solved,
                skipped: Skipped,
                errors: Errors));

        Audio.KBAudio.Stop();
        Question.SelfDestruct();
        SubmitAnswer.SelfDestruct();
        Keyboard.SelfDestruct();
        Desc.SelfDestruct();
        Hint.SelfDestruct();
        Listen?.SelfDestruct();
        Skip.SelfDestruct();
        Data.SaveTheoryPuzzleData();
    }

    protected override void Clicked(MouseAction action, Vector3 mousePos)
    {
        if (ReadyForEndPuzzle && action == MouseAction.LUp)
        {
            FadeToState(new Puzzle_State(new ScalePuzzle(), PuzzleType.Aural)); return;

            // if (UnityEngine.Random.value > .5f) FadeToState(PuzzleSelector.WeightedRandomPuzzleState(Data.TheoryPuzzleData));
            // else
            // {
            //     var RhythmSpecs = new RhythmSpecs()
            //     {
            //         Time = new FourFour(),
            //         NumberOfMeasures = 4,
            //         SubDivisionTier = SubDivisionTier.D1Only,
            //         HasTies = UnityEngine.Random.value > .5f,
            //         HasRests = UnityEngine.Random.value > .5f,
            //         HasTriplets = false,
            //         Tempo = 90
            //     };
            //     // FadeToState(new BatteryAndCadenceTestState(RhythmSpecs));
            // }

            // FadeToState(new Puzzle_State(new InvertedSeventhChordPuzzle(), PuzzleType.Theory));

            //FadeToState(new Puzzle_State<MusicTheory.SeventhChords.SeventhChord>(new InvertedSeventhChordPuzzle(), RandPuzzleType()));
            //PuzzleType RandPuzzleType() => UnityEngine.Random.value > .5f ? PuzzleType.Theory : PuzzleType.Aural;
            // return;
        }

        base.Clicked(action, mousePos);
    }

    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(Keyboard.Parent.transform)) KeyboardClicked(go);
        else if (go.transform.IsChildOf(Hint.GO.transform)) HintClicked();
        else if (go.transform.IsChildOf(Question.GO.transform) && Puzzle.AllowPlayQuestion) QuestionClicked();
        else if (go.transform.IsChildOf(SubmitAnswer.GO.transform)) { SubmitClicked(); return; }
        else if (go.transform.IsChildOf(Listen.GO.transform)) ListenClicked();
        else if (go.transform.IsChildOf(Skip.GO.transform)) SkipClicked();

        SubmitAnswer.GO.SetActive(AllNotesSelected());
        Listen.GO.SetActive(AllNotesSelected());
        //Hint.SetTextString(DataManager.Io.TheoryPuzzleData.GetHintsRemaining);
    }

    private void SkipClicked()
    {
        // FadeToState(new Puzzle_State(new ModePuzzle(), PuzzleType.Aural)); return;
        // Skipped++;
        // var RhythmSpecs = new RhythmSpecs()
        // {
        //     Time = new FourFour(),
        //     NumberOfMeasures = 4,
        //     SubDivisionTier = SubDivisionTier.D1Only,
        //     HasTies = true,
        //     HasRests = true,
        //     HasTriplets = false,
        //     Tempo = 90
        // };
        SetStateDirectly(new SeaScene_State());

    }

    private void ListenClicked()
    {
        HintsUsed++;
        Keyboard.PlayNotes(Keyboard.SelectedKeys, null, Puzzle.ListenPlaybackMode, KeyboardInteractionType.User);
    }

    private void SubmitClicked()
    {
        if (AllNotesCorrect())
        {
            DisableInput();
            Solved++;
            Question.SetTextString(Puzzle.Question).SetImageColor(Color.clear);
            Keyboard.PlayNotes(Puzzle.Notes, EndPuzzleCallback, Puzzle.AnswerPlaybackMode, KeyboardInteractionType.Keyboard);
            Listen.GO.SetActive(false);
            SubmitAnswer.GO.SetActive(false);
            Desc.GO.SetActive(false);
            Skip.GO.SetActive(false);
            CaretBlink.SelfDestruct();
            HintClicked();
        }
        else
        {
            WrongAnswers++;
            Audio.SFX.PlayOneShot(BatterieAssets.MissStick);
            //if (DataManager.Io.TheoryPuzzleData.PuzzleDifficulty == PuzzleDifficulty.Challenge &&
            //    DataManager.Io.TheoryPuzzleData.HintsRemaining == 0)
            //{
            //    SetStateDirectly(new DialogStart_State(new StartPuzzle_Dialogue()));
            //}

            //DataManager.Io.TheoryPuzzleData.HintsRemaining--;

            //if (DataManager.Io.TheoryPuzzleData.HintsRemaining < 0)
            //{
            //    DataManager.Io.TheoryPuzzleData.FailedPuzzles++;
            //    FadeToState(PuzzleSelector.WeightedRandomPuzzleState(Data.TheoryPuzzleData));
            //}
        }
    }

    private void QuestionClicked()
    {
        //if (DataManager.Io.TheoryPuzzleData.HintsRemaining <= 0) return;
        //DataManager.Io.TheoryPuzzleData.HintsRemaining--;
        HintsUsed++;
        Keyboard.PlayNotes(Puzzle.Notes, null, Puzzle.QuestionPlaybackMode, KeyboardInteractionType.Puzzle);
    }

    private void HintClicked()
    {
        Hint.SetTextString(Puzzle.Clue).SetImageColor(Color.clear);
    }

    private void KeyboardClicked(GameObject go)
    {
        KeyboardKey key = Keyboard.IdentifyKey(go);

        CaretBlink.ClearCaret();

        Keyboard.InteractWithKey(key, KeyboardInteractionType.User);

        CaretBlink.SetCaret(key.SR);

        bool containsKey = true;
        foreach (KeyboardNoteName note in Puzzle.Notes) if (note == key.KeyboardNoteName) { containsKey = true; break; }
        if (!containsKey) Errors++;
    }

    void EndPuzzleCallback()
    {
        ReadyForEndPuzzle = true;
        EnableInput();
        Desc.GO.SetActive(true);
        Desc.SetTextString("continue...");
    }

    private bool AllNotesSelected()
    {
        foreach (var key in Keyboard.SelectedKeys) if (key == null) return false;
        return true;
    }

    private bool AllNotesCorrect()
    {
        if (Puzzle.NumOfNotes == 1 && PuzzleType == PuzzleType.Theory)
            return Keyboard.SelectedKeys[0].Key == Puzzle.Notes[0].NoteNameToKey();

        bool[] answered = new bool[Puzzle.Notes.Length];

        foreach (var key in Keyboard.SelectedKeys)
            for (int i = 0; i < Puzzle.Notes.Length; i++)
                if (key.KeyboardNoteName == Puzzle.Notes[i])
                {
                    answered[i] = true;
                    break;
                }

        foreach (bool b in answered) if (!b) return false;
        return true;
    }

    protected override void DirectionPressed(Dir dir)
    {
        switch (dir)
        {
            case Dir.Left:
                for (int i = 1; i < Keyboard.Keys.Count; i++)
                {
                    if (CaretKey == Keyboard.Keys[i])
                    {
                        CaretKey = Keyboard.Keys[i - 1];
                        CaretBlink.SetCaret(CaretKey.SR);
                        return;
                    }
                }
                break;

            case Dir.Right:
                for (int i = 0; i < Keyboard.Keys.Count - 1; i++)
                {
                    if (CaretKey == Keyboard.Keys[i])
                    {
                        CaretKey = Keyboard.Keys[i + 1];
                        CaretBlink.SetCaret(CaretKey.SR);
                        return;
                    }
                }
                break;
        }
    }

    protected override void GPInput(GamePadButton gpb)
    {
        if (ReadyForEndPuzzle)
        {
            SetStateDirectly(
                new CameraPan_State(
                    new DialogStart_State(
                        new EndPuzzle_Dialogue()),
                    Cam.StoredCamRot,
                    Cam.StoredCamPos,
                    3));
            return;
        }
        base.GPInput(gpb);
    }

    protected override void ConfirmPressed()
    {
        Debug.Log("Clicked on: " + CaretKey.Go.name);
        ClickedOn(CaretKey.Go);
    }

    protected override void StartPressed()
    {
        if (SubmitAnswer.GO.activeInHierarchy) ClickedOn(SubmitAnswer.GO);
    }
    protected override void SelectPressed()
    {
        ClickedOn(Hint.GO);
    }
    protected override void InteractPressed()
    {
        if (Listen.GO.activeInHierarchy) ClickedOn(Listen.GO);
    }
    protected override void WestPressed()
    {
        ClickedOn(Question.GO);
    }

    private Card _answer;
    public Card SubmitAnswer => _answer ??= new Card(nameof(SubmitAnswer), null)
        .SetTextString(nameof(SubmitAnswer).SentenceCase())
        .SetTMPPosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 2))
        .SetFontScale(.6f, .6f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeTextContainer(true)
        .ScaleImageSizeToTMP(1.2f)
        .SetImagePosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 2))
        .SetImageColor(new Color(.7f, .7f, .7f, .4f))
        .ImageClickable()
        .AllowWordWrap(false)
        .SetTMPRectPivot(1, .5f)
        .SetImageToUILayer()
        .SetImageRectPivot(1 - (1f - (1f / 1.2f)) * .5f, .5f);

    private Card _skip;
    public Card Skip => _skip ??= new Card(nameof(Skip), null)
        .SetTextString(nameof(Skip))
        .SetTMPPosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 1))
        .SetFontScale(.4f, .4f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeTextContainer(true)
        .SetTMPRectPivot(1, .5f)
        .SetImagePosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 1))
        .SetImageColor(new Color(.7f, .7f, .7f, .4f))
        .ImageClickable()
        .AllowWordWrap(false)
        .ScaleImageSizeToTMP(1.2f)
        .SetImageRectPivot(1 - (1f - (1f / 1.2f)) * .5f, .5f)
        .SetImageToUILayer();

    private Card _desc;
    public Card Desc => _desc ??= new Card(nameof(Desc), null)
        .SetTextString(Puzzle.Desc)
        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, Cam.UIOrthoY - 1))
        .SetFontScale(.5f, .5f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Left)
        .AutoSizeTextContainer(true)
        .SetTextColor(Color.grey)
        .AllowWordWrap(false)
        .SetTMPRectPivot(0, .5f);

    private Card _hint;
    public Card Hint => _hint ??= new Card(nameof(Hint), null)
        .SetTextString("<i>hint")
        .SetTMPPosition(new Vector2(0, -Cam.UIOrthoY + 2f))
        .SetFontScale(.5f, .5f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeTextContainer(true)
        .ScaleImageSizeToTMP(1.2f)
        .SetImagePosition(new Vector2(0, -Cam.UIOrthoY + 2f))
        .SetImageColor(new Color(.7f, .7f, .7f, .4f))
        .ImageClickable()
        .AllowWordWrap(false);

    private Card _question;
    public Card Question => _question ??= new Card(nameof(Question), null)
        .SetTextString(PuzzleType == PuzzleType.Aural ? "Listen to question" : Puzzle.Question)
        .SetTMPPosition(new Vector2(0, Cam.UIOrthoY - 1.75f))
        .SetFontScale(.65f, .65f)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeFont(true)
        .AutoSizeTextContainer(true)
        .ScaleImageSizeToTMP(1.2f)
        .SetImagePosition(new Vector2(0, Cam.UIOrthoY - 1.75f))
        .SetImageColor(new Color(.7f, .7f, .7f, .4f))
        .ImageClickable()
        .AllowWordWrap(false)
        .SetImageToUILayer();

    private Card _listen;
    public Card Listen => _listen ??= new Card(nameof(Listen), null)
        .SetTextString("Listen to answer")
        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetFontScale(.6f, .6f)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeFont(true)
        .AutoSizeTextContainer(true)
        .ScaleImageSizeToTMP(1.2f)
        .SetImagePosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetImageColor(new Color(.7f, .7f, .7f, .4f))
        .ImageClickable()
        .AllowWordWrap(false)
        .SetTMPRectPivot(0, .5f)
        .SetImageToUILayer()
        .SetImageRectPivot((1f - (1f / 1.2f)) * .5f, .5f);
}




/*
 *hack this clickable works..
 *   private Card _listen;
    public Card Listen => _listen ??= new Card(nameof(Listen), null)
        .SetTextString("Listen to answer")
        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetFontScale(.6f, .6f)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeFont(true)
        .AutoSizeTextContainer(true)
        .ScaleImageSizeToTMP(1.2f)
        .SetImagePosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetImageColor(new Color(.8f, .8f, .8f, .4f))
        .ImageClickable()
        .AllowWordWrap(false)
        .SetImageToUILayer();
 * 
 * 
 * hack this clickable does not work
    private Card _listen;
    public Card Listen => _listen ??= new Card(nameof(Listen), null)
        .SetTextString("Listen to answer")
        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetFontScale(.6f, .6f)
        .AutoSizeFont(true)
        .AutoSizeTextContainer(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .SetImagePosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetImageColor(new Color(.8f, .8f, .8f, .4f))
        .ImageClickable()
        .AllowWordWrap(false)
        .ScaleImageSizeToTMP(1.2f)
        .SetImageToUILayer();
 */