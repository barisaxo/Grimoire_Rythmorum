using Dialog;
using Batterie;
using MusicTheory.Rhythms;

public class W_Dialogue : Dialogue
{
    public W_Dialogue(State consequentState) : base(consequentState) { }

    public override Dialogue Initiate()
    {
        MuteTypingSounds();
        FirstLine = StartLine;
        FirstLine.Responses[0].SetGoToLine(StartLine);


        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= new Line(s, Replies())
        .SetVideoClip(Assets.RhythmCellW);

    Response[] Replies() => new Response[4]{
        new Response("Play again"),
        new Response("Try", CountOff),
        new Response("Next", new HH_Dialogue(ConsequentState)),
        new Response("Quit", new FinishedRhythmCellTutorial_Dialogue(ConsequentState))
    };

    string s => "This is a Whole Note, and is worth four beats. " +
        "To perform it, tap on beat 1 and hold for four counts. " +
        "I highly recommend you count out loud\n     'One, Two, Three, Four'...";


    Line _countOff;
    Line CountOff => _countOff ??= new Line(count, TrainingBattery());

    string count => "Before you perform, there will be a 'Count Off'. You will hear a series of clicks to prepare you to start. These clicks will be spaced like this:" +
    "\n\n1   .   .   .   2   .   .   .   3   .   +   .   4   .   +   .";



    State TrainingBattery() => new BatteryTutorial_State(this, new HH_Dialogue(ConsequentState), GetMeasure(), GetSpecs());


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
                    .SetRhythmicShape(CellShape.L)
        }}};

        return rhythm;
    }
}
