using Dialog;
using MusicTheory.Arithmetic;

public class ShipTask_Dialogue : Dialogue
{
    readonly string OneTask_LineText = "We've got time for one task, so what will it be?";
    readonly string TradeGoods_RepText = "Trade goods";

    public ShipTask_Dialogue(Speaker speaker)
    {
        Speaker = speaker;
    }

    override public Dialogue Initiate()
    {
        FirstLine = OneTaskLine;
        return this;
    }

    string Leave_LineText => UnityEngine.Random.Range(0, 4) switch
    {
        0 => "Safe journey!",
        1 => "Until next time!",
        2 => "Keep an eye out for pirates, they can look just like trade ships.",
        _ => "Then why did you... I... We have work to do!"
    };

    Line _oneTaskLine;
    Line OneTaskLine => _oneTaskLine ??= new Line(OneTask_LineText, new Response[4]
        {
            Trade_Response,
            MusicaResponse,
            PirateResponse,
            Leave,
        })
        .SetSpeaker(Speaker)
        ;

    Line _leaveLine;
    Line Leave_Line => _leaveLine ??= new Line(Leave_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    Response _trade_response;
    Response Trade_Response => _trade_response ??= new Response(TradeGoods_RepText, new Trade_Dialogue(this, Speaker));

    Response _musicaResponse;
    Response MusicaResponse => _musicaResponse ??= new Response("Musica", new MusicaTask_Dialogue(this, Speaker));

    Response _pirateResponse;
    Response PirateResponse => _pirateResponse ??= new Response("Pirates", new PirateTask_Dialogue(this, Speaker));

    Response _leave;
    Response Leave => _leave ??= new Response(nameof(Leave), Leave_Line);

}
