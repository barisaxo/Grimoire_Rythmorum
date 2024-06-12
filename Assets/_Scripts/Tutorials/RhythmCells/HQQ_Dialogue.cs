
using Dialog;
using Batterie;
using MusicTheory.Rhythms;

public class HQQ_Dialogue : Dialogue
{
    public HQQ_Dialogue(State consequentState) : base(consequentState) { }
    public override Dialogue Initiate()
    {
        MuteTypingSounds();
        FirstLine = StartLine;
        FirstLine.Responses[0].SetGoToLine(StartLine);
        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= new Line(s, Replies())
        .SetVideoClip(Assets.RhythmCellHQQ)
    ;

    Response[] Replies() => new Response[4]{
        new Response("Play again"),
        new Response("Try", TrainingBattery()),
        new Response("Next", new QQH_Dialogue(ConsequentState)),
        new Response("Quit", new FinishedRhythmCellTutorial_Dialogue(ConsequentState))
    };

    string s => "Half Quarter Quarter, or long short short. To perform this cell, tap and hold for two counts on beat 1, then tap and hold for one counts on beats 3 and 4. I highly recommend you count out loud: 'One, Two, Three, Four'...";

    State TrainingBattery() => new BatteryTutorial_State(this, new QQH_Dialogue(ConsequentState), GetMeasure(), GetSpecs());


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
                    .SetRhythmicShape(CellShape.LSS)
        }}};

        return rhythm;
    }
}
