using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class SellGramo_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    int Gramos => Datum.Manager.Io.Inventory.GetLevel(new Datum.Gramophone());

    public SellGramo_Dialogue(Dialogue returnTo, Speaker speaker, Datum.Standing standingData)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
        Standing = standingData;
    }

    public override Dialogue Initiate()
    {
        FirstLine = SellGramo_Line;
        return base.Initiate();
    }

    readonly Datum.Standing Standing;


    int StandingLevel => Datum.Manager.Io.Standings.GetLevel(Standing);
    readonly int Mats = Datum.Manager.Io.Inventory.GetLevel(new Datum.Material());
    float StandingsModifier => 2f - (float)(1f - (float)((float)StandingLevel) / 9f);

    static readonly int largeAmount = 10000;
    static readonly int medAmount = 6450;
    static readonly int smallAmount = 2500;

    int largeGold => (int)(largeAmount * StandingsModifier);
    int medGold => (int)(medAmount * StandingsModifier);
    int smallGold => (int)(smallAmount * StandingsModifier);

    readonly string GramoLarge_RepText = "5 [" + largeAmount + " gold]";
    readonly string GramoMedium_RepText = "3 [" + medAmount + " gold]";
    readonly string GramoSmall_RepText = "1 [" + smallAmount + " gold]";
    readonly string Gramo_LineText = "How many gramophones do you want to sell?";

    Line _gramoLine;
    Line SellGramo_Line => _gramoLine ??= new Line(Gramo_LineText, MaterialResponses)
        .SetSpeaker(Speaker)
        ;

    Response _GramoLarge_response;
    Response GramoLarge_Response => _GramoLarge_response ??= new Response(GramoLarge_RepText, TradeComplete_Line)
        .SetPlayerAction(SellGramoLarge);

    Response _GramoMedium_response;
    Response GramoMedium_Response => _GramoMedium_response ??= new Response(GramoMedium_RepText, TradeComplete_Line)
        .SetPlayerAction(SellGramoMedium);

    Response _GramoSmall_response;
    Response GramoSmall_Response => _GramoSmall_response ??= new Response(GramoSmall_RepText, TradeComplete_Line)
        .SetPlayerAction(SellGramoSmall);


    Response[] _materialResponses;
    Response[] MaterialResponses => _materialResponses ??= GetMaterialResponses();
    Response[] GetMaterialResponses()
    {
        List<Response> responses = new();

        if (!(Gramos < 5)) { responses.Add(GramoLarge_Response); }
        if (!(Gramos < 3)) { responses.Add(GramoMedium_Response); }
        if (!(Gramos < 1)) { responses.Add(GramoSmall_Response); }
        responses.Add(BackResponse);

        return responses.ToArray();
    }

    void SellGramoSmall()
    {
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gramophone(), -1);
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gold(), smallAmount);
    }
    void SellGramoMedium()
    {
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gramophone(), -3);
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gold(), medAmount);
    }
    void SellGramoLarge()
    {
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gramophone(), -5);
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gold(), largeAmount);
    }

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
}
