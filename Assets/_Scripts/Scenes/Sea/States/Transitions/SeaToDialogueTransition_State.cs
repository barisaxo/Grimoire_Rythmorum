using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class SeaToDialogueTransition_State : State
{
    public SeaToDialogueTransition_State(Dialogue dialogue)
    {
        Dialogue = dialogue;
    }
    readonly Dialogue Dialogue;

    protected override void EngageState()
    {
        Audio.BGMusic.Pause();
        Audio.Ambience.Pause();

        SetStateDirectly(new DialogStart_State(Dialogue));
    }

}
