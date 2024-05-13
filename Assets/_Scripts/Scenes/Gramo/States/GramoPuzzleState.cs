// // using InGameData;
// using Gramophone;

// public class GramoPuzzle_State : State
// {
//     protected override void EngageState()
//     {
//     }

//     protected override void DisengageState()
//     {
//         // Entities.Io.SelfDestruct();
//     }

//     protected override void DirectionPressed(Dir dir)
//     {
//         if (dir == Dir.Reset || Entities.Io.Dollying) { return; }
//         Animator.Interact(dir);
//     }

//     protected override void EastPressed()
//     {
//         if (Entities.Io.Dollying) { return; }

//         if (AnswerChecker.CorrectAnswer())
//         {
//             // if (Character_Data.GetArea() == Area.Cave)
//             // {
//             //     Rewards.PushFinishResults(true, Area.Cave, 1);
//             //     StateMachine.FadeToState(new Cave.CaveExplorationState());
//             // }
//             // else if (Character_Data.GetArea() == Area.Practice)
//             // {
//             //     StateMachine.FadeToState(new Aether.AetherExploreState());
//             // }
//             // MusicDirector.Disable();
//         }
//     }

// }
