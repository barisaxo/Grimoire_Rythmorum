using System;
using System.Collections.Generic;

public class LatencyCalibration_State : State
{
    readonly State SubsequentState;

    public LatencyCalibration_State(State subsequentState)
    {
        SubsequentState = subsequentState;
    }

    protected override void PrepareState(Action callback)
    {
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        MonoHelper.OnUpdate += Tick;
    }

    protected override void DisengageState()
    {
        Info.SelfDestruct();
        MonoHelper.OnUpdate -= Tick;
    }

    void Complete()
    {
        DataManager.Gameplay.SetLevel(new Data.Latency(), (int)(Average() * 100));
        UnityEngine.Debug.Log(DataManager.Gameplay.GetLevel(new Data.Latency()));
        SetState(SubsequentState);
    }


    float timer;
    // float the1;
    int count = -1;
    readonly float interval = .7f;
    readonly List<float> lags = new();

    void Tick()
    {
        if (lags.Count == 25) { Complete(); return; }
        // the1 += UnityEngine.Time.deltaTime;

        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Space)) Hit();

        if ((timer += UnityEngine.Time.deltaTime) > interval)
        {
            // UnityEngine.Debug.Log(UnityEngine.Time.time + " " + UnityEngine.Time.deltaTime + " " + (timer - interval));

            timer -= interval;

            // if ((count = count + 1 == 4 ? 0 : count + 1) == 0) the1 = 0;
            // {
            //     Audio.SFX.CurrentVolumeLevel = DataManager.Volume.GetScaledLevel(new Data.SoundFX());
            //     Audio.SFX.PlayOneShot(BatterieAssets.RimShot);
            // }

            // else
            // {
            //     Audio.SFX.CurrentVolumeLevel = DataManager.Volume.GetScaledLevel(new Data.SoundFX()) * .15f;
            //     Audio.SFX.PlayOneShot(BatterieAssets.RimShot);
            // }

            Audio.SFX.PlayOneShot(BatterieAssets.RimShot);
        }
    }

    protected override void GPInput(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.Up_Press:
            case GamePadButton.Down_Press:
            case GamePadButton.Left_Press:
            case GamePadButton.Right_Press:
            case GamePadButton.North_Press:
            case GamePadButton.South_Press:
            case GamePadButton.West_Press:
            case GamePadButton.East_Press:
            case GamePadButton.L1_Press:
            case GamePadButton.R1_Press:
            case GamePadButton.L2_Press:
            case GamePadButton.R2_Press:
                // if (count != 0) return;

                Hit();
                break;

            case GamePadButton.Start_Press:
                SetState(SubsequentState);
                break;
        }
    }

    void Hit()
    {
        // lags.Add(Reduce(the1, interval));
        // UnityEngine.Debug.Log(Reduce(the1, interval) + " " + (int)(Average() * 100));
        lags.Add(timer * 100f);
        UnityEngine.Debug.Log(timer * 100f);
        Average();
        Info.SetTextString(_info + "\n" + (int)((float)((float)lags.Count / 25f) * 100f) + " % complete.");
    }

    private float Reduce(float a, float b)
    {
        while (a > b) a -= b;
        return a;
    }

    private float Average()
    {
        float total = 0;

        foreach (float f in lags) { total += f; }

        float mean = (float)((float)total / (float)lags.Count);

        float varianceSum = 0;
        for (int i = 0; i < lags.Count; i++)
        {
            varianceSum += (lags[i] - mean) * (lags[i] - mean);
        }

        float variance = (float)((float)varianceSum / (float)(lags.Count - 1f));
        variance = MathF.Sqrt(variance);

        UnityEngine.Debug.Log("2* Variance: " + (2 * variance) + ", Mean:" + mean);

        if (lags.Count > 20)
            for (int i = lags.Count - 1; i > 0; i--)
            {
                UnityEngine.Debug.Log(lags[i]);
                if (MathF.Abs(lags[i] - mean) > (2 * variance))
                {
                    UnityEngine.Debug.Log(lags[i] + " = " + MathF.Abs(lags[i] - mean) + " out of variance");
                    lags.RemoveAt(i);
                }
            }

        total = 0;

        foreach (float f in lags) { total += (f * .01f); }

        mean = (float)((float)total / (float)lags.Count);


        return mean;
    }

    readonly Card Info = new Card(nameof(Info), null)
        .SetTMPPosition(0, 0)
        .SetTMPSize(5, 5)
        .SetFontScale(.5f, .5f)
        .AutoSizeFont(false)
        .AllowWordWrap(false)
        .SetTextString(_info)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center);

    readonly static string _info = "Tap to the beat to calibrate latency.\n\n(Press <Start> to quit)\n\n";
}
