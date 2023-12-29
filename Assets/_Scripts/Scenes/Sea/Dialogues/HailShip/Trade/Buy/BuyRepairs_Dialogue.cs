using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class BuyRepairs_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;

    int Coins => DataManager.Io.CharacterData.Coins;
    int Mats => DataManager.Io.CharacterData.Materials;

    public BuyRepairs_Dialogue(Dialogue returnTo, Speaker speaker)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
    }

    public override Dialogue Initiate()
    {
        FirstLine = SellRepairs_Line;
        return base.Initiate();
    }

    readonly string Repair_LineText = "How much work do you need done?";
    Line _repairLine;
    Line SellRepairs_Line => _repairLine ??= new Line(Repair_LineText, RepairResponses)
        .SetSpeaker(Speaker)
        ;


    readonly string RepairLarge_RepText = "25 hull [750 gold, 50 mats]";
    readonly string RepairMedium_RepText = "10 hull [250 gold, 20 mats]";
    readonly string RepairSmall_RepText = "5 hull [100 gold, 10 mats]";
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

        if (!(Coins < 25 * 30 || Mats < 25 * 2)) { responses.Add(RepairLarge_Response); }
        if (!(Coins < 10 * 25 || Mats < 10 * 2)) { responses.Add(RepairMedium_Response); }
        if (!(Coins < 5 * 20 || Mats < 5 * 2)) { responses.Add(RepairSmall_Response); }
        responses.Add(BackResponse);

        return responses.ToArray();
    }

    void RepairSmall()
    {
        int amount = 5;
        DataManager.Io.CharacterData.CurrentHealth += amount;
        DataManager.Io.CharacterData.Materials -= amount * 2;
        DataManager.Io.CharacterData.Coins -= amount * 20;
    }
    void RepairMedium()
    {
        int amount = 10;
        DataManager.Io.CharacterData.CurrentHealth += amount;
        DataManager.Io.CharacterData.Materials -= amount * 2;
        DataManager.Io.CharacterData.Coins -= amount * 25;
    }
    void RepairLarge()
    {
        int amount = 25;
        DataManager.Io.CharacterData.CurrentHealth += amount;
        DataManager.Io.CharacterData.Materials -= amount * 2;
        DataManager.Io.CharacterData.Coins -= amount * 30;
    }

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
}
