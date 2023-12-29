using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        Scene.Io.HUD.Hud.GO.SetActive(false);
        Audio.BGMusic.Pause();
        Audio.Ambience.Pause();

        SetStateDirectly(new DialogStart_State(Dialogue));
    }

}
