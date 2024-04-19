using Dialog;
using Sea;

public class SeaToDialogueTransition_State : State
{
    public SeaToDialogueTransition_State(Dialogue dialogue)
    {
        Dialogue = dialogue;
    }
    readonly Dialogue Dialogue;

    protected override void EngageState()
    {
        Audio.Ambience.Pause();
        Audio.BGMusic.Pause();
        Audio.SFX.PlayOneShot(Assets.AlertHalfDim);
        Sea.WorldMapScene.Io.HUD.Disable();
        Sea.WorldMapScene.Io.Ship.ConfirmPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.Ship.AttackPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.MiniMap.Card.GO.SetActive(false);
        SetState(new DialogStart_State(Dialogue));
    }

}
