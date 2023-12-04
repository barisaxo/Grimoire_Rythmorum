using UnityEngine;
using Dialog;

public class HailShip_Dialogue : Dialogue
{
    // int Coins => DataManager.Io.CharacterData.Coins;
    // int Mats =>  DataManager.Io.CharacterData.Materials;
    public HailShip_Dialogue()
    {
        SpeakerIcon = new UnityEngine.Sprite[] { SeaScene.Io.NearNPCShip.Flag };
        SpeakerName = SeaScene.Io.NearNPCShip.Name;
        SpeakerColor = SeaScene.Io.NearNPCShip.FlagColor;
    }
    // public HailShip_Dialogue(Color flagColor, Sprite flag, Sprite ship, MusicTheory.RegionalMode shipsRegion)
    // {
    //     // Character = charData;
    //     SpeakerIcon = new Sprite[1] { flag };
    //     SpeakerColor = flagColor;
    //     SpeakerName = "[" + shipsRegion + " Ship]:\n";
    //     ShipSprite = ship;
    //     // StartSound = shipsRegion.RegionalTonality();
    // }
    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        return base.Initiate();
    }
    string Hail_LineText => UnityEngine.Random.Range(0, 4) switch
    {
        0 => "Ahoy! We're on a tight schedule.",
        1 => "The weather favors us this day.",
        2 => "You seem trust worthy enough...",
        _ => "We'll work with you this once, just don't try anything..."
    };
    string Trade_LineText => UnityEngine.Random.Range(0, 4) switch
    {
        0 => "Alright, let's see what we've got...",
        1 => "Anything for our fellow sailor! Let's see...",
        2 => "Good timing! We just stocked up at the ports in Lydia.",
        _ => "Our merchants are expecting these wares, but we can spare a few items..."
    };

    // readonly string TradeGoods_RepText = "Trade goods";
    // readonly string PlayMuscopa_RepText = "Play Muscopa";
    // readonly string Attack_RepText = "Attack the ship";
    // readonly string Leave_RepText = "Leave";

    Line _startLine;
    Line StartLine => _startLine ??= new Line(Hail_LineText, new ShipTask_Dialogue())
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerName(SpeakerName)
        .SetSpeakerColor(SpeakerColor);

}


// string Muscopa_LineText => UnityEngine.Random.Range(0, 4) switch
// {
//     0 => "Fair warning: we are undefeated!",
//     1 => "A fellow bard eh? Let's play!",
//     2 => "Anytime is a good time for Muscopa.",
//     _ => "Our merchants are expecting us soon, but I suppose one quick game..."
// };
// string Attack_LineText => UnityEngine.Random.Range(0, 4) switch
// {
//     0 => "What are you doing!?\nAll hands prepare for battle!",
//     1 => "Pirates!? I should have known.\nAll right sailors, we have some pirates to sink!",
//     2 => "We wont go down without a fight!",
//     _ => "Despicable..."
// };

// string Leave_LineText => UnityEngine.Random.Range(0, 4) switch
// {
//     0 => "Safe journey!",
//     1 => "Until next time!",
//     2 => "Keep an eye out for pirates, they can look just like trade ships.",
//     _ => "Then why did you... I... We have work to do!"
// };

// readonly string Repair_LineText = "How much work do you want done?";
// readonly string Materials_LineText = "How much material do you want?";
// readonly string Rations_LineText = "How much rations do you want?";

// readonly string OneTask_LineText = "We've got time for one task, so what will it be?";

// readonly string TransactionComplete_LineText = "Good deal! Until next time!";
// #region RESPONSE TEXTS
// readonly string Repair_RepText = "Repair ship";
// readonly string BuyMat_RepText = "Buy Materials";
// readonly string BuyRations_RepText = "Buy Rations";
// readonly string Back_RepText = "Never mind";
// readonly string RepairLarge_RepText = "25 hull [350 gold, 50 mats]";
// readonly string RepairMedium_RepText = "10 hull [250 gold, 20 mats]";
// readonly string RepairSmall_RepText = "5 hull [150 gold, 10 mats]";

// readonly string MaterialsLarge_RepText = "25 mats [250 gold]";
// readonly string MaterialsMedium_RepText = "10 mats [150 gold]";
// readonly string MaterialsSmall_RepText = "5 mats [100 gold]";

