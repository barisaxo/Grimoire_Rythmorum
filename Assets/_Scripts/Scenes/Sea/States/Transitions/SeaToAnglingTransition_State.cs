using System;

public class SeaToAnglingTransition_State : State
{
    public SeaToAnglingTransition_State(State subsequentState) => SubsequentState = subsequentState;
    readonly State SubsequentState;

    protected override void EngageState()
    {
        Sea.WorldMapScene.Io.HUD.Disable();
        Sea.WorldMapScene.Io.Ship.ConfirmPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.MiniMap.Card.GO.SetActive(false);
        Audio.BGMusic.Pause();
        Audio.Ambience.Pause();

        SetState(new Angling_State(SubsequentState));
    }


}
