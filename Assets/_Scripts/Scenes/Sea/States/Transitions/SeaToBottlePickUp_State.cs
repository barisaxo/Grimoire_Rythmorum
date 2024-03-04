using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Inventory;


public class SeaToBottlePickUp_State : State
{
    readonly Sea.ISceneObject Obj;
    readonly State SubsequentState;

    public SeaToBottlePickUp_State(State subsequentState, Sea.ISceneObject obj)
    {
        SubsequentState = subsequentState;
        Obj = obj;
    }

    protected override void PrepareState(Action callback)
    {
        //todo disable sound etc
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        if (Data.starChartsData.InventoryIsFull(Data.ShipData.GetLevel(ShipData.DataItem.Bottle)))
        {
            SetState(new DialogStart_State(new InventoryIsFull_Dialogue(new BottlePickupToSea_State(SubsequentState))));
            return;
        }

        Data.starChartsData.IncreaseLevel(StarChartsData.DataItem.NotesT);//todo difficulty levels

        SetState(new DisplayItem_State(
            Obj.Instantiator.ToInstantiate,
            new DialogStart_State(new FoundItem_Dialogue(Obj, new BottlePickupToSea_State(SubsequentState))),
            clearCell: true));
    }

}
