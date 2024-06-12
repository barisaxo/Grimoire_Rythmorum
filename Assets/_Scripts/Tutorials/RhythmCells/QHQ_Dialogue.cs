using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;
using Batterie;
using MusicTheory.Rhythms;

public class QHQ_Dialogue : Dialogue
{
    public QHQ_Dialogue(State consequentState) : base(consequentState) { }
    public override Dialogue Initiate()
    {
        MuteTypingSounds();
        FirstLine = StartLine;
        FirstLine.Responses[0].SetGoToLine(StartLine);
        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= new Line(s, Replies())
        .SetVideoClip(Assets.RhythmCellQHQ)
    ;

    Response[] Replies() => new Response[4]{
        new Response("Play again"),
        new Response("Try", TrainingBattery()),
        new Response("Next", new DHQ_Dialogue(ConsequentState)),
        new Response("Quit", new FinishedRhythmCellTutorial_Dialogue(ConsequentState))
    };

    string s => "Quarter Half Quarter, or short long short. To perform this cell, tap and hold for one count on beat 1, then tap and hold two counts on beat 3 and finally tap and hold for one count on beat 4. I highly recommend you count out loud: 'One, Two, Three, Four'...";

    State TrainingBattery() => new BatteryTutorial_State(this, new DHQ_Dialogue(ConsequentState), GetMeasure(), GetSpecs());


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
                    .SetRhythmicShape(CellShape.SLS)
        }}};

        return rhythm;
    }
}
