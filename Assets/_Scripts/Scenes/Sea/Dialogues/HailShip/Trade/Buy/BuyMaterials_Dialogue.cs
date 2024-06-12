using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class BuyMaterials_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    readonly Data.Standing Standing;
    int StandingMod => Data.Manager.Io.StandingData.GetLevel(Standing);
    int Gold => Data.Manager.Io.Inventory.GetLevel(new Data.Gold());

    int matsCapacity => Data.Manager.Io.ActiveShip.GetLevel(new Data.MaterialStorage());

    float availableMatsSpace => 1f - ((float)Data.Manager.Io.Inventory.GetLevel(new Data.Material()) / matsCapacity);

    float StandingsModifier => 1f + (float)(1f - (float)((float)StandingMod) / 9f);


    int largeGold => (int)(largeMat * 15f * StandingsModifier);
    int medGold => (int)(medMat * 17.5f * StandingsModifier);
    int smallGold => (int)(smallMat * 20f * StandingsModifier);

    int largeMat => (int)((float)matsCapacity * .25f);
    int medMat => (int)((float)matsCapacity * .15f);
    int smallMat => (int)((float)matsCapacity * .1f);


    public BuyMaterials_Dialogue(Dialogue returnTo, Speaker speaker, Data.Standing standing)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
        Standing = standing;
    }

    public override Dialogue Initiate()
    {
        FirstLine = SellMaterials_Line;
        return base.Initiate();
    }

    string MaterialsLarge_RepText => "+" + largeMat.ToString() + " mats; " + "-" + largeGold + " gold";
    string MaterialsMedium_RepText => "+" + medMat.ToString() + " mats; " + "-" + medGold + " gold";
    string MaterialsSmall_RepText => "+" + smallMat.ToString() + " mats; " + "-" + smallGold + " gold";
    string Materials_LineText => "How much material do you want?";

    Line _matsLine;
    Line SellMaterials_Line => _matsLine ??= new Line(Materials_LineText, MaterialResponses)
        .SetSpeaker(Speaker)
        ;

    Response _materialsLarge_response;
    Response MaterialsLarge_Response => _materialsLarge_response ??= new Response(MaterialsLarge_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyMaterialsLarge);

    Response _materialsMedium_response;
    Response MaterialsMedium_Response => _materialsMedium_response ??= new Response(MaterialsMedium_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyMaterialsMedium);

    Response _materialsSmall_response;
    Response MaterialsSmall_Response => _materialsSmall_response ??= new Response(MaterialsSmall_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyMaterialsSmall);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);

    Response[] _materialResponses;
    Response[] MaterialResponses => _materialResponses ??= GetMaterialResponses();
    Response[] GetMaterialResponses()
    {
        List<Response> responses = new();

        if (!(Gold < largeGold) && availableMatsSpace >= .25f) { responses.Add(MaterialsLarge_Response); }
        if (!(Gold < medGold) && availableMatsSpace >= .15f) { responses.Add(MaterialsMedium_Response); }
        if (!(Gold < smallGold) && availableMatsSpace >= .10f) { responses.Add(MaterialsSmall_Response); }
        responses.Add(BackResponse);

        return responses.ToArray();
    }

    void BuyMaterialsSmall()
    {
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Material(), smallMat);
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), -smallGold);
    }
    void BuyMaterialsMedium()
    {
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Material(), medMat);
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), -medGold);
    }
    void BuyMaterialsLarge()
    {
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Material(), largeMat);
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), -largeGold);
    }

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

}
