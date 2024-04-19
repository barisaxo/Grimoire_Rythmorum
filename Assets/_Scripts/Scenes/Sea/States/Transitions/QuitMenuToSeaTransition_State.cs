// using System;
// using Sea;

// public class QuitMenuToSeaTransition_State : State
// {
//     readonly State _seaSceneState;
//     public QuitMenuToSeaTransition_State(){}
//     protected override void PrepareState(Action callback)
//     {
//         WorldMapScene.Io.HUD.Hud.GO.SetActive(false);

//         Audio.BGMusic.Resume();
//         Audio.Ambience.Resume();

//         WorldMapScene.Io.RockTheBoat.Rocking = true;
//         WorldMapScene.Io.Board.Swells.EnableSwells();
//         base.PrepareState(callback);
//     }

//     protected override void EngageState()
//     {
//         SetState(
//             new CameraPan_State(
//                 new SeaScene_State(),
//                     pan: Cam.StoredCamRot,
//                     strafe: Cam.StoredCamPos,
//                     speed: 3));
//     }
// }