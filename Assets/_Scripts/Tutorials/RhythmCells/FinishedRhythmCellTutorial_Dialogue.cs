
using Dialog;

public class FinishedRhythmCellTutorial_Dialogue : Dialogue
{

    public FinishedRhythmCellTutorial_Dialogue(State consequentState) : base(consequentState) { }
    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        return base.Initiate();
    }

    Line StartLine => new Line("That's all for now.", ConsequentState).FadeToNextState();
}
