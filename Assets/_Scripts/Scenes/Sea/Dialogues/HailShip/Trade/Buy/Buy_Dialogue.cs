using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class Buy_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    readonly Data.Two.Standing Standing;

    int StandingMod => Data.Two.Manager.Io.StandingData.GetLevel(Standing);
    int Gold => Data.Two.Manager.Io.Inventory.GetLevel(new Data.Two.Gold());
    int Mats => Data.Two.Manager.Io.Inventory.GetLevel(new Data.Two.Material());
    int CurHP => Data.Two.Manager.Io.ActiveShip.GetLevel(new Data.Two.CurrentHitPoints());
    int MaxHP => Data.Two.Manager.Io.ActiveShip.GetLevel(new Data.Two.MaxHitPoints());
    float StandingsModifier => 1f + (float)(1f - (float)((float)StandingMod) / 9f);

    int smallAmount => (int)(MaxHP * .15f);
    readonly int matsPer = 3;
    readonly int goldPer = 25;

    public Buy_Dialogue(Dialogue returnTo, Speaker speaker, Data.Two.Standing standing)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
        Standing = standing;
    }

    public override Dialogue Initiate()
    {
        FirstLine = BuyLine;
        return base.Initiate();
    }

    readonly string BuyStarChart_RepText = "Buy Star Charts";
    readonly string BuyMat_RepText = "Buy Materials";
    readonly string BuyRations_RepText = "Buy Rations";

    Line _buyLine;
    Line BuyLine => _buyLine ??= new Line("Sure thing! What do you need?", BuyResponses)
        .SetSpeaker(Speaker)
        ;

    // Response _buyRepairs_response;
    // Response BuyRepairs_Response => _buyRepairs_response ??= new Response(Repair_RepText, new BuyRepairs_Dialogue(this, Speaker, Standing));


    Response _buyStarChart_response;
    Response BuyStarChart_Response => _buyStarChart_response ??= new Response(BuyStarChart_RepText, new BuyStarChart_Dialogue(this, Speaker, Standing));

    Response _buyMaterials_response;
    Response BuyMaterials_Response => _buyMaterials_response ??= new Response(BuyMat_RepText, new BuyMaterials_Dialogue(this, Speaker, Standing));

    Response _buyRations_response;
    Response BuyRations_Response => _buyRations_response ??= new Response(BuyRations_RepText, new BuyRations_Dialogue(this, Speaker, Standing));

    Response[] _BuyResponses;
    Response[] BuyResponses => _BuyResponses ??= GetBuyResponses();
    Response[] GetBuyResponses()
    {
        List<Response> responses = new();

        if (!(Gold < (5000f * StandingsModifier))) { responses.Add(BuyStarChart_Response); }
        if (!(Gold < (1000f * StandingsModifier))) { responses.Add(BuyMaterials_Response); }
        if (!(Gold < (500f * StandingsModifier))) { responses.Add(BuyRations_Response); }
        if (Gold < (500f * StandingsModifier) && !buyRepairs) responses.Add(CantResponse);
        else responses.Add(BackResponse);
        return responses.ToArray();
    }

    Response _cantResponse;
    Response CantResponse => _cantResponse ??= new Response("(not enough gold)", ReturnTo);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
    bool buyRepairs => CurHP < MaxHP && !(Gold < smallAmount * goldPer) && !(Mats < smallAmount * matsPer);
}
