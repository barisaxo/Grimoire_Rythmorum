using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class SellMaps_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    int Maps => DataManager.Io.CharacterData.Maps;

    public SellMaps_Dialogue(Dialogue returnTo, Speaker speaker)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
    }

    public override Dialogue Initiate()
    {
        FirstLine = BuyMaps_Line;
        return base.Initiate();
    }

    Line _MapsLine;
    Line BuyMaps_Line => _MapsLine ??= new Line(Maps_LineText, MapsResponses)
        .SetSpeaker(Speaker);

    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;

    readonly string TradeComplete_LineText = "Good deal! Until next time!";
    readonly string Maps_LineText = "How many maps do you want to sell?";

    readonly string MapsLarge_RepText = "10 [350 gold]";
    readonly string MapsMedium_RepText = "5 [200 gold]";
    readonly string MapsSmall_RepText = "1 [50 gold]";

    Response _MapsLarge_response;
    Response MapsLarge_Response => _MapsLarge_response ??= new Response(MapsLarge_RepText, TradeComplete_Line)
        .SetPlayerAction(SellMapsLarge);

    Response _MapsMedium_response;
    Response MapsMedium_Response => _MapsMedium_response ??= new Response(MapsMedium_RepText, TradeComplete_Line)
        .SetPlayerAction(SellMapsMedium);

    Response _MapsSmall_response;
    Response MapsSmall_Response => _MapsSmall_response ??= new Response(MapsSmall_RepText, TradeComplete_Line)
        .SetPlayerAction(SellMapsSmall);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);

    Response[] _MapsResponses;
    Response[] MapsResponses => _MapsResponses ??= GetMapsResponses();
    Response[] GetMapsResponses()
    {
        List<Response> responses = new();

        if (!(Maps < 10)) { responses.Add(MapsLarge_Response); }
        if (!(Maps < 5)) { responses.Add(MapsMedium_Response); }
        if (!(Maps < 1)) { responses.Add(MapsSmall_Response); }
        responses.Add(BackResponse);

        return responses.ToArray();
    }

    void SellMapsSmall()
    {
        DataManager.Io.CharacterData.Maps -= 1;
        DataManager.Io.CharacterData.Coins += 50;
    }

    void SellMapsMedium()
    {
        DataManager.Io.CharacterData.Maps -= 5;
        DataManager.Io.CharacterData.Coins += 5 * 40;
    }

    void SellMapsLarge()
    {
        DataManager.Io.CharacterData.Maps -= 10;
        DataManager.Io.CharacterData.Coins += 10 * 35;
    }

}
