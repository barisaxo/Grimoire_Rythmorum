using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class FishGotAway_Dialogue : Dialogue
{
    readonly State SubsequentState;

    public FishGotAway_Dialogue(State subsequentState)
    {
        SubsequentState = subsequentState;
    }

    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= new Line(
        "The fish got away...", SubsequentState);

}

public class EndFishPractice_Dialogue : Dialogue
{
    readonly State SubsequentState;
    readonly bool _won;

    public EndFishPractice_Dialogue(State subsequentState, bool won)
    {
        SubsequentState = subsequentState;
        _won = won;
    }

    public override Dialogue Initiate()
    {
        FirstLine = _won ? WonLine : LostLine;
        return this;
    }

    Line LostLine => new("No worries! Keep practicing, you'll get better in no time!", SubsequentState);
    Line WonLine => new("That's great work!\nThe more you practice here the better you'll be at sea!", SubsequentState);
}


public class InventoryIsFull_Dialogue : Dialogue
{
    readonly State SubsequentState;

    public InventoryIsFull_Dialogue(State subsequentState)
    {
        SubsequentState = subsequentState;
    }

    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= new Line(
        "Inventory is full...", SubsequentState);

}
