using UnityEngine;

namespace Dialog
{
    public class Dialogue
    {
        public Dialogue() { }
        public Dialogue(State consequentState) { ConsequentState = consequentState; }
        protected readonly State ConsequentState;

        public Line FirstLine;
        public AudioClip StartSound;
        public bool PlayTypingSounds = true;

        public Speaker Speaker = Speaker.Blank;
        public string SpeakerText = string.Empty;

        public Dialogue SetSpeaker(Speaker speaker) { Speaker = speaker; return this; }
        public Dialogue SetSpeakerText(string speakerText) { SpeakerText = speakerText; return this; }
        //public Dialogue SetCharacterData(CharacterData characterData) { CharacterData = characterData; return this; }
        public Dialogue MuteTypingSounds() { PlayTypingSounds = false; return this; }

        public virtual Dialogue Initiate()
        {
            return this;
        }
    }
}

public struct Speaker
{
    public Sprite[] Icon;
    public string Name;
    public Color Color;

    public Speaker(Sprite[] icon, string name, Color color)
    {
        Name = name;
        Icon = icon;
        Color = color;
    }
    public Speaker(Sprite icon, string name, Color color)
    {
        Name = name;
        Icon = new Sprite[] { icon };
        Color = color;
    }
    public Speaker(Sprite[] icon, string name)
    {
        Name = name;
        Icon = icon;
        Color = Color.white;
    }
    public Speaker(Sprite icon, string name)
    {
        Name = name;
        Icon = new Sprite[] { icon };
        Color = Color.white;
    }
    public Speaker(Sprite[] icon)
    {
        Name = "\n";
        Icon = icon;
        Color = Color.white;
    }
    public Speaker(Sprite icon)
    {
        Name = "\n";
        Icon = new Sprite[] { icon };
        Color = Color.white;
    }

    public Speaker(string name)
    {
        Name = name;
        Icon = new Sprite[] { };
        Color = Color.white;
    }

    public static readonly Speaker Blank = new(
         new Sprite[0],
        "\n",
         Color.white
    );
    public static readonly Speaker Pino = new(
       new Sprite[1] { Assets.Pino },
       "[Quartermaster Pino]:\n",
       Color.white
    );

    public static readonly Speaker RandomAL = new(
        ((FacialExpression)Random.Range(0, 9)).Sprites(),
        "[AL]:\n",
        Color.white
    );
}