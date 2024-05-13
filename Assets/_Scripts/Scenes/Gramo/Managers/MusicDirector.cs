// using UnityEngine;

// namespace Gramophone
// {
//     static class MusicDirector
//     {
//         static Entities Gramo => Entities.Io;

//         [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
//         private static void AutoInit()
//         {
//             NewGramoPuzzleState.NewGramoEvent += Enable;
//         }

//         internal static void Enable()
//         {
//             MonoHelper.OnUpdate += OnUpdate;
//             VolumeSettings_Data.VolumeChangedEvent += UpdateVolumeLevel;

//             Gramo.nextEventTimeChords = AudioSettings.dspTime + .25f;
//             Gramo.nextEventTimeBass = AudioSettings.dspTime + .25f;
//             Gramo.nextEventTimeDrums = AudioSettings.dspTime + .25f;
//         }

//         internal static void Disable()
//         {
//             Mono_Helper.Update_Action -= OnUpdate;
//             VolumeSettings_Data.VolumeChangedEvent -= UpdateVolumeLevel;

//             foreach (AudioSource a in Gramo.ChordsASs) { a.Stop(); }
//             //foreach (AudioSource a in Gramo.BassASs) { a.Stop(); }//TODO
//             foreach (AudioSource a in Gramo.DrumASs) { a.Stop(); }
//         }


//         static void OnUpdate()
//         {
//             double time = AudioSettings.dspTime;
//             if (time + 1.0f > Gramo.nextEventTimeChords)
//             {
//                 Gramo.ChordsASs[Gramo.cuedChordsAS].clip = Gramo.Settings.ChordsClips[Gramo.cuedChordsClip];
//                 Gramo.ChordsASs[Gramo.cuedChordsAS].PlayScheduled(Gramo.nextEventTimeChords);

//                 Gramo.nextEventTimeChords += 60.0f / Gramo.Settings.BPM * Gramo.Settings.CountsPerClipChords;

//                 if (++Gramo.cuedChordsAS == Gramo.ChordsASs.Length) { Gramo.cuedChordsAS = 0; }
//                 if (++Gramo.cuedChordsClip == Gramo.Settings.ChordsClips.Length) { Gramo.cuedChordsClip = 0; }
//             }

//             if (time + 1.0f > Gramo.nextEventTimeBass)
//             {
//                 //TODO
//                 //bassASs[cuedBassAS].clip = Gramo.settings.chordsClips[cuedBassClip];
//                 //bassASs[cuedBassAS].PlayScheduled(nextEventTimeBass);

//                 //nextEventTimeBass += 60.0f / Gramo.settings.bpm * Gramo.settings.countsPerClipBass;

//                 //if (++cuedBassAS == bassASs.Length) { cuedBassAS = 0; }
//                 //if (++cuedBassClip == Gramo.settings.bassClips.Length) { cuedBassClip = 0; }
//             }

//             if (time + 1.0f > Gramo.nextEventTimeDrums)
//             {
//                 Gramo.DrumASs[Gramo.cuedDrumAS].clip = Gramo.Settings.DrumClips[Gramo.cuedDrumClip];
//                 Gramo.DrumASs[Gramo.cuedDrumAS].PlayScheduled(Gramo.nextEventTimeDrums);

//                 Gramo.nextEventTimeDrums += 60.0f / Gramo.Settings.BPM * Gramo.Settings.CountsPerClipDrums;

//                 if (++Gramo.cuedDrumAS == Gramo.DrumASs.Length) { Gramo.cuedDrumAS = 0; }
//                 if (++Gramo.cuedDrumClip == Gramo.Settings.DrumClips.Length) { Gramo.cuedDrumClip = 0; }
//             }
//         }

//         private static void UpdateVolumeLevel(VolumeItem vi, int lvl)
//         {
//             switch (vi)
//             {
//                 case VolumeItem.PuzzleChords: foreach (AudioSource a in Gramo.ChordsASs) { a.volume = lvl * .01f; } break;
//                 case VolumeItem.PuzzleDrums: foreach (AudioSource a in Gramo.DrumASs) { a.volume = lvl * .01f; } break;
//                 case VolumeItem.PuzzleBass: foreach (AudioSource a in Gramo.BassASs) { a.volume = lvl * .01f; } break;
//             }
//         }
//     }


// }