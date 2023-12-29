using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class Bounty_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    public Bounty_Dialogue(Dialogue returnTo) { ReturnTo = returnTo; }
    public override Dialogue Initiate()
    {
        return base.Initiate();
    }


    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
}
