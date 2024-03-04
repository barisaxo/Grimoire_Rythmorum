using UnityEngine;
using Dialog;

public class EndPuzzle_Dialogue : Dialogue
{
    private readonly FunFact FunFact;
    public readonly int _gold, _mats, _rations, _damage;
    public readonly bool _won;

    string Recap => _won ? SextantDef :
                           "No worries Cap! You'll do better next time!\n";
    // string Won => _won ? "We won " : "We lost ";
    // string Gold => _gold + " gold, ";
    // string Mats => _mats + " materials, and ";
    // string Rations => _rations + " rations.\n";
    // int winSign => _won ? 1 : -1;
    string SextantDef => "Great job Cap'n, you decoded the Star Chart! This reveals the position " +
        "of some hidden ancient Bardic treasures!\n";

    public EndPuzzle_Dialogue(bool won, State subsequentState)
    {
        SubsequentState = subsequentState;
        _won = won;
        // Speaker = new Speaker(
        // Sea.WorldMapScene.Io.NearestNPC?.Flag,
        // Sea.WorldMapScene.Io.NearestNPC?.Name,
        // Sea.WorldMapScene.Io.NearestNPC?.FlagColor);
        FirstLine = _won ? RecapLineWithSextant : RecapLineWithOutSextant;
        Debug.Log(subsequentState);
    }

    readonly State SubsequentState;

    // public EndPuzzle_Dialogue(Color color, Sprite flag, string region)
    // {
    //     _won = true;
    //     Speaker = new Speaker(flag, "[" + region + " Ship]:\n", color);
    //     FirstLine = RecapLineWithSextant;
    // }

    // public EndPuzzle_Dialogue(bool won, int gold, int mats, int rations)
    // {
    //     // Speaker = new Speaker(
    //     //     Sea.WorldMapScene.Io.NearestNPC.Flag,
    //     //     Sea.WorldMapScene.Io.NearestNPC.Name,
    //     //     Sea.WorldMapScene.Io.NearestNPC.FlagColor);
    //     _won = won;
    //     _gold = gold * winSign; _mats = mats * winSign; _rations = rations * winSign;
    //     FirstLine = RecapLineWithOutSextant;
    // }

    Line RecapLineWithSextant => new Line(Recap, FunFactLine)
        // .SetSpeaker(Speaker)
        ;

    Line RecapLineWithOutSextant => new Line(Recap, FunFactLine)
        // .SetSpeaker(Speaker)
        ;

    // Line ResultsWithSextant => new Line( SextantLine)
    //     .SetSpeaker(Speaker)
    //     ;

    // Line ResultsWithOutSextant => new Line(Won + Gold + Mats + Rations, FunFactLine)
    //     .SetSpeaker(Speaker)
    //     ;

    Line FunFactLine => new Line(FunFact.GetFunFact(), SubsequentState)
        // .SetSpeaker(Speaker)
        ;

    // Line SextantLine => new Line(SextantDef, FunFactLine)
    //     .SetSpeaker(Speaker)
    //     ;

}

