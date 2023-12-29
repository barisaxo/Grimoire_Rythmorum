using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class BuyRations_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;

    int Coins => DataManager.Io.CharacterData.Coins;

    public BuyRations_Dialogue(Dialogue returnTo, Speaker speaker)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
    }

    public override Dialogue Initiate()
    {
        FirstLine = SellRations_Line;
        return base.Initiate();
    }

    Line _rationsLine;
    Line SellRations_Line => _rationsLine ??= new Line(Rations_LineText, RationsResponses)
        .SetSpeaker(Speaker);

    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    Response _rationsLarge_response;
    Response RationsLarge_Response => _rationsLarge_response ??= new Response(RationsLarge_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyRationsLarge)
        ;

    Response _rationsMedium_response;
    Response RationsMedium_Response => _rationsMedium_response ??= new Response(RationsMedium_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyRationsMedium)
        ;

    Response _rationsSmall_response;
    Response RationsSmall_Response => _rationsSmall_response ??= new Response(RationsSmall_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyRationsSmall)
        ;

    Response[] _rationsResponses;
    Response[] RationsResponses => _rationsResponses ??= GetRationsResponses();
    Response[] GetRationsResponses()
    {
        List<Response> responses = new();

        if (!(Coins < 250)) { responses.Add(RationsLarge_Response); }
        if (!(Coins < 150)) { responses.Add(RationsMedium_Response); }
        if (!(Coins < 50)) { responses.Add(RationsSmall_Response); }
        responses.Add(BackResponse);

        return responses.ToArray();
    }
    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    readonly string Rations_LineText = "How many rations do you want?";

    readonly string RationsLarge_RepText = "10 bottles [250 gold]";
    readonly string RationsMedium_RepText = "5 bottles [150 gold]";
    readonly string RationsSmall_RepText = "1 bottle [50 gold]";

    void BuyRationsSmall()
    {
        DataManager.Io.CharacterData.Rations += 1;
        DataManager.Io.CharacterData.Coins -= 50;
    }
    void BuyRationsMedium()
    {
        DataManager.Io.CharacterData.Rations += 5;
        DataManager.Io.CharacterData.Coins -= 150;
    }
    void BuyRationsLarge()
    {
        DataManager.Io.CharacterData.Rations += 10;
        DataManager.Io.CharacterData.Coins -= 250;
    }

}
