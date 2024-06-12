using UnityEngine;
using Dialog;

public class HailShip_Dialogue : Dialogue
{
    readonly Data.Standing Standing;
    public HailShip_Dialogue(Speaker speaker, Data.Standing standing)
    {
        Speaker = speaker;
        Standing = standing;
    }

    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        return base.Initiate();
    }

    int StandingLevel => Data.Manager.Io.StandingData.GetLevel(Standing);
    string Hail_LineText => StandingLevel switch
    {
        10 or 9 or 8 => "The weather favors us this day.",
        6 or 5 or 7 => "Ahoy! We're on a tight schedule.",
        4 or 3 => "You seem trust worthy enough...",
        _ => "We'll work with you this once, just don't try anything..."
    };

    Line _startLine;
    Line StartLine => _startLine ??= new Line(Hail_LineText, new ShipTask_Dialogue(Speaker, Standing))
        .SetSpeaker(Speaker);

}