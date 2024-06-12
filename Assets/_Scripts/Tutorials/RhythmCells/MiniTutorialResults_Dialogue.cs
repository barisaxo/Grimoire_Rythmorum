
using Dialog;
using Batterie;
using MusicTheory.Rhythms;

public class MiniTutorialResults_Dialogue : Dialogue
{
    public MiniTutorialResults_Dialogue(Dialogue previousDialogue, Dialogue subsequentDialogue, Measure[] measure, RhythmSpecs rhythmSpecs, BatteriePack pack)
    {
        RhythmSpecs = rhythmSpecs;
        PreviousDialogue = previousDialogue;
        SubsequentDialogue = subsequentDialogue;
        Pack = pack;
        Measures = measure;
    }

    readonly RhythmSpecs RhythmSpecs;
    readonly Measure[] Measures;
    readonly Dialogue PreviousDialogue;
    readonly Dialogue SubsequentDialogue;
    readonly BatteriePack Pack;

    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        return this;
    }

    string Results()
    {
        if (Pack.TotalErrors == 0) { return "Perfect!" + TryAgain; }
        else if (Pack.TotalErrors < 5) { return "Great!" + TryAgain; }
        else if (Pack.ErroneousAttacks > Pack.GoodHits + Pack.MissedHits) { return "Don't spam tap. Try again."; }
        else { return "Very good, but not quite perfect." + TryAgain; }
    }

    string TryAgain => "\nWould you like to try again?";

    Line StartLine => new Line(Results(), Replies());

    Response[] Replies()
    {
        if (Pack.ErroneousAttacks > Pack.GoodHits + Pack.MissedHits)
        {
            return new Response[1] { new Response("Try again", PreviousDialogue) };
        }

        else
        {
            return new Response[3] {
                new ("Review Tutorial", PreviousDialogue),
                new ("Try again", TrainingBattery()),
                new ("Continue to next cell", SubsequentDialogue)
            };
        }
    }

    State TrainingBattery() => new BatteryTutorial_State(PreviousDialogue, SubsequentDialogue, Measures, RhythmSpecs);

}
