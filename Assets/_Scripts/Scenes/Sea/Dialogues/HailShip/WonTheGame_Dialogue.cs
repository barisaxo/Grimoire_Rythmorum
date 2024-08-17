using Dialog;

public class WonTheGame_Dialogue : Dialogue
{
    public override Dialogue Initiate()
    {
        FirstLine = GetStartLines(_linesText, new SeaToCoveTransition_State()).SetSpeaker(Speaker.Pino);
        return this;
    }

    string[] _linesText => new string[]{
            "Congratulations!",
            "You have managed not only to activate every lighthouse, signaling for a new era of peace and unity.",
            "But you have taken upon yourself the challenge of learning something new.",
            "Dedicated time into building life long skill sets.",
            "Most importantly, you followed your efforts through to completion.",
            "I say 'Well done to you!'",
            "You owe it to yourself to go outside for a nice stroll and celebrate this moment.",
            "Also, you get 10,000 patterns =)"
        };
}

