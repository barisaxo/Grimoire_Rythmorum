using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Inventory;
using Data;


public class SeaToItemPickUp_State : State
{
    readonly Sea.ISceneObject Obj;
    readonly State SubsequentState;
    readonly IData iData;
    readonly ShipData.DataItem DataItem;

    public SeaToItemPickUp_State(State subsequentState, IData data, ShipData.DataItem dataItem, Sea.ISceneObject obj)
    {
        SubsequentState = subsequentState;
        Obj = obj;
        iData = data;
        DataItem = dataItem;
    }

    protected override void PrepareState(Action callback)
    {
        //todo disable sound etc
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        if (iData.InventoryIsFull(DataManager.ShipData.GetLevel(DataItem)))
        {
            SetState(new DialogStart_State(new InventoryIsFull_Dialogue(new ItemPickupToSea_State(SubsequentState))));
            return;
        }

        Obj.Questable.QuestComplete();
        Obj.Inventoriable.AddRewards();
        // Data.starChartsData.IncreaseLevel(StarChartsData.DataItem.Inverted7thChordsT);//todo difficulty levels

        SetState(new DisplayItem_State(
            Obj.Instantiator.ToInstantiate,
            new DialogStart_State(new FoundItem_Dialogue(Obj.Inventoriable, new ItemPickupToSea_State(SubsequentState))),
            clearCell: true));
    }

}
