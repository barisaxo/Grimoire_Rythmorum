using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class BountyRewards_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;

    public BountyRewards_Dialogue(Dialogue returnTo, Speaker speaker)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
    }

    public override Dialogue Initiate()
    {
        return base.Initiate();
    }


}
