
using Dialog;
using MusicTheory.Rhythms;
using Batterie;
public class DHQ_Dialogue : Dialogue
{
    public DHQ_Dialogue(State consequentState) : base(consequentState) { }
    public override Dialogue Initiate()
    {
        MuteTypingSounds();
        FirstLine = StartLine;
        FirstLine.Responses[0].SetGoToLine(StartLine);
        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= new Line(s, Replies())
        .SetVideoClip(Assets.RhythmCellDHQ)
    ;

    Response[] Replies() => new Response[4]{
        new Response("Play again"),
        new Response("Try", TrainingBattery()),
        new Response("Next", new QDH_Dialogue(ConsequentState)),
        new Response("Quit", new FinishedRhythmCellTutorial_Dialogue(ConsequentState))
    };

    string s => "This is a Dotted-Half Note followed by a Quarter Note. " +
        "The Dotted-Half Note gets three beats. " +
        "To perform this cell, tap and hold for three beats on beat 1, " +
        "then tap and hold for one count on beat 4. " +
        "I highly recommend you count out loud\n'One, Two, Three, Four'...";

    State TrainingBattery() => new BatteryTutorial_State(this, new QDH_Dialogue(ConsequentState), GetMeasure(), GetSpecs());


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
