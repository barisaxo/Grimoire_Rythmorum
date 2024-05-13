using System;
using UnityEngine;
using MusicTheory.Rhythms;
using MusicTheory;
using Sea;

public class BountyBatterie_State : State
{

    public BountyBatterie_State(ShipStats.ShipStats NMEShipStats, GameObject nmeGO, PlayerShip playerShip)
    {
        Fade = true;
        var specs = new RhythmSpecs()
        {
            Time = RandomTimeSignature.Get(),
            NumberOfMeasures = 4,
            SubDivisionTier = MusicTheory.Rhythms.SubDivisionTier.D1Only,
            HasTies = UnityEngine.Random.value > .5f,
            HasRests = UnityEngine.Random.value > .5f,
            HasTriplets = false,
            Tempo = 90
        };
        RhythmSpecs rhythmSpecs = new RhythmSpecs().SetTime(RandomTimeSignature.Get());
        BatteriePack pack = new(rhythmSpecs);
        Scene = new(NMEShipStats, nmeGO, playerShip, Tick, pack, "Bounty");
    }

    BatterieScene Scene;
    bool cadenceStarted = false;
    public int Counter { get; private set; }
    bool CountingOff = true;
    bool Playing = false;
    int score, cap;

    protected override void PrepareState(Action callback)
    {
        Cam.Io.Camera.transform.SetPositionAndRotation(Cam.Io.Camera.transform.position + (UnityEngine.Vector3.up * 7),
            Quaternion.Euler(new Vector3(-20f, Cam.Io.Camera.transform.rotation.eulerAngles.y + 180, Cam.Io.Camera.transform.rotation.eulerAngles.z))
        );

        Counter = 1;
        MonoHelper.OnUpdate += SpaceBar;

        Scene.Initialize();
        Scene.Pack.GetNewSettings(callback).StartCoroutine();
    }

    protected override void EngageState()
    {
        Fade = false;
        Scene.Pack.Synchro.KeepTime();
    }

    protected override void DisengageState()
    {
        Debug.Log((float)((float)score / (float)cap));
        Scene.Pack.Synchro.TickEvent -= Tick;
        Scene.Pack.Synchro.BeatEvent -= Click;
        MonoHelper.OnUpdate -= SpaceBar;

        Scene.BatterieFeedback.SelfDestruct();
        Scene.CountOffFeedBack.SelfDestruct();
        Audio.Batterie.Stop();
        Scene.Pack.MuscopaAudio.StopTheCadence();
        Scene.Pack.MusicSheet.SelfDestruct();

        Scene.BatterieHUD.PlayerCurrent -= Scene.NMEShipStats.VolleyDamage;
        // Debug.Log(DataManager.Io.CharData.GetLevel(Data.Player.CharacterData.DataItem.CurrentHP));
        Data.Two.Manager.Io.PlayerShip.SetLevel(
            new Data.Two.CurrentHitPoints(),
            Data.Two.Manager.Io.PlayerShip.GetLevel(new Data.Two.CurrentHitPoints()) - Scene.NMEShipStats.VolleyDamage);

        if (Scene.Pack.Spammed)
        {
            Data.Two.Manager.Io.PlayerShip.SetLevel(
                new Data.Two.CurrentHitPoints(),
                Data.Two.Manager.Io.PlayerShip.GetLevel(new Data.Two.CurrentHitPoints()) - Scene.NMEShipStats.VolleyDamage);

            Scene.BatterieHUD.NMECurrent = Scene.BatterieHUD.NMEMax;
        }

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
            // Scene.BatterieHUD.PlayerCurrent -= Scene.NPCShip.ShipStats.DamagePotential;
            // DataManager.Io.CharData.SetLevel(
            //     Data.Player.CharacterData.DataItem.CurrentHP,
            //     DataManager.Io.CharData.DataItems[Data.Player.CharacterData.DataItem.CurrentHP] - Scene.NPCShip.ShipStats.DamagePotential);

            // UnityEngine.Debug.Log("Damage recieved: " + Scene.NPCShip.ShipStats.DamagePotential);
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
