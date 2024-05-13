using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class CalibrateSextant_Dialogue : Dialogue
{
    public CalibrateSextant_Dialogue(Dialogue returnTo, Speaker speaker)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
    }

    public override Dialogue Initiate()
    {
        FirstLine = CalibrateLine;
        return base.Initiate();
    }

    readonly Dialogue ReturnTo;

    readonly string Calibrate_LineText = "Lost your way huh? We have some excellent cartographers. 200 gold and we'll get that sextant calibrated for you.";
    Line _calibrateLine;
    Line CalibrateLine => _calibrateLine ??= new Line(Calibrate_LineText, new Response[] { CalibrateResponse, BackResponse })
        .SetSpeaker(Speaker);

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    Response _calibrateLarge_response;
    Response CalibrateResponse => _calibrateLarge_response ??= new Response("OK [-200 gold]", TradeComplete_Line)
        .SetPlayerAction(CalibrateSextant);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);

    void CalibrateSextant()
    {
        // Data.Two.Manager.Io.CharacterData.Sextant = true;
    }
}
