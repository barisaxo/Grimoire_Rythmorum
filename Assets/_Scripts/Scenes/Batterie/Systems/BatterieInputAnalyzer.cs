using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MusicTheory.Rhythms;

namespace Batterie
{
    public class BatterieInputAnalyzer
    {
        public BatterieInputAnalyzer(Action<string> feedBack, Action<Hit> hit, float latencyOffset, MappedBeat[] beatmap)
        {
            FeedBack += feedBack;
            // LatencyOffset = latencyOffset * .02f;
            Hit = hit;
            BeatMap = beatmap.ToList(); ;

            FeedBack += FeedbackDebug;

        }

        public int score;

        private readonly Action<string> FeedBack;
        private readonly float LatencyOffset;
        private readonly Action<Hit> Hit;
        private readonly List<MappedBeat> BeatMap;

        public bool Running;
        private bool NoteAttacked;
        private bool NoteHolding;

        private double realTime;
        private double dspTime;
        private double NoteAttackMoment;
        private double StartTime;

        public struct Window
        {
            public double start, end;
            public int ind;
            public Window(double s, double e, int i)
            { start = s; end = e; ind = i; }
        }

        public List<Window> attackWindows;
        List<Window> holdWindows;
        List<Window> restWindows;

        public void InputDownAction()
        {
            NoteAttacked = true;
            NoteHolding = false;
            NoteAttackMoment = realTime;
        }

        public void InputUpAction()
        {
            NoteAttacked = false;
            NoteHolding = false;
        }

        public void SetUp()
        {
            attackWindows = SetAttackWindows();
            restWindows = SetRestWindows();
            holdWindows = SetHoldWindows();
            return;

            List<Window> SetAttackWindows(double time = 0, float buffer = .15f, NoteFunction nf = NoteFunction.Attack)
            {
                List<Window> ws = new List<Window>();
                for (int i = 0; i < BeatMap.Count; i++)
                {
                    if (BeatMap[i].NoteFunction == nf)
                    {
                        double start = time - buffer;
                        double end = time + buffer;
                        ws.Add(new Window(start, end, i));
                    }
                    time += BeatMap[i].TimeInterval;
                }
                return ws;
            }

            List<Window> SetRestWindows(double time = 0, float buffer = .05f, NoteFunction nf = NoteFunction.Rest)
            {
                List<Window> ws = new();
                for (int i = 0; i < BeatMap.Count; i++)
                {
                    if (BeatMap[i].NoteFunction == nf)
                    {
                        double start = time + buffer;
                        double end = BeatMap[i].TimeInterval + time - buffer;
                        ws.Add(new Window(start, end, i));
                    }
                    time += BeatMap[i].TimeInterval;
                }
                return ws;
            }

            List<Window> SetHoldWindows(double time = 0, float buffer = .15f, NoteFunction nf = NoteFunction.Attack)
            {
                List<Window> ws = new();
                for (int i = 0; i < BeatMap.Count; i++)
                {
                    if (BeatMap[i].NoteFunction == nf)
                    {
                        double start = time + buffer;
                        double end = BeatMap[i].TimeInterval + time - buffer - buffer;
                        ws.Add(new Window(start, end, i));
                    }
                    time += BeatMap[i].TimeInterval;
                }
                return ws;
            }
        }

        void FeedbackDebug(string s) => Debug.Log(s);
        public void Start() { StartTime = realTime = dspTime = AudioSettings.dspTime; }
        public void Tick()
        {
            UpdateClock();
            CheckAttackWindows();
            CheckHoldWindows();
            CheckRestWindows();
        }

        void UpdateClock()
        {
            if (dspTime == AudioSettings.dspTime) { realTime += UnityEngine.Time.unscaledDeltaTime; }
            else { realTime = dspTime = AudioSettings.dspTime; }
        }

        void CheckRestWindows()
        {
            if (restWindows.Count == 0) return;

            if (!NoteHolding && !NoteAttacked)
            {
                if (realTime > restWindows[0].end + StartTime + LatencyOffset)
                {
                    score++;
                    Debug.Log("Good Rest " + realTime + " " + (restWindows[0].end + StartTime + LatencyOffset));
                    restWindows.RemoveAt(0);
                }
            }
            else if (NoteAttacked)
            {
                if ((realTime < restWindows[0].end + StartTime + LatencyOffset) ||
                    (realTime > restWindows[0].start + StartTime + LatencyOffset))
                {
                    score--;
                    // FeedBack("REST! " + realTime + " " + (restWindows[0].end + StartTime + LatencyOffset));
                    restWindows.RemoveAt(0);
                    Hit?.Invoke(Batterie.Hit.BadHit);
                }
            }
        }

        void CheckHoldWindows()
        {
            if (holdWindows.Count == 0) return;

            if (!NoteHolding &&
                (realTime > holdWindows[0].start + StartTime + LatencyOffset) &&
                (realTime < holdWindows[0].end + StartTime + LatencyOffset))
            {
                score--;
                // Debug.Log("HOLD! " + realTime + " " + (holdWindows[0].end + StartTime + LatencyOffset));
                holdWindows.RemoveAt(0);
                Hit?.Invoke(Batterie.Hit.BadHold);
                return;
            }
            if (realTime > holdWindows[0].end + StartTime + LatencyOffset)
            {
                score++;
                // Debug.Log("Good Hold " + realTime + " " + (holdWindows[0].end + StartTime + LatencyOffset));
                holdWindows.RemoveAt(0);
            }
        }

        void CheckAttackWindows()
        {
            if (attackWindows.Count == 0 && NoteAttacked)
            {
                score--;
                FeedBack("Bad Attack!");
                NoteAttacked = false;
                NoteHolding = true;
                Hit?.Invoke(Batterie.Hit.BadHit);
                return;
            }
            else if (attackWindows.Count == 0) return;

            if (NoteAttacked &&
               (NoteAttackMoment >= attackWindows[0].start + StartTime + LatencyOffset) &&
               (NoteAttackMoment <= attackWindows[0].end + StartTime + LatencyOffset))
            {
                score++;

                int msec = (int)(100f * (NoteAttackMoment - (attackWindows[0].start + StartTime + LatencyOffset + .15f)));

                FeedBack(
                    Mathf.Abs(msec) switch
                    {
                        < 2 => "PERFECT!",
                        < 5 => "<size=90%>Excellent!",
                        < 10 => "<size=80%>Great!",
                        _ => "<size=70%>Good!",
                    } +
                    "<size=60%> (" +
                    msec +
                    " msec)");

                attackWindows.RemoveAt(0);
                NoteAttacked = false;
                NoteHolding = true;
                Hit?.Invoke(Batterie.Hit.Hit);
                return;
            }

            else if (NoteAttacked)
            {
                score--;
                FeedBack("Bad Attack!");
                NoteAttacked = false;
                NoteHolding = true;
                Hit?.Invoke(Batterie.Hit.BadHit);
            }

            if (realTime > attackWindows[0].end + StartTime + LatencyOffset)
            {
                score--;
                // Debug.Log("Missed: " + realTime + ", " + (attackWindows[0].start + StartTime) + " " + (attackWindows[0].end + StartTime));
                FeedBack("MISS!");
                attackWindows.RemoveAt(0);
                Hit?.Invoke(Batterie.Hit.Miss);
            }
        }


    }

    public enum Hit { Hit, Miss, BadHit, Break, BadHold }
}