//using System;
//using System.Collections.Generic;
//using UnityEngine;
//

//namespace Ritmo
//{
//    public class InputAnalyzer
//    {
//        public InputAnalyzer(Action<string> feedBack, Action<bool> healthBar, Action<Hit> hit, float latencyOffset, BatteryPack pack)
//        {
//            FeedBack = feedBack;
//            LatencyOffset = latencyOffset * .02f;
//            Hit = hit;
//            HealthBar = healthBar;
//            Pack = pack;
//        }

//        private readonly Action<string> FeedBack;
//        private readonly float LatencyOffset;
//        private readonly Action<Hit> Hit;
//        private readonly Action<bool> HealthBar;

//        public BatteryPack Pack;

//        public List<MappedBeat> BeatMap;

//        public bool Running;
//        private bool NoteAttacked;
//        private bool NoteHolding;

//        private double realTime;
//        private double dspTime;
//        private double NoteAttackMoment;
//        //private double NoteReleaseMoment;

//        struct Window
//        {
//            public double start, end;
//            public int ind;
//            public Window(double s, double e, int i)
//            { start = s; end = e; ind = i; }
//        }

//        List<Window> attackWindows;
//        List<Window> holdWindows;
//        List<Window> restWindows;

//        public void InputDownAction()
//        {
//            NoteAttacked = true;
//            NoteHolding = false;
//            NoteAttackMoment = realTime;
//        }

//        public void InputUpAction()
//        {
//            NoteAttacked = false;
//            NoteHolding = false;
//            //NoteReleaseMoment = realTime;
//        }

//        public void Start()
//        {//REDO rest and hold windows?
//            attackWindows = SetUpWindows(AudioSettings.dspTime, .15f, NoteFunction.Attack);
//            restWindows = SetUpWindows(AudioSettings.dspTime, .05f, NoteFunction.Rest);
//            holdWindows = SetUpWindows(AudioSettings.dspTime, .1f, NoteFunction.Hold);
//            return;

//            List<Window> SetUpWindows(double dspTime, float interval, NoteFunction nf)
//            {
//                List<Window> ws = new List<Window>();
//                for (int i = 1; i < BeatMap.Count - 1; i++)
//                { //start at 1 to avoid the null length rest that primes the system.
//                    if (BeatMap[i].NoteFunction == nf)
//                    {
//                        double start = dspTime - interval;
//                        double end = dspTime + interval;
//                        ws.Add(new Window(start, end, i));
//                    }
//                    dspTime += BeatMap[i].TimeInterval;
//                }
//                return ws;
//            }
//        }


//        public async void UpdateLoop()
//        {
//            while (Running)
//            {
//                await Task.Yield();
//                if (!Application.isPlaying) return;

//                UpdateClock();
//                CheckAttackWindows();
//                CheckHoldWindows();
//                CheckRestWindows();
//            }

//            void UpdateClock()
//            {
//                if (dspTime == AudioSettings.dspTime) { realTime += Time.unscaledDeltaTime; }
//                else { realTime = dspTime = AudioSettings.dspTime; }
//            }
//            void CheckRestWindows()
//            {
//                if (restWindows == null) return;

//                if (!NoteHolding && !NoteAttacked)
//                {
//                    for (int i = 0; i < restWindows.Count; i++)
//                    {
//                        if (!(realTime > restWindows[i].end + LatencyOffset)) continue;
//                        Pack.GoodRests++;
//                        restWindows.RemoveAt(i);
//                        Hit(Batteries.Hit.Hit);
//                        HealthBar(false);
//                    }
//                }
//                else if (NoteAttacked)
//                {
//                    for (int i = 0; i < restWindows.Count; i++)
//                    {
//                        if (!(realTime < restWindows[i].end + LatencyOffset) ||
//                            !(realTime > restWindows[i].start + LatencyOffset)) continue;
//                        FeedBack("REST!");
//                        Pack.MissedRests++;
//                        restWindows.RemoveAt(i);
//                        Hit(Batteries.Hit.Miss);
//                        HealthBar(true);
//                    }
//                }
//            }
//            void CheckHoldWindows()
//            {
//                if (holdWindows == null) return;
//                if (NoteHolding)
//                {
//                    for (int i = 0; i < holdWindows.Count; i++)
//                    {
//                        if (!(realTime < holdWindows[i].end + LatencyOffset) ||
//                            !(realTime > holdWindows[i].start + LatencyOffset)) continue;
//                        Pack.GoodHolds++;
//                        holdWindows.RemoveAt(i);
//                        // Hit(Batteries.Hit.Hit);
//                    }
//                }
//                else
//                {
//                    for (int i = 0; i < holdWindows.Count; i++)
//                    {
//                        if (!(realTime > holdWindows[i].end + LatencyOffset)) continue;
//                        FeedBack("HOLD!");
//                        Pack.MissedHolds++;
//                        holdWindows.RemoveAt(i);
//                        Hit(Batteries.Hit.Break);
//                        HealthBar(true);
//                    }
//                }
//            }

