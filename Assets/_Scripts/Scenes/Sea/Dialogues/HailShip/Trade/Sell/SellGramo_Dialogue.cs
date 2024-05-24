using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class SellGramo_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    int Gramos => Data.Two.Manager.Io.Inventory.GetLevel(new Data.Two.GramophoneStorage());

    public SellGramo_Dialogue(Dialogue returnTo, Speaker speaker)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
    }

    public override Dialogue Initiate()
    {
        FirstLine = SellGramo_Line;
        return base.Initiate();
    }

    readonly string GramoLarge_RepText = "5 [400 gold]";
    readonly string GramoMedium_RepText = "3 [275 gold]";
    readonly string GramoSmall_RepText = "1 [100 gold]";
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
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.GramophoneStorage(), -1);
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Gold(), 100);
    }
    void SellGramoMedium()
    {
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.GramophoneStorage(), -3);
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Gold(), 275);
    }
    void SellGramoLarge()
    {
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.GramophoneStorage(), -5);
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Gold(), 400);
    }

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
}
