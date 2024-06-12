using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;
using Batterie;
using MusicTheory.Rhythms;

public class Tie_Dialogue : Dialogue
{
    public Tie_Dialogue(State consequentState) : base(consequentState) { }
    public override Dialogue Initiate()
    {
        MuteTypingSounds();
        FirstLine = StartLine;
        FirstLine.Responses[0].SetGoToLine(StartLine);
        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= new Line(s, Replies())
        .SetVideoClip(Assets.RhythmCellTie)
    ;

    Response[] Replies() => new Response[3]{
        new Response("Play again"),
        new Response("Try", TrainingBattery()),
        new Response("Continue", new FinishedRhythmCellTutorial_Dialogue(ConsequentState)),
        // new Response("Quit", ConsequentState)
    };

    string s => "This is an example of two cells that are tied together. " +
        "Notes that are tied together should be treated as if they are the same note. " +
        "To perform this cell, tap and hold the notes (and continue holding for the tied notes duration), and wait for the rests. " +
        "I highly recommend you count out loud: 'One, Two, Three, Four, One, Two, Three, Four'...";

    State TrainingBattery() => new BatteryTutorial_State(this, new FinishedRhythmCellTutorial_Dialogue(ConsequentState), GetMeasure(), GetSpecs());


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
                    .SetRhythmicShape(CellShape.LL)
        }}};

        return rhythm;
    }
    // RhythmBars GetRhythmBar()
    // {
    //     RhythmBars whole = new RhythmBars(1, DataManager.Io.GameplayData)
    //         .SetSpecificRhythms(Rhythm())
    //         .SetTempo(80)
    //         .SetSubDivision(SubDivisionTier.QuartersOnly)
    //         .ConstructRhythmBars(false);
    //     ;

    //     return whole;

    //     RhythmCell[][] Rhythm()
    //     {
    //         RhythmCell[][] rhythm = new RhythmCell[2][];

    //         rhythm[0] = new RhythmCell[1];
    //         rhythm[0][0] = new RhythmCell();
    //         rhythm[0][0].Quantizement = Quantizement.Quarter;
    //         rhythm[0][0].LongCell = true;
    //         rhythm[0][0].Tied = true;
    //         rhythm[0][0].Rest = true;
    //         rhythm[0][0].RhythmicShape = CellShape.qhq;

    //         rhythm[1] = new RhythmCell[1];
    //         rhythm[1][0] = new RhythmCell();
    //         rhythm[1][0].Quantizement = Quantizement.Quarter;
    //         rhythm[1][0].LongCell = true;
    //         rhythm[1][0].Tied = false;
    //         rhythm[1][0].Rest = false;
    //         rhythm[1][0].RhythmicShape = CellShape.qhq;
    //         return rhythm;
    //     }
    // }
}