//            void CheckAttackWindows()
//            {
//                if (attackWindows == null) return;

//                if (NoteAttacked)
//                {
//                    for (int i = 0; i < attackWindows.Count; i++)
//                    {
//                        if (!(NoteAttackMoment > attackWindows[i].start + LatencyOffset) ||
//                            !(NoteAttackMoment < attackWindows[i].end + LatencyOffset)) continue;
//                        Debug.Log(
//                            NoteAttackMoment + ", " +
//                            (attackWindows[i].start + LatencyOffset + .15f) + ", " +
//                            (NoteAttackMoment - (attackWindows[i].start + LatencyOffset + .15f))
//                        );
//                        FeedBack("HIT! " + (int)(100f * (NoteAttackMoment - (attackWindows[i].start + LatencyOffset + .15f))));
//                        attackWindows.RemoveAt(i);
//                        NoteAttacked = false;
//                        NoteHolding = true;
//                        Pack.GoodHits++;
//                        Hit(Batteries.Hit.Hit);
//                        HealthBar(false);
//                        return;
//                    }
//                    FeedBack("MISS!");
//                    Pack.ErroneousAttacks++;
//                    NoteAttacked = false;
//                    NoteHolding = true;
//                    Hit(Batteries.Hit.Miss);
//                    HealthBar(true);
//                }

//                for (int i = 0; i < attackWindows.Count; i++)
//                {
//                    if (!(realTime > attackWindows[i].end + LatencyOffset)) continue;
//                    FeedBack("MISS!");
//                    Pack.MissedHits++;
//                    attackWindows.RemoveAt(i);
//                    Hit(Batteries.Hit.Break);
//                    HealthBar(true);
//                }

//                // for (int i = 0; i < attackWindows.Count; i++)
//                // {
//                //     if (realTime < attackWindows[i].end && realTime > attackWindows[i].start)
//                //     {
//                //         Debug.Log(attackWindows[i].ind + ", " + (attackWindows[i].end - .15f) + ", " + realTime);
//                //     }
//                // }
//            }
//        }



//    }

//    public enum Hit { Hit, Miss, Break }
//}


//// private float Mistakes;

//// private bool NoteChecked;
//// private bool BeatPending;
//// private (double timeEvent, int index) BeatQue;
//// private int BeatIndex;
//// double startTime;
//// double previousTimeEvent;
//// double NextEventTime;
//// bool Primed;
//// Dictionary<int, bool> Result = new();
//// List<MappedBeat> Good = new();
//// List<MappedBeat> Bad = new();
//// foreach (bool result in results)
//// {
////     overall += result ? 1 : 0;
//// }
//// Debug.Log("You made " + Mistakes + " mistakes.");
//// if ((overall - ErroneousAttacks) < 0)
//// {
////     Debug.Log("OMG HOW YOU COULD YOU!?!");
////     return -99;
//// }
//// else
//// {
////     Debug.Log("The results are in… you got " +
////      (int)(100f * ((float)((float)overall - (float)ErroneousAttacks) / (float)(results.Count))) +
////      "%!");

////     return 100 - (int)(100f * ((float)((float)overall - (float)ErroneousAttacks) / (float)(results.Count)));
//// }

