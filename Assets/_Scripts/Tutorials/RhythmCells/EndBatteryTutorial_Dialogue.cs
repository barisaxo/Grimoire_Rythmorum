using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;
public class EndBatteryTutorial_Dialogue : Dialogue
{
    public EndBatteryTutorial_Dialogue(Dialogue previousDialogue, Dialogue nextDialogue, int results)
    {
        PreviousDialogue = previousDialogue;
        NextDialogue = nextDialogue;
        Results = results;
    }

    readonly Dialogue PreviousDialogue;
    readonly Dialogue NextDialogue;
    readonly int Results;

    public override Dialogue Initiate()
    {
        return base.Initiate();
    }

}
