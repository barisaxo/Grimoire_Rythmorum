using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class BuyMaterials_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    int Coins => DataManager.Io.CharacterData.Coins;

    public BuyMaterials_Dialogue(Dialogue returnTo, Speaker speaker)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
    }

    public override Dialogue Initiate()
    {
        FirstLine = SellMaterials_Line;
        return base.Initiate();
    }

    readonly string MaterialsLarge_RepText = "25 mats [250 gold]";
    readonly string MaterialsMedium_RepText = "10 mats [150 gold]";
    readonly string MaterialsSmall_RepText = "5 mats [100 gold]";
    readonly string Materials_LineText = "How much material do you want?";

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

        if (!(Coins < 250)) { responses.Add(MaterialsLarge_Response); }
        if (!(Coins < 150)) { responses.Add(MaterialsMedium_Response); }
        if (!(Coins < 100)) { responses.Add(MaterialsSmall_Response); }
        responses.Add(BackResponse);

        return responses.ToArray();
    }

    void BuyMaterialsSmall()
    {
        DataManager.Io.CharacterData.Materials += 5;
        DataManager.Io.CharacterData.Coins -= 100;
    }
    void BuyMaterialsMedium()
    {
        DataManager.Io.CharacterData.Materials += 10;
        DataManager.Io.CharacterData.Coins -= 150;
    }
    void BuyMaterialsLarge()
    {
        DataManager.Io.CharacterData.Materials += 25;
        DataManager.Io.CharacterData.Coins -= 250;
    }

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

}
