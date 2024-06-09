using Dialog;
using Data.Two;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BuyStarChart_Dialogue : Dialogue
{
    public BuyStarChart_Dialogue(Buy_Dialogue buy_Dialogue, Speaker speaker, Standing standing)
    {
        ReturnTo = buy_Dialogue;
        Speaker = speaker;
        Standing = standing;
    }

    readonly Dialogue ReturnTo;
    readonly Data.Two.Standing Standing;

    int StandingMod => Data.Two.Manager.Io.StandingData.GetLevel(Standing);
    int Coins => Data.Two.Manager.Io.Inventory.GetLevel(new Data.Two.Gold());
    float StandingsModifier => 1f + (float)(1f - (float)((float)StandingMod) / 9f);

    int largeGold => (int)(largeStarChart * 700f * StandingsModifier);
    int medGold => (int)(medStarChart * 850f * StandingsModifier);
    int smallGold => (int)(smallStarChart * 1000f * StandingsModifier);


    int largeStarChart => 10;
    int medStarChart => 5;
    int smallStarChart => 1;

    public override Dialogue Initiate()
    {
        FirstLine = SellStarCharts_Line;
        return base.Initiate();
    }

    Line _StarChartsLine;
    Line SellStarCharts_Line => _StarChartsLine ??= new Line(StarCharts_LineText, StarChartsResponses)
        .SetSpeaker(Speaker);

    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    Response _StarChartsLarge_response;
    Response StarChartsLarge_Response => _StarChartsLarge_response ??= new Response(StarChartsLarge_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyStarChartsLarge)
        ;

    Response _StarChartsMedium_response;
    Response StarChartsMedium_Response => _StarChartsMedium_response ??= new Response(StarChartsMedium_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyStarChartsMedium)
        ;

    Response _StarChartsSmall_response;
    Response StarChartsSmall_Response => _StarChartsSmall_response ??= new Response(StarChartsSmall_RepText, TradeComplete_Line)
        .SetPlayerAction(BuyStarChartsSmall)
        ;

    Response[] _StarChartsResponses;
    Response[] StarChartsResponses => _StarChartsResponses ??= GetStarChartsResponses();
    Response[] GetStarChartsResponses()
    {
        List<Response> responses = new();

        if (!(Coins < largeGold)) { responses.Add(StarChartsLarge_Response); }
        if (!(Coins < medGold)) { responses.Add(StarChartsMedium_Response); }
        if (!(Coins < smallGold)) { responses.Add(StarChartsSmall_Response); }
        responses.Add(BackResponse);

        return responses.ToArray();
    }
    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    readonly string StarCharts_LineText = "How many Star Charts do you want?";

    string StarChartsLarge_RepText => "+" + largeStarChart.ToString() + " Star Charts; -" + largeGold.ToString() + " gold";
    string StarChartsMedium_RepText => "+" + medStarChart.ToString() + " Star Charts; -" + medGold.ToString() + " gold";
    string StarChartsSmall_RepText => "+" + smallStarChart.ToString() + " Star Chart; -" + smallGold.ToString() + " gold";

    void BuyStarChartsSmall()
    {
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.StarChart(), smallStarChart);
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Gold(), -smallGold);
    }
    void BuyStarChartsMedium()
    {
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.StarChart(), medStarChart);
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Gold(), -medGold);
    }
    void BuyStarChartsLarge()
    {
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.StarChart(), largeStarChart);
        Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Gold(), -largeGold);
    }

}
