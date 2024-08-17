using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class BuyMaterials_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    readonly Datum.Standing Standing;
    int StandingMod => Datum.Manager.Io.Standings.GetLevel(Standing);
    int Gold => Datum.Manager.Io.Inventory.GetLevel(new Datum.Gold());

    int matsCapacity => Datum.Manager.Io.ActiveShip.GetLevel(new Datum.MaterialStorage());

    float availableMatsSpace => 1f - ((float)Datum.Manager.Io.Inventory.GetLevel(new Datum.Material()) / matsCapacity);

    float StandingsModifier => 1f + (float)(1f - (float)((float)StandingMod) / 9f);


    int largeGold => (int)(largeMat * 15f * StandingsModifier);
    int medGold => (int)(medMat * 17.5f * StandingsModifier);
    int smallGold => (int)(smallMat * 20f * StandingsModifier);

    int largeMat => (int)((float)matsCapacity * .25f);
    int medMat => (int)((float)matsCapacity * .15f);
    int smallMat => (int)((float)matsCapacity * .1f);


    public BuyMaterials_Dialogue(Dialogue returnTo, Speaker speaker, Datum.Standing standing)
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


    Response _cantResponse;
    Response CantAffordResponse => _cantResponse ??= new Response("[not enough gold]", ReturnTo);

    Response _noSpace;
    Response InventoryFullResponse => _noSpace ??= new Response("[inventory full]", ReturnTo);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Back", ReturnTo);

    Response[] _materialResponses;
    Response[] MaterialResponses => _materialResponses ??= GetMaterialResponses();
    Response[] GetMaterialResponses()
    {
        List<Response> responses = new();

        if (!(Gold < largeGold) && availableMatsSpace >= .25f) { responses.Add(MaterialsLarge_Response); }
        if (!(Gold < medGold) && availableMatsSpace >= .15f) { responses.Add(MaterialsMedium_Response); }
        if (!(Gold < smallGold))
        {
            if (availableMatsSpace >= .10) responses.Add(MaterialsSmall_Response);
            else responses.Add(InventoryFullResponse);
        }
        else { responses.Add(CantAffordResponse); }
        responses.Add(BackResponse);

        return responses.ToArray();
    }

    void BuyMaterialsSmall()
    {
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Material(), smallMat);
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gold(), -smallGold);
    }
    void BuyMaterialsMedium()
    {
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Material(), medMat);
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gold(), -medGold);
    }
    void BuyMaterialsLarge()
    {
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Material(), largeMat);
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gold(), -largeGold);
    }

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

}
