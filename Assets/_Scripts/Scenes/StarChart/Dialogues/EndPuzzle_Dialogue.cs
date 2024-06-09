using UnityEngine;
using Dialog;

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

public class EndGramo_Dialogue : Dialogue
{
    readonly int PatternsFound;
    readonly bool _won;

    public EndGramo_Dialogue(bool won, State subsequentState, int patternsFound)
    {
        SubsequentState = subsequentState;
        _won = won;
        PatternsFound = patternsFound;
        FirstLine = _won ? RecapLineWon : RecapLineLost;
    }

    public EndGramo_Dialogue(State subsequentState)
    {
        SubsequentState = subsequentState;
        FirstLine = PracticeLine;
    }

    readonly State SubsequentState;

    Line RecapLineWon => new(Won_String, ResultsWithPatterns);

    Line RecapLineLost => new(Lost_String, SubsequentState);

    string Won_String => "Great job Cap'n, you opened the Gramophone!" +
        "\nThere is some ancient Bardic treasure inside!";

    string Lost_String => "No worries Cap! You'll do better next time!\n";

    Line ResultsWithPatterns => new(PatternsFoundString, SubsequentState);
    string PatternsFoundString => "You found " + PatternsFound + " patterns!";


    Line PracticeLine => new(Practice_String, SubsequentState);
    string Practice_String => "That's great work!\nThe more you practice here the better you'll be at sea!";
}

public class EndStarChartPractice_Dialogue : Dialogue
{
    readonly bool _won;

    public EndStarChartPractice_Dialogue(bool won, State subsequentState)
    {
        SubsequentState = subsequentState;
        _won = won;
        FirstLine = _won ? RecapLineWon : RecapLineLost;
    }

    readonly State SubsequentState;

    Line RecapLineWon => new(Won_String, SubsequentState);

    Line RecapLineLost => new(Lost_String, SubsequentState);

    string Won_String => "That's great work!\nThe more you practice here the better you'll be at sea!";

    string Lost_String => "No worries! Keep practicing, you'll get better in no time!";


}