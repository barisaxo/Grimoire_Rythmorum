using Dialog;

public class TutorialMenu_Dialogue : Dialogue
{
    public TutorialMenu_Dialogue(State subsequentState)
    {
        SubsequentState = subsequentState;
    }
    readonly State SubsequentState;

    public override Dialogue Initiate() { FirstLine = StartLine; return this; }

    Line StartLine => new Line("What would you like to focus on?", Replies);

    Response[] Replies => new Response[]{
        new Response("About 'Muscopa'", new GramophoneTutorial_Dialogue(SubsequentState)),
        new Response("About 'Batterie'", new BatteryTutorial_Dialogue(SubsequentState)),
        new Response("Back", SubsequentState),
    };
}
