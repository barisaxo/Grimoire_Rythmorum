// using System;
// using Sea;

// public class MenuToSeaTransition_State : State
// {
//     protected override void PrepareState(Action callback)
//     {
//         WorldMapScene.Io.RockTheBoat.Rocking = true;
//         WorldMapScene.Io.Board.Swells.EnableSwells();
//         base.PrepareState(callback);
//     }

//     protected override void EngageState()
//     {
//         Sea.WorldMapScene.Io.MiniMap.Show();
//         Audio.BGMusic.Resume();
//         Audio.Ambience.Resume();

//         UnityEngine.Debug.Log(nameof(MenuToSeaTransition_State));
//         SetState(
//             new CameraPan_State(
//                 new SeaScene_State(),
//                 Cam.StoredCamRot,
//                 Cam.StoredCamPos,
//                 3));
//     }


// }