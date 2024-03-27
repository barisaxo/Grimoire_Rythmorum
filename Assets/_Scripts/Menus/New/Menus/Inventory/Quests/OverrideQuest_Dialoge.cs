using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;
using Data;

public class OverrideQuest_Dialogue : Dialogue
{
    // readonly IData Data;
    readonly State ContinueState;
    readonly State BackState;
    public OverrideQuest_Dialogue(State continueState, State backState)
    {
        // Data = data;
        ContinueState = continueState;
        BackState = backState;
    }

    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= new Line(
        "This quest is already active, this action will override it.", new Response[] { Yes, No });

    Response Yes => new("[Continue]", ContinueState);
    Response No => new("[Back]", BackState);
}
