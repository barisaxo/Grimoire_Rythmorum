// using System;
// using OldMenus;
// using OldMenus.MainMenu;
// using OldMenus.HowToPlayMenu;
// using UnityEngine;

// public class HowToPlayMenu_State : State
// {
//     private HowToPlayMenu HowToPlay;
//     private MainMenuScene MainMenuScene;
//     public HowToPlayMenu_State(MainMenuScene scene) { MainMenuScene = scene; }
//     public HowToPlayMenu_State() { }

//     protected override void PrepareState(Action callback)
//     {
//         MainMenuScene ??= new();
//         HowToPlay = (HowToPlayMenu)new HowToPlayMenu().Initialize();
//         callback();
//     }

//     protected override void DisengageState()
//     {
//         HowToPlay.SelfDestruct();
//     }

//     protected override void ClickedOn(GameObject go)
//     {
//         if (go.transform.IsChildOf(HowToPlay.Back.Button.GO.transform))
//         {
//             SouthPressed();
//             return;
//         }

//         for (var i = 0; i < HowToPlay.MenuItems.Length; i++)
//         {
//             if (!go.transform.IsChildOf(HowToPlay.MenuItems[i].Card.GO.transform)) continue;
//             HowToPlay.Selection = HowToPlay.MenuItems[i];
//             EastPressed();
//             return;
//         }
//     }

//     protected override void DirectionPressed(Dir dir)
//     {
//         // if (dir == Dir.Reset) return;
//         HowToPlay.Selection = HowToPlay.Layout.ScrollMenuItems(dir, HowToPlay.Selection, HowToPlay.MenuItems);
//         HowToPlay.UpdateTextColors();
//     }

//     protected override void EastPressed()
//     {
//         if (HowToPlay.Selection == HowToPlayMenu.HowToPlayItem.Muscopa)
//         {
//             MainMenuScene.SelfDestruct();
//             SetStateDirectly(new DialogStart_State(new MuscopaTutorial_Dialogue()));
//             return;
//         }

//         if (HowToPlay.Selection == HowToPlayMenu.HowToPlayItem.Batterie)
//         {
//             MainMenuScene.SelfDestruct();
//             SetStateDirectly(new DialogStart_State(new BatterieTutorial_Dialogue()));
//         }
//     }

//     protected override void SouthPressed()
//     {
//         // SetStateDirectly(new MainMenu_State(MainMenuScene));

//         throw new System.NotImplementedException();
//     }

//     protected override void LStickInput(Vector2 v2)
//     {
//         MainMenuScene?.CatBoat.transform.Rotate(25 * Time.deltaTime * new Vector3(0, v2.x, 0), Space.World);
//         Cam.Io.SetObliqueness(v2);
//     }

//     protected override void RStickInput(Vector2 v2)
//     {
//         if (MainMenuScene == null) return;
//         MainMenuScene.CatBoat.transform.localScale = Vector3.one * 3 + (Vector3)v2 * 2;
//     }
// }