using Dialog;

public class BuySextant_Dialogue : Dialogue
{
    public BuySextant_Dialogue(Speaker speaker)
    {
        Speaker = speaker;
    }

    public override Dialogue Initiate()
    {
        FirstLine = Sextant_Line;
        return this;
    }

    Line _purchased_Line;
    Line Purchased_Line => _purchased_Line ??= new Line(BoughtSextant_Text, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    Line _muscopaLine;
    Line Muscopa_Line => _muscopaLine ??= new Line(Muscopa_LineText, PuzzleSelector.WeightedRandomPuzzleState(DataManager.Io.TheoryPuzzleData))
        .SetSpeaker(Speaker)
        .SetCameraPan(
            new UnityEngine.Vector3(-50, Cam.Io.Camera.transform.rotation.eulerAngles.y, Cam.Io.Camera.transform.rotation.eulerAngles.z),
            Cam.Io.Camera.transform.position,
            2.3f)
        ;

    Line _sextant_Line;
    Line Sextant_Line => _sextant_Line ??= new Line(SextantLine_Text, SextantChallenge_Response)
        .SetSpeaker(Speaker);

    Response _muscopa_response;
    Response Muscopa_Response => _muscopa_response ??= new Response(PlayMuscopa_RepText, Muscopa_Line);

    string Muscopa_LineText => UnityEngine.Random.Range(0, 4) switch
    {
        0 => "Fair warning: we are undefeated!",
        1 => "A fellow bard eh? Let's play!",
        2 => "Anytime is a good time for Muscopa.",
        _ => "Our merchants are expecting us soon, but I suppose one quick game..."
    };

    readonly string BoughtSextant_Text = "Good deal!";
    readonly string PlayMuscopa_RepText = "Play Muscopa";
    readonly string SextantLine_Text = "I could sell you one for 500 gold, or, if you beat me at Muscopa, I'll give you one. What say you?";

    Response[] _sextantChallenge_Response;
    Response[] SextantChallenge_Response => _sextantChallenge_Response ??= GetSextantReplies();
    Response[] GetSextantReplies()
    {
        if (DataManager.Io.CharacterData.Coins >= 500)
        {
            return new Response[3] {
            new Response("Buy (-500 gold)").SetGoToLine(Purchased_Line).SetPlayerAction(BuySextant),
            Muscopa_Response,
            Back_Response};
        }
        else
        {
            return new Response[3] {
            new Response("(not enough gold)"),
            Muscopa_Response,
            Back_Response
            };
        }
    }

    Response Back_Response => new Response("Back", new ShipTask_Dialogue(Speaker)
                .SetSpeaker(Speaker));

    private void BuySextant()
    {
        DataManager.Io.CharacterData.Coins -= 500;
        DataManager.Io.CharacterData.Sextant = true;
        //Board.TryInstantiateCromatica(Data.CharacterData, Data.GameplayData);
    }

}
