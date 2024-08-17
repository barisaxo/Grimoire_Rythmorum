namespace Dialog.Pino
{
    public class PinoFalseStart_Dialogue : Dialogue
    {
        public PinoFalseStart_Dialogue(State subsequentState) { SubsequentState = subsequentState; }
        readonly State SubsequentState;

        public override Dialogue Initiate()
        {
            FirstLine = StartLine;
            return this;
        }

        Line StartLine => new Line(
            "Quartermaster Pino here.",
            new PinoStart_Dialogue(SubsequentState))
            .SetSpeaker(Speaker.Pino);
    }

    public class PinoStart_Dialogue : Dialogue
    {
        public PinoStart_Dialogue(State subsequentState) { SubsequentState = subsequentState; }
        readonly State SubsequentState;

        public override Dialogue Initiate()
        {
            FirstLine = StartLine;
            return this;
        }

        Response[] Replies() => new Response[]
        {
            new ("What should I be doing?",new WhatShouldIBeDoing_Dialogue(this) ),
            new ("About...",new About_Dialogue(this) ),
            // new ("What is this place?", new WhereAreWe_Dialogue(this)),
            Back,
        };

        Line StartLine => new Line("What do you need Cap?", Replies()).SetSpeaker(Speaker.Pino);
        // .SetSpeaker(new Speaker(((FacialExpression)Random.Range(0, 4)).Sprites(), "AL"));

        Response Back => new("Back", SubsequentState);

    }



    public class About_Dialogue : Dialogue
    {
        public About_Dialogue(Dialogue returnTo) { BackTo = returnTo; }
        readonly Dialogue BackTo;

        public override Dialogue Initiate()
        {
            FirstLine = StartLine;
            return this;
        }

        Line StartLine => new Line("What would you like to know about?", Replies())
            .SetSpeaker(Speaker.Pino);

        Response[] Replies() => new Response[]
        {
            new ("Lighthouses", new AboutLighthouses_Dialogue(this)),
            new ("Standings", new AboutStandings_Dialogue(this)),
            new ("Pirates and Bounties", new AboutPiratesAndBounties_Dialogue(this)),
            Back,
        };

        Response Back => new("Back", BackTo);
    }

    public class AboutLighthouses_Dialogue : Dialogue
    {
        public AboutLighthouses_Dialogue(Dialogue returnTo) { BackTo = returnTo; }
        readonly Dialogue BackTo;

        public override Dialogue Initiate()
        {
            FirstLine = GetStartLines(_linesText, BackTo).SetSpeaker(Speaker.Pino);
            return this;
        }

        string[] _linesText => new string[]{
            "There are 8 lighthouses, one in each region: Ios, Doria, Phrygia, Lydia, Mixo-Lydia, Aeolia, Locria, and Chromatica.",
            "If we can find and activate each lighthouse we can signal a new era of peace and unity.",
            "However when you go back to Null Cove they will reset, so remember to write down the coordinates when you find one!",
        };
    }


    public class AboutStandings_Dialogue : Dialogue
    {
        public AboutStandings_Dialogue(Dialogue returnTo) { BackTo = returnTo; }
        readonly Dialogue BackTo;

        public override Dialogue Initiate()
        {
            FirstLine = GetStartLines(_linesText, BackTo).SetSpeaker(Speaker.Pino);
            return this;
        }

        string[] _linesText => new string[]{
            "There are 3 conflicting factions: Major, Minor, and Diminished.",
            "The Major faction consists of the regions Ios, Lydia, and Mixo-Lydia.",
            "The Minor faction consists of the regions Aeolia, Doria, and Phrygia",
            "The Diminished faction consists of the regions Locria and Chromatica (the pirates)",
            "Every region has it's own level of standing. Actions you do effects the standings between the other regions depending on their faction.",
            "Often times gaining standing with a region from one faction will lower your standing with regions from the other factions. So be careful how you choose to interact with other ships.",
            "A high standing will mean cheaper trade and services, while lower standings can lead towards acts of aggression from those factions."
        };
    }


    public class AboutPiratesAndBounties_Dialogue : Dialogue
    {
        public AboutPiratesAndBounties_Dialogue(Dialogue returnTo) { BackTo = returnTo; }
        readonly Dialogue BackTo;

        public override Dialogue Initiate()
        {
            FirstLine = GetStartLines(_linesText, BackTo).SetSpeaker(Speaker.Pino);
            return this;
        }

        string[] _linesText => new string[]{
            "Pirates plague the dark waters. They will chase you down the moment you get too close!",
            "Best to keep your distance, unless you are looking for trouble.",
            "Bounties are quests we can accept to hunt down specific pirate ships that are causing a region trouble.",
            "Those bounty ships are very unique challenges that's much different than the usual Batterie.",
            "Of course you too can choose to commit acts of piracy. However it will certainly effect your standings within the factions.",
            "If a region is hostile and you want to gain standings, attacking ships from their opposing faction will eventually raise your standings.",
            "The Major faction dislikes the Diminished faction,\nthe Minor faction dislikes the Major faction,\nand the Diminished faction dislikes the Minor faction."
        };
    }


    public class WhatShouldIBeDoing_Dialogue : Dialogue
    {
        public WhatShouldIBeDoing_Dialogue(Dialogue returnTo) { BackTo = returnTo; }
        readonly Dialogue BackTo;

        public override Dialogue Initiate()
        {
            FirstLine = StartLine;
            return this;
        }

        Line StartLine
        {
            get
            {
                if (Sea.WorldMapScene.Io.Ship.ShipStats.HullStats.Hull is Datum.CatBoat)
                    return GetStartLines(InCatboat, BackTo).SetSpeaker(Speaker.Pino);

                if (Datum.Manager.Io.Quests.GetQuest(new Datum.Bounty()) is not null)
                    if (Datum.Manager.Io.Quests.GetQuest(new Datum.Bounty()).Complete)
                        return GetStartLines(TurnInBountyQuest, BackTo).SetSpeaker(Speaker.Pino);
                    else return GetStartLines(FinishBountyQuest, BackTo).SetSpeaker(Speaker.Pino);

                if (Datum.Manager.Io.Quests.GetQuest(new Datum.Navigation()) is not null)
                    return GetStartLines(FinishNavigationQuest, BackTo).SetSpeaker(Speaker.Pino);

                if (Datum.Manager.Io.Inventory.GetLevel(new Datum.Gramophone()) > 0)
                    return GetStartLines(OpenGramo, BackTo).SetSpeaker(Speaker.Pino);

                if (Datum.Manager.Io.Inventory.GetLevel(new Datum.StarChart()) > 0)
                    return GetStartLines(StartNavigationQuest, BackTo).SetSpeaker(Speaker.Pino);

                if (Sea.WorldMapScene.Io.Ship.ShipStats.HullStats.Hull is Datum.Frigate or Datum.Barque)
                    return GetStartLines(ReadyForLightHouses, BackTo);

                return GetStartLines(Explore, BackTo);
            }
        }

        readonly string[] InCatboat = new string[] {
            "This little catboat is mostly meant for fishing.",
            "It's too small to interact with the other ships or lighthouses. But you can still find treasures!",
            "Also pirates will ignore you when you're in a little boat like this, so you're free to explore anywhere you like."
        };

        readonly string[] TurnInBountyQuest = new string[] {
            "You have a Bounty Quest ready to turn in!",
            "[press + to view the menu and review the QUESTS tab]"
        };

        readonly string[] FinishBountyQuest = new string[] {
            "You have an open Bounty contract!",
            "[press + to view the menu and review the QUESTS tab]"
        };

        readonly string[] StartNavigationQuest = new string[] {
            "You have a Star Chart in your inventory. You can decipher it, or employ a trade ship to do it for you.",
            "That will start a Navigation Quest, and we can find some treasure!",
            "[press + to view the menu and review the INVENTORY tab]"
        };

        readonly string[] FinishNavigationQuest = new string[] {
            "You have an active Navigation Quest! Let's go find some treasure!",
            "[press + to view the menu and review the QUESTS tab]"
        };

        readonly string[] OpenGramo = new string[] {
            "You have a Gramophone in your inventory. You can attempt to unlock it and get the treasures, or employ a trade ship to do it for you.",
            "You can also sell it for gold.",
            "[press + to view the menu and review the INVENTORY tab]"
        };

        readonly string[] Explore = new string[] {
            "Up to you Cap!",
            "We can explore, raise our standings with certain factions, find treasure, hunt pirates, collect bounties, or just even go fishing.",
            "Spend some time collecting patterns that you can use to upgrade your ships and skills back at Null Cove."
        };

        readonly string[] ReadyForLightHouses = new string[] {
            "If you feel ready, you should try to find and activate all 8 lighthouses to signal a new era of peace and unity.",
            "There is one in each region, they can be tricky to find, but they are always at the same location." +
            "I suggest when you do find one write down the coordinates someplace safe!"
        };
    }

    // public class WhereAreWe_Dialogue : Dialogue
    // {
    //     public WhereAreWe_Dialogue(Dialogue returnTo) { BackTo = returnTo; }
    //     readonly Dialogue BackTo;

    //     public override Dialogue Initiate()
    //     {
    //         FirstLine = GetStartLines(_linesText, BackTo);
    //         return this;
    //     }

    //     string[] _linesText => new string[]{
    //         "This is Null Cove.",
    //         "The very center of everything.\nThe beginning and the end.",
    //         "Somewhere in between the Æther and the physical realm.",
    //         "We are merely patterns that simply exist in the Æther.",
    //         "A manifestation of consciousness.",
    //         "Here in the Æther you are a dot.",
    //         "An augmentation dot to be specific.",
    //         "I need you're help because you have a unique ability.\nYou make things greater than what they already are.",
    //         "There is a node of conscious energy in this cove that allows me to instantiate you into the physical realm.",
    //         "Out there you are a Sea Captain, a Treasure Seeker, a Bounty Hunter, a Pirate, a Fisherman...",
    //         "Whatever you want to be."
    //     };
    // }
}