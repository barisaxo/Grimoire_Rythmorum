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
