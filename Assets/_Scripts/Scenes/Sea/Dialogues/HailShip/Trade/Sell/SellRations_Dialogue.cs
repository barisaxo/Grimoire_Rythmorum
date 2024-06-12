using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class SellRations_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    readonly Data.Standing Standing;

    int StandingLevel => Data.Manager.Io.StandingData.GetLevel(Standing);
    readonly int Rations = Data.Manager.Io.Inventory.GetLevel(new Data.Ration());
    float StandingsModifier => 2f - (float)(1f - (float)((float)StandingLevel) / 9f);

    int largeGold => (int)(largeRation * 17.5f * StandingsModifier);
    int medGold => (int)(medRation * 20f * StandingsModifier);
    int smallGold => (int)(smallRation * 22.5f * StandingsModifier);

    int largeRation => 50;
    int medRation => 25;
    int smallRation => 5;

    public SellRations_Dialogue(Dialogue returnTo, Speaker speaker, Data.Standing standing)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
        Standing = standing;
    }

    public override Dialogue Initiate()
    {
        FirstLine = BuyRations_Line;
        return base.Initiate();
    }

    Line _RationsLine;
    Line BuyRations_Line => _RationsLine ??= new Line(Rations_LineText, RationsResponses)
        .SetSpeaker(Speaker);

    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    readonly string Rations_LineText = "How many Rations do you want to sell?";

    string RationsLarge_RepText => "-" + largeRation.ToString() + " rations; +" + largeGold.ToString() + " gold";
    string RationsMedium_RepText => "-" + medRation.ToString() + " rations; +" + medGold.ToString() + " gold";
    string RationsSmall_RepText => "-" + smallRation.ToString() + " rations; +" + smallGold.ToString() + " gold";

    Response _RationsLarge_response;
    Response RationsLarge_Response => _RationsLarge_response ??= new Response(RationsLarge_RepText, TradeComplete_Line)
        .SetPlayerAction(SellRationsLarge);

    Response _RationsMedium_response;
    Response RationsMedium_Response => _RationsMedium_response ??= new Response(RationsMedium_RepText, TradeComplete_Line)
        .SetPlayerAction(SellRationsMedium);

    Response _RationsSmall_response;
    Response RationsSmall_Response => _RationsSmall_response ??= new Response(RationsSmall_RepText, TradeComplete_Line)
        .SetPlayerAction(SellRationsSmall);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);

    Response[] _RationsResponses;
    Response[] RationsResponses => _RationsResponses ??= GetRationsResponses();
    Response[] GetRationsResponses()
    {
        List<Response> responses = new();

        if (!(Rations < largeRation)) { responses.Add(RationsLarge_Response); }
        if (!(Rations < medRation)) { responses.Add(RationsMedium_Response); }
        if (!(Rations < smallRation)) { responses.Add(RationsSmall_Response); }
        responses.Add(BackResponse);

        return responses.ToArray();
    }

    void SellRationsSmall()
    {
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Ration(), -smallRation);
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), smallGold);
    }

    void SellRationsMedium()
    {
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Ration(), -medRation);
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), medGold);
    }

    void SellRationsLarge()
    {
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Ration(), -largeRation);
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), largeGold);
    }

}