// readonly string RationsLarge_RepText = "10 bottles [250 gold]";
// readonly string RationsMedium_RepText = "5 bottles [150 gold]";
// readonly string RationsSmall_RepText = "1 bottles [50 gold]";
// #endregion RESPONSE TEXTS
// Response _buyRepairs_response;
// Response BuyRepairs_Response => _buyRepairs_response ??= new Response(Repair_RepText, SellRepairs_Line);

// Response _repairLarge_response;
// Response RepairLarge_Response => _repairLarge_response ??= new Response(RepairLarge_RepText, TradeComplete_Line)
//     .SetPlayerAction(PlayerAction.RepairLarge);

// Response _repairMedium_response;
// Response RepairMedium_Response => _repairMedium_response ??= new Response(RepairMedium_RepText, TradeComplete_Line)
//     .SetPlayerAction(PlayerAction.RepairMedium);

// Response _repairSmall_response;
// Response RepairSmall_Response => _repairSmall_response ??= new Response(RepairSmall_RepText, TradeComplete_Line)
//     .SetPlayerAction(PlayerAction.RepairSmall);

// Response[] _repairResponses;
// Response[] RepairResponses => _repairResponses ??= GetRepairResponses();
// Response[] GetRepairResponses()
// {
//     List<Response> responses = new();

//     if (!(Coins < 350 || Mats < 50)) { responses.Add(RepairLarge_Response); }
//     if (!(Coins < 250 || Mats < 20)) { responses.Add(RepairMedium_Response); }
//     if (!(Coins < 100 || Mats < 10)) { responses.Add(RepairSmall_Response); }
//     responses.Add(Back_Response);

//     return responses.ToArray();
// }
// Response _buyMaterials_response;
// Response BuyMaterials_Response => _buyMaterials_response ??= new Response(BuyMat_RepText, SellMaterials_Line);

// Response _materialsLarge_response;
// Response MaterialsLarge_Response => _materialsLarge_response ??= new Response(MaterialsLarge_RepText, TradeComplete_Line)
//     .SetPlayerAction(PlayerAction.BuyMaterialsLarge);

// Response _materialsMedium_response;
// Response MaterialsMedium_Response => _materialsMedium_response ??= new Response(MaterialsMedium_RepText, TradeComplete_Line)
//     .SetPlayerAction(PlayerAction.BuyMaterialsMedium);

// Response _materialsSmall_response;
// Response MaterialsSmall_Response => _materialsSmall_response ??= new Response(MaterialsSmall_RepText, TradeComplete_Line)
//     .SetPlayerAction(PlayerAction.BuyMaterialsSmall);

// Response[] _materialResponses;
// Response[] MaterialResponses => _materialResponses ??= GetMaterialResponses();
// Response[] GetMaterialResponses()
// {
//     List<Response> responses = new();

//     if (!(Coins < 250)) { responses.Add(MaterialsLarge_Response); }
//     if (!(Coins < 150)) { responses.Add(MaterialsMedium_Response); }
//     if (!(Coins < 100)) { responses.Add(MaterialsSmall_Response); }
//     responses.Add(Back_Response);

//     return responses.ToArray();
// }
// Response _buyRations_response;
// Response BuyRations_Response => _buyRations_response ??= new Response(BuyRations_RepText, SellRations_Line);

// Response _rationsLarge_response;
// Response RationsLarge_Response => _rationsLarge_response ??= new Response(RationsLarge_RepText, TradeComplete_Line)
//     .SetPlayerAction(PlayerAction.BuyRationsLarge);

// Response _rationsMedium_response;
// Response RationsMedium_Response => _rationsMedium_response ??= new Response(RationsMedium_RepText, TradeComplete_Line)
//     .SetPlayerAction(PlayerAction.BuyRationsMedium);

// Response _rationsSmall_response;
// Response RationsSmall_Response => _rationsSmall_response ??= new Response(RationsSmall_RepText, TradeComplete_Line)
//     .SetPlayerAction(PlayerAction.BuyRationsSmall);

// Response[] _rationsResponses;
// Response[] RationsResponses => _rationsResponses ??= GetRationsResponses();
// Response[] GetRationsResponses()
// {
//     List<Response> responses = new();

