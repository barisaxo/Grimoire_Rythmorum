using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class BuyRations_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    readonly Datum.Standing Standing;

    int StandingMod => Datum.Manager.Io.Standings.GetLevel(Standing);
    int Coins => Datum.Manager.Io.Inventory.GetLevel(new Datum.Gold());
    int rationsCapacity => Datum.Manager.Io.ActiveShip.GetLevel(new Datum.RationStorage());

    float availableRationsSpace => 1f -
        ((float)Datum.Manager.Io.Inventory.GetLevel(new Datum.Ration()) /
        (float)rationsCapacity);

    float StandingsModifier => 1f + (float)(1f - (float)((float)StandingMod) / 9f);

    int largeGold => (int)(largeRation * 35f * StandingsModifier);
    int medGold => (int)(medRation * 42.5f * StandingsModifier);
    int smallGold => (int)(smallRation * 50f * StandingsModifier);


    int largeRation => (int)((float)rationsCapacity * .5f);
    int medRation => (int)((float)rationsCapacity * .25f);
    int smallRation => (int)((float)rationsCapacity * .15f);


    public BuyRations_Dialogue(Dialogue returnTo, Speaker speaker, Datum.Standing standing)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
        Standing = standing;
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

    Response _cantResponse;
    Response CantAffordResponse => _cantResponse ??= new Response("[not enough gold]", ReturnTo);

    Response _noSpace;
    Response InventoryFullResponse => _noSpace ??= new Response("[inventory full]", ReturnTo);


    Response[] _rationsResponses;
    Response[] RationsResponses => _rationsResponses ??= GetRationsResponses();
    Response[] GetRationsResponses()
    {
        List<Response> responses = new();

        if (!(Coins < largeGold) && availableRationsSpace < .75f) { responses.Add(RationsLarge_Response); }
        if (!(Coins < medGold) && availableRationsSpace < .85f) { responses.Add(RationsMedium_Response); }
        if (!(Coins < smallGold))
        {
            if (availableRationsSpace < 9f) responses.Add(RationsSmall_Response);
            else responses.Add(InventoryFullResponse);
        }
        else responses.Add(CantAffordResponse);

        responses.Add(BackResponse);

        return responses.ToArray();
    }
    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    readonly string Rations_LineText = "How many rations do you want?";

    string RationsLarge_RepText => "+" + largeRation.ToString() + " rations; -" + largeGold.ToString() + " gold";
    string RationsMedium_RepText => "+" + medRation.ToString() + " rations; -" + medGold.ToString() + " gold";
    string RationsSmall_RepText => "+" + smallRation.ToString() + " rations; -" + smallGold.ToString() + " gold";

    void BuyRationsSmall()
    {
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Ration(), smallRation);
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gold(), -smallGold);
    }
    void BuyRationsMedium()
    {
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Ration(), medRation);
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gold(), -medGold);
    }
    void BuyRationsLarge()
    {
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Ration(), largeRation);
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gold(), -largeGold);
    }

}
