using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;
using Batterie;
using MusicTheory.Rhythms;
using System;

public class QDH_Dialogue : Dialogue
{
    public QDH_Dialogue(State consequentState) : base(consequentState) { }
    public override Dialogue Initiate()
    {
        MuteTypingSounds();
        FirstLine = StartLine;
        FirstLine.Responses[0].SetGoToLine(StartLine);
        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= new Line(s, Replies())
        .SetVideoClip(Assets.RhythmCellQDH)
    ;

    Response[] Replies() => new Response[4]{
        new ("Play again"),
        new ("Try", TrainingBattery()),
        new ("Next", new RestsAndTies_Dialogue(ConsequentState)),
        new ("Quit", new FinishedRhythmCellTutorial_Dialogue(ConsequentState))
    };

    string s => "Quarter Dotted-Half. To perform this cell, tap and hold for one count on beat 1, then tap and whole for three counts on beat 2. I highly recommend you count out loud: 'One, Two, Three, Four'...";

    State TrainingBattery()
    {
        return new BatteryTutorial_State(this, new RestsAndTies_Dialogue(ConsequentState), GetMeasure(), GetSpecs());
    }

    private RhythmSpecs GetSpecs()
    {
        return new RhythmSpecs()
            .SetNumberOfMeasures(1)
            ;
    }

    Measure[] GetMeasure()
    {
        return new Measure[1] {
            new (){Cells = new RhythmCell[1]{
                new RhythmCell()
                    .SetMetricLevel(MetricLevel.Beat)
                    .SetQuantizement(Quantizement.Quarter)
                    .SetRhythmicShape(CellShape.SL)
        }}};
    }
}
