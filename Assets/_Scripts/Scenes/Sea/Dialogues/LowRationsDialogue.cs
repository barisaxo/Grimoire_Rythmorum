using UnityEngine;
using Dialog;

public class LowRationsDialogue : Dialogue
{
    readonly int Ration;
    readonly State SubsequentState;
    public LowRationsDialogue(int ration, State subsequent)
    {
        Ration = ration;
        SubsequentState = subsequent;
        Speaker = Speaker.Pino;
    }

    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        return base.Initiate();
    }

    string Hail_LineText => Ration switch
    {
        0 => "We're out of rations! I'm afraid this is as far as we go Cap...",
        2 => "Cap'n! We're running low on rations. We better fish or trade for some soon...",
        1 => "We're on our last ration Cap! We better fish or trade for some straight away...",
        _ => throw new System.Exception(Ration.ToString()),
    };

    Line _startLine;
    Line StartLine => _startLine ??= Ration == 0 ?
     new Line(Hail_LineText, new SeaToGameOverTransition_State())
        .SetSpeaker(Speaker) :
         new Line(Hail_LineText, SubsequentState)
        .SetSpeaker(Speaker)
        ;

}