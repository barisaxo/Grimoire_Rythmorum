using Dialog;

public class BeatFishingTutorial_Dialogue : Dialogue
{
    public BeatFishingTutorial_Dialogue(Dialogue returnTo) { ReturnToDialogue = returnTo; }
    readonly Dialogue ReturnToDialogue;


    public BeatFishingTutorial_Dialogue(State returnTo) { ReturnToState = returnTo; }
    readonly State ReturnToState;


    private Response _exit;
    private Response Exit => _exit ??= ReturnToDialogue != null ? new("back", ReturnToDialogue) : new("back", ReturnToState);

    private static string[] _string => new[]
    {
        "Beat fishing is a rhythmic aural puzzle with an emphasis on 'finding the one'.",
        "There is no count in to prepare you. Instead you must use your ear to find count one and perform the rhythm on the proper count.",
        "Musicians call this a sense of time, or simply having 'good time'.",
        "In common time, the drum beat is almost always a Kick-Snare-Kick-Snare pattern.",
        "Count's 1 & 3 are 'strong beats' and you'll hear the kick - or bass drum, and 2 & 4 are 'weak beats' and you'll hear the snare drum.",
        "Listen to the drums and keep practicing and you'll start to hear what beat your on.",
        "I always highly recommend counting out loud with the music 'One - Two - Three - Four'."
    };

    public override Dialogue Initiate()
    {
        FirstLine = GetStartLine();
        return this;
    }

    private Line GetStartLine()
    {
        var muscopa = _string;

        var lines = new Line[muscopa.Length];
        for (var i = 0; i < lines.Length; i++) lines[i] = new Line(muscopa[i]);

        lines[^1].SetResponses(new[] { new("Previous", lines[^2]), Exit });

        for (var i = 1; i < lines.Length - 1; i++) lines[i].SetResponses(Replies(lines[i + 1], lines[i - 1]));

        lines[0].SetResponses(new[] { new("Next", lines[1]), Exit });

        return lines[0];
    }

    private Response[] Replies(Line nextLine, Line prevLine)
    {
        return new[]
        {
            new("Previous", prevLine),
            new("Next", nextLine),
            Exit
        };
    }
}
