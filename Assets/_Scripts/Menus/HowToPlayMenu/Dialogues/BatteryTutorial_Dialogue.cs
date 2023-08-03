using Dialog;
using UnityEngine;

public class BatteryTutorial_Dialogue : Dialogue
{
    private Response _exit;
    private Response Exit => _exit ??= new Response("Exit", new HowToPlayMenu_State());

    private static string[] _battery => new[]
    {
        "Battery is a rhythm game emulating both the battery of a drum line, and the battery of a ships cannons.",
        "The goal is to sight-read rhythms with sheet music, and perform these rhythms by tapping.",
        "The key to this reading rhythms is understanding Rhythm Cells. Just like there are only 12 notes in music, there are only 12 rhythm shapes. Really!",
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
        var battery = _battery;

        var lines = new Line[battery.Length];
        for (var i = 0; i < lines.Length; i++)
            lines[i] = new Line(battery[i])
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
}


// Response _exit;
// Response Exit => _exit ??= new Response("Exit", new StartMenu_Dialogue());
// Response[] Replies(Line nextLine) => new Response[2] { new Response("Next", nextLine), Exit };

// string batteryDescription => "Battery is a rhythm game emulating both the battery of a drum line, and the battery of a ships cannons.";
// Line StartLine => new Line(batteryDescription, Replies(Line2));

// string s2 => "The goal is to sight-read rhythms with sheet music, and perform these rhythms by tapping.";
// Line Line2 => new Line(s2, Replies(Line3));

// string s3 => "The key to this reading rhythms is understanding Rhythm Cells. Just like there are only 12 notes in music, there are only 12 rhythms. Really!";
// Line Line3 => new Line(s3, Replies(Line4));

// string s4 => "By combining these 12 rhythm cells with ties and rests, we can create any rhythm possible!";
// Line Line4 => new Line(s4, Replies(Line5));

// string s5 => "There is a bit too much to explain about reading rhythms for this tutorial, so if you need more in depth tutorials to get started, please check out the videos at Youtube.com/@ProtoBard";
// Line Line5 => new Line(s5, new StartMenu_Dialogue());


// string s6 => "A Bar line is a vertical line that separates groups of beats into Measures.";
// Line Line6 => new Line(s6, Replies(Line7));

// string s7 => "A Measure is a group of beats that we count in order to know where we are at in music. The number of beats per measure is part of that musics Time Signature. Most music is grouped into 4 beats per measure, a signature known as Common Time";
// Line Line7 => new Line(s7, Replies(Line8));

// string s8 => "This game will exclusively use Common Time, but there can be any number of beats per measure. Waltzes, for instance, have 3 beats per measure, and Marches have only 2.";
// Line Line8 => new Line(s8, Replies(Line9));

// string s9 => "The beats we play are expressed as Notes. Which have different lengths. This is a Whole Note. Represented by an empty circle.";
// Line Line9 => new Line(s9, Replies(Line10)).SetSpeakerIcon(Assets.White);

// string s10 => "A whole note is worth four beats, and we count it just like that: 'One, Two, Three, Four'.";
// Line Line10 => new Line(s10, Replies(Line11)).SetSpeakerIcon(Assets.White);

// string s11 => "A rest is musical silence. It means you must wait, and not press anything. This is a whole rest, and you will see it hanging below a line like a whole in the staff, and is counted the same way: 'One, Two, Three, Four'.";
// Line Line11 => new Line(s0, Replies(Line12)).SetSpeakerIcon(Assets.WholeRest);

// string s12 => "This is a half note, it worth exactly half of the beats whole note, which is two. But we don't always count it 'One, Two' because it depends on what count it starts on.";
// Line Line12 => new Line(s12, Replies(Line13)).SetSpeakerIcon(new Sprite[2] { Assets.WhiteNote, Assets.Stem });

// string s13 => "If the note starts on count 2 we would count 'Two, Three', and if starts on count 3 we count 'Three, Four'.";
// Line Line13 => new Line(s13);

// string s0 => " ";
// Line Line0 => new Line(s0, Replies(Line0));
// string s0 => " ";
// Line Line0 => new Line(s0, Replies(Line0));
// string s0 => " ";
// Line Line0 => new Line(s0, Replies(Line0));
// string s0 => " ";
// Line Line0 => new Line(s0, Replies(Line0));


// string s0 => " ";
// Line Line0 => new Line(s0, Replies(Line0));