//// if (Primed)
//// {
////     if (NoteAttacked && !NoteChecked)
////     {
////         if (Result.ContainsKey(BeatIndex - 1))
////         {
////             Debug.Log("Already answered" + (BeatIndex - 1)); Mistakes++;
////             FeedBack("Already answered");
////         }
////         else if ((Mathf.Abs((float)(NoteAttackMoment - previousTimeEvent)) - .25f) < .15f)
////         {
////             if (BeatMap[BeatIndex - 1].NoteFunction == NoteFunction.Attack)
////             {
////                 FeedBack("Good Attack");
////                 Debug.Log("Good Attack Prev Note" + (BeatIndex - 1) + ", " + NoteAttackMoment);
////                 Result.Add(BeatIndex - 1, true);
////             }
////             else
////             {
////                 FeedBack("Not Supposed To Attack");
////                 Debug.Log("Not Supposed To Attack Prev Note" + (BeatIndex - 1) + ", " + NoteAttackMoment);
////                 Result.Add(BeatIndex - 1, false);
////             }
////         }
////         else if ((Mathf.Abs((float)(NoteAttackMoment - NextEventTime)) - .25f) < .15f)
////         {
////             if (BeatMap[BeatIndex].NoteFunction == NoteFunction.Attack)
////             {
////                 FeedBack("Good Attack");
////                 Debug.Log("Good Attack Next Note" + (BeatIndex) + ", " + NoteAttackMoment);
////                 Result.Add(BeatIndex, true);
////             }
////             else
////             {
////                 FeedBack("Not Supposed To Attack Next Note");
////                 Debug.Log("Not Supposed To Attack" + (BeatIndex) + ", " + NoteAttackMoment);
////                 Result.Add(BeatIndex, false);
////             }
////         }
////         else if (BeatMap[BeatIndex - 1].NoteFunction == NoteFunction.Attack)
////         {
////             FeedBack("Missed");
////             Debug.Log("Missed " + (BeatIndex - 1) + ", " + (Mathf.Abs((float)(NoteAttackMoment - previousTimeEvent)) - .25f));
////             Result.Add(BeatIndex - 1, false);
////         }
////         else
////         {
////             FeedBack("WHAT??");
////             Debug.Log("What??" + (BeatIndex - 1));
////             Result.Add(BeatIndex - 1, false);
////         }
////         NoteChecked = true;
////         NoteHolding = true;
////         NoteAttacked = false;
////     }

////     if (realTime > NextEventTime)
////     {
////         previousTimeEvent = NextEventTime;
////         if (!Result.ContainsKey(BeatIndex - 1))
////         {
////             Result.Add(BeatIndex - 1, BeatMap[BeatIndex - 1].NoteFunction == NoteFunction.Attack ? false : true);
////         }
////         BeatIndex++;
////         if (BeatIndex >= BeatMap.Count) return;
////         NextEventTime += BeatMap[BeatIndex].TimeInterval;
////         Debug.Log("Next note " + BeatIndex + ", " + BeatMap[BeatIndex].NoteFunction);
////     }
//// }

//// if (NextEventTime + .15f < realTime)
//// {
////     if (Result.ContainsKey(BeatMap[BeatIndex])) { }

//// }

//// if (Mathf.Abs((float)(NextEventTime - realTime)) < .0515f)
//// {
////     // Debug.Log(BeatIndex + ", " + Time.time + ", " + realTime + ", " + Mathf.Abs((float)(NextEventTime - realTime)));
//// }



//// private void OldLoop()
//// {
////     if (NoteChecked is true && BeatPending is false) { return; }

////     switch (BeatMap[BeatQue.index].NoteFunction)
////     {
////         case NoteFunction.Attack:
////             if (NoteAttacked is true && Math.Abs(BeatQue.timeEvent - NoteAttackMoment) - .3f < .15f)
////             {
////                 // if (BeatPending)
////                 // {
////                 NoteChecked = true;
////                 BeatPending = false;
////                 results.Add(true);//hit the attack, 
////                 FeedBack("Hit!");
////                 Mistakes += MathF.Abs((float)(BeatQue.timeEvent - NoteAttackMoment));
////                 //results.Add(true);//add weight
////                 //results.Add(true);//add weight

////                 Debug.Log("GOOD ATTACK " + (MathF.Abs((float)(BeatQue.timeEvent - NoteAttackMoment)) - .3f));
////                 // }
////                 // else
////                 // {
////                 //     NoteChecked = true;
////                 //     ErroneousAttacks++;//erroneous attack
////                 //     FeedBack("Missed!");
////                 //     Mistakes += MathF.Abs((float)(BeatQue.timeEvent - NoteAttackMoment));
////                 // }
////             }
////             break;

////         case NoteFunction.Hold:
////             if (NoteChecked is false &&
////                 BeatMap[BeatQue.index + 1].NoteFunction is NoteFunction.Hold &&
////                 BeatMap[BeatQue.index - 1].NoteFunction is not NoteFunction.Attack)
////             {
////                 NoteChecked = true;
////                 ErroneousAttacks++;//erroneous attack
////                 FeedBack("Missed!");
////                 Mistakes += MathF.Abs((float)(BeatQue.timeEvent - NoteAttackMoment));
////                 //ErroneousAttacks++;//add weight

////                 Debug.Log("Erroneous. Erroneous.  " + MathF.Abs((float)(BeatQue.timeEvent - NoteAttackMoment)));
////             }

