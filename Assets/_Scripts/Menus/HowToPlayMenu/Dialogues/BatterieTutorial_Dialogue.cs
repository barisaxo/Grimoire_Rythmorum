using Dialog;
using UnityEngine;

public class BatterieTutorial_Dialogue : Dialogue
{
    private static string[] Batterie => new[]
    {
        "Batterie is a rhythm challenge emulating both the batterie of a drum line, and the battery of a ships cannons.",
        "The goal is to sight-read rhythms from sheet music, and perform these rhythms by tapping.",
        "The key to this reading rhythms is understanding Rhythm Cells. Just like there are only 12 notes in music, there are only 12 rhythmic shapes. Really!",
        "By combining these 12 shapes with ties and rests, we can create any rhythm!",
        "There are two main types of these 12 shapes, those with 4 counts, and those with 3 counts, sometimes called triplets.",
        "There are eight 4-count shapes, and four 3-count shapes. For now we will focus on the eight 4-count shapes.",
        "Let's take a look at them..."
    };

    public override Dialogue Initiate()
    {
        FirstLine = GetStartLine();
        return this;
    }

    private Line GetStartLine()
    {
        var batterie = Batterie;

        var lines = new Line[batterie.Length];
        for (var i = 0; i < lines.Length; i++)
            lines[i] = new Line(batterie[i])
                .SetSpeakerName(AL)
                .SetSpeakerIcon(((FacialExpression)Random.Range(0, 9)).Sprites());

        lines[^1].SetResponses(new[]
        {
            new("Rhythm Cells Tutorial"), //new W_Dialogue(new MainMenu_State())),
            new("Previous", lines[^2]), Exit
        });

        for (var i = 1; i < lines.Length - 1; i++) lines[i].SetResponses(Replies(lines[i + 1], lines[i - 1]));

        lines[0].SetResponses(new[] { new("Next", lines[1]), Exit });

        return lines[0];
    }

    private Response[] Replies(Line nextLine, Line prevLine)
    {
        return new[]
        {
            new("Next", nextLine),
            new("Previous", prevLine),
            Exit
        };
    }

    private Response _exit;
    private Response Exit => _exit ??= new Response("Exit", new HowToPlayMenu_State());
}