using Dialog;
using MusicTheory.Arithmetic;

public class ShipTask_Dialogue : Dialogue
{
    readonly string OneTask_LineText = "We've got time for one task, so what will it be?";
    readonly Data.Standing Standing;

    public ShipTask_Dialogue(Speaker speaker, Data.Standing standing)
    {
        Speaker = speaker;
        Standing = standing;
    }

    override public Dialogue Initiate()
    {
        FirstLine = OneTaskLine;
        return this;
    }

    int StandingLevel => Data.Manager.Io.StandingData.GetLevel(Standing);
    string Leave_LineText => StandingLevel switch
    {
        10 or 9 or 8 => "Safe journey!",
        7 or 6 or 5 => "Until next time!",
        4 or 3 => "Keep an eye out for pirates, they can look just like trade ships.",
        _ => "Then why did you... I... We have work to do!"
    };

    Line _oneTaskLine;
    Line OneTaskLine => _oneTaskLine ??= new Line(OneTask_LineText, new Response[4]
        {
            Trade_Response,
            RepairResponse,
            BountyResponse,
            Leave,
        })
        .SetSpeaker(Speaker)
        ;


    Line _leaveLine;
    Line Leave_Line => _leaveLine ??= new Line(Leave_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    Response _trade_response;
    Response Trade_Response => _trade_response ??= new Response("Trade goods", new Trade_Dialogue(this, Speaker, Standing));

    Response _repairResponse;
    Response RepairResponse => _repairResponse ??= new Response("Services", new Services_Dialogue(this, Speaker, Standing));

    Response _bountyResponse;
    Response BountyResponse => _bountyResponse ??= new Response("Bounties", new Bounty_Dialogue(this, Speaker, Standing));

    Response _leave;
    Response Leave => _leave ??= new Response(nameof(Leave), Leave_Line);

}
