using Dialog;
using UnityEngine;

public class PirateEncounter_Dialogue : Dialogue
{
    public PirateEncounter_Dialogue()
    {
        Speaker = Speaker.Pino;
    }


    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        DataManager.Io.GamePlay.ExplainBatterie = false;
        return base.Initiate();
    }

    Line StartLine => new Line("Pirates off the starboard bow!", DataManager.Io.GamePlay.ExplainBatterie ? Line2 : Line5)
        .SetSpeaker(Speaker);

    Line Line2 => new Line("Executing proper timing of the Battery is critical in sea battles.", Line3)
        .SetSpeaker(Speaker);

    Line Line3 => new Line("I will show you the beat map, but the performance is up to you.", Line4)
        .SetSpeaker(Speaker);

    Line Line4 => new Line("(press space or tap to perform the rhythms)", Line5);

    Line Line5 => new Line("Sending drum cadence now... Counting off...", new BatterieAndCadence_State())
        .SetSpeaker(Speaker)
        .FadeToNextState();

}