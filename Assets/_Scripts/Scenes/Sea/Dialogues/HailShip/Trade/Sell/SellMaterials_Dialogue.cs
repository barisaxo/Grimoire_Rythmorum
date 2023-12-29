using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class SellMaterials_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    int Mats => DataManager.Io.CharacterData.Materials;

    public SellMaterials_Dialogue(Dialogue returnTo, Speaker speaker)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
    }

    public override Dialogue Initiate()
    {
        FirstLine = SellMaterials_Line;
        return base.Initiate();
    }

    readonly string MaterialsLarge_RepText = "50 [150 gold]";
    readonly string MaterialsMedium_RepText = "25 [80 gold]";
    readonly string MaterialsSmall_RepText = "5 [20 gold]";

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
        DataManager.Io.CharacterData.Materials -= 5;
        DataManager.Io.CharacterData.Coins += 20;
    }
    void SellMaterialsMedium()
    {
        DataManager.Io.CharacterData.Materials -= 25;
        DataManager.Io.CharacterData.Coins += 80;
    }
    void SellMaterialsLarge()
    {
        DataManager.Io.CharacterData.Materials -= 50;
        DataManager.Io.CharacterData.Coins += 150;
    }

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;
}
