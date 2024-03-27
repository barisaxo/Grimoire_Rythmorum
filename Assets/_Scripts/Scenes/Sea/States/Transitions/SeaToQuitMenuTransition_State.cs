// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Sea;

// public class SeaToQuitMenuTransition_State : State
// {
//     protected override void PrepareState(Action callback)
//     {
//         WorldMapScene.Io.HUD.Hud.GO.SetActive(false);
//         base.PrepareState(callback);
//     }

//     protected override void EngageState()
//     {
//         SetState(
//             new CameraPan_State(
//                 new QuitSeaMenu_State(
//                     new QuitMenuToSeaTransition_State()),
//                 new Vector3(
//                     -50,
//                     Cam.Io.Camera.transform.rotation.eulerAngles.y,
//                     Cam.Io.Camera.transform.rotation.eulerAngles.z),
//                 Cam.Io.Camera.transform.position,
//                 3));
//     }

//     protected override void DisengageState()
//     {
//         Audio.BGMusic.Pause();
//         Audio.Ambience.Pause();

//         WorldMapScene.Io.RockTheBoat.Rocking = false;
//         WorldMapScene.Io.Board.Swells.DisableSwells();
//     }
// }
