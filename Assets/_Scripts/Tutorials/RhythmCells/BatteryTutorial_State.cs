using UnityEngine;
using Batterie;
using Dialog;
using System;
using SheetMusic;
using MusicTheory.Rhythms;
using Datum;

public class BatteryTutorial_State : State
{
    public BatteryTutorial_State(Dialogue previousDialogue, Dialogue nextDialogue, Measure[] measures, RhythmSpecs rhythmSpecs, bool cadence)
    {
        Cadence = cadence;
        PreviousDialogue = previousDialogue;
        NextDialogue = nextDialogue;
        RhythmSpecs = rhythmSpecs;
        Measures = measures;
    }

    public BatteryTutorial_State(Measure[] measures, RhythmSpecs rhythmSpecs, IData data, IItem item, State subsequentState, bool cadence)
    {
        Cadence = cadence;
        RhythmSpecs = rhythmSpecs;
        Measures = measures;
        Data = data;
        Item = item;
        SubsequentState = subsequentState;
    }

    readonly bool Cadence;
    readonly Measure[] Measures;
    readonly Dialogue PreviousDialogue;
    readonly Dialogue NextDialogue;
    readonly RhythmSpecs RhythmSpecs;
    readonly State SubsequentState;

    readonly IData Data;
    readonly IItem Item;

    void FinishBattery()
    {
        bool won = Scene.Pack.TotalErrors == 0;

        if (won) Data.AdjustLevel(Item, 1);

        SetState(new DialogStart_State(new EndPractice_Dialogue(won, SubsequentState)));

        // SetState(new DialogStart_State(new MiniTutorialResults_Dialogue(PreviousDialogue, NextDialogue, Measures, RhythmSpecs, Scene.Pack)));
    }

    BatterieTutorialScene Scene;
    bool cadenceStarted = false;
    public int Counter { get; private set; }
    bool CountingOff = true;
    bool Playing = false;

    // public static Genre RandomGenre() => (Genre)UnityEngine.Random.Range(0, Count());
    // public static int Count() => Enum.GetNames(typeof(Genre)).Length;

    protected override void PrepareState(Action callback)
    {
        Counter = 1;
        // MonoHelper.OnUpdate += SpaceBar;

        Cam.Io.Camera.transform.SetPositionAndRotation((UnityEngine.Vector3.up * 7),
           Quaternion.Euler(new Vector3(-20f, 180, Cam.Io.Camera.transform.rotation.eulerAngles.z))
        );
        Scene = new(BatteryTutorialTick, RhythmSpecs, Measures);
        (Scene as IScene).Initialize();

        Scene.Pack.GetNewSettings(callback).StartCoroutine();
    }

    protected override void EngageState()
    {
        Scene.Pack.Synchro.KeepTime();
    }

    protected override void DisengageState()
    {
        Scene.Pack.Synchro.TickEvent -= BatteryTutorialTick;
        if (Cadence) Scene.Pack.MuscopaAudio.StopTheCadence();
        else Scene.Pack.Synchro.BeatEvent -= Click;
        // MonoHelper.OnUpdate -= SpaceBar;

        Scene.BatterieFeedback.SelfDestruct();
        Scene.CountOffFeedBack.SelfDestruct();
        Audio.Batterie.FadeAndStop();
        Scene.Pack.MusicSheet.SelfDestruct();
    }


