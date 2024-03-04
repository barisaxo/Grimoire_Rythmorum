using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Inventory;

public class AnglingToSeaTransition_State : State
{
    readonly State SubsequentState;
    readonly Sea.ISceneObject Obj;
    readonly bool Won;

    public AnglingToSeaTransition_State(State subsequentState, Sea.ISceneObject obj, bool won)
    {
        SubsequentState = subsequentState;
        Obj = obj;
        Won = won;
    }

    protected override void EngageState()
    {
        if (Won)
        {
            UnityEngine.Debug.Log(Data.ShipData.GetLevel(ShipData.DataItem.Fish));
            if (Data.FishData.InventoryIsFull(Data.ShipData.GetLevel(ShipData.DataItem.Fish)))
            {
                SetState(new DialogStart_State(new InventoryIsFull_Dialogue(SubsequentState)));
            }
            else
            {
                Data.FishData.IncreaseLevel(FishData.DataItem.SailFish);

                SetState(new DisplayItem_State(
                    Obj.Instantiator.ToInstantiate,
                    new DialogStart_State(new FoundItem_Dialogue(Obj, SubsequentState)),
                    clearCell: true));
            }
        }
        else SetState(new DialogStart_State(new FishGotAway_Dialogue(SubsequentState)));
    }


}
