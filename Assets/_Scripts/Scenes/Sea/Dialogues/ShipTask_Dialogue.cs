using Dialog;
using MusicTheory.Arithmetic;

public class ShipTask_Dialogue : Dialogue
{
    public ShipTask_Dialogue()
    {
        SpeakerIcon = new UnityEngine.Sprite[] { SeaScene.Io.NearNPCShip.Flag };
        SpeakerName = SeaScene.Io.NearNPCShip.Name;
        SpeakerColor = SeaScene.Io.NearNPCShip.FlagColor;
    }

    override public Dialogue Initiate() { FirstLine = OneTaskLine; return this; }

    readonly string OneTask_LineText = "We've got time for one task, so what will it be?";

    readonly string TradeGoods_RepText = "Trade goods";
    readonly string Sextant_RepText = "Buy Sextant";
    readonly string PlayMuscopa_RepText = "Play Muscopa";
    readonly string Attack_RepText = "Attack the ship";
    readonly string Leave_RepText = "Leave";

    string Muscopa_LineText => UnityEngine.Random.Range(0, 4) switch
    {
        0 => "Fair warning: we are undefeated!",
        1 => "A fellow bard eh? Let's play!",
        2 => "Anytime is a good time for Muscopa.",
        _ => "Our merchants are expecting us soon, but I suppose one quick game..."
    };

    string Attack_LineText => UnityEngine.Random.Range(0, 4) switch
    {
        0 => "What are you doing!?\nAll hands prepare for battle!",
        1 => "Pirates!? I should have known.\nAll right sailors, we have some pirates to sink!",
        2 => "We wont go down without a fight!",
        _ => "Despicable..."
    };

    string Leave_LineText => UnityEngine.Random.Range(0, 4) switch
    {
        0 => "Safe journey!",
        1 => "Until next time!",
        2 => "Keep an eye out for pirates, they can look just like trade ships.",
        _ => "Then why did you... I... We have work to do!"
    };

    Line _oneTaskLine;
    Line OneTaskLine => _oneTaskLine ??= new Line(OneTask_LineText, new Response[4]
        {
            Trade_Response,
            DataManager.Io.CharacterData.Sextant?  Muscopa_Response : Sextant_Response,
            Attack_Response,
            Leave_Response,
        })
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerName(SpeakerName)
        .SetSpeakerColor(SpeakerColor);


    Line _muscopaLine;
    Line Muscopa_Line => _muscopaLine ??= new Line(Muscopa_LineText, PuzzleSelector.WeightedRandomPuzzleState(DataManager.Io.TheoryPuzzleData))
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerName(SpeakerName)
        .SetSpeakerColor(SpeakerColor)
        .SetCameraPan(
            new UnityEngine.Vector3(-50, Cam.Io.Camera.transform.rotation.eulerAngles.y, Cam.Io.Camera.transform.rotation.eulerAngles.z),
            Cam.Io.Camera.transform.position,
            2.3f)
        ;

    Line _attackLine;
    Line Attack_Line => _attackLine ??= new Line(Attack_LineText, new BatterieAndCadence_State(new BatteriePack()))
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerName(SpeakerName)
        .SetSpeakerColor(SpeakerColor)
        .FadeToNextState()
        ;

    Line _leaveLine;
    Line Leave_Line => _leaveLine ??= new Line(Leave_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerName(SpeakerName)
        .SetSpeakerColor(SpeakerColor);


    Response _trade_response;
    Response Trade_Response => _trade_response ??= new Response(TradeGoods_RepText, new Trade_Dialogue()
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerName(SpeakerName)
        .SetSpeakerColor(SpeakerColor)
    );

    Response _sextant_Response;
    Response Sextant_Response => _sextant_Response ??= new Response(Sextant_RepText, new BuySextant_Dialogue()
        .SetSpeakerIcon(SpeakerIcon)
        .SetSpeakerName(SpeakerName)
        .SetSpeakerColor(SpeakerColor)
    );

    Response _muscopa_response;
    Response Muscopa_Response => _muscopa_response ??= new Response(PlayMuscopa_RepText, Muscopa_Line);

    Response _attack_response;
    Response Attack_Response => _attack_response ??= new Response(Attack_RepText, Attack_Line);

    Response _leave_response;
    Response Leave_Response => _leave_response ??= new Response(Leave_RepText, Leave_Line);


}
