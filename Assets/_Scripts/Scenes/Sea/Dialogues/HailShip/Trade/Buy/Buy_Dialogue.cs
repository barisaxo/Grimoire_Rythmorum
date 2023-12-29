using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class Buy_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;

    int Coins => DataManager.Io.CharacterData.Coins;
    int Mats => DataManager.Io.CharacterData.Materials;

    public Buy_Dialogue(Dialogue returnTo, Speaker speaker)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
    }

    public override Dialogue Initiate()
    {
        FirstLine = BuyLine;
        return base.Initiate();
    }

    readonly string Repair_RepText = "Buy Repairs";
    readonly string BuyMat_RepText = "Buy Materials";
    readonly string BuyRations_RepText = "Buy Rations";

    Line _buyLine;
    Line BuyLine => _buyLine ??= new Line("Sure thing! What do you need?", BuyResponses)
        .SetSpeaker(Speaker)
        ;

    Response _buyRepairs_response;
    Response BuyRepairs_Response => _buyRepairs_response ??= new Response(Repair_RepText, new BuyRepairs_Dialogue(this, Speaker));

    Response _buyMaterials_response;
    Response BuyMaterials_Response => _buyMaterials_response ??= new Response(BuyMat_RepText, new BuyMaterials_Dialogue(this, Speaker));

    Response _buyRations_response;
    Response BuyRations_Response => _buyRations_response ??= new Response(BuyRations_RepText, new BuyRations_Dialogue(this, Speaker));

    Response[] _BuyResponses;
    Response[] BuyResponses => _BuyResponses ??= GetBuyResponses();
    Response[] GetBuyResponses()
    {
        List<Response> responses = new();

        if (!(Coins < 100 || Mats < 10)) { responses.Add(BuyMaterials_Response); }
        if (!(Coins < 100)) { responses.Add(BuyRations_Response); }
        if (!(Coins < 50)) { responses.Add(BuyRepairs_Response); }
        if (Coins < 50) responses.Add(CantResponse);
        else responses.Add(BackResponse);

        return responses.ToArray();
    }

    Response _cantResponse;
    Response CantResponse => _cantResponse ??= new Response("(not enough gold)", ReturnTo);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
}
