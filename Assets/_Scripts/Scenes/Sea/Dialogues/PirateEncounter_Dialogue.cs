using Dialog;
using UnityEngine;

public class PirateEncounter_Dialogue : Dialogue
{
    public PirateEncounter_Dialogue(Sprite ship)
    {
        ShipSprite = ship;
    }

    readonly Sprite ShipSprite;

    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        DataManager.Io.GamePlay.ExplainBatterie = false;
        return base.Initiate();
    }

    Line StartLine => new Line("Pirates off the starboard bow!", DataManager.Io.GamePlay.ExplainBatterie ? Line2 : Line5)
             .SetSpeakerIcon(Assets.Pino)
             .SetSpeakerName(Pino);

    Line Line2 => new Line("Executing proper timing of the Battery is critical in sea battles.", Line3)
              .SetSpeakerIcon(Assets.Pino)
              .SetSpeakerName(Pino);

    Line Line3 => new Line("I will show you the beat map, but the performance is up to you.", Line4)
              .SetSpeakerIcon(Assets.Pino)
              .SetSpeakerName(Pino);

    Line Line4 => new Line("(press space or tap to perform the rhythms)", Line5);

    Line Line5 => new Line("Sending drum cadence now... Counting off...", new NewBatterie_State(new BatteriePack()))
         .SetSpeakerIcon(Assets.Pino)
         .SetSpeakerName(Pino)
         .FadeToNextState();

}