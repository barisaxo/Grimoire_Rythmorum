using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaToInspection_State : State
{
    readonly State SubsequentState;
    public SeaToInspection_State(State subsequentState)
    {
        SubsequentState = subsequentState;
    }
    protected override void EngageState()
    {
        Sea.WorldMapScene.Io.HUD.Disable();
        Sea.WorldMapScene.Io.Ship.ConfirmPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.Ship.AttackPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.MiniMap.Card.GO.SetActive(false);

        SetState(
            new CameraPan_State(
                new SeaInspection_State(SubsequentState),
                SeaInspection_State.CameraRot,
                SeaInspection_State.CameraPos,
                4.5f
            ));
    }
}
