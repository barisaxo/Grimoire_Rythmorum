using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGramoPuzzle_State : State
{
    readonly State SubsequentState;
    readonly bool IsPractice;
    public NewGramoPuzzle_State(State subsequentState, bool isPractice)
    {
        Fade = true;
        SubsequentState = subsequentState;
        IsPractice = isPractice;
    }

    protected override void PrepareState(Action callback)
    {
        Cam.Io.Camera.transform.SetPositionAndRotation(Vector3.up * 15, Quaternion.identity);
        base.PrepareState(callback);
    }
    protected override void EngageState()
    {
        SetState(IsPractice ? new GramoPractice_State(SubsequentState) : new Gramo_State(SubsequentState));
    }
}
