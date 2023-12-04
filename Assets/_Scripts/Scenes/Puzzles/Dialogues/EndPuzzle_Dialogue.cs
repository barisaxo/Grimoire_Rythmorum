using UnityEngine;
using Dialog;

public class EndPuzzle_Dialogue : Dialogue
{
    private readonly FunFact FunFact;
    public readonly int _gold, _mats, _rations, _damage;
    public readonly bool _won;

    string Recap => _won ? "Great job! Muscopa champ in the making!\n" :
                           "No worries mate! You'll do better next time.\nCheck the menu for tutorials if you need help.\n";
    string Won => _won ? "We won " : "We lost ";
    string Gold => _gold + " gold, ";
    string Mats => _mats + " materials, and ";
    string Rations => _rations + " rations.\n";
    int winSign => _won ? 1 : -1;
    string Sextant => "We found a sextant!";
    string SextantDef => "Great find Cap'n! This sextant reveals our current position.\n" +
        "We need both the map and the sextant to find the Cromatica.";

    public EndPuzzle_Dialogue()
    {
        _won = true;
        SpeakerIcon = new UnityEngine.Sprite[] { SeaScene.Io.NearNPCShip.Flag };
        SpeakerName = SeaScene.Io.NearNPCShip.Name;
        SpeakerColor = SeaScene.Io.NearNPCShip.FlagColor;
        FirstLine = RecapLineWithSextant;
    }


    public EndPuzzle_Dialogue(Color color, Sprite flag, string region)
    {
        _won = true;
        SpeakerColor = color;
        SpeakerIcon = new Sprite[1] { flag };
        SpeakerName = "[" + region + " Ship]:\n";
        FirstLine = RecapLineWithSextant;
    }

    public EndPuzzle_Dialogue(bool won, int gold, int mats, int rations)
    {
        SpeakerIcon = new UnityEngine.Sprite[] { SeaScene.Io.NearNPCShip.Flag };
        SpeakerName = SeaScene.Io.NearNPCShip.Name;
        SpeakerColor = SeaScene.Io.NearNPCShip.FlagColor;
        _won = won;
        _gold = gold * winSign; _mats = mats * winSign; _rations = rations * winSign;
        FirstLine = RecapLineWithOutSextant;
    }

    Line RecapLineWithSextant => new Line(Recap, ResultsWithSextant)
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerColor(SpeakerColor)
        .SetSpeakerName(SpeakerName);

    Line RecapLineWithOutSextant => new Line(Recap, ResultsWithOutSextant)
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerColor(SpeakerColor)
        .SetSpeakerName(SpeakerName);

    Line ResultsWithSextant => new Line(Sextant, SextantLine)
        .SetSpeakerIcon(Assets.Pino)
        .SetSpeakerName(Pino);

    Line ResultsWithOutSextant => new Line(Won + Gold + Mats + Rations, FunFactLine)
        .SetSpeakerIcon(Assets.Pino)
        .SetSpeakerName(Pino);

    Line FunFactLine => new Line(FunFact.GetFunFact(), new NPCSailAway_State(new SeaScene_State()))
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerColor(SpeakerColor)
        .SetSpeakerName(SpeakerName)
        ;

    Line SextantLine => new Line(SextantDef, FunFactLine)
        .SetSpeakerIcon(Assets.Pino)
        .SetSpeakerName(Pino);

}

