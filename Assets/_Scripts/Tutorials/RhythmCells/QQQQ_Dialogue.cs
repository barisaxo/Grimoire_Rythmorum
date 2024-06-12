using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;
using Batterie;
using MusicTheory.Rhythms;

public class QQQQ_Dialogue : Dialogue
{
    public QQQQ_Dialogue(State consequentState) : base(consequentState) { }
    public override Dialogue Initiate()
    {
        MuteTypingSounds();
        FirstLine = StartLine;
        FirstLine.Responses[0].SetGoToLine(StartLine);
        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= new Line(s, Replies())
        .SetVideoClip(Assets.RhythmCellQQQQ)
    ;

    Response[] Replies() => new Response[4]{
        new Response("Play again"),
        new Response("Try", TrainingBattery()),
        new Response("Next", new HQQ_Dialogue(ConsequentState)),
        new Response("Quit", new FinishedRhythmCellTutorial_Dialogue(ConsequentState))
    };

    string s => "These are Quarter Notes. " +
        "They each get one count. " +
        "To perform this cell, tap and hold for one count on beats 1, 2, 3, and 4. " +
        "I highly recommend you count out loud\n'One, Two, Three, Four'...";

    State TrainingBattery() => new BatteryTutorial_State(this, new HQQ_Dialogue(ConsequentState), GetMeasure(), GetSpecs());


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
                    .SetRhythmicShape(CellShape.SSSS)
        }}};

        return rhythm;
    }
}


// rhythm[0][0] = new RhythmCell()
//     .SetQuantizement(Quantizement.Quarter);
// rhythm[0][0].Quantizement = Quantizement.Quarter;
// rhythm[0][0].LongCell = true;
// rhythm[0][0].Tied = false;
// rhythm[0][0].Rest = false;
// rhythm[0][0].RhythmicShape = CellShape.qqqq;