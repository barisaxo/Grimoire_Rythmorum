using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;
using Batterie;
using MusicTheory.Rhythms;

public class QrQQrQ_Dialogue : Dialogue
{
    public QrQQrQ_Dialogue(State consequentState) : base(consequentState) { }
    public override Dialogue Initiate()
    {
        MuteTypingSounds();
        FirstLine = StartLine;
        FirstLine.Responses[0].SetGoToLine(StartLine);
        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= new Line(s, Replies())
        .SetVideoClip(Assets.RhythmCellQrQQrQ)
    ;

    Response[] Replies() => new Response[4]{
        new Response("Play again"),
        new Response("Try", TrainingBattery()),
        new Response("Next", new Tie_Dialogue(ConsequentState)),
        new Response("Quit", new FinishedRhythmCellTutorial_Dialogue(ConsequentState))
    };

    string s => "Quarter note rests. " +
        "This is the same rhythmic shape as the all quarter note cell we did before, " +
        "but some of the quarter notes are now rests. " +
        "To perform this cell, tap and hold the notes, and wait during the rests. " +
        "I highly recommend you count out loud: 'One, Two, Three, Four'...";

    State TrainingBattery() => new BatteryTutorial_State(this, new Tie_Dialogue(ConsequentState), GetMeasure(), GetSpecs());


    private RhythmSpecs GetSpecs()
    {
        return new RhythmSpecs()
            .SetNumberOfMeasures(1)
            ;
    }



    Measure[] GetMeasure()
    {
        Measure[] rhythm = new Measure[1] {
            new (){Cells = new RhythmCell[1]{
                new RhythmCell()
                    .SetMetricLevel(MetricLevel.Beat)
                    .SetQuantizement(Quantizement.Quarter)
                    .SetRhythmicShape(CellShape.LS)
        }}};

        return rhythm;
    }
}
