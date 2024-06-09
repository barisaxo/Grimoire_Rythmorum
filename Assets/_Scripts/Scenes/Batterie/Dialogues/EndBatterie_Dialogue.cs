using Dialog;

public class EndBatterie_Dialogue : Dialogue
{
    readonly int _gold, _rations, _mats, _patterns;
    readonly BatterieResultType Result;

    public EndBatterie_Dialogue(
        int gold,
        int mats,
        int rations,
        int patterns,
        BatterieResultType result)
    {
        _gold = gold;
        _mats = mats;
        _rations = rations;
        _patterns = patterns;
        Result = result;
        Speaker = Speaker.Pino;
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
                FirstLine = Won;
                break;

            case BatterieResultType.Spam: FirstLine = CheatLine; break;
        }

        return base.Initiate();
    }

    Line Won => new Line(Found + Gold + Mats + Rations + Patterns, new SeaScene_State())
        .SetSpeaker(Speaker)
        ;

    Line FledLine => new Line("We're safe now Cap.\n", new SeaScene_State())
        .SetSpeaker(Speaker)
        ;

    Line NMEEscapedLine => new Line("They got away Cap!\n", new SeaScene_State())
        .SetSpeaker(Speaker)
        ;

    Line NMESurrendered => new Line(Found + Gold + Mats + Rations + Patterns, new SeaScene_State())
        .SetSpeaker(Speaker)
        ;

    Line SurrenderedToNME => new Line(Lost + Gold + Mats + RationsEnd, new SeaScene_State())
        .SetSpeaker(Speaker)
        ;

    Line CheatLine => new Line("...", CheatLine2)
        .SetSpeaker(Speaker)
        ;

    Line CheatLine2 => new Line("I'm sorry Cap, but acting like that isn't going to get us very far!", new SeaScene_State())
        .SetSpeaker(Speaker)
        ;

    string Lost => "We lost ";
    string Found => "We found ";
    string Gold => _gold + " gold, ";
    string Mats => _mats + " materials, ";
    string Rations => _rations + " rations, and ";
    string RationsEnd => _rations + " rations.\n";
    string Patterns => _patterns + " patterns.\n";
}

