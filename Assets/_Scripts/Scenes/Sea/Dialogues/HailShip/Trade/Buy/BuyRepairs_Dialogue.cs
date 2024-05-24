using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;
using Data.Two;

public class BuyRepairs_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    readonly Standing Standing;

    int StandingMod => Data.Two.Manager.Io.StandingData.GetLevel(Standing);
    int Gold => Manager.Io.Inventory.GetLevel(new Gold());
    int Mats => Manager.Io.Inventory.GetLevel(new MaterialStorage());
    int CurHP => Manager.Io.PlayerShip.GetLevel(new CurrentHitPoints());
    int MaxHP => Manager.Io.PlayerShip.GetLevel(new CurrentHitPoints());

    float PCHP => CurHP / MaxHP;
    int HPDown => MaxHP - CurHP;

    float StandingsModifier => 1f + (float)(1f - (float)((float)StandingMod) / 9f);

    int largeAmount => (int)(MaxHP * .5f * StandingsModifier);
    int medAmount => (int)(MaxHP * .25f * StandingsModifier);
    int smallAmount => (int)(MaxHP * .15f * StandingsModifier);

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

    //TODOTODO
    readonly string RepairLarge_RepText = "50% hull [*** gold, ** mats]";
    readonly string RepairMedium_RepText = "25% hull [*** gold, ** mats]";
    readonly string RepairSmall_RepText = "15% hull [*** gold, ** mats]";
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

    bool LargeRepairs => PCHP < .75f && !(Gold < largeAmount * goldPer) && !(Mats < largeAmount * matsPer);
    bool MedRepairs => PCHP < .85f && !(Gold < medAmount * goldPer) && !(Mats < medAmount * matsPer);
    bool SmallRepairs => PCHP < 1f && !(Gold < smallAmount * goldPer) && !(Mats < smallAmount * matsPer);

    readonly int matsPer = 3;
    readonly int goldPer = 25;

    void RepairSmall()
    {
        Manager.Io.PlayerShip.AdjustLevel(new CurrentHitPoints(), smallAmount);
        Manager.Io.Inventory.AdjustLevel(new MaterialStorage(), -smallAmount * matsPer);
        Manager.Io.Inventory.AdjustLevel(new Gold(), -smallAmount * goldPer);
    }

    void RepairMedium()
    {
        Manager.Io.PlayerShip.AdjustLevel(new CurrentHitPoints(), medAmount);
        Manager.Io.Inventory.AdjustLevel(new MaterialStorage(), -medAmount * matsPer);
        Manager.Io.Inventory.AdjustLevel(new Gold(), -medAmount * goldPer);
    }

    void RepairLarge()
    {
        Manager.Io.PlayerShip.AdjustLevel(new CurrentHitPoints(), largeAmount);
        Manager.Io.Inventory.AdjustLevel(new MaterialStorage(), -largeAmount * matsPer);
        Manager.Io.Inventory.AdjustLevel(new Gold(), -largeAmount * goldPer);
    }

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
}
