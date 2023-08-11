using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Synchronizer
{
    public double dspTime;
    public double realTime;
    public bool Running;
    public double NextEventTime { get; set; }
    private double click;
    public bool KeepingTime;
    public event Action ClickEvent;
    public event Action TimeEvent;

    public void KeepTime(float bpm)
    {
        UpdateLoop(bpm).StartCoroutine();

        IEnumerator UpdateLoop(float bpm)
        {
            while (KeepingTime)
            {
                if (dspTime == AudioSettings.dspTime) { realTime += UnityEngine.Time.unscaledDeltaTime; }
                else { realTime = dspTime = AudioSettings.dspTime; }

                if (realTime >= NextEventTime)
                {
                    TimeEvent?.Invoke();
                    NextEventTime += 60 / bpm;
                }

                if (realTime > click)
                {
                    ClickEvent?.Invoke();
                    click += 60D / bpm;
                }
                yield return null;
            }
        }
    }

}