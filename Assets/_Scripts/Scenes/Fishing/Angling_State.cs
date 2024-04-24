using System;
using MusicTheory.Rhythms;
using Batterie;

using UnityEngine;

public class Angling_State : State
{

    public Angling_State(State subsequentState)
    {
        SubsequentState = subsequentState;
    }

    readonly State SubsequentState;
    RhythmSpecs Specs;
    Synchronizer Synchro;
    BatterieInputAnalyzer Analyzer;
    BatterieFeedback BatterieFeedback;
    MappedBeat[] BeatMap;

    Card Reel;
    Card Handle;
    Card Spool;

    Card FishingReel;

    Transform Parent;

    private int Score = 20;
    private int Difficulty;
    public int Counter = 1;

    protected override void PrepareState(Action callback)
    {
        Difficulty = (int)(60 * (1 / DataManager.SkillsData.GetBonusRatio(Data.Player.SkillsData.DataItem.PulsePerception)));
        Debug.Log(Difficulty);
        SetUpFishingReel();
        Specs = new RhythmSpecs()
        {
            Time = new FourFour(),
            NumberOfMeasures = 16,
            SubDivisionTier = SubDivisionTier.D1Only,
            Tempo = 90
        };

        Synchro = new(Specs.Time.GetQuantizement(), Specs.Tempo);

        BatterieFeedback = new();
        BeatMap = FishingForBeats.BeatsFromTimeSig(Specs.Time, Specs.NumberOfMeasures).MapBeats(Specs.Tempo);

        Analyzer = new(BatterieFeedback.CreateCard, HandleHit, 5, BeatMap);
        Analyzer.SetUp();

        BatterieFeedback.UpdateLoop();
        MonoHelper.OnUpdate += SpaceBar;

        //TODO get volume data
        Audio.Ambience.VolumeLevelSetting = .8f;
        Audio.BGMusic.VolumeLevelSetting = .8f;
        Audio.SFX.VolumeLevelSetting = .8f;
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
        Audio.BGMusic.PlayClip(Resources.Load<AudioClip>("Audio/Drums/Stax_Drums_90_1"));
        MonoHelper.OnUpdate += Analyzer.Tick;
        MonoHelper.OnUpdate += ReelingAnimations;
        Synchro.TickEvent += Tick;
        Synchro.KeepTime();
        Analyzer.Start();

        //todo this is debug
        // Synchro.BeatEvent += () => Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Ritmo/RimShot"));
    }

    protected override void DisengageState()
    {
        GameObject.Destroy(Parent.gameObject);
        // Audio.BGMusic.Stop();
        // Audio.SFX.Stop();
        // Audio.Ambience.Stop();
        Sea.WorldMapScene.Io.ClearCell();

        Synchro.Stop();
        MonoHelper.OnUpdate -= Analyzer.Tick;
        MonoHelper.OnUpdate -= ReelingAnimations;
        Synchro.TickEvent -= Tick;
        MonoHelper.OnUpdate -= SpaceBar;
    }

    void Tick()
    {
        if (!(BeatMap.Length > ++Counter))
        {
            SetState(
                new AnglingToSeaTransition_State(
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
                    Synchro.TickEvent -= Tick;
                    Audio.Ambience.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing/FishOutOfWaterA"));
                    Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/SFX/AlertHalfDim"));
                    //Todo Finish with crash cymbal?
                    SetState(
                        new AnglingToSeaTransition_State(
                            SubsequentState,
                            Sea.WorldMapScene.Io.NearestInteractableCell.SceneObject,
                            won: true));
                }

                break;

            case Hit.BadHit:
                Score -= Score > 0 ? 3 : 0;

                if (Score > 0)
                {
                    Audio.SFX.Stop();
                    Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing/ReelingQuickA"));

                    Reeling = false;
                    Spooling = true;
                }

                else
                {
                    SetState(
                        new AnglingToSeaTransition_State(
                            SubsequentState,
                            Sea.WorldMapScene.Io.NearestInteractableCell.SceneObject,
                            won: false));
                    //todo lose the fish
                }
                break;

            case Hit.Miss:
                Score -= Score > 0 ? 2 : 0;

                if (Score > 0)
                {
                    Audio.SFX.Stop();
                    Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing/ReelingQuickB"));

                    Reeling = false;
                    Spooling = true;
                }
                else
                {
                    SetState(
                        new AnglingToSeaTransition_State(
                            SubsequentState,
                            Sea.WorldMapScene.Io.NearestInteractableCell.SceneObject,
                            won: false));
                    //todo lose the fish
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
