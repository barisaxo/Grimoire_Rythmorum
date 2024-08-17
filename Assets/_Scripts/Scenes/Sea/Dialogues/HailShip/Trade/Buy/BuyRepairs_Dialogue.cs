using System.Collections;
using System.Collections.Generic;
using Dialog;
using Datum;

public class BuyRepairs_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    readonly Standing Standing;

    int StandingMod => Manager.Io.Standings.GetLevel(Standing);
    int Gold => Manager.Io.Inventory.GetLevel(new Gold());
    int Mats => Manager.Io.Inventory.GetLevel(new Material());
    int CurHP => Manager.Io.ActiveShip.GetLevel(new CurrentHitPoints());
    int MaxHP => Manager.Io.ActiveShip.GetLevel(new MaxHitPoints());

    float HPPercent => (float)CurHP / (float)MaxHP;
    // int HPDown => MaxHP - CurHP;

    float StandingsModifier => 1f + (float)(1f - (float)((float)StandingMod) / 9f);

    int largeHealthAmount => (int)((float)MaxHP * .5f);
    int medHealthAmount => (int)((float)MaxHP * .25f);
    int smallHealthAmount => (int)((float)MaxHP * .15f);

    public BuyRepairs_Dialogue(Dialogue returnTo, Speaker speaker, Standing standing)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
        Standing = standing;
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

    string RepairLarge_RepText => "50% hull [" + (-largeHealthAmount * goldPer) + " gold, " + (-largeHealthAmount * matsPer) + " mats]";
    string RepairMedium_RepText => "25% hull [" + (-medHealthAmount * goldPer) + " gold, " + (-medHealthAmount * matsPer) + " mats]";
    string RepairSmall_RepText => "15% hull [" + (-smallHealthAmount * goldPer) + " gold, " + (-smallHealthAmount * matsPer) + " mats]";

    Response _repairLarge_response;
    Response RepairLarge_Response => _repairLarge_response ??= new Response(RepairLarge_RepText, TradeComplete_Line)
        .SetPlayerAction(RepairLarge);

    Response _cantAffordRepairLarge_response;
    Response CantAffordRepairLarge_Response => _cantAffordRepairLarge_response ??= new Response("50% hull [can't afford]");


    Response _repairMedium_response;
    Response RepairMedium_Response => _repairMedium_response ??= new Response(RepairMedium_RepText, TradeComplete_Line)
        .SetPlayerAction(RepairMedium);
    Response _cantAffordRepairMedium_response;
    Response CantAffordRepairMedium_Response => _cantAffordRepairMedium_response ??= new Response("25% hull [can't afford]");

    Response _repairSmall_response;
    Response RepairSmall_Response => _repairSmall_response ??= new Response(RepairSmall_RepText, TradeComplete_Line)
        .SetPlayerAction(RepairSmall);

    Response _cantAffordRepairSmall_response;
    Response CantAffordRepairSmall_Response => _cantAffordRepairSmall_response ??= new Response("15% hull [can't afford]");

    Response[] _repairResponses;
    Response[] RepairResponses => _repairResponses ??= GetRepairResponses();
    Response[] GetRepairResponses()
    {
        List<Response> responses = new();

        if (LargeRepairsNeeded)
        {
            if (AffordLargeRepairs) responses.Add(RepairLarge_Response);
            else responses.Add(CantAffordRepairLarge_Response);
        }

        if (MedRepairsNeeded)
        {
            if (AffordMedRepairs) responses.Add(RepairMedium_Response);
            else responses.Add(CantAffordRepairMedium_Response);
        }

        if (SmallRepairsNeeded)
        {
            if (AffordSmallRepairs) responses.Add(RepairSmall_Response);
            else responses.Add(CantAffordRepairSmall_Response);
        }
        else responses.Add(new("[No repairs needed]"));

        responses.Add(BackResponse);

        return responses.ToArray();
    }

    bool LargeRepairsNeeded => HPPercent < .75f;
    bool AffordLargeRepairs => !(Gold < largeHealthAmount * goldPer) && !(Mats < largeHealthAmount * matsPer);
    bool MedRepairsNeeded => HPPercent < .85f;
    bool AffordMedRepairs => !(Gold < medHealthAmount * goldPer) && !(Mats < medHealthAmount * matsPer);
    bool SmallRepairsNeeded => HPPercent < 1f;
    bool AffordSmallRepairs => !(Gold < smallHealthAmount * goldPer) && !(Mats < smallHealthAmount * matsPer);

    int matsPer => (int)(1);
    int goldPer => (int)(2.66f * StandingsModifier);

    void RepairSmall()
    {
        Manager.Io.ActiveShip.AdjustLevel(new CurrentHitPoints(), smallHealthAmount);
        Manager.Io.Inventory.AdjustLevel(new Material(), -smallHealthAmount * matsPer);
        Manager.Io.Inventory.AdjustLevel(new Gold(), -smallHealthAmount * goldPer);
    }

    void RepairMedium()
    {
        Manager.Io.ActiveShip.AdjustLevel(new CurrentHitPoints(), medHealthAmount);
        Manager.Io.Inventory.AdjustLevel(new Material(), -medHealthAmount * matsPer);
        Manager.Io.Inventory.AdjustLevel(new Gold(), -medHealthAmount * goldPer);
    }

    void RepairLarge()
    {
        Manager.Io.ActiveShip.AdjustLevel(new CurrentHitPoints(), largeHealthAmount);
        Manager.Io.Inventory.AdjustLevel(new Material(), -largeHealthAmount * matsPer);
        Manager.Io.Inventory.AdjustLevel(new Gold(), -largeHealthAmount * goldPer);
    }

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Back", ReturnTo);
}
