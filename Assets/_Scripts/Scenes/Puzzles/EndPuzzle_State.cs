using System;
using System.Collections.Generic;
using UnityEngine;
using Data.Inventory;
using Data.Player;
using Quests;

public class EndPuzzle_State : State
{
    readonly bool Won;
    readonly State SubsequentState;
    readonly IPuzzle Puzzle;
    readonly PuzzleType PuzzleType;
    readonly StarChartsData.DataItem DataItem;

    public EndPuzzle_State(bool winLose, State subsequentState, IPuzzle puzzle, PuzzleType puzzleType, StarChartsData.DataItem dataItem)
    {
        Won = winLose;
        SubsequentState = subsequentState;
        Puzzle = puzzle;
        PuzzleType = puzzleType;
        DataItem = dataItem;
    }

    protected override void EngageState()
    {
        Vector2Int loc = Sea.WorldMapScene.Io.Ship.GlobalCoord + Vector2Int.one;
        string latLong = loc.GlobalCoordsToLatLongs(Sea.WorldMapScene.Io.Map.GlobalSize);
        int patternsFound = (int)((float)(DataItem.Id + 1f) * 10f * (1f + UnityEngine.Random.value));

        if (Won)
        {
            DataManager.QuestsData.SetQuest(QuestData.DataItem.StarChart, new NavigationQuest(
                new Sea.Inventoriable((DataManager.GramophoneData, GramophoneData.DataItem.Lvl1, 1))
                , loc, latLong));

            Sea.WorldMapScene.Io.Map.AddToMap(DataManager.QuestsData.GetQuest(QuestData.DataItem.StarChart).QuestLocation, Sea.CellType.Gramo);

            DataManager.PlayerData.SetLevel(
                PlayerData.DataItem.Patterns,
                DataManager.PlayerData.GetLevel(PlayerData.DataItem.Patterns) + patternsFound);

            DataManager.PlayerData.IncreaseLevel(PuzzleType == PuzzleType.Aural ?
                PlayerData.DataItem.AuralSolved : PlayerData.DataItem.TheorySolved);
        }
        else
        {
            DataManager.PlayerData.IncreaseLevel(PuzzleType == PuzzleType.Aural ?
                PlayerData.DataItem.AuralFailed : PlayerData.DataItem.TheoryFailed);
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

    Action GiveRewards() => PuzzleType switch
    {
        PuzzleType.Aural => GiveAuralRewards(),
        PuzzleType.Theory => GiveTheoryRewards(),
        _ => throw new System.NotImplementedException(),
    };

    Action GiveTheoryRewards() => Puzzle switch
    {
        _ when Puzzle is NotePuzzle => () => { DataManager.QuestsData.IncreaseLevel(QuestData.DataItem.StarChart); }
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