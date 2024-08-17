using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog.Cove
{
    public class ALFalseStart_Dialogue : Dialogue
    {
        public ALFalseStart_Dialogue(State subsequentState) { SubsequentState = subsequentState; }
        readonly State SubsequentState;

        public override Dialogue Initiate()
        {
            FirstLine = StartLine;
            return this;
        }

        Line StartLine => new("Hi, I'm 'the algorithm'. You can call me AL",
            new ALStart_Dialogue(SubsequentState));
    }
    public class ALStart_Dialogue : Dialogue
    {
        public ALStart_Dialogue(State subsequentState) { SubsequentState = subsequentState; }
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
            new ("What is this place?", new WhereAreWe_Dialogue(this)),
            Back,
        };

        Line StartLine => new Line("What can I help you with?", Replies());
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

        Line StartLine => new("What would you like to know about?", Replies());

        Response[] Replies() => new Response[]
        {
            new ("Grimoire Rhythmorum", new AboutGR_Dialogue(this)),
            new("Patterns", new AboutPatterns_Dialogue(this)),
            Back,
        };

        Response Back => new("Back", BackTo);
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
                if (Datum.Manager.Io.QRhythmCellData.GetLevel(new Datum.QRhythm.QHQ()) < 1)
                    return GetStartLines(FinishRhythmCells, BackTo);

                if (Datum.Manager.Io.QBatterieOptionData.GetLevel(new Datum.QRhythm.RestsAndTies()) < 1)
                    return GetStartLines(FinishQNoteBattery, BackTo);

                if (Datum.Manager.Io.BeatFishingPracticeData.GetLevel(new Datum.BeatFishing.QHQ()) == 0)
                    return GetStartLines(FinishBeatFishing, BackTo);

                if (Datum.Manager.Io.ShipPurchaseData.GetLevel(new Datum.CutterPurchase()) == 0)
                {
                    if (Datum.Manager.Io.Player.GetLevel(new Datum.PatternsAvailable()) >= Datum.ShipPurchaseEnum.CutterPurchase.Cost)
                        return GetStartLines(PurchaseShip("Cutter"), BackTo);

                    else return GetStartLines(Explore, BackTo);
                }

                if (Datum.Manager.Io.EBatterieOptionData.GetLevel(new Datum.ERhythm.RestsAndTies()) < 1)
                    return GetStartLines(FinishENoteBattery, BackTo);

                if (Datum.Manager.Io.ShipPurchaseData.GetLevel(new Datum.SchoonerPurchase()) == 0)
                {
                    if (Datum.Manager.Io.Player.GetLevel(new Datum.PatternsAvailable()) >= Datum.ShipPurchaseEnum.SchoonerPurchase.Cost)
                        return GetStartLines(PurchaseShip("Schooner"), BackTo);

                    else return GetStartLines(Explore, BackTo);
                }

                if (Datum.Manager.Io.ShipPurchaseData.GetLevel(new Datum.BrigPurchase()) == 0)
                {
                    if (Datum.Manager.Io.Player.GetLevel(new Datum.PatternsAvailable()) >= Datum.ShipPurchaseEnum.BrigPurchase.Cost)
                        return GetStartLines(PurchaseShip("Brig"), BackTo);

                    else return GetStartLines(Explore, BackTo);
                }

                if (Datum.Manager.Io.SBatterieOptionData.GetLevel(new Datum.SRhythm.RestsAndTies()) < 1)
                    return GetStartLines(FinishSNoteBattery, BackTo);

                if (Datum.Manager.Io.ShipPurchaseData.GetLevel(new Datum.FrigatePurchase()) == 0)
                {
                    if (Datum.Manager.Io.Player.GetLevel(new Datum.PatternsAvailable()) >= Datum.ShipPurchaseEnum.FrigatePurchase.Cost)
                        return GetStartLines(PurchaseShip("Frigate"), BackTo);

                    else return GetStartLines(Explore, BackTo);
                }

                if (Datum.Manager.Io.ShipPurchaseData.GetLevel(new Datum.BarquePurchase()) == 0)
                {
                    if (Datum.Manager.Io.Player.GetLevel(new Datum.PatternsAvailable()) >= Datum.ShipPurchaseEnum.BarquePurchase.Cost)
                        return GetStartLines(PurchaseShip("Barque"), BackTo);

                    else return GetStartLines(ReadyForLightHouses, BackTo);
                }

                return GetStartLines(ReadyForLightHouses, BackTo);
                // .SetSpeaker(new Speaker(((FacialExpression)Random.Range(0, 4)).Sprites(), "AL")); ;
            }
        }

        readonly string[] FinishRhythmCells = new string[] {
            "I see you haven't finished the Rhythm Cell tutorials yet.",
            "Interact with the cannon and select Quarter Note Rhythms => Rhythm Cells.",
            "Once you complete that you can do the fishing tutorial.",
            "After the fishing tutorial you can make your way to sea!"
        };

        readonly string[] FinishQNoteBattery = new string[] {
            "I see you haven't finished the Quarter Note Batterie tutorials yet.",
            "Interact with the cannon and select Quarter Note Rhythms => Batterie Practice.",
            "Once you complete that you can set sail in the Sloop and interact with other ships and lighthouses at Sea.",
            "But you must be careful of pirates!"
        };

        readonly string[] FinishBeatFishing = new string[] {
            "I see you haven't finished the Beat Fishing tutorials yet.",
            "Interact with the fish to complete the tutorials.",
            "After the fishing tutorial the Catboat is unlocked and you can make your way to sea!"
        };

        readonly string[] FinishENoteBattery = new string[] {
            "I see you are ready to do the Eighth Note Batterie tutorials.",
            "Interact with the cannon and select Eight Note Rhythms => Batterie Practice.",
            "Finishing these will allow you to purchase the Schooner and the Brig.",
            "These are much stronger ships and will allow you a lot more freedom while explore.",
            "You will still have to be careful though, there are more powerful ships.",
        };

        readonly string[] FinishSNoteBattery = new string[] {
            "I see you are ready to do the Sixteenth Note Batterie tutorials.",
            "Interact with the cannon and select Sixteenth Note Rhythms => Batterie Practice.",
            "Finishing these will allow you to purchase the Frigate and the Barque.",
            "The two most powerful ships that dominate the seas!"
        };

        string[] PurchaseShip(string ship) => new string[] {
            "You have enough patterns to unlock the " + ship + ".",
        };

        readonly string[] Explore = new string[] {
            "Time to start finding patterns at sea and upgrading your ships and skills.",
            "Once you have some patterns, you can upgrade skills by interacting with the window beside me. It will be flashing different shapes.",
            "You can find patterns by fishing, seeking treasure, hunting bounties, or pirating trade ships.",
            "Treasure is found by deciphering Star Charts, sailing to their revealed location, and unlocking Gramophones.",
            "Alternatively you can employ trade ships to solve Star Charts for you, and sell the Gramophones for Gold.",
            "However you wont find any patterns unless you solve these puzzles yourself.",
            "Bounties can be contracted through trade ships. Hail one for the quest, and sail to the location.",
            "Once you have taken care of the bounty ship, hail any ship from the contracted region for your rewards.",
            "You should be aware that most actions you choose at sea will have an effect on your standings between specific regions."
        };

        readonly string[] ReadyForLightHouses = new string[] {
            "If you feel ready, you should try to find and activate all 8 lighthouses to signal a new era of peace and unity.",
            "There is one in each region, they can be tricky to find, but they are always at the same location." +
            "I suggest when you do find one write down the coordinates someplace safe!"
        };
    }

    public class WhereAreWe_Dialogue : Dialogue
    {
        public WhereAreWe_Dialogue(Dialogue returnTo) { BackTo = returnTo; }
        readonly Dialogue BackTo;

        public override Dialogue Initiate()
        {
            FirstLine = GetStartLines(_linesText, BackTo);
            return this;
        }

        string[] _linesText => new string[]{
            "This is Null Cove.",
            "The very center of everything.\nThe beginning and the end.",
            "Somewhere in between the Æther and the physical realm.",
            "We are merely patterns that simply exist in the Æther.",
            "A manifestation of consciousness.",
            "Here in the Æther you are a dot.",
            "An augmentation dot to be specific.",
            "I need you're help because you have a unique ability.\nYou make things greater than what they already are.",
            "There is a node of conscious energy in this cove that allows me to instantiate you into the physical realm.",
            "Out there you are a Sea Captain, a Treasure Seeker, a Bounty Hunter, a Pirate, a Fisherman...",
            "Whatever you want to be."
        };
    }

    public class AboutGR_Dialogue : Dialogue
    {
        public AboutGR_Dialogue(Dialogue returnTo) { BackTo = returnTo; }
        readonly Dialogue BackTo;

        public override Dialogue Initiate()
        {
            FirstLine = GetStartLines(_linesText, BackTo);
            return this;
        }

        string[] _linesText => new string[]{
            "First of all, I would like to thank you for playing Grimoire Rhythmorum!",
            "It means a lot to me that you would take the time to try out something like this.",
            "After twenty years of teaching and playing music, I decided it was time to try something new.",
            "Something that has never been seen before.\nAnd so I started working on this project.",
            "Grimoire Rhythmorum is an music education project with the hopes of introducing you " +
            "to real world skills that you can develop and use throughout your entire musical journey.",
            "It's ok if you aren't a musician, or if you don't plan on ever being a musician.",
            "There's still a lot to be gained in terms of music appreciation.",
            "The goal of Grimoire Rhythmorum is to educate and entertain you.\n"+
            "Your goal should be to have fun and to challenge yourself.",
            "The mechanics in this game are probably unlike anything else you've played before. "+
            "Don't be discouraged if you don't pick up on them right away.",
            "It's not about winning or losing, it's about the experience and trying new things.",
            "So don't be afraid put yourself out there, make mistakes, and embarrass yourself.",
            "Enjoy the journey!"
        };
    }
    public class AboutPatterns_Dialogue : Dialogue
    {
        public AboutPatterns_Dialogue(Dialogue returnTo) { BackTo = returnTo; }
        readonly Dialogue BackTo;

        public override Dialogue Initiate()
        {
            FirstLine = GetStartLines(_linesText, BackTo);
            return this;
        }

        string[] _linesText => new string[]{
            "Patterns exist in everything. Everything that repeats, and everything that doesn't. They are all patterns.",
            "Even universes themselves are patterns.",
            "There are infinite universes, but you don't exist in all of them. The hard part is finding realms to instantiate you in that are appropriate.",
            "By spending patterns to upgrade skills and ships, we can refine our search to more appropriate realms.",
            "You can only be instantiated once per any realm. Every time you leave here I have to instantiate you somewhere different. Which is why your ship and the lighthouses reset.",
            "This window next to me will flash a unique shape for each pattern you find. Those are one representation of the patterns. Interact with it and you can upgrade your skills.",
            "If it's blank right now then you don't have any available patterns to spend.",
            "All that means is it's time to set sail and find some.",
            "You can find patterns at sea by fishing, seeking treasure, bounty hunting, and acts of piracy.",
        };
    }
    public class AboutBatterie_Dialogue : Dialogue
    {
        public AboutBatterie_Dialogue(Dialogue returnTo) { ReturnToDialogue = returnTo; }
        public AboutBatterie_Dialogue(State returnTo) { ReturnToState = returnTo; }
        readonly Dialogue ReturnToDialogue;
        readonly State ReturnToState;

        public override Dialogue Initiate()
        {
            FirstLine = GetStartLine();
            return this;
        }


        private Response _exit;
        private Response Exit => _exit ??= ReturnToDialogue != null ? new("back", ReturnToDialogue) : new("back", ReturnToState);


        string[] _string => new string[]{
            "Batterie is a rhythm game emulating both the batterie of a drum line, and the battery of a ships cannons.",
            "The goal is to sight-read sheet music rhythms, and perform them by tapping.",
            "The key to this reading rhythms is understanding Rhythm Cells. Just like there are only 12 notes in music, we can write any rhythm with just 12 rhythmic shapes. Really!",
            "By combining these 12 shapes with ties and rests, we can create any rhythm possible!",
            "There are two main types of these 12 shapes, those with 4 counts, and those with 3 counts - sometimes called triplets.",
            "There are eight 4-count shapes, and four 3-count shapes.",
            "Rhythm cells may use different subdivisions such as quarter notes, eight notes, or sixteenth notes, but that doesn't change the rhythmic shape, only how we count it."
        };


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
}