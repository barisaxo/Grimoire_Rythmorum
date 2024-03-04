using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class NewPuzzleSetup_State : State
{
    public NewPuzzleSetup_State(PuzzleDifficulty difficulty, State subsequentState)
    {
        PuzzleDifficulty = difficulty;
        SubsequentState = subsequentState;
    }
    readonly State SubsequentState;
    readonly PuzzleDifficulty PuzzleDifficulty;

    protected override void PrepareState(Action callback)
    {
        Data.TheoryPuzzleData.PuzzleDifficulty = PuzzleDifficulty;
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        SetState(PuzzleSelector.WeightedRandomPuzzleState(Data.TheoryPuzzleData, SubsequentState));
    }
}
