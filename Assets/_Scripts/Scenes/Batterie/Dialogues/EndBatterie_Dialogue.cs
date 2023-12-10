using Dialog;

public class EndBatterie_Dialogue : Dialogue
{
    readonly int _gold, _rations, _damage, _mats;
    readonly bool _map;
    readonly BatterieResultType Result;

    public EndBatterie_Dialogue(
        int gold,
        int mats,
        int rations,
        int damage,
        bool map,
        BatterieResultType result)
    {
        _gold = gold;
        _mats = mats;
        _rations = rations;
        _damage = damage;
        _map = map;
        Result = result;
    }

    public override Dialogue Initiate()
    {
        switch (Result)
        {
            case BatterieResultType.NMEscaped: FirstLine = NMEEscapedLine; break;

            case BatterieResultType.NMESurrender: FirstLine = NMESurrendered; break;

            case BatterieResultType.Surrender: FirstLine = SurrenderedToNME; break;

            case BatterieResultType.Fled: FirstLine = FledLine; break;

            case BatterieResultType.Won:
                if (_map) { FirstLine = WonWithMap; }
                else { FirstLine = WonWithOutMap; }
                break;

            case BatterieResultType.Spam: FirstLine = CheatLine; break;
        }

        return base.Initiate();
    }

    Line WonWithMap => new Line(Damage + Found + Gold + Mats + Rations + Map, MapLine)
        .SetSpeakerIcon(Assets.Pino)
        .SetSpeakerName(Pino);

    Line MapLine => new Line(GhostShip, new SeaScene_State())
        .SetSpeakerIcon(Assets.Pino)
        .SetSpeakerName(Pino)
        ;

    Line WonWithOutMap => new Line(Damage + Found + Gold + Mats + Rations, new SeaScene_State())
        .SetSpeakerIcon(Assets.Pino)
        .SetSpeakerName(Pino)
        ;

    Line FledLine => new Line("We're safe now Cap.\n" + Damage, new SeaScene_State())
        .SetSpeakerIcon(Assets.Pino)
        .SetSpeakerName(Pino)
        ;

    Line NMEEscapedLine => new Line("They got away Cap!\n" + Damage, new SeaScene_State())
        .SetSpeakerIcon(Assets.Pino)
        .SetSpeakerName(Pino)
        ;

    Line NMESurrendered => new Line(Damage + Found + Gold + Mats + Rations, new SeaScene_State())
        .SetSpeakerIcon(Assets.Pino)
        .SetSpeakerName(Pino)
        ;

    Line SurrenderedToNME => new Line(Damage + Lost + Gold + Mats + Rations, new SeaScene_State())
        .SetSpeakerIcon(Assets.Pino)
        .SetSpeakerName(Pino)
        ;

    Line CheatLine => new Line("...", CheatLine2)
        .SetSpeakerIcon(Assets.Pino)
        .SetSpeakerName(Pino)
        ;

    Line CheatLine2 => new Line("I'm sorry Cap, but acting like that isn't going to get us very far!", new SeaScene_State())
        .SetSpeakerIcon(Assets.Pino)
        .SetSpeakerName(Pino)
        ;


    string Damage => "Our ship took " + _damage + " damage.\n";
    string Lost => "We lost ";
    string Found => "We found ";
    string Gold => _gold + " gold, ";
    string Mats => _mats + " materials, and ";
    string Rations => _rations + " rations.\n";
    string Map => "We found the map of Cromatica!";
    string GhostShip => "Great find Cap'n! This map tells us where the ghost ship known as Cromatica makes berth.\n" +
            "We need both the map and the sextant to find the Cromatica.";
}

