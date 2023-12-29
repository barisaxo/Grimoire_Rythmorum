using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class OpenGramoTask_Dialogue : Dialogue
{
    public OpenGramoTask_Dialogue(Dialogue returnTo, Speaker speaker)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
    }

    public override Dialogue Initiate()
    {
        FirstLine = ConfirmLine;
        return base.Initiate();
    }

    readonly Dialogue ReturnTo;

    Line _confirmLine;
    Line ConfirmLine => _confirmLine ??= new Line("That will be 500 gold please.",
            new Response[] { YesResponse, BackResponse, })
        .SetSpeaker(Speaker)
        ;

    Response _yesResponse;
    Response YesResponse => _yesResponse ??= new Response("Ok [-500 gold]", TradeComplete_Line)
        .SetPlayerAction(OpenGramo);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);


    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    Line _tradeCompleteLine;//TODO sail away to new gramo contents dialogue to seaScene.
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    void OpenGramo() { }
}
