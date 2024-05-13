// using UnityEngine;
// // using InGameData;
// using System.Collections.Generic;

// namespace Gramophone
// {
//     internal static class Initializer
//     {
//         // static Entities Gramo => Entities.Io;

//         [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
//         private static void AutoInit()
//         {
//             //NewGramoPuzzleState.NewGramoEvent += Enable;
//         }

//         static void Enable()
//         {

//         }

//         internal static AudioSource[] InitializeChordAudioSources(GameObject parent)
//         {
//             AudioSource[] chordASs = new AudioSource[3];
//             for (int i = 0; i < 3; i++)
//             {
//                 GameObject child = new GameObject("ChordAS " + i);
//                 child.transform.parent = parent.transform;
//                 // chordASs[i] = child.AddComponent<AudioSource>();
//                 // chordASs[i].loop = false;
//                 // chordASs[i].playOnAwake = false;
//                 // chordASs[i].volume = VolumeSettings_Data.GetVolLVL(VolumeItem.PuzzleChords) * .01f;
//             }
//             return chordASs;
//         }

//         internal static AudioSource[] InitializeBassAudioSources()
//         {
//             AudioSource[] bassASs = new AudioSource[3];
//             for (int i = 0; i < 3; i++)
//             {
//                 GameObject child = new GameObject("BassAS " + i);
//                 child.transform.parent = Gramo.Gramophone.transform;
//                 // bassASs[i] = child.AddComponent<AudioSource>();
//                 // bassASs[i].loop = false;
//                 // bassASs[i].playOnAwake = false;
//                 // bassASs[i].volume = VolumeSettings_Data.GetVolLVL(VolumeItem.PuzzleBass) * .01f;
//             }
//             return bassASs;
//         }

//         internal static AudioSource[] InitializeDrumAudioSources(GameObject parent)
//         {
//             AudioSource[] DrumASs = new AudioSource[2];
//             for (int i = 0; i < 2; i++)
//             {
//                 GameObject child = new GameObject("DrumAS " + i);
//                 child.transform.parent = parent.transform;
//                 // DrumASs[i] = child.AddComponent<AudioSource>();
//                 // DrumASs[i].loop = false;
//                 // DrumASs[i].playOnAwake = false;
//                 // DrumASs[i].volume = .7f;
//                 // DrumASs[i].volume = VolumeSettings_Data.GetVolLVL(VolumeItem.PuzzleDrums) * .01f;
//             }
//             return DrumASs;
//         }

//         internal static GameObject SetUpGramophone(Entities gramo, GameObject parent)
//         {
//             gramo.dialMats = Assets.DialMats;

//             GameObject go = Object.Instantiate(
//                     Assets._gramoPuzzle, new Vector3(0, -.7f, 10),
//                     Quaternion.identity, parent.transform);

//             AssignLeftArrows();
//             AssignRightArrows();
//             AssignAnswerDials();
//             AssignOpenButton();
//             GetMeshes();

//             return go;
//             GameObject[] AssignLeftArrows() => gramo.leftArrows = new GameObject[4] {
//             (gramo.left1 = gramo.left1 != null ? gramo.left1 : GameObject.Find("Left1")),
//             (gramo.left2 = gramo.left2 != null ? gramo.left2 : GameObject.Find("Left2")),
//             (gramo.left3 = gramo.left3 != null ? gramo.left3 : GameObject.Find("Left3")),
//             (gramo.left4 = gramo.left4 != null ? gramo.left4 : GameObject.Find("Left4"))};

//             GameObject[] AssignRightArrows() => gramo.rightArrows = new GameObject[4]{
//             (gramo.right1 = gramo.right1 != null ? gramo.right1 : GameObject.Find("Right1")),
//             (gramo.right2 = gramo.right2 != null ? gramo.right2 : GameObject.Find("Right2")),
//             (gramo.right3 = gramo.right3 != null ? gramo.right3 : GameObject.Find("Right3")),
//             (gramo.right4 = gramo.right4 != null ? gramo.right4 : GameObject.Find("Right4"))};

