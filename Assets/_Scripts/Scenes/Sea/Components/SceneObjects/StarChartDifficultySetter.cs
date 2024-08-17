using UnityEngine;
using Datum;
using System.Collections.Generic;

namespace Sea
{
    public class StarChartDifficultySetter : IDifficulty
    {
        public StarChartDifficultySetter(Manager manager)
        {
            Manager = manager;
        }

        public Manager Manager { get; }
        public IItem DifficultyLevel
        {
            get
            {
                int theorySolved = Manager.Player.GetLevel(new TheorySolved());
                if (theorySolved < 6) return new NotesT();

                int auralSolved = Manager.Player.GetLevel(new AuralSolved());
                if (auralSolved < 6) return new NotesA();

                int theoryRecentSolved = Manager.PlayerRecent.GetLevel(new TheoryRecentSolved());
                int totalRecentTheory = theoryRecentSolved + Manager.PlayerRecent.GetLevel(new TheoryRecentFailed());

                int auralRecentSolved = Manager.PlayerRecent.GetLevel(new AuralRecentSolved());
                int totalRecentAural = auralRecentSolved + Manager.PlayerRecent.GetLevel(new AuralRecentFailed());

                bool tOrA = Random.value < .5;
                float recentSolveRate = tOrA ?
                    (float)((float)theoryRecentSolved / (float)totalRecentTheory) :
                    (float)((float)auralRecentSolved / (float)totalRecentAural);

                List<IItem> Puzzles = new() { tOrA ? new NotesT() : new NotesA() };

                if (tOrA)
                {
                    if (recentSolveRate > .25f && theorySolved > 5) Puzzles.Add(new StepsT());
                    if (recentSolveRate > .3f && theorySolved > 10) Puzzles.Add(new ScalesT());
                    if (recentSolveRate > .35f && theorySolved > 15) Puzzles.Add(new IntervalsT());
                    if (recentSolveRate > .4f && theorySolved > 20) Puzzles.Add(new TriadsT());
                    if (recentSolveRate > .45f && theorySolved > 25) Puzzles.Add(new InversionsT());
                    if (recentSolveRate > .5f && theorySolved > 30) Puzzles.Add(new InvertedTriadsT());
                    if (recentSolveRate > .55f && theorySolved > 35) Puzzles.Add(new SeventhChordsT());
                    if (recentSolveRate > .6f && theorySolved > 40) Puzzles.Add(new ModesT());
                    if (recentSolveRate > .65f && theorySolved > 45) Puzzles.Add(new Inverted7thChordsT());
                }
                else
                {
                    if (recentSolveRate > .25f && auralSolved > 5) Puzzles.Add(new StepsA());
                    if (recentSolveRate > .3f && auralSolved > 10) Puzzles.Add(new ScalesA());
                    if (recentSolveRate > .35f && auralSolved > 15) Puzzles.Add(new IntervalsA());
                    if (recentSolveRate > .4f && auralSolved > 20) Puzzles.Add(new TriadsA());
                    if (recentSolveRate > .45f && auralSolved > 25) Puzzles.Add(new InversionsA());
                    if (recentSolveRate > .5f && auralSolved > 30) Puzzles.Add(new InvertedTriadsA());
                    if (recentSolveRate > .55f && auralSolved > 35) Puzzles.Add(new SeventhChordsA());
                    if (recentSolveRate > .6f && auralSolved > 40) Puzzles.Add(new ModesA());
                    if (recentSolveRate > .65f && auralSolved > 45) Puzzles.Add(new Inverted7thChordsA());
                }

                // int weightedRand = Helpers.WeightedRandomInt(Puzzles.Count);
                // Debug.Log(Puzzles.Count + " " + rand + " " + percent + " " + weightedRand);

                return Puzzles[Helpers.WeightedRandomInt(Puzzles.Count)];
            }
        }
    }
}
