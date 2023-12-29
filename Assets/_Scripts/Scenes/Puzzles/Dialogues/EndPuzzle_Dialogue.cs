using UnityEngine;
using Dialog;

public class EndPuzzle_Dialogue : Dialogue
{
    private readonly FunFact FunFact;
    public readonly int _gold, _mats, _rations, _damage;
    public readonly bool _won;

    string Recap => _won ? "Great job Cap'n! You'll be the best Bard yet!\n" :
                           "No worries Cap! You'll do better next time.\nCheck the menu for tutorials if you need help.\n";
    string Won => _won ? "We won " : "We lost ";
    string Gold => _gold + " gold, ";
    string Mats => _mats + " materials, and ";
    string Rations => _rations + " rations.\n";
    int winSign => _won ? 1 : -1;
    string Sextant => "We found a sextant!";
    string SextantDef => "Great find Cap'n! This sextant reveals our current position.\n" +
        "We need both the map and the sextant to find the Cromatica.";

    public EndPuzzle_Dialogue(bool won)
    {
        _won = won;
        Speaker = new Speaker(
            Sea.Scene.Io.NearestNPC.Flag,
            Sea.Scene.Io.NearestNPC.Name,
            Sea.Scene.Io.NearestNPC.FlagColor);
        FirstLine = _won ? RecapLineWithSextant : RecapLineWithOutSextant;
    }


    public EndPuzzle_Dialogue(Color color, Sprite flag, string region)
    {
        _won = true;
        Speaker = new Speaker(flag, "[" + region + " Ship]:\n", color);
        FirstLine = RecapLineWithSextant;
    }

    public EndPuzzle_Dialogue(bool won, int gold, int mats, int rations)
    {
        Speaker = new Speaker(
            Sea.Scene.Io.NearestNPC.Flag,
            Sea.Scene.Io.NearestNPC.Name,
            Sea.Scene.Io.NearestNPC.FlagColor);
        _won = won;
        _gold = gold * winSign; _mats = mats * winSign; _rations = rations * winSign;
        FirstLine = RecapLineWithOutSextant;
    }

    Line RecapLineWithSextant => new Line(Recap, ResultsWithSextant)
        .SetSpeaker(Speaker)
        ;

    Line RecapLineWithOutSextant => new Line(Recap, FunFactLine)
        .SetSpeaker(Speaker)
        ;

    Line ResultsWithSextant => new Line(Sextant, SextantLine)
        .SetSpeaker(Speaker)
        ;

    Line ResultsWithOutSextant => new Line(Won + Gold + Mats + Rations, FunFactLine)
        .SetSpeaker(Speaker)
        ;

    Line FunFactLine => new Line(FunFact.GetFunFact(), new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    Line SextantLine => new Line(SextantDef, FunFactLine)
        .SetSpeaker(Speaker)
        ;

}

