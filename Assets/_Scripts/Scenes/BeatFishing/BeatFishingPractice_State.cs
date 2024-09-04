using System;
using MusicTheory.Rhythms;
using Rhythm;
using Datum.BeatFishing;
using UnityEngine;
using SheetMusic;

public class BeatFishingPractice_State : State
{

    public BeatFishingPractice_State(State subsequentState, Datum.IItem item)
    {
        SubsequentState = subsequentState;
        Item = item;
    }

    readonly Datum.IItem Item;
    readonly State SubsequentState;
    RhythmSpecs Specs;
    Synchronizer Synchro;
    RhythmInputAnalyzer Analyzer;
    BatterieFeedback BatterieFeedback;
    MappedBeat[] BeatMap;
    SheetMusic.MusicSheet MusicSheet;
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
            SubDivisionTier = SubDivisionTier.BeatOnly,
            Tempo = 90
        };

        MusicSheet = new()
        {
            RhythmSpecs = new RhythmSpecs()
            {
                Time = new FourFour(),
                NumberOfMeasures = 1,
                SubDivisionTier = SubDivisionTier.BeatOnly,
                Tempo = 90
            },
            Measures = FishingForBeats.GetMeasures((IBeatFishingPractice)Item)
        };

        MusicSheet.GetNotes();
        MusicSheet.DrawRhythms(true);
        MusicSheet.BeatMap = MusicSheet.Notes.MapBeats(MusicSheet.RhythmSpecs.Time.GetQuantizement(), MusicSheet.RhythmSpecs.Tempo);

        Synchro = new(Specs.Time.GetQuantizement(), Specs.Tempo);

        BatterieFeedback = new();
        BeatMap = FishingForBeats.GetBeats((IBeatFishingPractice)Item, 16).MapBeats(Specs.Time.GetQuantizement(), Specs.Tempo);

        Analyzer = new(BatterieFeedback.CreateCard, HandleHit, BeatMap);
        Analyzer.SetUp();

        BatterieFeedback.UpdateLoop();
        MonoHelper.OnUpdate += SpaceBar;

        Audio.Ambience.VolumeLevelSetting = DataManager.Volume.GetScaledLevel(new Datum.SoundFX());
        Audio.BeatFishing.VolumeLevelSetting = DataManager.Volume.GetScaledLevel(new Datum.BatterieVolume());
        Audio.SFX.VolumeLevelSetting = DataManager.Volume.GetScaledLevel(new Datum.SoundFX()) * .75f;
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
        MonoHelper.OnUpdate += Analyzer.RhythmInputAnalyzerTick;
        MonoHelper.OnUpdate += ReelingAnimations;
        Synchro.TickEvent += BeatFishingPracticeTick;
        Synchro.KeepTime();
        Analyzer.Start();

        //hack this is debug
        // Synchro.BeatEvent += () => Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Ritmo/RimShot"));
    }

    protected override void DisengageState()
    {
        MusicSheet.SelfDestruct();
        GameObject.Destroy(Parent.gameObject);
        Audio.Ambience.FadeAndStop();
        Audio.BeatFishing.FadeAndStop();

        Synchro.Stop();
        MonoHelper.OnUpdate -= Analyzer.RhythmInputAnalyzerTick;
        MonoHelper.OnUpdate -= ReelingAnimations;
        Synchro.TickEvent -= BeatFishingPracticeTick;
        MonoHelper.OnUpdate -= SpaceBar;
    }

    void BeatFishingPracticeTick()
    {
        count++;
        if (Reeling && (count %= 4) == 0) Score++;
        if (!(BeatMap.Length > ++Counter))
        {
            SetState(SubsequentState);
        }
    }

    private void HandleHit(Hit hit)
    {
        switch (hit)
        {
            case Hit.Hit:
                Reeling = true;
                Spooling = false;
                if (!Audio.SFX.AudioSources[0].isPlaying)
                    Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing/ReelingSlowA"));

                if (Score > Difficulty)
                {
                    Synchro.TickEvent -= BeatFishingPracticeTick;
                    Audio.Ambience.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing/FishOutOfWaterA"));
                    Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/SFX/AlertHalfDim"));
                    //Todo Finish with crash cymbal?
                    DataManager.BeatFishingPracticeData.AdjustLevel(Item, 1);
                    SetState(new DialogStart_State(new EndFishPractice_Dialogue(SubsequentState, true)));
                }

                break;

            case Hit.BadHit:
                Score -= Score > 0 ? 4 : 0;

                if (Score > 0)
                {
                    Audio.SFX.FadeAndStop();
                    Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing/ReelingQuickA"));

                    Reeling = false;
                    Spooling = true;
                }

                else
                {
                    SetState(new DialogStart_State(new EndFishPractice_Dialogue(SubsequentState, false)));
                }
                break;

            case Hit.Miss:
                Score -= Score > 0 ? 3 : 0;

                if (Score > 0)
                {
                    Audio.SFX.FadeAndStop();
                    Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing/ReelingQuickB"));

                    Reeling = false;
                    Spooling = true;
                }
                else
                {
                    SetState(new DialogStart_State(new EndFishPractice_Dialogue(SubsequentState, false)));
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
            Reel.Image.rectTransform.Rotate(-30 * UnityEngine.Time.deltaTime * Vector3.forward);
            Spool.Image.rectTransform.Rotate(-30 * UnityEngine.Time.deltaTime * Vector3.forward);
            Handle.Image.rectTransform.Rotate(-50 * UnityEngine.Time.deltaTime * Vector3.forward);
        }
        else if (Spooling)
        {
            Reel.Image.rectTransform.Rotate(110 * UnityEngine.Time.deltaTime * Vector3.forward);
            Spool.Image.rectTransform.Rotate(110 * UnityEngine.Time.deltaTime * Vector3.forward);
            Handle.Image.rectTransform.Rotate(15 * UnityEngine.Time.deltaTime * Vector3.forward);
        }
    }
}