//     if (!(Coins < 250)) { responses.Add(RationsLarge_Response); }
//     if (!(Coins < 150)) { responses.Add(RationsMedium_Response); }
//     if (!(Coins < 50)) { responses.Add(RationsSmall_Response); }
//     responses.Add(Back_Response);

//     return responses.ToArray();
// }

// Response[] _tradeResponses;
// Response[] TradeResponses => _tradeResponses ??= GetTradeResponses();
// Response[] GetTradeResponses()
// {
//     List<Response> responses = new();

//     if (!(Coins < 100 || Mats < 10)) { responses.Add(BuyMaterials_Response); }
//     if (!(Coins < 100)) { responses.Add(BuyRations_Response); }
//     if (!(Coins < 50)) { responses.Add(BuyRepairs_Response); }

//     return responses.ToArray();
// }

// Response _trade_response;
// Response Trade_Response => _trade_response ??= new Response(TradeGoods_RepText, DialogueName.Trade);

// Response _muscopa_response;
// Response Muscopa_Response => _muscopa_response ??= new Response(PlayMuscopa_RepText, Muscopa_Line);

// Response _attack_response;
// Response Attack_Response => _attack_response ??= new Response(Attack_RepText, Attack_Line);

// Response _leave_response;
// Response Leave_Response => _leave_response ??= new Response(Leave_RepText, Leave_Line);

// // Response _back_response;
// // Response Back_Response => _back_response ??= new Response(Back_RepText, OneTaskLine);

// Line _tradeLine;
// Line Trade_Line => _tradeLine ??= new Line(Trade_LineText, DialogueName.Trade)//new Trade_Dialogue(Character, SpeakerColor, Flag[0], ShipsRegion)
//     .SetSpeakerIcon(Flag)
//     .SetSpeakerName(ShipName);

// Line _repairLine;
// Line SellRepairs_Line => _repairLine ??= new Line(Repair_LineText, RepairResponses)
//     .SetSpeakerIcon(Flag)
//     .SetSpeakerName(ShipName);

// Line _matsLine;
// Line SellMaterials_Line => _matsLine ??= new Line(Materials_LineText, MaterialResponses)
//     .SetSpeakerIcon(Flag)
//     .SetSpeakerName(ShipName);

// Line _rationsLine;
// Line SellRations_Line => _rationsLine ??= new Line(Rations_LineText, RationsResponses)
//     .SetSpeakerIcon(Flag)
//     .SetSpeakerName(ShipName);

// Line _muscopaLine;
// Line Muscopa_Line => _muscopaLine ??= new Line(Muscopa_LineText, MacroState.Muscopa)
//     .SetSpeakerIcon(Flag)
//     .SetSpeakerName(ShipName);

// Line _attackLine;
// Line Attack_Line => _attackLine ??= new Line(Attack_LineText, MacroState.AttackTradeShip)
//      .SetSpeakerIcon(Flag)
//      .SetSpeakerName(ShipName);

// Line _oneTaskLine;
// Line OneTaskLine => _oneTaskLine ??= new Line(OneTask_LineText, new Response[4]
//     {
//         Trade_Response,
//         Muscopa_Response,
//         Attack_Response,
//         Leave_Response,
//     })
//     .SetSpeakerIcon(Flag)
//     .SetSpeakerColor(SpeakerColor)
//     .SetSpeakerName(ShipName);

// Line _transCompleteLine;
// Line TradeComplete_Line => _transCompleteLine ??= new Line(TransactionComplete_LineText, MacroState.Exploration)
//     .SetSpeakerIcon(Flag)
//     .SetSpeakerName(ShipName);

// Line _leaveLine;
// Line Leave_Line => _leaveLine ??= new Line(Leave_LineText, MacroState.Exploration)
//     .SetSpeakerIcon(Flag)
//     .SetSpeakerName(ShipName);
// NextLines.TryAdd(StartLine, OneTaskLine);

// GoToLines.TryAdd(Trade_Response, Trade_Line);
// GoToLines.TryAdd(Muscopa_Response, Muscopa_Line);
// GoToLines.TryAdd(Attack_Response, Attack_Line);
// GoToLines.TryAdd(Leave_Response, Leave_Line);

// GoToLines.TryAdd(Back_Response, OneTaskLine);
// GoToLines.TryAdd(BuyMaterials_Response, SellMaterials_Line);
// GoToLines.TryAdd(BuyRations_Response, SellRations_Line);
// GoToLines.TryAdd(BuyRepairs_Response, SellRepairs_Line);