////             if (NoteAttacked is false &&
////                 BeatMap[BeatQue.index + 1].NoteFunction is not NoteFunction.Attack)
////             {//Allows a grace period the 16th before the next attack to lift the key.
////                 NoteChecked = true;
////                 BeatPending = false;
////                 results.Add(false);//not holding the note
////                 Mistakes += .5f;
////                 // FeedBack("Miss");

////                 Debug.Log("not holding");
////             }
////             break;

////         case NoteFunction.Rest:
////             if (BeatQue.index == BeatMap.Count - 1) return;
////             if (NoteAttacked is true)
////             {
////                 NoteChecked = true;
////                 BeatPending = false;
////                 results.Add(false);//not resting
////                 FeedBack("Wait!");
////                 Mistakes += .5f;

////                 Debug.Log("not resting");
////             }
////             break;
////     }
//// }





//// public void SetBeatQue((double time, int index) beatQue)
//// {
////     attackWindows ??= SetUpAttackWindows(AudioSettings.dspTime);

////     // Primed = true;
////     // startTime = startTime > 0 ? startTime : AudioSettings.dspTime;
////     // NextEventTime = NextEventTime > 0 ? NextEventTime : AudioSettings.dspTime + BeatMap[0].TimeInterval;
////     // if (BeatIndex == 0) BeatIndex++;
////     // previousTimeEvent = previousTimeEvent == 0 ? NextEventTime : previousTimeEvent;

////     // NextEventTime += BeatMap[BeatIndex].TimeInterval;
////     // BeatIndex++;
////     // Debug.Log(previousTimeEvent + ", " + AudioSettings.dspTime + ", " + realTime);
////     // Debug.Log(nameof(beatQue) + beatQue.Item1 + ", " + AudioSettings.dspTime);

////     // if (!Primed)
////     // {
////     //     Timer = AudioSettings.dspTime + BeatMap[0].TimeInterval;
////     //     Primed = true;
////     // }


////     // if (BeatQue.index == BeatMap.Count - 1) return;

////     // if (BeatPending)
////     // {
////     //     switch (BeatMap[BeatQue.index].NoteFunction)
////     //     {
////     //         case NoteFunction.Attack:
////     //             // if (Result.ContainsKey(BeatMap[beatQue.index - 1]))
////     //             // {
////     //             //     if (Result.ContainsKey(BeatMap[beatQue.index]))
////     //             //     {
////     //             //         Mistakes++;
////     //             //     }
////     //             // }



////     //             results.Add(false);//attack missed
////     //             Mistakes++;
////     //             FeedBack("Missed!");

////     //             // Debug.Log("note missed " + BeatQue.timeEvent + ", " + NoteAttackMoment + ", " +
////     //             //         MathF.Abs((float)(BeatQue.timeEvent - NoteAttackMoment)));
////     //             break;

////     //         case NoteFunction.Hold:
////     //             if (NoteAttacked)
////     //             {
////     //                 //results.Add(true);//holding
////     //                 // FeedBack("Hold!");
////     //                 Debug.Log("note held");
////     //             }
////     //             break;

////     //         case NoteFunction.Rest:
////     //             //results.Add(true);//resting
////     //             // FeedBack("Rest!");
////     //             Debug.Log("good rest");
////     //             break;
////     //     }
////     // }

////     // BeatPending = true;


////     // BeatQue = beatQue;
//// }
//// private List<window> SetUpRestWindows(double dspTime)
//// {
////     List<window> ws = new List<window>();

////     for (int i = 0; i < BeatMap.Count; i++)
////     {
////         if (BeatMap[i].NoteFunction == NoteFunction.Rest)
////         {
////             double start = dspTime - .05f;
////             double end = dspTime + .05f;
////             Debug.Log(start + ", " + end + ", " + i);
////             ws.Add(new window(start, end, i));
////         }
////         dspTime += BeatMap[i].TimeInterval;
////     }
////     return ws;
//// }

//// private List<window> SetUpAttackWindows(double dspTime)
//// {
////     List<window> ws = new List<window>();

////     for (int i = 0; i < BeatMap.Count; i++)
////     {
////         if (BeatMap[i].NoteFunction == NoteFunction.Attack)
////         {
////             double start = dspTime - .15f;
////             double end = dspTime + .15f;
////             Debug.Log(start + ", " + end + ", " + i);
////             ws.Add(new window(start, end, i));
////         }
////         dspTime += BeatMap[i].TimeInterval;
////     }
////     return ws;
//// }