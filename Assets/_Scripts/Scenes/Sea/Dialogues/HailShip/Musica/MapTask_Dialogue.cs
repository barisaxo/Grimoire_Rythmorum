using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class MapTask_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;

    public MapTask_Dialogue(Dialogue returnTo, Speaker speaker)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
    }

    public override Dialogue Initiate()
    {
        FirstLine = ConfirmLine;
        return base.Initiate();
    }

    Line _confirmLine;
    Line ConfirmLine => _confirmLine ??= new Line("We have some excellent cartographers. 200 gold and we can deciphering that map for you in no time.",
            new Response[] { YesResponse, BackResponse, })
        .SetSpeaker(Speaker)
        ;

    Response _yesResponse;
    Response YesResponse => _yesResponse ??= new Response("Ok [-200 gold]", TradeComplete_Line)
        .SetPlayerAction(OpenMap);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    Line _tradeCompleteLine;//TODO sail away to new contents dialogue to seaScene.
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    void OpenMap()
    {

    }
}
