using System.Collections;
using System;
using UnityEngine;
using SheetMusic;
using Batterie;
using MusicTheory.Rhythms;
using MusicTheory;
using Muscopa;

public class BatterieAndCadence_State : State
{
    public BatterieAndCadence_State(BatterieScene batterieScene)
    {
        Scene = batterieScene;
        Scene.Tick = Tick;
    }

    public BatterieAndCadence_State(RhythmSpecs specs)
    {
        // Debug.Log(Sea.WorldMapScene.Io.NearestNPC.Name + " " +
        //     Sea.WorldMapScene.Io.NearestNPC.ShipStats.CannonStats.Cannon.Modifier + " " +
        //     Sea.WorldMapScene.Io.NearestNPC.ShipStats.CannonStats.Metal.Modifier + " " +
        //     Sea.WorldMapScene.Io.NearestNPC.ShipStats.NumOfCannons + " " +
        //     Sea.WorldMapScene.Io.NearestNPC.ShipStats.VolleyDamage);

        Scene =
            new BatterieScene(
              Sea.WorldMapScene.Io.NearestNPC.ShipStats,
              Sea.WorldMapScene.Io.NearestNPC.SceneObject.GO,
              playerShip: Sea.WorldMapScene.Io.Ship,
              tick: Tick,
              specs: specs,
              nmeName: Sea.WorldMapScene.Io.NearestNPC.Name
        );
    }
    // public BatteriePack Pack;
    readonly BatterieScene Scene;
    bool cadenceStarted = false;
    public int Counter { get; private set; }
    bool CountingOff = true;
    bool Playing = false;
    int score, cap;

    protected override void PrepareState(Action callback)
    {
        Counter = 1;
        MonoHelper.OnUpdate += SpaceBar;

        Cam.Io.Camera.transform.SetPositionAndRotation((UnityEngine.Vector3.up * 7),
           Quaternion.Euler(new Vector3(-20f, 180, Cam.Io.Camera.transform.rotation.eulerAngles.z))
        );

        Scene.Initialize();


        Scene.Pack.GetNewSettings(callback).StartCoroutine();
    }

    protected override void EngageState()
    {
        Scene.Pack.Synchro.KeepTime();
    }

    protected override void DisengageState()
    {
        Scene.Pack.Synchro.TickEvent -= Tick;
        Scene.Pack.Synchro.BeatEvent -= Click;
        MonoHelper.OnUpdate -= SpaceBar;

        Scene.BatterieFeedback.SelfDestruct();
        Scene.CountOffFeedBack.SelfDestruct();
        Audio.Batterie.Stop();
        Scene.Pack.MuscopaAudio.StopTheCadence();
        Scene.Pack.MusicSheet.SelfDestruct();
    }


    void Tick()
    {
        if (CountingOff)
        {
            CountOffTimeEvent();
            if (++Counter == Scene.Pack.CountOffBeatmap.Length - 1)
            {
                MonoHelper.OnUpdate += Scene.Pack.Analyzer.Tick;
                Scene.Pack.Analyzer.Start();
                Scene.Pack.Synchro.BeatEvent += Click;
                CountingOff = false; Playing = true; Counter = 0;
                // Audio.Batterie.Miss();
            }
            return;
        }

        if (!cadenceStarted)
        {
            Scene.Pack.MuscopaAudio.PlayNewMuscopaPuzzleMusic();
            cadenceStarted = true;
        }

        if (Playing)
        {
            BatterieTimeEvent();
            Playing = !(++Counter >= Scene.Pack.MusicSheet.BeatMap.Length);
        }

        if (!Playing && !CountingOff)
        {
            Audio.Batterie.Stop();
            Scene.Pack.Synchro.Stop();
            MonoHelper.OnUpdate -= Scene.Pack.Analyzer.Tick;
            Scene.Pack.MuscopaAudio.StopTheCadence();

            // FadeToState(PuzzleSelector.WeightedRandomPuzzleState(Data.TheoryPuzzleData));
            // Scene.NMEHealth.cur -= Scene.Pack.GoodHits * DataManager.ShipData.ShipStats.DamagePotential;

            SetState(new DialogStart_State(new BatterieIntermission_Dialogue(Scene)));
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
        if (UnityEngine.Random.value < .04f)
        {
            Scene.NMEFire.Play();
        }
    }

    void Click()
    {
        //Audio.Batterie.PlayClick();
    }

    protected override void Clicked(MouseAction action, Vector3 mousePos)
    {
        switch (action)
        {
            case MouseAction.LUp:
                Scene.Pack.Analyzer.InputUpAction();
                break;

            case MouseAction.LDown:
                Scene.Pack.Analyzer.InputDownAction();
                break;
        }
    }

    protected void SpaceBar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Scene.Pack.Analyzer.InputDownAction();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Scene.Pack.Analyzer.InputUpAction();
        }

        // if (Input.GetKeyDown(KeyCode.Tab))
        // {
        //     SetState(new Batterie_State(Specs));
        // }
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
                Scene.Pack.Analyzer.InputDownAction(); break;
        }
    }

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

    public static Genre RandomGenre() => (Genre)UnityEngine.Random.Range(0, Count());
    public static int Count() => Enum.GetNames(typeof(Genre)).Length;

}