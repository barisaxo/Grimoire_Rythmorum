using Dialog;

public class AboutGame_Dialogue : Dialogue
{
    private Response _exit;
    private Response Exit => _exit ??= new Response("Exit", new ThrowState());

    private static string[] _about => new[]
    {
        "The goal of ",
        " ",
        " "
    };

    public override Dialogue Initiate()
    {
        FirstLine = GetLines();
        return this;
    }

    private Line GetLines()
    {
        var about = _about;

        var lines = new Line[about.Length];
        for (var i = 0; i < lines.Length; i++) lines[i] = new Line(about[i]);

        lines[^1].SetResponses(new[] { new("Previous", lines[^2]), Exit });

        for (var i = 1; i < lines.Length - 1; i++) lines[i].SetResponses(Replies(lines[i + 1], lines[i - 1]));

        lines[0].SetResponses(new[] { new("Next", lines[1]), Exit });

        return lines[0];
    }

    private Response[] Replies(Line nextLine, Line prevLine)
    {
        return new[]
        {
            new Response("Next", nextLine),
            new Response("Previous", prevLine),
            Exit
        };
    }
}