using System;
using MusicTheory.Rhythms;
using Batterie;
using SheetMusic;
using Datum.BeatFishing;
using UnityEngine;
using Rhythm;

//todo Visual Aid for fishing tutorial!
namespace BeatFishing
{
    public class BeatFishing_State : State
    {
        public BeatFishing_State(State subsequentState)
        {
            SubsequentState = subsequentState;
        }

        BeatFishingScene Scene;
        public readonly State SubsequentState;

        protected override void PrepareState(Action callback)
        {
            Scene = new BeatFishingScene(SubsequentState);
            (Scene as IScene).Initialize();
            Scene.Difficulty = (int)(60 * (1 / Datum.Manager.Io.Skill.GetBonusRatio(new Datum.PulsePerception())));
            Debug.Log(Scene.Difficulty);
            Scene.Specs = new RhythmSpecs()
            {
                Time = new FourFour(),
                NumberOfMeasures = 16,
                SubDivisionTier = SubDivisionTier.D1Only,
                Tempo = 90
            };

            IBeatFishingPractice item = (IBeatFishingPractice)BeatFishingPracticeEnum.ToItem(Enumeration.All<BeatFishingPracticeEnum>()[UnityEngine.Random.Range(0, 8)]);

            Scene.MusicSheet = new()
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

            Scene.MusicSheet.GetNotes();
            Scene.MusicSheet.DrawRhythms(false);
            Scene.MusicSheet.BeatMap = Scene.MusicSheet.Notes.MapBeats(Scene.MusicSheet.RhythmSpecs.Time.GetQuantizement(), Scene.MusicSheet.RhythmSpecs.Tempo);

            Scene.Synchro = new(Scene.Specs.Time.GetQuantizement(), Scene.Specs.Tempo);

            Scene.BatterieFeedback = new();

            Scene.BeatMap = FishingForBeats.GetBeats(item, 16).MapBeats(Scene.Specs.Time.GetQuantizement(), Scene.Specs.Tempo);
            // BeatMap = FishingForBeats.BeatsFromTimeSig(Specs.Time, Specs.NumberOfMeasures).MapBeats(Specs.Tempo);

            Scene.Analyzer = new(Scene.BatterieFeedback.CreateCard, HandleHit, Scene.BeatMap);
            Scene.Analyzer.SetUp();

            Scene.BatterieFeedback.UpdateLoop();
            MonoHelper.OnUpdate += SpaceBar;

            Audio.Ambience.VolumeLevelSetting = DataManager.Volume.GetScaledLevel(new Datum.SoundFX());
            Audio.BeatFishing.VolumeLevelSetting = DataManager.Volume.GetScaledLevel(new Datum.Chords());
            Audio.SFX.VolumeLevelSetting = DataManager.Volume.GetScaledLevel(new Datum.BatterieVolume());
            Audio.Ambience.Loop = false;
            Audio.Ambience.PlayClip(Resources.Load<AudioClip>("Audio/Fishing/SoftWaterSoundsA"));

            base.PrepareState(callback);
        }

        protected override void EngageState()
        {
            Audio.BeatFishing.PlayClip(Resources.Load<AudioClip>("Audio/Drums/Stax_Drums_90_1"));
            MonoHelper.OnUpdate += Scene.Analyzer.RhythmInputAnalyzerTick;
            MonoHelper.OnUpdate += Scene.ReelingAnimations;
            Scene.Synchro.TickEvent += BeatFishingTick;
            Scene.Synchro.KeepTime();
            Scene.Analyzer.Start();

            // this is for debug
            // Synchro.BeatEvent += () => Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Ritmo/RimShot"));
        }

        protected override void DisengageState()
        {
            (Scene as IScene).SelfDestruct();
            // GameObject.Destroy(Scene.Parent.gameObject);
            Audio.BeatFishing.FadeAndStop();
            Sea.WorldMapScene.Io.ClearCell();

            Scene.Synchro.Stop();
            MonoHelper.OnUpdate -= Scene.Analyzer.RhythmInputAnalyzerTick;
            MonoHelper.OnUpdate -= Scene.ReelingAnimations;
            Scene.Synchro.TickEvent -= BeatFishingTick;
            MonoHelper.OnUpdate -= SpaceBar;
        }

        void BeatFishingTick()
        {
            Scene.count++;
            if (Scene.Reeling && (Scene.count %= 4) == 0) Scene.Score++;
            if (!(Scene.BeatMap.Length > ++Scene.Counter))
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
                    Scene.Score += 2;
                    Scene.Reeling = true;
                    Scene.Spooling = false;
                    if (!Audio.SFX.AudioSources[0].isPlaying)
                        Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing/ReelingSlowA"));

                    if (Scene.Score > Scene.Difficulty)
                    {
                        Scene.Synchro.TickEvent -= BeatFishingTick;
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
                    Scene.Score -= Scene.Score > 0 ? 3 : 0;

                    if (Scene.Score > 0)
                    {
                        Audio.SFX.FadeAndStop();
                        Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing/ReelingQuickA"));

                        Scene.Reeling = false;
                        Scene.Spooling = true;
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
                    Scene.Score -= Scene.Score > 0 ? 2 : 0;

                    if (Scene.Score > 0)
                    {
                        Audio.SFX.FadeAndStop();
                        Audio.SFX.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing/ReelingQuickB"));

                        Scene.Reeling = false;
                        Scene.Spooling = true;
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
                    Scene.Analyzer.InputUpAction(); break;

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
                    Scene.Analyzer.InputDownAction(); break;
            }
        }

        protected void SpaceBar()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Scene.Analyzer.InputDownAction();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                Scene.Analyzer.InputUpAction();
            }
        }
    }
}