using System;
using System.Collections.Generic;
using UnityEngine;
using Data.Inventory;

public class EndPuzzle_State : State
{
    readonly bool Won;
    readonly State SubsequentState;
    readonly IPuzzle Puzzle;
    readonly PuzzleType PuzzleType;

    public EndPuzzle_State(bool won, State subsequentState, IPuzzle puzzle, PuzzleType puzzleType)
    {
        Won = won;
        SubsequentState = subsequentState;
        Puzzle = puzzle;
        PuzzleType = puzzleType;
    }

    protected override void EngageState()
    {
        if (Won) GiveRewards();

        SetState(new CameraPan_State(
            new DialogStart_State(
                new EndPuzzle_Dialogue(won: Won, SubsequentState)),
            pan: Cam.StoredCamRot,
            strafe: Cam.StoredCamPos,
            speed: 3));
    }

    Action GiveRewards() => PuzzleType switch
    {
        PuzzleType.Aural => GiveAuralRewards(),
        PuzzleType.Theory => GiveTheoryRewards(),
        _ => throw new System.NotImplementedException(),
    };

    Action GiveTheoryRewards() => Puzzle switch
    {
        _ when Puzzle is NotePuzzle => () => { Data.QuestsData.IncreaseLevel(QuestData.DataItem.StarChart); }
        ,
        _ => null,
    };

    Action GiveAuralRewards() => Puzzle switch
    {
        _ when Puzzle is NotePuzzle => () => { }
        ,
        _ => null,
    };


}