    void BatteryTutorialTick()
    {
        if (CountingOff)
        {
            CountOffTimeEvent();
            if (++Counter == Scene.Pack.CountOffBeatmap.Length - 1)
            {
                MonoHelper.OnUpdate += Scene.Pack.Analyzer.RhythmInputAnalyzerTick;
                Scene.Pack.Analyzer.Start();
                // Scene.Pack.Synchro.BeatEvent += Click;
                CountingOff = false; Playing = true; Counter = 0;
                // Audio.Batterie.Miss();
            }
            return;
        }

        if (!cadenceStarted)
        {
            if (Cadence) Scene.Pack.MuscopaAudio.PlayNewMuscopaPuzzleMusic();
            else Scene.Pack.Synchro.BeatEvent += Click;

            cadenceStarted = true;
        }

        if (Playing)
        {
            BatterieTimeEvent();
            Playing = !(++Counter >= Scene.Pack.MusicSheet.BeatMap.Length);
        }

        if (!Playing && !CountingOff)
        {
            Audio.Batterie.FadeAndStop();
            Scene.Pack.Synchro.Stop();
            MonoHelper.OnUpdate -= Scene.Pack.Analyzer.RhythmInputAnalyzerTick;
            Scene.Pack.MuscopaAudio.StopTheCadence();

            // FadeToState(PuzzleSelector.WeightedRandomPuzzleState(Data.TheoryPuzzleData));
            // Scene.NMEHealth.cur -= Scene.Pack.GoodHits * DataManager.ShipData.ShipStats.DamagePotential;
            FinishBattery();
            // SetState(new DialogStart_State(new BatterieIntermission_Dialogue(Scene)));
        }
    }

    void CountOffTimeEvent()
    {
        switch (Scene.Pack.CountOffBeatmap[Counter].NoteFunction)
        {
            case NoteFunction.Attack:
                Scene.CountOffFeedBack.ReadCountOff();
                Audio.Batterie.PlayClick();
                break;

            case NoteFunction.Hold:
                Audio.Batterie.PlayClick();
                break;
        }
    }

    void BatterieTimeEvent()
    {
        switch (Scene.Pack.MusicSheet.BeatMap[Counter].NoteFunction)
        {
            case NoteFunction.Attack:
                Audio.Batterie.PlaySnareRoll();
                break;

            case NoteFunction.Rest:
                Audio.Batterie.RestSnareRoll();
                break;

            case NoteFunction.Hold:
                break;
        }
    }

    protected override void GPInput(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.Up_Release:
            case GamePadButton.Down_Release:
            case GamePadButton.Left_Release:
            case GamePadButton.Right_Release:
            case GamePadButton.North_Release:
            case GamePadButton.South_Release:
            case GamePadButton.West_Release:
            case GamePadButton.East_Release:
            case GamePadButton.L1_Release:
            case GamePadButton.R1_Release:
            case GamePadButton.L2_Release:
            case GamePadButton.R2_Release:
            case GamePadButton.R3_Release:
                Scene.Pack.Analyzer.InputUpAction(); break;

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
            case GamePadButton.R3_Press:
                Scene.Pack.Analyzer.InputDownAction();
                UnityEngine.Debug.Log(gpb.ToString());
                break;
        }

    }
    void Click()
    {
        Audio.Batterie.PlayClick();
    }

    // protected override void Clicked(MouseAction action, Vector3 mousePos)
    // {
    //     switch (action)
    //     {
    //         case MouseAction.LUp:
    //             Scene.Pack.Analyzer.InputUpAction();
    //             break;

    //         case MouseAction.LDown:
    //             Scene.Pack.Analyzer.InputDownAction();
    //             break;
    //     }
    // }

    // protected void SpaceBar()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         Scene.Pack.Analyzer.InputDownAction();
    //     }
    //     else if (Input.GetKeyUp(KeyCode.Space))
    //     {
    //         Scene.Pack.Analyzer.InputUpAction();
    //     }

    //     // if (Input.GetKeyDown(KeyCode.Tab))
    //     // {
    //     //     SetState(new Batterie_State(Specs));
    //     // }
    // }
    // private void HandleHit(Batterie.Hit hit)
    // {
    //     cap++;
    //     switch (hit)
    //     {
    //         case Hit.Hit:
    //             score++;
    //             Pack.GoodHits++;
    //             Audio.Batterie.Hit();
    //             Pack.ShipFire.Play();
    //             break;
    //         case Hit.Miss:
    //             score--;
    //             Pack.MissedHits++;
    //             Audio.Batterie.Miss();
    //             break;
    //         case Hit.BadHit:
    //             score--;
    //             Pack.ErroneousAttacks++;
    //             Audio.Batterie.MissStick();
    //             Pack.NMEFire.Play();
    //             break;
    //         case Hit.Break:
    //             break;
    //     }
    // }

    //RegionalMode GetShipRegion()
    //{
    //    return Board.TargetTile.ShipType.ToRegion();
    //}
}
