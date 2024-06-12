using Dialog;
using System.Collections.Generic;

public class Services_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    readonly Data.Standing Standing;

    int StandingMod => Data.Manager.Io.StandingData.GetLevel(Standing);
    public Services_Dialogue(Dialogue returnTo, Speaker speaker, Data.Standing standing)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
        Standing = standing;
    }

    override public Dialogue Initiate()
    {
        FirstLine = Trade_Line;
        return this;
    }

    string Trade_LineText => StandingMod switch
    {
        10 or 9 => "Happy to help.",
        7 or 8 or 6 => "It's good luck to help a fellow sailor in need!",
        5 or 4 or 3 => "Alright then, let's get on with it.",
        _ => "Our merchants are expecting us soon... I suppose we can help, for a price."
    };

    int Coins => Data.Manager.Io.Inventory.GetLevel(new Data.Gold());
    float StandingsModifier => 1f + (float)(1f - (float)((float)StandingMod) / 9f);

    // int largeGold => (int)(largeStarChart * 700f * StandingsModifier);
    // int medGold => (int)(medStarChart * 850f * StandingsModifier);
    // int smallGold => (int)(smallStarChart * 1000f * StandingsModifier);


    int Gold => Data.Manager.Io.Inventory.GetLevel(new Data.Gold());
    int Mats => Data.Manager.Io.Inventory.GetLevel(new Data.Material());
    int CurHP => Data.Manager.Io.ActiveShip.GetLevel(new Data.CurrentHitPoints());
    int MaxHP => Data.Manager.Io.ActiveShip.GetLevel(new Data.MaxHitPoints());


    int costStarChart => (int)(700f * StandingsModifier);
    int costGramo => (int)(5000f * StandingsModifier);


    bool buyRepairs => CurHP < MaxHP && !(Gold < smallRepair * goldPer) && !(Mats < smallRepair * matsPer);

    int smallRepair => (int)(MaxHP * .15f);
    readonly int matsPer = 3;
    readonly int goldPer = 25;

    string LatLong => RandomLoc.GlobalCoordsToLatLongs(Sea.WorldMapScene.Io.Map.GlobalSize);
    // string LatLong => _latLong ??= 

    UnityEngine.Vector2Int RandomLoc = Sea.WorldMapScene.Io.Ship.GlobalCoord +
      new UnityEngine.Vector2Int(
             UnityEngine.Random.Range(30, 60) * (UnityEngine.Random.value < .5f ? 1 : -1),
             UnityEngine.Random.Range(30, 60) * (UnityEngine.Random.value < .5f ? 1 : -1)
         );

    Line _tradeLine;
    Line Trade_Line => _tradeLine ??= new Line(Trade_LineText, TradeResponses)
        .SetSpeaker(Speaker);

    Response[] _tradeResponses;
    Response[] TradeResponses => GetResponses();

    Response[] GetResponses()
    {
        List<Response> responses = new();

        if (!(Coins < costStarChart &&
            Data.Manager.Io.Inventory.GetLevel(new Data.StarChart()) > 0))
        {
            responses.Add(StarChartResponse);
        }
        if (!(Coins < costGramo) &&
            Data.Manager.Io.Inventory.GetLevel(new Data.Gramophone()) > 0)
        {
            responses.Add(GramoResponse);
        }
        if (buyRepairs) { responses.Add(RepairResponse); }
        responses.Add(BackResponse);

        return responses.ToArray();
    }

    void DecipherStarChart()
    {
        Data.Manager.Io.Inventory.AdjustLevel(new Data.StarChart(), -1);
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), -costStarChart);


        Data.Manager.Io.Quests.SetQuest(new Data.Navigation(),
               new Quests.NavigationQuest(
                   new Sea.Inventoriable((Data.Manager.Io.Gramophones, new Data.Gramo1(), 1)),//TODO make sliding scale difficulty
                   RandomLoc,
                   LatLong));

        Sea.WorldMapScene.Io.Map.AddToMap(Data.Manager.Io.Quests.GetQuest(new Data.Navigation()).QuestLocation, Sea.CellType.Gramo);





    }

    void UnlockGramo()
    {
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Gramophone(), -1);
        Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), -costGramo);
    }
    // void BuyStarChartsLarge()
    // {
    //     Data.Manager.Io.Inventory.AdjustLevel(new Data.StarChart(), largeStarChart);
    //     Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), -largeGold);
    // }


    Response _repairResponse;
    Response RepairResponse => _repairResponse ??= new Response("Repair ship", new BuyRepairs_Dialogue(this, Speaker, Standing));

    // Response _buyResponse;
    // Response BuyResponse => _buyResponse ??= new Response("Buy", new Buy_Dialogue(this, Speaker, Standing));

    // Response _sellResponse;
    // Response SellResponse => _sellResponse ??= new Response("Sell", new Sell_Dialogue(this, Speaker, Standing));

    Response _starChartResponse;
    Response StarChartResponse => _starChartResponse ??= new Response("Decipher Star Chart, -" + costStarChart + " gold",
        Data.Manager.Io.Quests.GetQuest(new Data.Navigation()) is null ? StarChartLocLine : ConfirmStarChart)
        .SetPlayerAction(Data.Manager.Io.Quests.GetQuest(new Data.Navigation()) is null ? DecipherStarChart : () => { });//, new NavigateStarChart_Dialogue(this, Speaker, Standing));

    Line _starChartLocLine;
    Line StarChartLocLine => _starChartLocLine ??= new Line("The Star Chart points to these coordinates: " + LatLong + ".\n[A new navigation quest is available]", TradeComplete_Line);

    Line _confirmStarChart;
    Line ConfirmStarChart => _confirmStarChart ??= new Line("You already have an active navigation quest, do you wish to abandon it?", new Response[] { ConfirmOverwrite, BackResponse });

    Response _confirmOverwrite;
    Response ConfirmOverwrite => _confirmOverwrite ??= new Response("Yes", StarChartLocLine).SetPlayerAction(DecipherStarChart);

    Response _gramoResponse;
    Response GramoResponse => _gramoResponse ??= new Response("Unlock Gramophone, -" + costGramo + " gold", TradeComplete_Line)
        .SetPlayerAction(UnlockGramo);//, new UnlockGramophone_Dialogue(this, Speaker, Standing));

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);


    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
}