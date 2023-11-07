using System;
using System.Collections;
using UnityEngine;
using Musica.Rhythms;

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

            interval = 60d / (double)(bpm * 12d);
        }

        public bool KeepingTime { get; private set; }

        public event Action BeatEvent;
        public event Action TickEvent;

        private readonly float bpm;
        private readonly double interval;
        private readonly int beatSpaces;

        private int beatSpacer;
        private int counter;
        private double startTime;
        private double dspTime;
        private double realTime;

        public void ResetQueues()
        {
            beatSpacer = beatSpaces;
        }

        public void Stop()
        {
            KeepingTime = false;
        }

        public void KeepTime() => UpdateLoop().StartCoroutine();

        private IEnumerator UpdateLoop()
        {
            startTime = realTime = dspTime = AudioSettings.dspTime;
            yield return null;

            ResetQueues();

            if (dspTime == AudioSettings.dspTime) realTime += UnityEngine.Time.unscaledDeltaTime;
            else realTime = dspTime = AudioSettings.dspTime;

            KeepingTime = true;

            while (KeepingTime)
            {
                if (realTime >= startTime + (interval * counter))
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

                yield return null;

                //Sometimes AudioSettings.dspTime doesn't update properly. Might need more looking into.
                if (dspTime == AudioSettings.dspTime) realTime += UnityEngine.Time.unscaledDeltaTime;
                else realTime = dspTime = AudioSettings.dspTime;
            }
        }
    }
}