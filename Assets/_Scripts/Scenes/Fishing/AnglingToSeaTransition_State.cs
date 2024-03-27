using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Player;
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
            DataManager.PlayerData.IncreaseLevel(PlayerData.DataItem.FishCaught);
            Obj.Inventoriable.AddRewards();

            SetState(
                new DisplayItem_State(
                    Obj.Instantiator.ToInstantiate,
                    new DialogStart_State(
                        new FoundItem_Dialogue(Obj.Inventoriable, SubsequentState)),
                        clearCell: true));
        }
        else
        {
            DataManager.PlayerData.IncreaseLevel(PlayerData.DataItem.FishLost);
            SetState(new DialogStart_State(new FishGotAway_Dialogue(SubsequentState)));
        }
    }


}
