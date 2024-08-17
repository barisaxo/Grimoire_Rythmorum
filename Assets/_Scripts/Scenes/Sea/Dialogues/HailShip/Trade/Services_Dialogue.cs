using Dialog;
using System.Collections.Generic;

public class Services_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    readonly Datum.Standing Standing;

    int StandingMod => Datum.Manager.Io.Standings.GetLevel(Standing);
    public Services_Dialogue(Dialogue returnTo, Speaker speaker, Datum.Standing standing)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
        Standing = standing;
    }

    override public Dialogue Initiate()
    {
        Loc = Sea.WorldMapScene.Io.Map.GetAvailableLocalCoordFromGlobalLoc(Sea.WorldMapScene.Io.Ship.GlobalCoord + RandomLoc);
        GetLatLong = LatLong(Loc);
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

    int Coins => Datum.Manager.Io.Inventory.GetLevel(new Datum.Gold());
    float StandingsModifier => 1f + (float)(1f - (float)((float)StandingMod) / 9f);

    // int largeGold => (int)(largeStarChart * 700f * StandingsModifier);
    // int medGold => (int)(medStarChart * 850f * StandingsModifier);
    // int smallGold => (int)(smallStarChart * 1000f * StandingsModifier);
    bool HasStarChart => Datum.Manager.Io.Inventory.GetLevel(new Datum.StarChart()) > 0;
    bool HasGramo => Datum.Manager.Io.Inventory.GetLevel(new Datum.Gramophone()) > 0;

    int Gold => Datum.Manager.Io.Inventory.GetLevel(new Datum.Gold());
    int Mats => Datum.Manager.Io.Inventory.GetLevel(new Datum.Material());
    int CurHP => Datum.Manager.Io.ActiveShip.GetLevel(new Datum.CurrentHitPoints());
    int MaxHP => Datum.Manager.Io.ActiveShip.GetLevel(new Datum.MaxHitPoints());

    int costStarChart => (int)(700f * StandingsModifier);
    int costGramo => (int)(5000f * StandingsModifier);

    bool buyRepairs
    {
        get
        {
            UnityEngine.Debug.Log("Buy repairs... HP down? " + (CurHP < MaxHP) +
                ", gold? " + !(Gold < smallRepair * goldPer) + ", cost? " + (smallRepair * goldPer) + ", available? " + Gold +
                ", mats? " + !(Mats < smallRepair * matsPer) + ", mats cost? " + (smallRepair * matsPer) + ", available? " + Mats);

            return (CurHP < MaxHP) && !(Gold < smallRepair * goldPer) && !(Mats < smallRepair * matsPer);
        }
    }

    int smallRepair => (int)(MaxHP * .15f);
    int matsPer => (int)(1);
    int goldPer => (int)(2.66f * StandingsModifier);

    string GetLatLong;
    UnityEngine.Vector2Int Loc;
    string LatLong(UnityEngine.Vector2Int loc) => loc.GlobalCoordsToLatLongs(Sea.WorldMapScene.Io.Map.GlobalSize);
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

        if (HasStarChart)
            if (!(Coins < costStarChart) && Datum.Manager.Io.Inventory.GetLevel(new Datum.StarChart()) > 0)
                responses.Add(StarChartResponse);
            else responses.Add(CantAffordStarChartResponse);
        else responses.Add(SCInventoryEmpty);

        if (HasGramo)
            if (!(Coins < costGramo) && Datum.Manager.Io.Inventory.GetLevel(new Datum.Gramophone()) > 0)
                responses.Add(GramoResponse);
            else responses.Add(CantAffordGramoResponse);
        else responses.Add(GramoInventoryEmpty);

        responses.Add(RepairResponse);

        responses.Add(BackResponse);

        return responses.ToArray();
    }

    void DecipherStarChart()
    {
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.StarChart(), -1);
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gold(), -costStarChart);

        Datum.Manager.Io.Quests.SetQuest(new Datum.Navigation(),
               new Quests.NavigationQuest(
                   new Sea.Inventoriable((Datum.Manager.Io.Gramophones, new Datum.Gramo1(), 1)),
                   Loc,
                  GetLatLong));

        Sea.WorldMapScene.Io.Map.AddCellToMap(Datum.Manager.Io.Quests.GetQuest(new Datum.Navigation()).QuestLocation, Sea.CellType.Gramo);
    }

    void UnlockGramo()
    {
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gramophone(), -1);
        Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gold(), -costGramo);
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
        Datum.Manager.Io.Quests.GetQuest(new Datum.Navigation()) is null ? StarChartLocLine : ConfirmStarChart)
        .SetPlayerAction(Datum.Manager.Io.Quests.GetQuest(new Datum.Navigation()) is null ? DecipherStarChart : () => { });


    Response _cantAffordStarChartResponse;
    Response CantAffordStarChartResponse => _cantAffordStarChartResponse ??= new Response("Decipher Star Chart [not enough gold]");


    Line _starChartLocLine;
    Line StarChartLocLine => _starChartLocLine ??= new Line("The Star Chart points to these coordinates: " + GetLatLong + ".\n[A new navigation quest is available]", TradeComplete_Line);

    Line _confirmStarChart;
    Line ConfirmStarChart => _confirmStarChart ??= new Line("You already have an active navigation quest, do you wish to abandon it?", new Response[] { ConfirmOverwrite, BackResponse });

    Response _confirmOverwrite;
    Response ConfirmOverwrite => _confirmOverwrite ??= new Response("Yes", StarChartLocLine).SetPlayerAction(DecipherStarChart);

    Response _gramoResponse;
    Response GramoResponse => _gramoResponse ??= new Response("Unlock Gramophone, -" + costGramo + " gold", TradeComplete_Line)
        .SetPlayerAction(UnlockGramo);//, new UnlockGramophone_Dialogue(this, Speaker, Standing));

    Response _cantAffordGramoResponse;
    Response CantAffordGramoResponse => _cantAffordGramoResponse ??= new Response("Unlock Gramophone [not enough gold]");

    Response _gramoInventoryEmpty;
    Response GramoInventoryEmpty => _gramoInventoryEmpty ??= new Response("Unlock Gramophone [no gramophones]");

    Response _scInventoryEmpty;
    Response SCInventoryEmpty => _scInventoryEmpty ??= new Response("Decipher Star Chart [no star charts]");

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);


    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
}