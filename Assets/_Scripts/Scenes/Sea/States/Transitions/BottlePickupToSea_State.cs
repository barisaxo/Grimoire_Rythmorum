using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupToSea_State : State
{
    readonly State SubsequentState;

    public ItemPickupToSea_State(State subsequentState)
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
