using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class MusicaTask_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;

    public MusicaTask_Dialogue(Dialogue returnTo, Speaker speaker)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
    }

    override public Dialogue Initiate()
    {
        FirstLine = MusicaLine;
        return this;
    }

    string Musica_LineText => UnityEngine.Random.Range(0, 4) switch
    {
        0 => "Sure thing! We are have some of the best cartographers in the fleet!",
        1 => "Lost your way huh? I think we can help.",
        2 => "Shouldn't be a problem, what do you need?",
        _ => "Musica can be tricky. It might not be absolutely necessary, but it sure helps to know it!"
    };

    Response[] GetResponses()
    {
        List<Response> temp = new();
        // if (!DataManager.Io.CharacterData.Sextant) temp.Add(SextantResponse);
        if (Data.Manager.Io.ActiveShip.GetLevel(new Data.StarChartStorage()) > 0) temp.Add(MapResponse);
        if (Data.Manager.Io.ActiveShip.GetLevel(new Data.GramophoneStorage()) > 0) temp.Add(GramoResponse);

        if (temp.Count == 0) temp.Add(CantResponse);
        else temp.Add(BackResponse);

        return temp.ToArray();
    }

    Line _musicaLine;
    Line MusicaLine => _musicaLine ??= new Line(Musica_LineText, GetResponses())
        .SetSpeaker(Speaker)
        ;

    Response _sextantResponse;
    Response SextantResponse => _sextantResponse ??= new Response("Calibrate Sextant",
        new CalibrateSextant_Dialogue(this, Speaker)
            .SetSpeaker(Speaker));

    Response _mapResponse;
    Response MapResponse => _mapResponse ??= new Response("Decipher Map",
        new MapTask_Dialogue(this, Speaker)
            .SetSpeaker(Speaker));

    Response _gramoResponse;
    Response GramoResponse => _gramoResponse ??= new Response("Open Gramophone",
        new OpenGramoTask_Dialogue(this, Speaker)
            .SetSpeaker(Speaker));


    Response _cantResponse;
    Response CantResponse => _cantResponse ??= new Response("(nothing to do here)", ReturnTo);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
}
