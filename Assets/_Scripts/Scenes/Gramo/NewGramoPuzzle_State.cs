using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGramoPuzzle_State : State
{
    readonly State SubsequentState;
    readonly bool IsPractice;
    readonly Data.IGramophone Gramo;

    public NewGramoPuzzle_State(State subsequentState, Data.IGramophone gramo, bool isPractice)
    {
        Fade = true;
        SubsequentState = subsequentState;
        IsPractice = isPractice;
        Gramo = gramo;
    }

    protected override void PrepareState(Action callback)
    {
        Cam.Io.Camera.transform.SetPositionAndRotation(Vector3.up * 15, Quaternion.identity);
        base.PrepareState(callback);
    }
    protected override void EngageState()
    {
        SetState(IsPractice ? new GramoPractice_State(SubsequentState, Gramo) : new Gramo_State(SubsequentState, Gramo));
    }
}
