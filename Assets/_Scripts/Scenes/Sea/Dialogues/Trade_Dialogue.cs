using System.Collections.Generic;
using Dialog;
using System;

public class Trade_Dialogue : Dialogue
{
    public Trade_Dialogue()
    {
        SpeakerIcon = new UnityEngine.Sprite[] { SeaScene.Io.NearNPCShip.Flag };
        SpeakerName = SeaScene.Io.NearNPCShip.Name;
        SpeakerColor = SeaScene.Io.NearNPCShip.FlagColor;
    }
    int Coins => DataManager.Io.CharacterData.Coins;
    int Mats => DataManager.Io.CharacterData.Materials;

    override public Dialogue Initiate() { FirstLine = Trade_Line; return this; }

    Response _buyRepairs_response;
    Response BuyRepairs_Response => _buyRepairs_response ??= new Response(Repair_RepText, SellRepairs_Line);

    Response _repairLarge_response;
    Response RepairLarge_Response => _repairLarge_response ??= new Response(RepairLarge_RepText, TradeComplete_Line)
        .SetPlayerAction(RepairLarge);

    Response _repairMedium_response;
    Response RepairMedium_Response => _repairMedium_response ??= new Response(RepairMedium_RepText, TradeComplete_Line)
        .SetPlayerAction(RepairMedium);

    Response _repairSmall_response;
    Response RepairSmall_Response => _repairSmall_response ??= new Response(RepairSmall_RepText, TradeComplete_Line)
        .SetPlayerAction(RepairSmall);

    Response[] _repairResponses;
    Response[] RepairResponses => _repairResponses ??= GetRepairResponses();
    Response[] GetRepairResponses()
    {
        List<Response> responses = new();

        if (!(Coins < 350 || Mats < 50)) { responses.Add(RepairLarge_Response); }
        if (!(Coins < 250 || Mats < 20)) { responses.Add(RepairMedium_Response); }
        if (!(Coins < 100 || Mats < 10)) { responses.Add(RepairSmall_Response); }
        responses.Add(Back_Response);

        return responses.ToArray();
    }

    #region BUY MATERIALS
    Response _buyMaterials_response;
    Response BuyMaterials_Response => _buyMaterials_response ??= new Response(BuyMat_RepText, SellMaterials_Line);

    Response _materialsLarge_response;
    Response MaterialsLarge_Response => _materialsLarge_response ??= new Response(MaterialsLarge_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyMaterialsLarge);

    Response _materialsMedium_response;
    Response MaterialsMedium_Response => _materialsMedium_response ??= new Response(MaterialsMedium_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyMaterialsMedium);

    Response _materialsSmall_response;
    Response MaterialsSmall_Response => _materialsSmall_response ??= new Response(MaterialsSmall_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyMaterialsSmall);

    Response[] _materialResponses;
    Response[] MaterialResponses => _materialResponses ??= GetMaterialResponses();
    Response[] GetMaterialResponses()
    {
        List<Response> responses = new();

        if (!(Coins < 250)) { responses.Add(MaterialsLarge_Response); }
        if (!(Coins < 150)) { responses.Add(MaterialsMedium_Response); }
        if (!(Coins < 100)) { responses.Add(MaterialsSmall_Response); }
        responses.Add(Back_Response);

        return responses.ToArray();
    }
    #endregion BUY MATERIALS

    #region BUY RATIONS
    Response _buyRations_response;
    Response BuyRations_Response => _buyRations_response ??= new Response(BuyRations_RepText, SellRations_Line);

    Response _rationsLarge_response;
    Response RationsLarge_Response => _rationsLarge_response ??= new Response(RationsLarge_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyRationsLarge);

    Response _rationsMedium_response;
    Response RationsMedium_Response => _rationsMedium_response ??= new Response(RationsMedium_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyRationsMedium);

    Response _rationsSmall_response;
    Response RationsSmall_Response => _rationsSmall_response ??= new Response(RationsSmall_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyRationsSmall);

    Response[] _rationsResponses;
    Response[] RationsResponses => _rationsResponses ??= GetRationsResponses();
    Response[] GetRationsResponses()
    {
        List<Response> responses = new();

        if (!(Coins < 250)) { responses.Add(RationsLarge_Response); }
        if (!(Coins < 150)) { responses.Add(RationsMedium_Response); }
        if (!(Coins < 50)) { responses.Add(RationsSmall_Response); }
        responses.Add(Back_Response);

        return responses.ToArray();
    }
    #endregion BUY RATIONS

    Response[] _tradeResponses;
    Response[] TradeResponses => _tradeResponses ??= GetTradeResponses();
    Response[] GetTradeResponses()
    {
        List<Response> responses = new();

        if (!(Coins < 100 || Mats < 10)) { responses.Add(BuyMaterials_Response); }
        if (!(Coins < 100)) { responses.Add(BuyRations_Response); }
        if (!(Coins < 50)) { responses.Add(BuyRepairs_Response); }
        responses.Add(Back_Response);

        return responses.ToArray();
    }

    Response _back_response;
    Response Back_Response => _back_response ??= new Response(Back_RepText, new ShipTask_Dialogue()
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerName(SpeakerName)
        .SetSpeakerColor(SpeakerColor));

    Line _tradeLine;
    Line Trade_Line => _tradeLine ??= new Line(Trade_LineText, TradeResponses)
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerName(SpeakerName)
        .SetSpeakerColor(SpeakerColor);

    Line _repairLine;
    Line SellRepairs_Line => _repairLine ??= new Line(Repair_LineText, RepairResponses)
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerName(SpeakerName)
        .SetSpeakerColor(SpeakerColor);

