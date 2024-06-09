using System.Collections.Generic;
using Dialog;
using System;

public class Trade_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    readonly Data.Two.Standing Standing;

    public Trade_Dialogue(Dialogue returnTo, Speaker speaker, Data.Two.Standing standing)
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
    string Trade_LineText => UnityEngine.Random.Range(0, 4) switch
    {
        0 => "Alright, let's see what we've got...",
        1 => "Anything for our fellow sailor! Let's see...",
        2 => "Good timing! We just stocked up at the ports in Lydia.",
        _ => "Our merchants are expecting these wares, but we can spare a few items..."
    };

    Line _tradeLine;
    Line Trade_Line => _tradeLine ??= new Line(Trade_LineText, TradeResponses)
        .SetSpeaker(Speaker);

    Response[] _tradeResponses;
    Response[] TradeResponses => _tradeResponses ??= new Response[]{
        BuyResponse,
        SellResponse,
        // SolveResponse,
        BackResponse
    };


    Response _buyResponse;
    Response BuyResponse => _buyResponse ??= new Response("Buy", new Buy_Dialogue(this, Speaker, Standing));

    Response _sellResponse;
    Response SellResponse => _sellResponse ??= new Response("Sell", new Sell_Dialogue(this, Speaker, Standing));

    // Response _solveResponse;
    // Response SolveResponse => _solveResponse ??= new Response("Solve", new SolveMuscopa_Dialogue(this, Speaker));

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
}