// GoToLines.TryAdd(MaterialsLarge_Response, TradeComplete_Line);
// GoToLines.TryAdd(MaterialsMedium_Response, TradeComplete_Line);
// GoToLines.TryAdd(MaterialsSmall_Response, TradeComplete_Line);

// GoToLines.TryAdd(RationsLarge_Response, TradeComplete_Line);
// GoToLines.TryAdd(RationsMedium_Response, TradeComplete_Line);
// GoToLines.TryAdd(RationsSmall_Response, TradeComplete_Line);

// GoToLines.TryAdd(RepairLarge_Response, TradeComplete_Line);
// GoToLines.TryAdd(RepairMedium_Response, TradeComplete_Line);
// GoToLines.TryAdd(RepairSmall_Response, TradeComplete_Line);

// List<Response> RepairResponses = new();
// if (!(Coins < 350 || Mats < 50)) { RepairResponses.Add(RepairLarge_Response); }
// if (!(Coins < 250 || Mats < 20)) { RepairResponses.Add(RepairMedium_Response); }
// if (!(Coins < 100 || Mats < 10)) { RepairResponses.Add(RepairSmall_Response); }
// RepairResponses.Add(Back_Response);
// Replies.TryAdd(SellRepairs_Line, RepairResponses.ToArray());

// List<Response> BuyMatsResponses = new();
// if (!(Coins < 250)) { BuyMatsResponses.Add(MaterialsLarge_Response); }
// if (!(Coins < 150)) { BuyMatsResponses.Add(MaterialsMedium_Response); }
// if (!(Coins < 100)) { BuyMatsResponses.Add(MaterialsSmall_Response); }
// BuyMatsResponses.Add(Back_Response);
// Replies.TryAdd(SellMaterials_Line, BuyMatsResponses.ToArray());

// List<Response> BuyRationsResponses = new();
// if (!(Coins < 250)) { BuyRationsResponses.Add(RationsLarge_Response); }
// if (!(Coins < 150)) { BuyRationsResponses.Add(RationsMedium_Response); }
// if (!(Coins < 50)) { BuyRationsResponses.Add(RationsSmall_Response); }
// BuyRationsResponses.Add(Back_Response);
// Replies.TryAdd(SellRations_Line, BuyRationsResponses.ToArray());

// Replies.TryAdd(OneTaskLine, new Response[4]
// {
//     Trade_Response,
//     Muscopa_Response,
//     Attack_Response,
//     Leave_Response,
// });

// List<Response> TradeResponses = new();
// if (!(Coins < 100 || Mats < 10)) { TradeResponses.Add(BuyMaterials_Response); }
// if (!(Coins < 100)) { TradeResponses.Add(BuyRations_Response); }
// if (!(Coins < 50)) { TradeResponses.Add(BuyRepairs_Response); }
// TradeResponses.Add(Back_Response);
// Replies.TryAdd(Trade_Line, TradeResponses.ToArray());

// Player_Actions.TryAdd(MaterialsLarge_Response, PlayerAction.BuyMaterialsLarge);
// Player_Actions.TryAdd(MaterialsMedium_Response, PlayerAction.BuyMaterialsMedium);
// Player_Actions.TryAdd(MaterialsSmall_Response, PlayerAction.BuyMaterialsSmall);

// Player_Actions.TryAdd(RationsLarge_Response, PlayerAction.BuyRationsLarge);
// Player_Actions.TryAdd(RationsMedium_Response, PlayerAction.BuyRationsMedium);
// Player_Actions.TryAdd(RationsSmall_Response, PlayerAction.BuyRationsSmall);

// Player_Actions.TryAdd(RepairLarge_Response, PlayerAction.RepairLarge);
// Player_Actions.TryAdd(RepairMedium_Response, PlayerAction.RepairMedium);
// Player_Actions.TryAdd(RepairSmall_Response, PlayerAction.RepairSmall);

// Line_Outcomes.TryAdd(Muscopa_Line, MacroState.Muscopa);
// Line_Outcomes.TryAdd(Attack_Line, MacroState.AttackTradeShip);
// Line_Outcomes.TryAdd(Leave_Line, MacroState.Exploration);
// Line_Outcomes.TryAdd(TradeComplete_Line, MacroState.Exploration);
