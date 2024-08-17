using UnityEngine;
using Dialog;

public class Intro_Dialogue : Dialogue
{

    public Intro_Dialogue(State subsequentState)
    {
        SubsequentState = subsequentState;
    }

    readonly State SubsequentState;
    // public Intro_Dialogue(GameDifficulty difficulty)
    // {
    //     Difficulty = difficulty;
    // }
    // readonly GameDifficulty Difficulty;
    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        return this;
    }

    Line StartLine => new Line("Hi. I'm the 'Algorithm'. You can call me AL.", FamiliarWithGR_Line)
        .SetSpeaker(new Speaker(FacialExpression.Bliss.Sprites(), "AL"));

    Line FamiliarWithGR_Line => new Line("Are you familiar with Grimoire Rhythmorum?",
             new Response[2] { FamiliarWithMusica_Response, NotFamiliarWithMusica_Response })
        .SetSpeaker(new Speaker(FacialExpression.Cool.Sprites(), "AL"));

    // Line ViewTutorial_Line => new Line("Would you like to review the Rhythm Cell Tutorial?", new Response[2]{
    //     new Response("Yes", new W_Dialogue(new StartGame_State(Difficulty))),
    //     new Response("No", LetsBegin_Line)
    // });

    Line LetsBegin_Line => new Line("Good luck!", SubsequentState)// TestCapabilities_Line)
        .SetSpeaker(new Speaker(FacialExpression.Cool.Sprites(), "AL"))
        .FadeToNextState();

    // Line TestCapabilities_Line => new Line(TestCapabilities_Text, CapabilitiesTutorial_Line)
    //     .SetSpeakerIcon(FacialExpression.Cool.Sprites())
    //     .SetSpeakerName(AL);

    // Line CapabilitiesTutorial_Line => new Line(CapabilitiesTutorial_Text, new CapabilitiesTestStart_State());

    Line[] _diatribe_Lines = null;
    Line[] Diatribe_Lines => _diatribe_Lines ??= SetUpDiatribe();
    Line[] SetUpDiatribe()
    {
        string[] intro = Diatribe_Strings;


        Line[] lines = new Line[Diatribe_Strings.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = new(intro[i]);
        }

        lines[^1] = new Line(Diatribe_Strings[^1], LetsBegin_Line)
            .SetSpeaker(new Speaker(((FacialExpression)0).Sprites(), "AL"));

        // for (int i = lines.Length - 2; i > -1; i--)
        // {
        //     lines[i] = new Line(Diatribe_Strings[i], SkipCont_Responses(lines[i + 1]))
        //         .SetSpeakerIcon(((FacialExpression)Random.Range(0, 9)).Sprites())
        //         .SetSpeakerName(AL);
        // }
        for (int i = 1; i < lines.Length - 1; i++)
        {
            lines[i].SetResponses(Replies(lines[i + 1], lines[i - 1]))
            .SetSpeaker(new Speaker(((FacialExpression)Random.Range(0, 4)).Sprites(), "AL"));
        }

        lines[0].SetResponses(new Response[2] { new Response("Next", lines[1]), Skip_Response })
            .SetSpeaker(new Speaker(((FacialExpression)0).Sprites(), "AL"));

        // return lines[0];
        return lines;
    }
    Response FamiliarWithMusica_Response => new Response("Yes [skip intro]", LetsBegin_Line);
    Response NotFamiliarWithMusica_Response => new Response("No", Diatribe_Lines[0]);

    Response _skip_Response;
    Response Skip_Response => _skip_Response ??= new Response("Skip", LetsBegin_Line);// ViewTutorial_Line);
    Response Continue_Response(Line line) => new Response("Continue", line);
    Response[] SkipCont_Responses(Line line) => new Response[2] { Continue_Response(line), Skip_Response };

    Response[] Replies(Line nextLine, Line prevLine) => new Response[3]{
            new Response("Next", nextLine),
            new Response("Previous", prevLine),
            Skip_Response,
    };



    // string TestCapabilities_Text => "I need to test your capabilities to determine which mode to instantiate. It will only take a moment.";
    // string CapabilitiesTutorial_Text => "(Try to tap the rhythms along with the drums. If you are new to reading sheet music, ask 'How To Play'. For video tutorials check out youtube.com/@ProtoBard)";
    string[] Diatribe_Strings => new string[]{
        "First I would like to say thank you for playing Grimoire Rhythmorum",
        "I wish you well on your musical journeys.",
        // "Oh, don't worry too much about my expressions right now, these are random faces. I'm still warming up the ol' processors...\n...Anyway",
        "This game is meant to encourage you to develop your musical abilities, and hopefully have fun doing it.",
        "If you have no musical background, this might be rather intimidating to get started. But I promise you that, with a little practice, these concepts become much easier.",
        // "The [?] icon can give tool tips to help you navigate the HUD, and there is access to tutorials about the musical puzzles in the help menu.",
        // "If you have a lot of musical experience and want more of a challenge, don't worry, there is much in development that is soon to come!",
        "Moving on...",
        "Musica is an ancient magic, once mastered by the old seafaring Bards.",
        // "All that's left today is a game know as Muscopa, a mere remanence of the power what once was.",
        "Rhythm is the side of Musica that deals with time.",
        "It has been weaponized as Batterie for naval warfare.",
        // "Your task is to captain the D.S. al Coda. You must find and confront the ghost ship known as Cromatica.",
        "Your task is to captain the a ship and activate the lighthouses in each region.",
        // // "You will need to find a map that shows the location where the Cromatica makes berth, and a sextant to read your position.",
        // // "The map can be found in the treasure of sunken ships, and the sextant can be purchased or won by challenging trade ships to Muscopa.",
        // "You and your crew need daily rations. If you run out, you will perish.",
        // "You can get rations by winning Batterie challenges, as purchase from trade ships, or catching fish.",
        // // "Trade ships play Muscopa cadences in the mode of their region.",
        // "If your ship is damaged you can ask a trade ship to repair it, provided you have enough gold and materials.",
    };



}


