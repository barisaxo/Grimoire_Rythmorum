using Dialog;

public class CelestialNavigationTutorial_Dialogue : Dialogue
{
    public CelestialNavigationTutorial_Dialogue(Dialogue returnTo) { ReturnToDialogue = returnTo; }
    readonly Dialogue ReturnToDialogue;


    public CelestialNavigationTutorial_Dialogue(State returnTo) { ReturnToState = returnTo; }
    readonly State ReturnToState;

    private Response _exit;
    private Response Exit => _exit ??= ReturnToDialogue != null ? new("back", ReturnToDialogue) : new("back", ReturnToState);

    private static string[] _string => new[]
    {
        "There are bottles floating around at sea with scrolls inside. These scrolls are Star Charts.",
        "Celestial Navigation is the means of deciphering Star Charts. These puzzles are a series of music theory and ear training exercises.",
        "By solving the puzzle, you've deciphered the Star Chart, and a location reveals itself as a Navigation Quest.",
        "Please note, you do not need to solve these puzzles to beat this game, but it can help you progress faster and find more rewards.",
        "Alternatively you can employ trade ships to solve Star Charts for you, in trade for Gold. However you won't earn any patterns if you do so.",
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