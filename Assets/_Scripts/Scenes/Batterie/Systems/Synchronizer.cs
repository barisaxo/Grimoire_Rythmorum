using System;
using System.Collections;
using UnityEngine;
using MusicTheory.Rhythms;

namespace Batterie
{
    public class Synchronizer
    {
        public Synchronizer(Quantizement quant, float tempo)
        {
            bpm = tempo;

            beatSpaces = quant switch
            {
                Quantizement.Eighth => 6,
                Quantizement.EighthTrips => 18,
                Quantizement.Half => 24,
                _ => 12,
            };

            interval = 60d / (double)(bpm * beatSpaces);
        }

        public bool KeepingTime { get; private set; }

        public event Action BeatEvent;
        public event Action TickEvent;

        private readonly float bpm;
        private readonly double interval;
        private readonly int beatSpaces;

        private int beatSpacer;
        private int counter;
        private double d_startTime;

        public void ResetQueues()
        {
            beatSpacer = beatSpaces;
        }

        public void Stop()
        {
            KeepingTime = false;
        }

        public void KeepTime()
        {
            KeepingTime = true;
            d_startTime = UnityEngine.Time.timeAsDouble;
            ResetQueues();

            MonoHelper.OnUpdate += UpdateLoop;
        }

        private void UpdateLoop()
        {
            if (KeepingTime)
            {
                if (UnityEngine.Time.timeAsDouble >= d_startTime + (double)(interval * counter))
                {
                    counter++;
                    beatSpacer++;
                    TickEvent?.Invoke();
                }

                if (beatSpacer >= beatSpaces)
                {
                    beatSpacer = 0;
                    BeatEvent?.Invoke();
                }
            }
            else MonoHelper.OnUpdate -= UpdateLoop;

        }


        // private double dspStartTime;
        // private double dspTime;
        // private double realTime;
        // private IEnumerator UpdateLoop()
        // {
        //     startTime = realTime = dspTime = AudioSettings.dspTime;
        //     yield return null;

        //     ResetQueues();

        //     if (dspTime == AudioSettings.dspTime) realTime += UnityEngine.Time.unscaledDeltaTime;
        //     else if (AudioSettings.dspTime < realTime) { }
        //     else realTime = dspTime = AudioSettings.dspTime;

        //     KeepingTime = true;

        //     while (KeepingTime)
        //     {
        //         if (realTime >= startTime + (double)(interval * counter))
        //         {
        //             counter++;
        //             beatSpacer++;
        //             TickEvent?.Invoke();
        //         }

        //         if (beatSpacer >= beatSpaces)
        //         {
        //             beatSpacer = 0;
        //             BeatEvent?.Invoke();
        //         }

        //         yield return null;

        //         //Sometimes AudioSettings.dspTime doesn't update properly. Might need more looking into.
        //         // if (dspTime == AudioSettings.dspTime) realTime += UnityEngine.Time.unscaledDeltaTime;
        //         // else realTime = dspTime = AudioSettings.dspTime;

        //         // Debug.Log("BEFORE: saved dspTime: " + dspTime + ", AudioSettings.dspTime: " + AudioSettings.dspTime +  ", realTime: " + realTime);
        //         if (dspTime == AudioSettings.dspTime) realTime += UnityEngine.Time.unscaledDeltaTime;
        //         else if (AudioSettings.dspTime < realTime) { Debug.Log("BACKPEDAL!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n!\n!\n!\n!\nPREVENTED?!??!"); }//attempt to prevent backpedaling 
        //         else realTime = dspTime = AudioSettings.dspTime;
        //         // Debug.Log("AFTER: saved dspTime: " + dspTime + ", AudioSettings.dspTime: " + AudioSettings.dspTime +  ", realTime: " + realTime);

        //     }
        // }
        // frame++;
        // Debug.Log("test frame#: " + frame + ", timeAsDouble: " + UnityEngine.Time.timeAsDouble + ", dspTime: " + AudioSettings.dspTime + ", drift: " + ((UnityEngine.Time.timeAsDouble - d_startTime) - (AudioSettings.dspTime - dspStartTime)));
        // Debug.Log("time as double: " + (UnityEngine.Time.timeAsDouble - d_startTime) + ", dsp time: " + (AudioSettings.dspTime - dspStartTime));
        // Debug.Log("drift from start time : " + ((UnityEngine.Time.timeAsDouble - d_startTime) - (AudioSettings.dspTime - dspStartTime)) +
        //         "drift real time: " + (UnityEngine.Time.timeAsDouble - AudioSettings.dspTime));


    }
}