//             GameObject[] AssignAnswerDials() => gramo.answerDials = new GameObject[4] {
//             (gramo.answer1 =gramo.answer1 != null ?gramo.answer1 : GameObject.Find("GramAnswer1")),
//             (gramo.answer2 =gramo.answer2 != null ?gramo.answer2 : GameObject.Find("GramAnswer2")),
//             (gramo.answer3 =gramo.answer3 != null ?gramo.answer3 : GameObject.Find("GramAnswer3")),
//             (gramo.answer4 =gramo.answer4 != null ?gramo.answer4 : GameObject.Find("GramAnswer4"))};

//             GameObject AssignOpenButton() => gramo.openButton = gramo.openButton != null ? gramo.openButton : GameObject.Find("OpenButton");

//             MeshRenderer[] GetMeshes()
//             {
//                 MeshRenderer[] ms = new MeshRenderer[gramo.answerDials.Length];
//                 for (int i = 0; i < ms.Length; i++)
//                 {
//                     ms[i] = gramo.answerDials[i].GetComponent<MeshRenderer>();
//                 }
//                 return gramo.dialMeshes = ms;
//             }
//         }

//         internal static Camera SetUpCam(GameObject parent)
//         {
//             var c = new GameObject(nameof(Camera)).AddComponent<Camera>();
//             c.transform.SetParent(parent.transform, false);
//             c.orthographic = false;
//             c.clearFlags = CameraClearFlags.Skybox;
//             c.transform.position = Vector3.back * 10;

//             var l = c.gameObject.AddComponent<Light>();
//             l.type = LightType.Directional;
//             l.color = new Color(.9f, .8f, .65f);
//             l.shadows = LightShadows.Soft;
//             return c;
//         }

//         internal static void AssignRandomMats(Entities gramo)
//         {
//             // int firstAnswer = gramo.Chords[0] switch
//             // {
//             //     // Chords.I => 0,
//             //     // Chords.II => 1,
//             //     // Chords.III => 0,
//             //     // Chords.IV => 1,
//             //     // Chords.V => 2,
//             //     // Chords.VI => 0,
//             //     // Chords.VII => 2,
//             //     //Function.Dominant => 2,
//             //     //Function.Recessive => 1,
//             //     //Function.Tonic => 0,
//             //     _ => 0,
//             // };
//             // gramo.dialMeshes[0].material = gramo.dialMats[0][firstAnswer];
//             // gramo.currentAnswer[0] = firstAnswer;

//             // for (int i = 1; i < gramo.dialMeshes.Length; i++)
//             // {
//             //     int r = UnityEngine.Random.Range(0, 3);
//             //     gramo.dialMeshes[i].material = gramo.dialMats[i][r];
//             //     gramo.currentAnswer[i] = r;
//             // }
//         }

//         internal static PuzzleAudioSettings? GeneratePuzzleSettings()
//         {
//             // List<AudioClip> chordACs = Assets.Gb90Rock;
//             // AudioClip[] chords = new AudioClip[Gramo.Chords.Count];
//             // for (int i = 0; i < chords.Length; i++) { chords[i] = chordACs[(int)Gramo.Chords[i]]; }

//             // List<AudioClip> drumACs = Assets.Drums;
//             // AudioClip[] drums = new AudioClip[drumACs.Count];
//             // for (int i = 0; i < drums.Length; i++) { drums[i] = drumACs[i]; }

//             // return new PuzzleAudioSettings(90, 4, 4, 16, chords, null, drums) { };
//             return null;
//         }
//     }

//     internal struct PuzzleAudioSettings
//     {
//         internal int BPM;
//         internal int CountsPerClipChords;
//         internal int CountsPerClipBass;
//         internal int CountsPerClipDrums;
//         internal AudioClip[] ChordsClips;
//         internal AudioClip[] BassClips;
//         internal AudioClip[] DrumClips;

//         internal PuzzleAudioSettings(
//              int bpm, int cpcc, int cpcb, int cpcd,
//              AudioClip[] c, AudioClip[] b, AudioClip[] d)
//         {
//             BPM = bpm;
//             CountsPerClipChords = cpcc;
//             CountsPerClipBass = cpcb;
//             CountsPerClipDrums = cpcd;
//             ChordsClips = c;
//             BassClips = b;
//             DrumClips = d;
//         }
//     }
// }