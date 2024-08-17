using System;
using MusicTheory.Rhythms;
using Batterie;
using SheetMusic;
using Datum.BeatFishing;

using UnityEngine;

//todo Visual Aid for fishing tutorial!

public class BeatFishing_State : State
{
    public BeatFishing_State(State subsequentState)
    {
        SubsequentState = subsequentState;
    }

    readonly State SubsequentState;
    RhythmSpecs Specs;
    Synchronizer Synchro;
    BatterieInputAnalyzer Analyzer;
    BatterieFeedback BatterieFeedback;
    MappedBeat[] BeatMap;
    MusicSheet MusicSheet;

    Card Reel;
    Card Handle;
    Card Spool;
    Card FishingReel;

    Transform Parent;

    private int Score = 20;
    private int Difficulty;
    public int Counter = 1;
    private int count = 3;

    protected override void PrepareState(Action callback)
    {
        Difficulty = (int)(60 * (1 / Datum.Manager.Io.Skill.GetBonusRatio(new Datum.PulsePerception())));
        Debug.Log(Difficulty);
        SetUpFishingReel();
        Specs = new RhythmSpecs()
        {
            Time = new FourFour(),
            NumberOfMeasures = 16,
            SubDivisionTier = SubDivisionTier.D1Only,
            Tempo = 90
        };

        IBeatFishingPractice item = (IBeatFishingPractice)BeatFishingPracticeEnum.ToItem(Enumeration.All<BeatFishingPracticeEnum>()[UnityEngine.Random.Range(0, 8)]);

        MusicSheet = new()
        {
            RhythmSpecs = new RhythmSpecs()
            {
                Time = new FourFour(),
                NumberOfMeasures = 1,
                SubDivisionTier = SubDivisionTier.BeatOnly,
                Tempo = 90
            },
            Measures = FishingForBeats.GetMeasures(item)
        };

        MusicSheet.GetNotes();
        MusicSheet.DrawRhythms(false);
        MusicSheet.BeatMap = MusicSheet.Notes.MapBeats(MusicSheet.RhythmSpecs.Time.GetQuantizement(), MusicSheet.RhythmSpecs.Tempo);

        Synchro = new(Specs.Time.GetQuantizement(), Specs.Tempo);

        BatterieFeedback = new();

        BeatMap = FishingForBeats.GetBeats(item, 16).MapBeats(Specs.Time.GetQuantizement(), Specs.Tempo);
        // BeatMap = FishingForBeats.BeatsFromTimeSig(Specs.Time, Specs.NumberOfMeasures).MapBeats(Specs.Tempo);

        Analyzer = new(BatterieFeedback.CreateCard, HandleHit, BeatMap);
        Analyzer.SetUp();

        BatterieFeedback.UpdateLoop();
        MonoHelper.OnUpdate += SpaceBar;

        Audio.Ambience.VolumeLevelSetting = DataManager.Volume.GetScaledLevel(new Datum.SoundFX());
        Audio.BeatFishing.VolumeLevelSetting = DataManager.Volume.GetScaledLevel(new Datum.Chords());
        Audio.SFX.VolumeLevelSetting = DataManager.Volume.GetScaledLevel(new Datum.BatterieVolume());
        Audio.Ambience.Loop = false;
        Audio.Ambience.PlayClip(Resources.Load<AudioClip>("Audio/Fishing/SoftWaterSoundsA"));

        base.PrepareState(callback);
    }

    private void SetUpFishingReel()
    {
        Parent = new GameObject(nameof(Parent)).transform;
        FishingReel = new Card(nameof(FishingReel), Parent)
            ;

        Spool = FishingReel.CreateChild(nameof(Spool), FishingReel.Canvas)
            .SetImageColor(new Color(0, .6f, .9f))
            .SetImageSprite(Resources.Load<Sprite>("Sprites/Fishing/Spool"))
            .SetImagePosition(Vector2.zero)
            .SetImageSize(Vector2.one * 5)
            .SetCanvasSortingOrder(1)
            ;

        Reel = FishingReel.CreateChild(nameof(Reel), FishingReel.Canvas)
            .SetImageColor(new Color(1, .7f, 0))
            .SetImageSprite(Resources.Load<Sprite>("Sprites/Fishing/Reel"))
            .SetImagePosition(Vector2.zero)
            .SetImageSize(Vector2.one * 5)
            .SetCanvasSortingOrder(2)
            ;

        Handle = FishingReel.CreateChild(nameof(Handle), FishingReel.Canvas)
            .SetImageColor(new Color(.7f, .7f, 0))
            .SetImageSprite(Resources.Load<Sprite>("Sprites/Fishing/ReelHandle"))
            .SetImagePosition(Vector2.zero)
            .SetImageSize(Vector2.one * 5)
            .SetCanvasSortingOrder(3)
            ;
    }

