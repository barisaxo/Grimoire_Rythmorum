using System;
using System.Collections.Generic;
using UnityEngine;
using Data.Two;
using Quests;

public class EndPuzzle_State : State
{
    readonly bool Won;
    readonly State SubsequentState;
    readonly IPuzzle Puzzle;
    readonly PuzzleType PuzzleType;
    readonly StarChart StarChart;

    public EndPuzzle_State(bool winLose, State subsequentState, IPuzzle puzzle, PuzzleType puzzleType, StarChart item)
    {
        Won = winLose;
        SubsequentState = subsequentState;
        Puzzle = puzzle;
        PuzzleType = puzzleType;
        StarChart = item;
    }

    protected override void EngageState()
    {
        Vector2Int loc = Sea.WorldMapScene.Io.Ship.GlobalCoord + RandomLoc();
        string latLong = loc.GlobalCoordsToLatLongs(Sea.WorldMapScene.Io.Map.GlobalSize);
        int patternsFound = (int)((float)(StarChart.ID + 1f) * 10f * (1f + UnityEngine.Random.value));

        if (Won)
        {
            Manager.Io.Quests.SetQuest(new Navigation(),
                new NavigationQuest(
                    new Sea.Inventoriable((Manager.Io.Gramophones, new Gramo1(), 1)),//TODO make sliding scale difficulty
                    loc,
                    latLong));

            Sea.WorldMapScene.Io.Map.AddToMap(Manager.Io.Quests.GetQuest(new Navigation()).QuestLocation, Sea.CellType.Gramo);

            Manager.Io.Player.SetLevel(new PatternsAvailable(),
                Manager.Io.Player.GetLevel(new PatternsAvailable()) + patternsFound);

            Manager.Io.Player.SetLevel(new PatternsFound(),
                Manager.Io.Player.GetLevel(new PatternsFound()) + patternsFound);

            Manager.Io.Player.AdjustLevel(PuzzleType == PuzzleType.Aural ?
                new AuralSolved() : new TheorySolved(), 1);
        }
        else
        {
            Manager.Io.Player.AdjustLevel(PuzzleType == PuzzleType.Aural ?
                new AuralFailed() : new TheoryFailed(), 1);
        }

        SetState(new CameraPan_State(
            new DialogStart_State(
                new EndPuzzle_Dialogue(
                    Won,
                    SubsequentState,
                    latLong,
                    patternsFound)),
            pan: Cam.StoredCamRot,
            strafe: Cam.StoredCamPos,
            speed: 3));
    }

    private Vector2Int RandomLoc()
    {
        return new Vector2Int(
            UnityEngine.Random.Range(30, 60) * (UnityEngine.Random.value < .5f ? 1 : -1),
            UnityEngine.Random.Range(30, 60) * (UnityEngine.Random.value < .5f ? 1 : -1)
        );
    }

    // Action GiveRewards() => PuzzleType switch
    // {
    //     PuzzleType.Aural => GiveAuralRewards(),
    //     PuzzleType.Theory => GiveTheoryRewards(),
    //     _ => throw new System.NotImplementedException(),
    // };

    // Action GiveTheoryRewards() => Puzzle switch
    // {
    //     _ when Puzzle is NotePuzzle => () => { Manager.Io.QuestsData.IncreaseLevel(QuestData.DataItem.StarChart); }
    //     ,
    //     _ => null,
    // };

    // Action GiveAuralRewards() => Puzzle switch
    // {
    //     _ when Puzzle is NotePuzzle => () => { Manager.Io.QuestsData.IncreaseLevel(QuestData.DataItem.StarChart); }
    //     ,
    //     _ => null,
    // };


}