// Line StartLine = new Line()
//     .SetSpeakerIcon(FacialExpression.Cool.Sprites())
//     .SetSpeakerName(AL)
//     .SetSpeakerText("Hi. I'm the 'Algorithm'. You can call me AL.");


// FirstLine = StartLine;

// NextLines.TryAdd(StartLine, FamiliarWithMusica_Line);
// Replies.TryAdd(FamiliarWithMusica_Line, new Response[2] { FamiliarWithMusica_Response, NotFamiliarWithMusica_Response });
// GoToLines.TryAdd(FamiliarWithMusica_Response, LetsBegin_Line);

// Line[] Diatribe = new Line[Diatribe_Strings.Length];
// for (int i = 0; i < Diatribe.Length; i++)
// {
//     Diatribe[i] = new Line()
//       .SetSpeakerIcon(((FacialExpression)Random.Range(0, 9)).Sprites())
//       .SetSpeakerName(AL)
//       .SetSpeakerText(Diatribe_Strings[i]);
// }

// Response continue_response;
// for (int i = 0; i < Diatribe.Length - 1; i++)
// {
//     continue_response = NewContinue_Response;
//     Replies.Add(Diatribe[i], new Response[2] { Skip_Response, continue_response });
//     GoToLines.Add(continue_response, Diatribe[i + 1]);
// }
// continue_response = NewContinue_Response;
// Replies.Add(Diatribe[^1], new Response[1] { continue_response });
// GoToLines.Add(continue_response, LetsBegin_Line);
// GoToLines.TryAdd(NotFamiliarWithMusica_Response, Diatribe[0]);
// GoToLines.TryAdd(Skip_Response, LetsBegin_Line);

// NextLines.TryAdd(LetsBegin_Line, TestCapabilities_Line);
// NextLines.TryAdd(TestCapabilities_Line, CapabilitiesTutorial_Line);
// Line_Outcomes.TryAdd(CapabilitiesTutorial_Line, MacroState.CapabilitiesTest);


// Response NewContinue_Response(Line line) => new Response("Continue", line);