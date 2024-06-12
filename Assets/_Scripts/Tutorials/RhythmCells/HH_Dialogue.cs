
using Dialog;
using Batterie;
using MusicTheory.Rhythms;

public class HH_Dialogue : Dialogue
{
    public HH_Dialogue(State consequentState) : base(consequentState) { }
    public override Dialogue Initiate()
    {
        MuteTypingSounds();
        FirstLine = StartLine;
        FirstLine.Responses[0].SetGoToLine(StartLine);
        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= new Line(s, Replies())
        .SetVideoClip(Assets.RhythmCellHH)
    ;

    Response[] Replies() => new Response[4]{
        new ("Play again"),
        new ("Try", TrainingBattery()),
        new ("Next", new QQQQ_Dialogue(ConsequentState)),
        new ("Quit", new FinishedRhythmCellTutorial_Dialogue(ConsequentState))
    };

    string s => "These are Half Notes. They each get two counts. " +
        "To perform this cell, tap and hold for two counts on beats 1 and 3. " +
        "I highly recommend you count out loud: 'One, Two, Three, Four'...";

    State TrainingBattery() => new BatteryTutorial_State(this, new QQQQ_Dialogue(ConsequentState), GetMeasure(), GetSpecs());


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
}
