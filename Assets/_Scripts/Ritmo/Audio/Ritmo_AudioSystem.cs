using UnityEngine;
using System;
using System.Collections.Generic;
using Audio;
using MusicTheory.Rhythms;
using System.Collections;

namespace Ritmo
{
    public class Ritmo_AudioSystem : AudioSystem
    {
        public Ritmo_AudioSystem(VolumeData volumeData) : base(2, nameof(Ritmo_AudioSystem))
        {
            VolumeData = volumeData;
        }

        //CountOffFeedback CountOffFeedback = new();

        readonly VolumeData VolumeData;
        private double click;
        public event Action Finished;
        // public event Action<(double timeEvent, int index)> TimeEventCallBack;
        public event Action StartCallBack;

        public override void ResetCues()
        {
            click = AudioSettings.dspTime + .5D;
            base.ResetCues();
        }

        public void CountOff(List<MappedBeat> countOff, float bpm, Action CallBack)
        {
            ResetCues();
            //AudioSources[1].clip = Assets.SnareRoll;
            //AudioSources[0].clip = Assets.RimShot;
            AudioSources[0].volume = VolumeData.GetScaledLevel(VolumeData.DataItem.Click);
            //CountOffFeedback.Running = true;
            //CountOffFeedback.UpdateLoop();

            CountOffUpdateLoop(0, bpm, countOff).StartCoroutine();

            IEnumerator CountOffUpdateLoop(int countIndex, float bpm, List<MappedBeat> countOff)
            {
                yield return null;

                if (AudioSettings.dspTime >= NextEventTime)
                {
                    switch (countOff[countIndex].NoteFunction)
                    {
                        case NoteFunction.Attack:
                            //CountOffFeedback.ReadCountOff(countIndex);
                            AudioSources[0].Stop();
                            AudioSources[0].Play();
                            break;

                        case NoteFunction.Hold: break;

                        case NoteFunction.Rest:
                            AudioSources[0].Stop();
                            break;
                    }

                    NextEventTime += countOff[countIndex].TimeInterval;
                    countIndex++;
                }

                if (countIndex < countOff.Count)
                {
                    CountOffUpdateLoop(countIndex, bpm, countOff);
                }
                else
                {
                    foreach (AudioSource a in AudioSources) a.Stop();
                    CallBack.Invoke();
                }
            }
        }

        public void PlayBeat(List<MappedBeat> beatMap, float bpm)
        {
            NextEventTime = click = AudioSettings.dspTime;
            AudioSources[1].volume = 0;
            BeatMapFeedBack bmf = new(bpm);
            StartCallBack?.Invoke();
            bmf.StartScrolling();
            UpdateLoop(0, bpm, beatMap).StartCoroutine();

            IEnumerator UpdateLoop(int beatIndex, float bpm, List<MappedBeat> beatMap)
            {
                if (dspTime == AudioSettings.dspTime) { realTime += UnityEngine.Time.unscaledDeltaTime; }
                else { realTime = dspTime = AudioSettings.dspTime; }

                if (realTime >= NextEventTime)
                {
                    switch (beatMap[beatIndex].NoteFunction)
                    {
                        case NoteFunction.Attack:
                            AudioSources[1].Stop();
                            AudioSources[1].Play();
                            Debug.Log("Playing " + beatIndex + ", " + realTime + ", " + NextEventTime);
                            break;

                        case NoteFunction.Hold:
                            break;

                        case NoteFunction.Rest:
                            AudioSources[1].Stop();
                            break;
                    }

                    NextEventTime += beatMap[beatIndex].TimeInterval;

                    beatIndex++;
                }

                if (realTime > click)
                {
                    AudioSources[0].Play();
                    click += 60D / bpm;
                }
                yield return null;

                if (beatIndex < beatMap.Count)
                {
                    UpdateLoop(beatIndex, bpm, beatMap);
                }
                else
                {
                    foreach (AudioSource a in AudioSources) a.Stop();
                    Finished?.Invoke();
                    bmf.SelfDestruct();
                }
            }
        }

        public void Miss()
        {
            AudioSources[1].volume = 0;
        }
        public void Hit()
        {
            AudioSources[1].volume = VolumeData.GetScaledLevel(VolumeData.DataItem.Battery);
        }

        public override void Stop()
        {
            base.Stop();
        }


    }
}

/*
 *
            BeatMapFeedBack beatMapFeedBack = new();
            Debug.Log(beatMap.Count.ToString());
            
                               Debug.Log(beatIndex);
                    beatMapFeedBack.BeatScroller.transform.position = 
                        RhythmScriberSystems.NotePosition(
                            RhythmScriberSystems.BeatIndexToNoteLocation(GetBeatIndex(beatIndex-1, beatMap.Count))
                            );
                    Debug.Log(beatMapFeedBack.BeatScroller.transform.position.ToString());
                    
 *        int GetBeatIndex(int beatIndex, int beatCount)
        {
            int subDivision = beatCount switch
            {
                _ when beatCount < 46 => 2,
                _ => 1
            };
            return beatIndex * subDivision;
        }
 *
 *
 * 
                    beatMapFeedBack.SelfDestruct();
 */
