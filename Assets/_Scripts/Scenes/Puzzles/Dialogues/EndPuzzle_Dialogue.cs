using UnityEngine;
using Dialog;
using Data.Inventory;

public class EndPuzzle_Dialogue : Dialogue
{
    readonly int PatternsFound;
    readonly bool _won;
    readonly string LatLong;

    public EndPuzzle_Dialogue(bool won, State subsequentState, string latLong, int patternsFound)
    {
        LatLong = latLong;
        SubsequentState = subsequentState;
        _won = won;
        PatternsFound = patternsFound;
        FirstLine = _won ? RecapLineWon : RecapLineLost;
    }

    readonly State SubsequentState;

    Line RecapLineWon => new(Won_String, ResultsWithPatterns);

    Line RecapLineLost => new(Lost_String, SubsequentState);

    string Won_String => "Great job Cap'n, you decoded the Star Chart! This reveals the position " +
        "of some hidden ancient Bardic treasure!\n" + "It is located near: " + LatLong;

    string Lost_String => "No worries Cap! You'll do better next time!\n";

    Line ResultsWithPatterns => new(PatternsFoundString, SubsequentState);
    string PatternsFoundString => "You found " + PatternsFound + " patterns!";

}

