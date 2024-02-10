using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class FoundBottle_Dialogue : Dialogue
{
    readonly State SubsequentState;

    public FoundBottle_Dialogue(State subsequentState)
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
        "You found a scroll in this bottle!" +
        "\n[A Constellation Chart has been added to your inventory]",
        SubsequentState)
        ;

}
