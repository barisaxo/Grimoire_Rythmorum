// using UnityEngine;
// using System.Collections.Generic;
// using System.Collections;

// namespace Gramophone
// {
//     sealed class Entities
//     {
//         #region INSTANCE

//         internal static Entities Io => Instance.Entities;

//         private Entities()
//         {
//             _ = Gramophone;
//             _ = Cam;
//             _ = GramPuzzle;
//             // _ = ChordsASs;
//             //_ = Gramo.BassASs;
//             // _ = DrumASs;
//             Initializer.AssignRandomMats(gramo: this);
//         }

//         private class Instance
//         {
//             static Entities entities; internal static Entities Entities => entities ??= new Entities();
//             static Instance() { }
//             internal static void Destruct() => entities = null;
//         }

//         internal void SelfDestruct()
//         {
//             Object.Destroy(gramophone);
//             Instance.Destruct();
//             Resources.UnloadUnusedAssets();
//         }

//         //internal void KeepGramoAlive() { keepAlive = true; }
//         //internal void DestroyGramo()
//         //{
//         //    keepAlive = false;
//         //    Mono_Helper.Io.StartCoroutine(Destruct());

//         //    IEnumerator Destruct()
//         //    {
//         //        yield return new WaitForEndOfFrame();
//         //        if (!keepAlive) { SelfDestruct(); }
//         //    }
//         //}

//         #endregion


//         #region FIELDS

//         private GameObject gramophone = null;
//         internal GameObject Gramophone => gramophone != null ? gramophone :
//             gramophone = new GameObject(nameof(gramophone));

//         private Camera cam = null;
//         internal Camera Cam => cam != null ? cam : cam = Initializer.SetUpCam(Gramophone);

//         internal bool keepAlive;

//         internal bool Dollying;
//         internal readonly float moveSpeed = 75f;
//         internal readonly List<bool> isSpinning = new List<bool>();
//         internal GameObject currDial = null;


//         // private List<Chords> chords = null;
//         // public List<Chords> Chords => chords ??= Musica.RandomChordalCadence();
//         //private List<MajChord> cadence = null;
//         //internal List<MajChord> Cadence => cadence ??= PuzzleMaker.NewCadence();
//         //private List<Functions> puzzleFunctions = null;
//         //internal List<Functions> PuzzleFunctions => puzzleFunctions ??= PuzzleMaker.NewPuzzleFunctions(Cadence);
//         internal int[] currentAnswer = new int[4];
//         internal Dial? currentDial = null;

//         private GameObject gramPuzzle = null;
//         internal GameObject GramPuzzle => gramPuzzle != null ? gramPuzzle :
//             gramPuzzle = Initializer.SetUpGramophone(this, Gramophone);
//         internal GameObject[] leftArrows = null;
//         internal GameObject[] rightArrows = null;
//         internal GameObject[] answerDials;
//         internal Material[][] dialMats = null;
//         internal MeshRenderer[] dialMeshes = null;

//         internal GameObject openButton = null;

//         internal GameObject answer1 = null;
//         internal GameObject answer2 = null;
//         internal GameObject answer3 = null;
//         internal GameObject answer4 = null;

//         internal GameObject right1 = null;
//         internal GameObject right2 = null;
//         internal GameObject right3 = null;
//         internal GameObject right4 = null;

//         internal GameObject left1 = null;
//         internal GameObject left2 = null;
//         internal GameObject left3 = null;
//         internal GameObject left4 = null;

//         private PuzzleAudioSettings? settings = null;
//         internal PuzzleAudioSettings Settings => (PuzzleAudioSettings)(settings ??= Initializer.GeneratePuzzleSettings());
//         internal double nextEventTimeChords;
//         internal double nextEventTimeBass;
//         internal double nextEventTimeDrums;
//         internal int cuedChordsAS = 0;
//         internal int cuedBassAS = 0;
//         internal int cuedDrumAS = 0;
//         internal int cuedChordsClip = 0;
//         internal int cuedBassClip = 0;
//         internal int cuedDrumClip = 0;
//         private AudioSource[] chordsASs = null;
//         internal AudioSource[] ChordsASs => chordsASs ??= Initializer.InitializeChordAudioSources(Gramophone);
//         private AudioSource[] bassASs = null;
//         internal AudioSource[] BassASs => bassASs ??= Initializer.InitializeBassAudioSources();
//         private AudioSource[] drumASs = null;
//         internal AudioSource[] DrumASs => drumASs ??= Initializer.InitializeDrumAudioSources(Gramophone);

//         #endregion

//     }
// }