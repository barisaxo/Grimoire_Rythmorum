using UnityEngine;
using Dialog;

public class HailShip_Dialogue : Dialogue
{
    readonly Data.Two.Standing Standing;
    public HailShip_Dialogue(Speaker speaker, Data.Two.Standing standing)
    {
        Speaker = speaker;
        Standing = standing;
    }

    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        return base.Initiate();
    }

    string Hail_LineText => UnityEngine.Random.Range(0, 4) switch
    {
        0 => "Ahoy! We're on a tight schedule.",
        1 => "The weather favors us this day.",
        2 => "You seem trust worthy enough...",
        _ => "We'll work with you this once, just don't try anything..."
    };

    Line _startLine;
    Line StartLine => _startLine ??= new Line(Hail_LineText, new ShipTask_Dialogue(Speaker, Standing))
        .SetSpeaker(Speaker);

}