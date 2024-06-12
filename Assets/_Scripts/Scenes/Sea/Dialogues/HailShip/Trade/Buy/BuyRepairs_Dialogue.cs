using System.Collections;
using System.Collections.Generic;
using Dialog;
using Data;

public class BuyRepairs_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    readonly Standing Standing;

    int StandingMod => Data.Manager.Io.StandingData.GetLevel(Standing);
    int Gold => Manager.Io.Inventory.GetLevel(new Gold());
    int Mats => Manager.Io.Inventory.GetLevel(new Material());
    int CurHP => Manager.Io.ActiveShip.GetLevel(new CurrentHitPoints());
    int MaxHP => Manager.Io.ActiveShip.GetLevel(new MaxHitPoints());

    float HPPercent => (float)CurHP / (float)MaxHP;
    int HPDown => MaxHP - CurHP;

    float StandingsModifier => 1f + (float)(1f - (float)((float)StandingMod) / 9f);

    int largeHealthAmount => (int)((float)MaxHP * .5f);
    int medHealthAmount => (int)((float)MaxHP * .25f);
    int smallHealthAmount => (int)((float)MaxHP * .15f);

    public BuyRepairs_Dialogue(Dialogue returnTo, Speaker speaker, Standing standing)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
        Standing = standing;
        UnityEngine.Debug.Log(HPPercent + nameof(HPPercent) + "StandingsMod:" + StandingsModifier + ", mats: " + Mats + ", mats cost: " + (smallHealthAmount * matsPer) + ", smallHealth:" + smallHealthAmount + ", SmallHealthBool:" + SmallRepairs);
        UnityEngine.Debug.Log(RepairSmall_RepText);
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

    //TODOTODO
    string RepairLarge_RepText => "50% hull [" + (-largeHealthAmount * goldPer) + " gold, " + (-largeHealthAmount * matsPer) + " mats]";
    string RepairMedium_RepText => "25% hull [" + (-medHealthAmount * goldPer) + " gold, " + (-medHealthAmount * matsPer) + " mats]";
    string RepairSmall_RepText => "15% hull [" + (-smallHealthAmount * goldPer) + " gold, " + (-smallHealthAmount * matsPer) + " mats]";

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

        if (LargeRepairs) { responses.Add(RepairLarge_Response); }
        if (MedRepairs) { responses.Add(RepairMedium_Response); }
        if (SmallRepairs) { responses.Add(RepairSmall_Response); }

        responses.Add(BackResponse);

        return responses.ToArray();
    }

    bool LargeRepairs => HPPercent < .75f && !(Gold < largeHealthAmount * goldPer) && !(Mats < largeHealthAmount * matsPer);
    bool MedRepairs => HPPercent < .85f && !(Gold < medHealthAmount * goldPer) && !(Mats < medHealthAmount * matsPer);
    bool SmallRepairs => HPPercent < 1f && !(Gold < smallHealthAmount * goldPer) && !(Mats < smallHealthAmount * matsPer);

    int matsPer => (int)(3f * StandingsModifier);
    int goldPer => (int)(25f * StandingsModifier);

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
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
}
