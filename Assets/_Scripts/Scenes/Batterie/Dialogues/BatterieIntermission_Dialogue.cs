using Dialog;
using UnityEngine;

public class BatterieIntermission_Dialogue : Dialogue
{
    public BatterieIntermission_Dialogue(BatteriePack pack)
    {
        Pack = pack;
        Speaker = Speaker.Pino;
    }
    readonly BatteriePack Pack;

    public override Dialogue Initiate()
    {
        if (Pack.NMEHealth.cur < 1)
        {
            FirstLine = Victory;
        }
        else if (Pack.NMEHealth.cur < (float)(Pack.NMEHealth.max * .3f) &&
            Random.value > .65f)
        {
            FirstLine = NMEAttemptingFlee;
            Pack.Escaping = true;
        }
        else if (DataManager.Io.CharacterData.CurrentHealth < (float)(DataManager.Io.CharacterData.MaxHealth * .3f))
        {
            FirstLine = LowPlayerHealth;
        }
        else if (Pack.NMEHealth.cur < (float)(Pack.NMEHealth.max * .21f) &&
                (DataManager.Io.CharacterData.CurrentHealth > (float)(DataManager.Io.CharacterData.MaxHealth * .5f)))
        {
            FirstLine = NMESurrender;
        }
        else if (Pack.NMEHealth.cur < (float)(Pack.NMEHealth.max * .3f))
        {
            FirstLine = LowNMEHealth;
        }
        else
        {
            FirstLine = EvenHealth;
        }
        return base.Initiate();
    }

    Line Victory => new Line("We got 'em Cap! Blew 'em out of the water!",
                    new EndBatterie_State(Pack.SetResultType(BatterieResultType.Won)))
        .SetSpeaker(Speaker)
        ;

    Line LowPlayerHealth => new Line("We're in bad shape Cap'n! We can't take much more!")
        .SetSpeaker(Speaker)
        .SetResponses(new Response[3] { Attack, Flee, Surrender })
        ;

    Line LowNMEHealth => new Line("We almost got'em Cap! Another volley should surely do it!")
        .SetSpeaker(Speaker)
        .SetResponses(new Response[2] { Attack, Flee })
        ;

    Line NMESurrender => new Line("Cap'n, the ship is flying a white flag, they're surrendering!")
        .SetSpeaker(Speaker)
        .SetResponses(new Response[2] { Attack, AcceptSurrender })
        ;

    Line NMEAttemptingFlee => new Line("Cap! The ship is attempting to flee")
        .SetSpeaker(Speaker)
        .SetResponses(new Response[2] { Attack, LetThemGo })
        ;

    Line EvenHealth => new Line("It's not over yet. What'er your orders Cap'n?")
        .SetSpeaker(Speaker)
        .SetResponses(new Response[2] { Attack, Flee })
        ;

    Response Attack => new("Fire another volley", AttackAgain);
    Line AttackAgain => new Line("Load the cannons! Counting off!...", new ResumeBatterie_State(Pack))
        .SetSpeaker(Speaker)
        ;

    Response Flee => new("Attempt to flee", Random.value > .7f ? CantFlee : Fled);
    Line Fled => new Line("It's better we run and live to fight another day!",
                 new EndBatterie_State(Pack.SetResultType(BatterieResultType.Fled)))
        .SetSpeaker(Speaker)
        ;

    Line CantFlee => new Line("It's no good Cap! They've got the wind, we can't outrun them like this. We've no choice but to fight!", AttackAgain)
        .SetSpeaker(Speaker)
        ;

    Response Surrender => new("Surrender to the ship", new EndBatterie_State(Pack.SetResultType(BatterieResultType.Surrender)));
    Response AcceptSurrender => new("Accept their surrender", new EndBatterie_State(Pack.SetResultType(BatterieResultType.NMESurrender)));
    Response LetThemGo => new("Let them go.", new EndBatterie_State(Pack.SetResultType(BatterieResultType.NMEscaped)));
}
