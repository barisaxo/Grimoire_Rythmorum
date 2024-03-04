// using System;
// using OldMenus;
// using OldMenus.MainMenu;
// using UnityEngine;

// public class LoadGameSelectSlot_State : State
// {
//     private SaveSlotMenu SaveSlotMenu;
//     readonly MainMenuScene MainMenuScene;

//     public LoadGameSelectSlot_State(MainMenuScene scene) { MainMenuScene = scene; }

//     protected override void PrepareState(Action callback)
//     {
//         SaveSlotMenu = (SaveSlotMenu)new SaveSlotMenu().Initialize();//Data.GamePlay
//         base.PrepareState(callback);
//     }

//     protected override void DisengageState()
//     {
//         SaveSlotMenu.SelfDestruct();
//     }

//     protected override void ClickedOn(GameObject go)
//     {
//         if (go.transform.IsChildOf(SaveSlotMenu.Back.Button.GO.transform))
//         {
//             SetStateDirectly(new MainMenu_State(MainMenuScene));
//             return;
//         }

//         for (var i = 0; i < SaveSlotMenu.MenuItems.Length; i++)
//         {
//             if (!go.transform.IsChildOf(SaveSlotMenu.MenuItems[i].Card.GO.transform)) continue;
//             SaveSlotMenu.Selection = SaveSlotMenu.MenuItems[i];
//             SaveSlotMenu.UpdateTextColors();
//             EastPressed();
//             return;
//         }
//     }

//     protected override void DirectionPressed(Dir dir)
//     {
//         // if (dir == Dir.Reset) return;
//         SaveSlotMenu.Selection = SaveSlotMenu.Selection = SaveSlotMenu.Layout.ScrollMenuItems(dir, SaveSlotMenu.Selection, SaveSlotMenu.MenuItems);
//         SaveSlotMenu.UpdateTextColors();
//     }

//     protected override void SouthPressed()
//     {
//         SetStateDirectly(new MainMenu_State(MainMenuScene));
//     }

//     protected override void LStickInput(Vector2 v2)
//     {
//         MainMenuScene.CatBoat.transform.Rotate(25 * Time.deltaTime * new Vector3(0, v2.x, 0), Space.World);
//         Cam.Io.SetObliqueness(v2);
//     }

//     protected override void RStickInput(Vector2 v2)
//     {
//         MainMenuScene.CatBoat.transform.localScale = Vector3.one * 3 + (Vector3)v2 * 2;
//     }
// }