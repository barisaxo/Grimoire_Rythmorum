using System;

public class SeaToAnglingTransition_State : State
{
    public SeaToAnglingTransition_State(State subsequentState) => SubsequentState = subsequentState;
    readonly State SubsequentState;

    protected override void EngageState()
    {
        Sea.WorldMapScene.Io.HUD.Hide(-2);
        Sea.WorldMapScene.Io.Ship.ConfirmPopup.GO.SetActive(false);
        Audio.BGMusic.Pause();
        Audio.Ambience.Pause();

        SetState(new Angling_State(SubsequentState));
    }


}
