using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class MuscopaWager_Dialogue : Dialogue
{
    public MuscopaWager_Dialogue(Dialogue returnToDialogue, Speaker speaker)
    {
        ReturnTo = returnToDialogue;
        Speaker = speaker;
    }
    override public Dialogue Initiate()
    {
        FirstLine = MuscopaLine;
        return this;
    }
    readonly Dialogue ReturnTo;

    string MuscopaLineText => UnityEngine.Random.Range(0, 4) switch
    {
        0 => "Fair warning: we are undefeated!",
        1 => "A fellow bard eh? Let's play!",
        2 => "Anytime is a good time for Muscopa.",
        _ => "Our merchants are expecting us soon, but I suppose one quick game..."
    };

    Line _muscopaLine;
    Line MuscopaLine => _muscopaLine ??= new Line(MuscopaLineText, new Response[]
        {
            TwentyResponse,
            OneHundredResponse,
            TwoFiftyResponse,
            BackResponse,
        })
            .SetSpeaker(Speaker)
        ;

    Response _twentyResponse;
    Response TwentyResponse => _twentyResponse ??= new Response("Wager 20 gold", ReturnTo);

    Response _oneHundredResponse;
    Response OneHundredResponse => _oneHundredResponse ??= new Response("Wager 100 gold", ReturnTo);

    Response _twoFiftyResponse;
    Response TwoFiftyResponse => _twoFiftyResponse ??= new Response("Wager 250 gold", ReturnTo);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
}