    protected override void EngageState()
    {
        Audio.BeatFishing.PlayClip(Resources.Load<AudioClip>("Audio/Drums/Stax_Drums_90_1"));
        MonoHelper.OnUpdate += Analyzer.BatterieInputAnalyzerTick;
        MonoHelper.OnUpdate += ReelingAnimations;
        Synchro.TickEvent += BeatFishingTick;
        Synchro.KeepTime();
        Analyzer.Start();

        // this is debug
        // Synchro.BeatEvent += () => Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Ritmo/RimShot"));
    }

    protected override void DisengageState()
    {
        MusicSheet.SelfDestruct();
        GameObject.Destroy(Parent.gameObject);
        Audio.BeatFishing.FadeAndStop();
        Sea.WorldMapScene.Io.ClearCell();

        Synchro.Stop();
        MonoHelper.OnUpdate -= Analyzer.BatterieInputAnalyzerTick;
        MonoHelper.OnUpdate -= ReelingAnimations;
        Synchro.TickEvent -= BeatFishingTick;
        MonoHelper.OnUpdate -= SpaceBar;
    }

    void BeatFishingTick()
    {
        count++;
        if (Reeling && (count %= 4) == 0) Score++;
        if (!(BeatMap.Length > ++Counter))
        {
            SetState(
                new BeatFishingToSeaTransition_State(
                    SubsequentState,
                    Sea.WorldMapScene.Io.NearestInteractableCell.SceneObject,
                    won: false));
        }
    }

    private void HandleHit(Hit hit)
    {
        switch (hit)
        {
            case Hit.Hit:
                Score += 2;
                Reeling = true;
                Spooling = false;
                if (!Audio.SFX.AudioSources[0].isPlaying)
                    Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing/ReelingSlowA"));

                if (Score > Difficulty)
                {
                    Synchro.TickEvent -= BeatFishingTick;
                    Audio.Ambience.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing/FishOutOfWaterA"));
                    Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/SFX/AlertHalfDim"));
                    //Todo Finish with crash cymbal?
                    SetState(
                        new BeatFishingToSeaTransition_State(
                            SubsequentState,
                            Sea.WorldMapScene.Io.NearestInteractableCell.SceneObject,
                            won: true));
                }

                break;

            case Hit.BadHit:
                Score -= Score > 0 ? 3 : 0;

                if (Score > 0)
                {
                    Audio.SFX.FadeAndStop();
                    Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing/ReelingQuickA"));

                    Reeling = false;
                    Spooling = true;
                }

                else
                {
                    SetState(
                        new BeatFishingToSeaTransition_State(
                            SubsequentState,
                            Sea.WorldMapScene.Io.NearestInteractableCell.SceneObject,
                            won: false));
                    //todo lose the fish //7/5/24 - is this still todo?
                }
                break;

            case Hit.Miss:
                Score -= Score > 0 ? 2 : 0;

                if (Score > 0)
                {
                    Audio.SFX.FadeAndStop();
                    Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing/ReelingQuickB"));

                    Reeling = false;
                    Spooling = true;
                }
                else
                {
                    SetState(
                        new BeatFishingToSeaTransition_State(
                            SubsequentState,
                            Sea.WorldMapScene.Io.NearestInteractableCell.SceneObject,
                            won: false));
                    //todo lose the fish //7/5/24 - is this still todo?
                }
                break;

            case Hit.Break:
            case Hit.BadHold:
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
                Analyzer.InputUpAction(); break;

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
                Analyzer.InputDownAction(); break;
        }
    }

    protected void SpaceBar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Analyzer.InputDownAction();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Analyzer.InputUpAction();
        }
    }

    bool Reeling, Spooling;
    void ReelingAnimations()
    {
        Spool.SetImageSize((float)(((float)Score + Difficulty) / (float)(Difficulty * 2)) * 5 * Vector3.one);
        if (Reeling)
        {
            Reel.Image.rectTransform.Rotate(40 * UnityEngine.Time.deltaTime * -Vector3.forward);
            Spool.Image.rectTransform.Rotate(40 * UnityEngine.Time.deltaTime * -Vector3.forward);
            Handle.Image.rectTransform.Rotate(40 * UnityEngine.Time.deltaTime * -Vector3.forward);
        }
        else if (Spooling)
        {
            Reel.Image.rectTransform.Rotate(100 * UnityEngine.Time.deltaTime * Vector3.forward);
            Spool.Image.rectTransform.Rotate(100 * UnityEngine.Time.deltaTime * Vector3.forward);
            Handle.Image.rectTransform.Rotate(100 * UnityEngine.Time.deltaTime * Vector3.forward);
        }
    }
}
