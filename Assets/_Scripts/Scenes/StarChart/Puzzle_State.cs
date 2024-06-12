using System;
using UnityEngine;
using MusicTheory.Rhythms;
using Data;

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
    private CaretBlink CaretBlink;
    readonly State SubsequentState;
    // readonly IStarChart StarChart;

    // public Puzzle_State(IPuzzle puzzle, PuzzleType puzzleType, State subsequentState, StarChartsData.StarChart dataItem)
    // {
    //     Puzzle = puzzle;
    //     PuzzleType = puzzleType;
    //     SubsequentState = subsequentState;
    //     Debug.Log(subsequentState);
    //     StarChart = dataItem;
    // }
    public Puzzle_State(IPuzzle puzzle, PuzzleType puzzleType, State subsequentState)
    {
        Puzzle = puzzle;
        PuzzleType = puzzleType;
        SubsequentState = subsequentState;
        Debug.Log(subsequentState);
        // StarChart = starChart;
    }

    protected override void PrepareState(Action callback)
    {
        Manager.Io.Inventory.AdjustLevel(new StarChart(), -1);
        Keyboard = Puzzle.NumOfNotes == 1 ? new(Puzzle.NumOfNotes) :
            new(Puzzle.NumOfNotes, Puzzle.Notes[0]);

        CaretKey = Keyboard.SelectedKeys[0] ?? Keyboard.Keys[12];
        CaretBlink = new CaretBlink().Start();
        CaretBlink.SetCaret(CaretKey.SR);
        //Data.TheoryPuzzleData.ResetHints();
        _ = Question;
        _ = Desc;
        _ = Hint;
        _ = Listen;
        _ = SubmitAnswer;
        _ = GiveUp;
        base.PrepareState(callback);
    }

    protected override void PreEngageState(Action callback)
    {
        Listen.GO.SetActive(false);
        SubmitAnswer.GO.SetActive(false);
        GiveUp.GO.SetActive(false);
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
        GiveUp.GO.SetActive(true);
        Hint.GO.SetActive(true);
    }

    protected override void DisengageState()
    {
        // Manager.TheoryPuzzleData.AddStat(//TODO TODO TODO!!!
        // new PuzzleStat(
        //     specs: new PuzzleSpec(PuzzleType, Puzzle),
        //     hints: HintsUsed,
        //     wrong: WrongAnswers,
        //     fail: Failed,
        //     solve: Solved,
        //     skipped: Skipped,
        //     errors: Errors));



        Audio.KBAudio.Stop();
        Question.SelfDestruct();
        SubmitAnswer.SelfDestruct();
        Keyboard.SelfDestruct();
        Desc.SelfDestruct();
        Hint.SelfDestruct();
        Listen?.SelfDestruct();
        GiveUp.SelfDestruct();
        // Data.SaveTheoryPuzzleData();
    }

    protected override void Clicked(MouseAction action, Vector3 mousePos)
    {
        if (ReadyForEndPuzzle && action == MouseAction.LUp)
        {
            SetState(new EndPuzzle_State(winLose: true, SubsequentState, Puzzle, PuzzleType));
            return;
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
        else if (go.transform.IsChildOf(GiveUp.GO.transform)) SkipClicked();

        SubmitAnswer.GO.SetActive(AllNotesSelected());
        Listen.GO.SetActive(AllNotesSelected());
        //Hint.SetTextString(DataManager.Io.TheoryPuzzleData.GetHintsRemaining);
    }

    private void SkipClicked()
    {
        SetState(new EndPuzzle_State(winLose: false, SubsequentState, Puzzle, PuzzleType));
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
        // SetStateDirectly(new SeaScene_State());

    }

    private void ListenClicked()
    {
        HintsUsed++;
        Keyboard.PlayNotes(Keyboard.SelectedKeys, null, Puzzle.ListenPlaybackMode, KeyboardInteractionType.User);
    }

    private void SubmitClicked()
    {
        if (AllNotesSelected() && AllNotesCorrect())
        {
            DisableInput();
            Solved++;
            Question.SetTextString(Puzzle.Question).SetImageColor(Color.clear);
            Keyboard.PlayNotes(Puzzle.Notes, EndPuzzleCallback, Puzzle.AnswerPlaybackMode, KeyboardInteractionType.Keyboard);
            Listen.GO.SetActive(false);
            SubmitAnswer.GO.SetActive(false);
            Desc.GO.SetActive(false);
            GiveUp.GO.SetActive(false);
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
        Hint.SetTextString(Puzzle.Clue)
            .SetImageColor(Color.clear);
    }
    private void HintReleased()
    {
        Hint.SetTextString("<i>show hint")
            .SetImageColor(Color.white);
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
            Finish();
            return;
        }
        base.GPInput(gpb);
    }

    protected override void EastPressed()
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
        SkipClicked();
    }
    protected override void NorthPressed()
    {
        if (Listen.GO.activeInHierarchy) ClickedOn(Listen.GO);
    }

    protected override void WestPressed()
    {
        ClickedOn(Question.GO);
    }

    protected override void SouthPressed()
    {
        HintClicked();
    }
    protected override void SouthReleased()
    {
        HintReleased();
    }

    void Finish()
    {
        SetState(new EndPuzzle_State(winLose: true, SubsequentState, Puzzle, PuzzleType));
    }

    private Card _answer;
    public Card SubmitAnswer => _answer ??= new Card(nameof(SubmitAnswer), null)
        .SetTextString(nameof(SubmitAnswer).SentenceCase())
        // .SetTMPPosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 2))
        .SetPositionAll(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 2))
        .SetFontScale(.6f, .6f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Right)
        .AutoSizeTextContainer(true)
        // .ScaleImageSizeToTMP(1.2f)
        // .SetImagePosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 2))
        // .SetImageColor(new Color(.7f, .7f, .7f, .4f))
        //.ImageClickable()
        .AllowWordWrap(false)
        .SetTMPRectPivot(1, .5f)
        // .SetImageToUILayer()
        // .SetImageRectPivot(1 - (1f - (1f / 1.2f)) * .5f, .5f)
        .SetImageRectPivot(1, .5f)
        .SetImageSprite(Assets.StartButton)
        .SetImageSize(Vector2.one * .6f)
        .OffsetImageFromTMP(Vector2.right * .5f)
        ;

    private Card _skip;
    public Card GiveUp => _skip ??= new Card(nameof(GiveUp), null)
        .SetTextString(nameof(GiveUp).SentenceCase())
        // .SetTMPPosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 1))
        .SetPositionAll(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 1))
        .SetFontScale(.4f, .4f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Right)
        .AutoSizeTextContainer(true)
        .SetTMPRectPivot(1, .5f)
        // .SetImagePosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 1))
        // .SetImageColor(new Color(.7f, .7f, .7f, .4f))
        //.ImageClickable()
        .AllowWordWrap(false)
        // .ScaleImageSizeToTMP(1.2f)
        // .SetImageRectPivot(1 - (1f - (1f / 1.2f)) * .5f, .5f)
        // .SetImageToUILayer()
        .SetImageSprite(Assets.SelectButton)
        .SetImageSize(Vector2.one * .6f)
        .OffsetImageFromTMP(Vector2.right * .5f);

    private Card _desc;
    public Card Desc => _desc ??= new Card(nameof(Desc), null)
        .SetTextString(Puzzle.Desc)
        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, Cam.UIOrthoY - 1))
        .SetFontScale(.5f, .5f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeTextContainer(true)
        .AllowWordWrap(false)
        .SetTMPRectPivot(0, .5f);

    private Card _hint;
    public Card Hint => _hint ??= new Card(nameof(Hint), null)
        .SetTextString("<i>show hint")
        // .SetTMPPosition(new Vector2(0, -Cam.UIOrthoY + 2f))
        .SetPositionAll(new Vector2(0, -Cam.UIOrthoY + 2f))
        .SetFontScale(.5f, .5f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeTextContainer(true)
        // .ScaleImageSizeToTMP(1.2f)
        // .SetImagePosition(new Vector2(0, -Cam.UIOrthoY + 2f))
        //.ImageClickable()
        .AllowWordWrap(false)
        .SetImageSprite(Assets.SouthButton)
        .SetImageSize(Vector2.one * .6f)
        .OffsetImageFromTMP(Vector2.right);

    private Card _question;
    public Card Question => _question ??= new Card(nameof(Question), null)
        .SetTextString(PuzzleType == PuzzleType.Aural ? "Listen to question" : Puzzle.Question)
        // .SetTMPPosition(new Vector2(0, Cam.UIOrthoY - 1.75f))
        .SetPositionAll(new Vector2(0, Cam.UIOrthoY - 1.75f))
        .SetFontScale(.65f, .65f)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeFont(true)
        .AutoSizeTextContainer(true)
        // .ScaleImageSizeToTMP(1.2f)
        // .SetImagePosition(new Vector2(0, Cam.UIOrthoY - 1.75f))
        // .SetImageColor(new Color(.7f, .7f, .7f, .4f))
        //.ImageClickable()
        .AllowWordWrap(false)
        // .SetImageToUILayer()
        .SetImageSprite(Assets.WestButton)
        .SetImageSize(Vector2.one * .6f)
        .OffsetImageFromTMP(Vector2.right);

    private Card _listen;
    public Card Listen => _listen ??= new Card(nameof(Listen), null)
        .SetTextString("Listen to answer")
        // .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetPositionAll(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetFontScale(.6f, .6f)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Left)
        .AutoSizeFont(true)
        .AutoSizeTextContainer(true)
        // .ScaleImageSizeToTMP(1.2f)
        // .SetImagePosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        // .SetImageColor(new Color(.7f, .7f, .7f, .4f))
        //.ImageClickable()
        .AllowWordWrap(false)
        .SetTMPRectPivot(0, .5f)
        // .SetImageToUILayer()
        // .SetImageRectPivot((1f - (1f / 1.2f)) * .5f, .5f)
        .SetImageSprite(Assets.NorthButton)
        .SetImageSize(Vector2.one * .6f)
        .OffsetImageFromTMP(Vector2.right * 2);
}




/*
 * hack this clickable works..
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
        .SetImageColor(new Color(.8f, .8f, .8f, .4f))
        //.ImageClickable()
        .AllowWordWrap(false)
        .SetImageToUILayer();
 
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
        //.ImageClickable()
        .AllowWordWrap(false)
        .ScaleImageSizeToTMP(1.2f)
        .SetImageToUILayer();
 */

public class StarChartPractice_State : State
{
    private Keyboard Keyboard;
    private readonly IPuzzle Puzzle;
    private readonly AudioParserB AudioParser = new();
    private readonly PuzzleType PuzzleType;
    private bool ReadyForEndPuzzle = false;


    private KeyboardKey CaretKey;
    private CaretBlink CaretBlink;
    readonly State SubsequentState;
    // readonly IStarChart StarChart;

    // public Puzzle_State(IPuzzle puzzle, PuzzleType puzzleType, State subsequentState, StarChartsData.StarChart dataItem)
    // {
    //     Puzzle = puzzle;
    //     PuzzleType = puzzleType;
    //     SubsequentState = subsequentState;
    //     Debug.Log(subsequentState);
    //     StarChart = dataItem;
    // }
    public StarChartPractice_State(IPuzzle puzzle, PuzzleType puzzleType, State subsequentState)
    {
        Puzzle = puzzle;
        PuzzleType = puzzleType;
        SubsequentState = subsequentState;
        Debug.Log(subsequentState);
        // StarChart = starChart;
    }

    protected override void PrepareState(Action callback)
    {
        Keyboard = Puzzle.NumOfNotes == 1 ? new(Puzzle.NumOfNotes) :
            new(Puzzle.NumOfNotes, Puzzle.Notes[0]);

        CaretKey = Keyboard.SelectedKeys[0] ?? Keyboard.Keys[12];
        CaretBlink = new CaretBlink().Start();
        CaretBlink.SetCaret(CaretKey.SR);
        //Data.TheoryPuzzleData.ResetHints();
        _ = Question;
        _ = Desc;
        _ = Hint;
        _ = Listen;
        _ = SubmitAnswer;
        _ = GiveUp;
        base.PrepareState(callback);
    }

    protected override void PreEngageState(Action callback)
    {
        Listen.GO.SetActive(false);
        SubmitAnswer.GO.SetActive(false);
        GiveUp.GO.SetActive(false);
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
        GiveUp.GO.SetActive(true);
        Hint.GO.SetActive(true);
    }

    protected override void DisengageState()
    {
        Audio.KBAudio.Stop();
        Question.SelfDestruct();
        SubmitAnswer.SelfDestruct();
        Keyboard.SelfDestruct();
        Desc.SelfDestruct();
        Hint.SelfDestruct();
        Listen?.SelfDestruct();
        GiveUp.SelfDestruct();
    }

    protected override void Clicked(MouseAction action, Vector3 mousePos)
    {
        if (ReadyForEndPuzzle && action == MouseAction.LUp)
        {
            SetState(new EndPuzzle_State(winLose: true, SubsequentState, Puzzle, PuzzleType));
            return;
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
        else if (go.transform.IsChildOf(GiveUp.GO.transform)) SkipClicked();

        SubmitAnswer.GO.SetActive(AllNotesSelected());
        Listen.GO.SetActive(AllNotesSelected());
        //Hint.SetTextString(DataManager.Io.TheoryPuzzleData.GetHintsRemaining);
    }

    private void SkipClicked()
    {
        SetState(new DialogStart_State(new EndStarChartPractice_Dialogue(false, SubsequentState)));
    }

    private void ListenClicked()
    {
        Keyboard.PlayNotes(Keyboard.SelectedKeys, null, Puzzle.ListenPlaybackMode, KeyboardInteractionType.User);
    }

    private void SubmitClicked()
    {
        if (AllNotesSelected() && AllNotesCorrect())
        {
            DisableInput();
            Question.SetTextString(Puzzle.Question).SetImageColor(Color.clear);
            Keyboard.PlayNotes(Puzzle.Notes, EndPuzzleCallback, Puzzle.AnswerPlaybackMode, KeyboardInteractionType.Keyboard);
            Listen.GO.SetActive(false);
            SubmitAnswer.GO.SetActive(false);
            Desc.GO.SetActive(false);
            GiveUp.GO.SetActive(false);
            CaretBlink.SelfDestruct();
            HintClicked();
        }
        else
        {
            Audio.SFX.PlayOneShot(BatterieAssets.MissStick);
        }
    }

    private void QuestionClicked()
    {
        Keyboard.PlayNotes(Puzzle.Notes, null, Puzzle.QuestionPlaybackMode, KeyboardInteractionType.Puzzle);
    }

    private void HintClicked()
    {
        Hint.SetTextString(Puzzle.Clue)
            .SetImageColor(Color.clear);
    }
    private void HintReleased()
    {
        Hint.SetTextString("<i>show hint")
            .SetImageColor(Color.white);
    }

    private void KeyboardClicked(GameObject go)
    {
        KeyboardKey key = Keyboard.IdentifyKey(go);

        CaretBlink.ClearCaret();

        Keyboard.InteractWithKey(key, KeyboardInteractionType.User);

        CaretBlink.SetCaret(key.SR);
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
            Finish();
            return;
        }
        base.GPInput(gpb);
    }

    protected override void EastPressed()
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
        SkipClicked();
    }
    protected override void NorthPressed()
    {
        if (Listen.GO.activeInHierarchy) ClickedOn(Listen.GO);
    }

    protected override void WestPressed()
    {
        ClickedOn(Question.GO);
    }

    protected override void SouthPressed()
    {
        HintClicked();
    }
    protected override void SouthReleased()
    {
        HintReleased();
    }

    void Finish()
    {
        SetState(new DialogStart_State(new EndStarChartPractice_Dialogue(true, SubsequentState)));
    }

    private Card _answer;
    public Card SubmitAnswer => _answer ??= new Card(nameof(SubmitAnswer), null)
        .SetTextString(nameof(SubmitAnswer).SentenceCase())
        // .SetTMPPosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 2))
        .SetPositionAll(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 2))
        .SetFontScale(.6f, .6f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Right)
        .AutoSizeTextContainer(true)
        // .ScaleImageSizeToTMP(1.2f)
        // .SetImagePosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 2))
        // .SetImageColor(new Color(.7f, .7f, .7f, .4f))
        //.ImageClickable()
        .AllowWordWrap(false)
        .SetTMPRectPivot(1, .5f)
        // .SetImageToUILayer()
        // .SetImageRectPivot(1 - (1f - (1f / 1.2f)) * .5f, .5f)
        .SetImageRectPivot(1, .5f)
        .SetImageSprite(Assets.StartButton)
        .SetImageSize(Vector2.one * .6f)
        .OffsetImageFromTMP(Vector2.right * .5f)
        ;

    private Card _skip;
    public Card GiveUp => _skip ??= new Card(nameof(GiveUp), null)
        .SetTextString(nameof(GiveUp).SentenceCase())
        // .SetTMPPosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 1))
        .SetPositionAll(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 1))
        .SetFontScale(.4f, .4f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Right)
        .AutoSizeTextContainer(true)
        .SetTMPRectPivot(1, .5f)
        // .SetImagePosition(new Vector2(Cam.UIOrthoX - 1, -Cam.UIOrthoY + 1))
        // .SetImageColor(new Color(.7f, .7f, .7f, .4f))
        //.ImageClickable()
        .AllowWordWrap(false)
        // .ScaleImageSizeToTMP(1.2f)
        // .SetImageRectPivot(1 - (1f - (1f / 1.2f)) * .5f, .5f)
        // .SetImageToUILayer()
        .SetImageSprite(Assets.SelectButton)
        .SetImageSize(Vector2.one * .6f)
        .OffsetImageFromTMP(Vector2.right * .5f);

    private Card _desc;
    public Card Desc => _desc ??= new Card(nameof(Desc), null)
        .SetTextString(Puzzle.Desc)
        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, Cam.UIOrthoY - 1))
        .SetFontScale(.5f, .5f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeTextContainer(true)
        .AllowWordWrap(false)
        .SetTMPRectPivot(0, .5f);

    private Card _hint;
    public Card Hint => _hint ??= new Card(nameof(Hint), null)
        .SetTextString("<i>show hint")
        // .SetTMPPosition(new Vector2(0, -Cam.UIOrthoY + 2f))
        .SetPositionAll(new Vector2(0, -Cam.UIOrthoY + 2f))
        .SetFontScale(.5f, .5f)
        .AutoSizeFont(true)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeTextContainer(true)
        // .ScaleImageSizeToTMP(1.2f)
        // .SetImagePosition(new Vector2(0, -Cam.UIOrthoY + 2f))
        //.ImageClickable()
        .AllowWordWrap(false)
        .SetImageSprite(Assets.SouthButton)
        .SetImageSize(Vector2.one * .6f)
        .OffsetImageFromTMP(Vector2.right);

    private Card _question;
    public Card Question => _question ??= new Card(nameof(Question), null)
        .SetTextString(PuzzleType == PuzzleType.Aural ? "Listen to question" : Puzzle.Question)
        // .SetTMPPosition(new Vector2(0, Cam.UIOrthoY - 1.75f))
        .SetPositionAll(new Vector2(0, Cam.UIOrthoY - 1.75f))
        .SetFontScale(.65f, .65f)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .AutoSizeFont(true)
        .AutoSizeTextContainer(true)
        // .ScaleImageSizeToTMP(1.2f)
        // .SetImagePosition(new Vector2(0, Cam.UIOrthoY - 1.75f))
        // .SetImageColor(new Color(.7f, .7f, .7f, .4f))
        //.ImageClickable()
        .AllowWordWrap(false)
        // .SetImageToUILayer()
        .SetImageSprite(Assets.WestButton)
        .SetImageSize(Vector2.one * .6f)
        .OffsetImageFromTMP(Vector2.right);

    private Card _listen;
    public Card Listen => _listen ??= new Card(nameof(Listen), null)
        .SetTextString("Listen to answer")
        // .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetPositionAll(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        .SetFontScale(.6f, .6f)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Left)
        .AutoSizeFont(true)
        .AutoSizeTextContainer(true)
        // .ScaleImageSizeToTMP(1.2f)
        // .SetImagePosition(new Vector2(-Cam.UIOrthoX + 1, -Cam.UIOrthoY + 1))
        // .SetImageColor(new Color(.7f, .7f, .7f, .4f))
        //.ImageClickable()
        .AllowWordWrap(false)
        .SetTMPRectPivot(0, .5f)
        // .SetImageToUILayer()
        // .SetImageRectPivot((1f - (1f / 1.2f)) * .5f, .5f)
        .SetImageSprite(Assets.NorthButton)
        .SetImageSize(Vector2.one * .6f)
        .OffsetImageFromTMP(Vector2.right * 2);
}
