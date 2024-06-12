using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class NavigateStarChart_Dialogue : Dialogue
{
    readonly Data.Standing Standing;
    int StandingMod => Data.Manager.Io.StandingData.GetLevel(Standing);
    public NavigateStarChart_Dialogue(Dialogue returnToDialogue, Speaker speaker, Data.Standing standing)
    {
        ReturnTo = returnToDialogue;
        Speaker = speaker;
        Standing = standing;
    }
    override public Dialogue Initiate()
    {
        FirstLine = MuscopaLine;
        return this;
    }
    readonly Dialogue ReturnTo;

    string MuscopaLineText => StandingMod switch
    {
        10 or 9 or 8 => "Happy to help. We have the best navigators.",
        7 or 6 => "It's good luck to help a fellow sailor in need!",
        5 or 4 or 3 => "Our merchants are expecting us soon, but I suppose one quick task...",
        _ => "Alright then, let's get on with it."
    };

    Line _muscopaLine;
    Line MuscopaLine => _muscopaLine ??= new Line(MuscopaLineText, new Response[]
        {
            // TwentyResponse,
            // OneHundredResponse,
            // TwoFiftyResponse,
            BackResponse,
        })
            .SetSpeaker(Speaker)
        ;

    // Response _twentyResponse;
    // Response TwentyResponse => _twentyResponse ??= new Response("Wager 20 gold", ReturnTo);

    // Response _oneHundredResponse;
    // Response OneHundredResponse => _oneHundredResponse ??= new Response("Wager 100 gold", ReturnTo);

    // Response _twoFiftyResponse;
    // Response TwoFiftyResponse => _twoFiftyResponse ??= new Response("Wager 250 gold", ReturnTo);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
}

public class UnlockGramophone_Dialogue : Dialogue
{
    readonly Data.Standing Standing;
    int StandingMod => Data.Manager.Io.StandingData.GetLevel(Standing);
    public UnlockGramophone_Dialogue(Dialogue returnToDialogue, Speaker speaker, Data.Standing standing)
    {
        ReturnTo = returnToDialogue;
        Speaker = speaker;
        Standing = standing;
    }
    override public Dialogue Initiate()
    {
        FirstLine = MuscopaLine;
        return this;
    }
    readonly Dialogue ReturnTo;

    string MuscopaLineText => StandingMod switch
    {
        10 or 9 or 8 => "Happy to help. We have the best navigators.",
        7 or 6 => "It's good luck to help a fellow sailor in need!",
        5 or 4 or 3 => "Our merchants are expecting us soon, but I suppose one quick task...",
        _ => "Alright then, let's get on with it."
    };

    Line _muscopaLine;
    Line MuscopaLine => _muscopaLine ??= new Line(MuscopaLineText, new Response[]
        {
            // TwentyResponse,
            // OneHundredResponse,
            // TwoFiftyResponse,
            BackResponse,
        })
            .SetSpeaker(Speaker)
        ;

    // Response _twentyResponse;
    // Response TwentyResponse => _twentyResponse ??= new Response("Wager 20 gold", ReturnTo);

    // Response _oneHundredResponse;
    // Response OneHundredResponse => _oneHundredResponse ??= new Response("Wager 100 gold", ReturnTo);

    // Response _twoFiftyResponse;
    // Response TwoFiftyResponse => _twoFiftyResponse ??= new Response("Wager 250 gold", ReturnTo);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
}