    Line _matsLine;
    Line SellMaterials_Line => _matsLine ??= new Line(Materials_LineText, MaterialResponses)
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerName(SpeakerName)
        .SetSpeakerColor(SpeakerColor);

    Line _rationsLine;
    Line SellRations_Line => _rationsLine ??= new Line(Rations_LineText, RationsResponses)
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerName(SpeakerName)
        .SetSpeakerColor(SpeakerColor);

    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerName(SpeakerName)
        .SetSpeakerColor(SpeakerColor)
        // .FadeToNextState()
        ;



    #region RESPONSE TEXTS
    readonly string Repair_RepText = "Repair ship";
    readonly string BuyMat_RepText = "Buy Materials";
    readonly string BuyRations_RepText = "Buy Rations";
    readonly string Back_RepText = "Never mind";

    // readonly string TradeGoods_RepText = "Trade goods";
    // readonly string PlayMuscopa_RepText = "Play Muscopa";
    // readonly string Attack_RepText = "Attack the ship";
    // readonly string Leave_RepText = "Leave";

    readonly string RepairLarge_RepText = "25 hull [350 gold, 50 mats]";
    readonly string RepairMedium_RepText = "10 hull [250 gold, 20 mats]";
    readonly string RepairSmall_RepText = "5 hull [150 gold, 10 mats]";

    readonly string MaterialsLarge_RepText = "25 mats [250 gold]";
    readonly string MaterialsMedium_RepText = "10 mats [150 gold]";
    readonly string MaterialsSmall_RepText = "5 mats [100 gold]";

    readonly string RationsLarge_RepText = "10 bottles [250 gold]";
    readonly string RationsMedium_RepText = "5 bottles [150 gold]";
    readonly string RationsSmall_RepText = "1 bottle [50 gold]";
    #endregion RESPONSE TEXTS



    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    readonly string Repair_LineText = "How much work do you want done?";
    readonly string Materials_LineText = "How much material do you want?";
    readonly string Rations_LineText = "How much rations do you want?";

    string Trade_LineText => UnityEngine.Random.Range(0, 4) switch
    {
        0 => "Alright, let's see what we've got...",
        1 => "Anything for our fellow sailor! Let's see...",
        2 => "Good timing! We just stocked up at the ports in Lydia.",
        _ => "Our merchants are expecting these wares, but we can spare a few items..."
    };

    void BuyMaterialsSmall()
    {
        DataManager.Io.CharacterData.Materials += 1;
        DataManager.Io.CharacterData.Coins -= 50;
    }
    void BuyMaterialsMedium()
    {
        DataManager.Io.CharacterData.Materials += 5;
        DataManager.Io.CharacterData.Coins -= 150;
    }
    void BuyMaterialsLarge()
    {
        DataManager.Io.CharacterData.Materials += 10;
        DataManager.Io.CharacterData.Coins -= 250;
    }

    void BuyRationsSmall()
    {
        DataManager.Io.CharacterData.Rations += 1;
        DataManager.Io.CharacterData.Coins -= 50;
    }
    void BuyRationsMedium()
    {
        DataManager.Io.CharacterData.Rations += 5;
        DataManager.Io.CharacterData.Coins -= 150;
    }
    void BuyRationsLarge()
    {
        DataManager.Io.CharacterData.Rations += 10;
        DataManager.Io.CharacterData.Coins -= 250;
    }

    void RepairSmall()
    {
        DataManager.Io.CharacterData.CurrentHealth += 5;
        DataManager.Io.CharacterData.Materials -= 150;
        DataManager.Io.CharacterData.Coins -= 5;
    }
    void RepairMedium()
    {
        DataManager.Io.CharacterData.CurrentHealth += 10;
        DataManager.Io.CharacterData.Materials -= 250;
        DataManager.Io.CharacterData.Coins -= 20;
    }
    void RepairLarge()
    {
        DataManager.Io.CharacterData.CurrentHealth += 25;
        DataManager.Io.CharacterData.Materials -= 350;
        DataManager.Io.CharacterData.Coins -= 50;
    }
}





// public Sprite[] Flag { get; private set; }
// public string SpeakerName { get; private set; }
// public Color SpeakerColor { get; private set; }
// public string ShipRegion { get; private set; }

// public Trade_Dialogue()
// {
//     // Character = charData;
//     // Flag = new Sprite[1] { flag };
//     // SpeakerColor = flagColor;
//     // SpeakerName = "[" + shipsRegion + " Ship]:\n";

//     // FirstLine = Trade_Line;
// }

// public Trade_Dialogue SetCharacterData(CharacterData charData) { Character = charData; return this; }
// public Trade_Dialogue SetFlagColor(Color flagColor) { SpeakerColor = flagColor; return this; }
// public Trade_Dialogue SetSpeakerIcon(Sprite flag) { SpeakerIcon = new Sprite[1] { flag }; return this; }
// public Trade_Dialogue SetShipRegion(string speakerName) { SpeakerName = speakerName; return this; }


/*

   case PlayerAction.BuyRationsLarge: BuyRations(10, 250); break;
   case PlayerAction.BuyRationsMedium: BuyRations(5, 150); break;
   case PlayerAction.BuyRationsSmall: BuyRations(1, 50); break;
   case PlayerAction.BuyMaterialsLarge: BuyMaterials(25, 250); break;
   case PlayerAction.BuyMaterialsMedium: BuyMaterials(10, 150); break;
   case PlayerAction.BuyMaterialsSmall: BuyMaterials(1, 50); break;
   case PlayerAction.BuySextant: BuySextant(); break;


*/