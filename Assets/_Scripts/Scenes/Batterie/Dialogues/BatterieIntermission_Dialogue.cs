using Dialog;
using UnityEngine;

public class BatterieIntermission_Dialogue : Dialogue
{
    public BatterieIntermission_Dialogue(BatterieScene scene)
    {
        Scene = scene;
        Speaker = Speaker.Pino;
        damageTaken = (int)(Scene.NMEShipStats.VolleyDamage * Random.Range(.8f, 1f));

        if (Scene.Pack.Spammed) { damageTaken *= 2; Scene.DamageDealt /= 2; }

        else if (Scene.Pack.Crit)
        {
            damageTaken = (int)(.5f * (float)damageTaken * (float)(1f / Datum.Manager.Io.Skill.GetBonusRatio(new Datum.CriticalVolley())));
            Scene.DamageDealt = (int)((float)Scene.DamageDealt + ((float)Scene.DamageDealt * (float)Datum.Manager.Io.Skill.GetBonusRatio(new Datum.CriticalVolley())));
        }

        ((BatterieHUD)Scene.Hud).PlayerCurrent -= damageTaken;
        Datum.Manager.Io.ActiveShip.AdjustLevel(new Datum.CurrentHitPoints(), -damageTaken);
        ((BatterieHUD)Scene.Hud).NMECurrent = Scene.NMEHealth.cur -= Scene.DamageDealt;
        // Debug.Log("Spammed: " + Scene.Pack.Spammed +
        //     ", Damage taken: " + damageTaken +
        //     ", Damage dealt: " + Scene.DamageDealt +
        //     ", current Health: " + Datum.Manager.Io.ActiveShip.GetLevel(new Datum.CurrentHitPoints()));

    }
    readonly int damageTaken;
    readonly BatterieScene Scene;

    public override Dialogue Initiate()
    {
        if (Scene.Pack.Crit) { }
        if (Scene.NMEHealth.cur < (float)(Scene.NMEHealth.max * .3f) &&
            Random.value > .65f)
        {
            _startLine = NMEAttemptingFlee;
            Scene.Escaping = true;
        }
        else if (Datum.Manager.Io.ActiveShip.GetLevel(new Datum.CurrentHitPoints()) < (float)(Datum.Manager.Io.ActiveShip.GetLevel(new Datum.MaxHitPoints()) * .3f))
        {
            _startLine = LowPlayerHealth;
        }
        else if (Scene.NMEHealth.cur < (float)(Scene.NMEHealth.max * .21f) &&
                (Datum.Manager.Io.ActiveShip.GetLevel(new Datum.CurrentHitPoints()) > (float)(Datum.Manager.Io.ActiveShip.GetLevel(new Datum.MaxHitPoints()) * .5f)))
        {
            _startLine = NMESurrender;
        }
        else if (Scene.NMEHealth.cur < (float)(Scene.NMEHealth.max * .3f))
        {
            _startLine = LowNMEHealth;
        }
        else
        {
            _startLine = EvenHealth;
        }


        if (Scene.NMEHealth.cur < 1)
        {
            FirstLine = Victory;
            Scene.Pack.SetResultType(BatterieResultType.Won);
        }
        else
        {
            FirstLine = DamageReport;
        }

        if (Scene.Pack.Spammed)
        {
            FirstLine = Spammed;
        }
        if (Datum.Manager.Io.ActiveShip.GetLevel(new Datum.CurrentHitPoints()) < 1)
        {
            Debug.Log("YOU LOSE");

            FirstLine = GameOver;
        }
        return base.Initiate();
    }

    Line _startLine;

    Line Victory =>
        new Line("We got 'em Cap! Blew 'em out of the water!",
            new EndBatterie_State(Scene, BatterieResultType.Won))
        .SetSpeaker(Speaker)
        ;

    Line Spammed => new Line("Cap, spamming like that only hurts us, the cannons back fired!\nWe've taken " + damageTaken + " damage", AttackAgain)
        .SetSpeaker(Speaker);

    Line DamageReport => new Line(
            "Damage report Cap!" +
                (Scene.Pack.Crit ? "\nCritical Volley! Great execution!" : "") +
                "\nWe dealt " +
                Scene.DamageDealt +
                " damage to the enemy, and have taken " +
                damageTaken +
                " damage to our hull.",
            _startLine)
        .SetSpeaker(Speaker);

    Line LowPlayerHealth => new Line("We're in bad shape, we can't take much more!")
        .SetSpeaker(Speaker)
        .SetResponses(new Response[3] { Attack, Flee, Surrender })
        ;

    Line LowNMEHealth => new Line("We almost got'em! Another volley should surely do it!")
        .SetSpeaker(Speaker)
        .SetResponses(new Response[2] { Attack, Flee })
        ;

    Line NMESurrender => new Line("The ship is flying a white flag, they're surrendering!")
        .SetSpeaker(Speaker)
        .SetResponses(new Response[2] { Attack, AcceptSurrender })
        ;

    Line NMEAttemptingFlee => new Line("The ship is attempting to flee")
        .SetSpeaker(Speaker)
        .SetResponses(new Response[2] { Attack, LetThemGo })
        ;

    Line EvenHealth => new Line("It's not over yet. What'er your orders")
        .SetSpeaker(Speaker)
        .SetResponses(new Response[2] { Attack, Flee })
        ;

    Response Attack => new("Fire another volley", AttackAgain);
    Line AttackAgain => new Line("Load the cannons! Counting off!...", new ResumeBatterie_State(Scene))
        .SetSpeaker(Speaker)
        ;

    Response _flee;
    Response Flee
    {
        get
        {
            if (Random.value > .7f)
            {
                _flee ??= new("Attempt to flee", Fled);
            }
            else
            {
                _flee ??= new("Attempt to flee", CantFlee);
            }
            return _flee;
        }
    }
    Line Fled => new Line("It's better we run and live to fight another day!",
                 new EndBatterie_State(Scene, BatterieResultType.Fled))
        .SetSpeaker(Speaker)
        ;

    Line CantFlee => new Line("It's no good Cap! They've got the wind, we can't outrun them like this. We've no choice but to fight!", AttackAgain)
        .SetSpeaker(Speaker)
        ;

    Response Surrender => new("Surrender to the ship", new EndBatterie_State(Scene, BatterieResultType.Surrender));
    Response AcceptSurrender => new("Accept their surrender", new EndBatterie_State(Scene, BatterieResultType.NMESurrender));
    Response LetThemGo => new("Let them go.", new EndBatterie_State(Scene, BatterieResultType.NMEscaped));

    Line GameOver => new("This is it Cap. Looks like where going down with the ship.", new BatterieToGameOverTransition_State(Scene));

}
