using Dialog;

public class MuscopaTutorial_Dialogue : Dialogue
{
    private Response _exit;
    private Response Exit => _exit ??= new("Exit", new HowToPlayMenu_State());

    private static string[] _muscopa => new[]
    {
        "Muscopa is a musical card game that challenges your aural capacities.",
        "You must use your ear and solve chordal cadences to win. Think of it as solving musical puzzles.",
        "Please note, you do not need to play Muscopa to beat this game, but it can help you progress faster and earn more coin. Let's continue...",
        "You'll hear a four-chord cadence, and your objective is to identify the harmonic functions of these chords.",
        "There are three types of harmonic functions: tonic, predominant, and dominant. However, there are seven chords in all.",

        "There are seven notes in a scale, each note the root of a chord, and each chord its own function. We label them with roman numerals like this:\nI  II  III  IV  V  VI  VII.",
        "The first scale degree 'I' is called Do. It is the root of our tonic chord, known as the I chord.",
        "The fifth scale degree 'V' is called So. It is the root of our dominant chord, known as the V chord.",
        "The fourth scale degree 'IV' is called Fa. It is the root of our predominant chord, known as the IV chord.",
        "These represent the three main functions of chords, but there are still a few more chords in our scale.",

        "The third scale degree 'III' is called Mi. It is the root of another tonic chord called the mediant-tonic, known as the III- chord. Mediant meaning in the middle.",
        "The sixth scale degree 'VI' is called La. It is the root of another tonic chord called the submediant-tonic, known as the VI- chord.",
        "The second scale degree 'II' is called Re. It is the root of another predominant chord called the lateral-predominant, known as the II- chord. Lateral meaning adjacent to the root",
        "If you're wondering what the '-' is doing there, we call chords that have those minor chords.",
        "I, IV, & V are major chords, and II- III- & VI- are minor chords. Major and minor are types of chord quality.",

        "Finally we have the seventh scale degree 'VII', called Ti. It is the root of another dominant chord called the lateral-dominant, and is known as the VIIø chord.",
        "The symbol ø (read as ''half-diminished'') is another, rather unique, type of chord quality that can be easily recognized by it's dissonant sound. That dissonance is caused by the tritone.",
        "It's important to note that it doesn't matter what key you're in, this relationship of chords and qualities never changes.",
        "This is known as diatonic music.",
        "If you hear chords that are different from, or outside of, these seven diatonic chords, they could be modal or nondiatonic. Only diatonic chords be found in this game. Though that may change in the future...",

        "Now that we know some basics of what chords are, lets talk about how to apply our ear to music.",
        "The first thing to do is to listen to the bass line.",
        "The bass is almost always playing the root of the chord, and that information alone is enough to solve these puzzles.",
        "The next thing to listen for is the quality of the chord.",
        "Since we know which chords might be major, minor, or diminished in quality, we can try to identify those sounds and process what diatonic function that chord might belong to. For instance, minor chords are never dominant in function.",
        "I highly encourage you to play your instrument along with the music and try to find the first bass note of every measure.",
        "The notes of the scale, their corresponding scale degrees, and their chord qualities are givin at the bottom of every puzzle.",

        "For more tutorials please check out some videos at Youtube.com/@ProtoBard"
    };

    public override Dialogue Initiate()
    {
        FirstLine = GetStartLine();
        return this;
    }

    private Line GetStartLine()
    {
        var muscopa = _muscopa;

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
            new("Next", nextLine),
            new("Previous", prevLine),
            Exit
        };
    }
}