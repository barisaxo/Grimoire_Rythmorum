using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class Sell_Dialogue : Dialogue
{
    public Sell_Dialogue(Dialogue returnTo, Speaker speaker)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
    }

    public override Dialogue Initiate()
    {
        FirstLine = SellLine;
        return base.Initiate();
    }

    readonly Dialogue ReturnTo;

    int Gramos => DataManager.Io.CharacterData.Gramos;
    int Maps => DataManager.Io.CharacterData.Maps;
    int Mats => DataManager.Io.CharacterData.Materials;

    readonly string SellGramo_RepText = "Sell Gramophones";
    readonly string SellMat_RepText = "Sell Materials";
    readonly string SellMaps_RepText = "Sell Maps";

    readonly string Repair_LineText = "Sure thing, let's see what you've got.";
    Line _sellLine;
    Line SellLine => _sellLine ??= new Line(Repair_LineText, TradeResponses)
        .SetSpeaker(Speaker)
        ;

    Response _SellGramo_response;
    Response SellGramo_Response => _SellGramo_response ??= new Response(SellGramo_RepText, new SellGramo_Dialogue(this, Speaker));

    Response _SellMaterials_response;
    Response SellMaterials_Response => _SellMaterials_response ??= new Response(SellMat_RepText, new SellMaterials_Dialogue(this, Speaker));

    Response _SellMaps_response;
    Response SellMaps_Response => _SellMaps_response ??= new Response(SellMaps_RepText, new SellMaps_Dialogue(this, Speaker));

    Response[] _tradeResponses;
    Response[] TradeResponses => _tradeResponses ??= GetTradeResponses();
    Response[] GetTradeResponses()
    {
        List<Response> responses = new();

        if (!(Mats < 5)) { responses.Add(SellMaterials_Response); }
        if (!(Maps < 1)) { responses.Add(SellMaps_Response); }
        if (!(Gramos < 1)) { responses.Add(SellGramo_Response); }

        if (responses.Count == 0) responses.Add(CantResponse);
        else responses.Add(BackResponse);

        return responses.ToArray();
    }

    Response _cantResponse;
    Response CantResponse => _cantResponse ??= new Response("(nothing to sell)", ReturnTo);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
}
