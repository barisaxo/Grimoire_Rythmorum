using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePickupToSea_State : State
{
    readonly State SubsequentState;

    public BottlePickupToSea_State(State subsequentState)
    {
        SubsequentState = subsequentState;
    }

    protected override void PrepareState(Action callback)
    {
        //todo set bg music etc.

        base.PrepareState(callback);
    }
    protected override void EngageState()
    {
        SetState(SubsequentState);
    }
}
