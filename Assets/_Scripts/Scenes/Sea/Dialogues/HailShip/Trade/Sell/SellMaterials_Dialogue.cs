using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class SellMaterials_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    readonly Data.Two.Standing Standing;


    int StandingLevel => Data.Two.Manager.Io.StandingData.GetLevel(Standing);
    readonly int Mats = Data.Two.Manager.Io.Inventory.GetLevel(new Data.Two.Material());
    float StandingsModifier => 2f - (float)(1f - (float)((float)StandingLevel) / 9f);

    int largeGold => (int)(largeMat * 7f * StandingsModifier);
    int medGold => (int)(medMat * 8.5f * StandingsModifier);
    int smallGold => (int)(smallMat * 10f * StandingsModifier);

    int largeMat => 150;
    int medMat => 75;
    int smallMat => 25;

    public SellMaterials_Dialogue(Dialogue returnTo, Speaker speaker, Data.Two.Standing standing)
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

    string MaterialsLarge_RepText => "-" + largeMat.ToString() + " mats; +" + largeGold.ToString() + " gold";
    string MaterialsMedium_RepText => "-" + medMat.ToString() + " mats; +" + medGold.ToString() + " gold";
    string MaterialsSmall_RepText => "-" + smallMat.ToString() + " mats; +" + smallGold.ToString() + " gold";

    readonly string Materials_LineText = "How much material do you want to sell?";

    Line _matsLine;
    Line SellMaterials_Line => _matsLine ??= new Line(Materials_LineText, MaterialResponses)
       .SetSpeaker(Speaker)
       ;


    Response _materialsLarge_response;
    Response MaterialsLarge_Response => _materialsLarge_response ??= new Response(MaterialsLarge_RepText, TradeComplete_Line)
        .SetPlayerAction(SellMaterialsLarge);

    Response _materialsMedium_response;
    Response MaterialsMedium_Response => _materialsMedium_response ??= new Response(MaterialsMedium_RepText, TradeComplete_Line)
        .SetPlayerAction(SellMaterialsMedium);

    Response _materialsSmall_response;
    Response MaterialsSmall_Response => _materialsSmall_response ??= new Response(MaterialsSmall_RepText, TradeComplete_Line)
        .SetPlayerAction(SellMaterialsSmall);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);

    Response[] _materialResponses;
    Response[] MaterialResponses => _materialResponses ??= GetMaterialResponses();
    Response[] GetMaterialResponses()
    {
        List<Response> responses = new();

        if (!(Mats < 50)) { responses.Add(MaterialsLarge_Response); }
        if (!(Mats < 25)) { responses.Add(MaterialsMedium_Response); }
        if (!(Mats < 5)) { responses.Add(MaterialsSmall_Response); }
        responses.Add(BackResponse);

        return responses.ToArray();
    }

    void SellMaterialsSmall()
    {
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Material(), -smallMat);
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Gold(), smallGold);
    }
    void SellMaterialsMedium()
    {
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Material(), -medMat);
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Gold(), medGold);
    }
    void SellMaterialsLarge()
    {
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Material(), -largeMat);
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Gold(), largeGold);
    }

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;
}
