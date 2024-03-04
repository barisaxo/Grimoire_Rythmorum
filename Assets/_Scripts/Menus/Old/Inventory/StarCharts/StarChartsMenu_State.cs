// using System;
// using OldMenus;
// using OldMenus.Inventory.StarCharts;
// using OldMenus.InventoryMenu;
// using UnityEngine;

// public class StarChartsMenu_State : State
// {
//     private readonly State ConsequentState;
//     private InventoryMenu Inventory;
//     private StarChartsMenu StarChartsInventory_Menu;
//     // readonly MainMenuScene MainMenuScene;

//     public StarChartsMenu_State(State consequentState)
//     {
//         ConsequentState = consequentState;
//     }
//     // public StarChartsMenu_State(State consequentState, InventoryMenu inventory)
//     // {
//     //     ConsequentState = consequentState;
//     //     Inventory = inventory;
//     // }
//     protected override void PrepareState(Action callback)
//     {
//         Inventory = Inventory == null ?
//             (InventoryMenu)new InventoryMenu().Initialize(InventoryMenu.InventoryItem.StarCharts) :
//             (InventoryMenu)Inventory.Initialize(InventoryMenu.InventoryItem.StarCharts);
//         StarChartsInventory_Menu = (StarChartsMenu)new StarChartsMenu().Initialize(Data.StarChartsData);
//         callback();
//     }

//     protected override void DisengageState()
//     {
//         //LoadSaveSystems.SaveCurrentGame();
//         Inventory.SelfDestruct();
//         StarChartsInventory_Menu.SelfDestruct();
//     }

//     protected override void ClickedOn(GameObject go)
//     {
//         if (go.transform.IsChildOf(Inventory.Back.Button.GO.transform))
//         {
//             SetStateDirectly(ConsequentState);
//             return;
//         }

//         for (var i = 0; i < Inventory.MenuItems.Length; i++)
//             if (go.transform.IsChildOf(Inventory.MenuItems[i].Card.GO.transform))
//             {
//                 if (i == Inventory.Selection) return;
//                 Inventory.Selection = Inventory.MenuItems[i];
//                 UpdateMenu();
//                 return;
//             }

//         // for (var i = 0; i < StarChartsInventory_Menu.MenuItems.Length; i++)
//         //     if (go.transform.IsChildOf(StarChartsInventory_Menu.MenuItems[i].Card.GO.transform))
//         //     {
//         //         if (StarChartsInventory_Menu.Selection.Item == i)
//         //         {
//         //             IncreaseItem(StarChartsInventory_Menu.MenuItems[i]);
//         //             return;
//         //         }

//         //         StarChartsInventory_Menu.Selection = StarChartsInventory_Menu.MenuItems[i];
//         //         StarChartsInventory_Menu.UpdateTextColors();
//         //         return;
//         //     }
//     }

//     protected override void DirectionPressed(Dir dir)
//     {
//         // if (dir == Dir.Reset) return;
//         StarChartsInventory_Menu.Selection = StarChartsInventory_Menu.Layout.ScrollMenuItems(dir, StarChartsInventory_Menu.Selection, StarChartsInventory_Menu.MenuItems);
//         StarChartsInventory_Menu.UpdateTextColors();
//     }

//     protected override void L1Pressed()
//     {
//         Inventory.Selection = Inventory.Layout.ScrollMenuItems(Dir.Left, Inventory.Selection, Inventory.MenuItems);
//         Inventory.UpdateTextColors();
//         UpdateMenu();
//     }

//     protected override void R1Pressed()
//     {
//         Inventory.Selection = Inventory.Layout.ScrollMenuItems(Dir.Right, Inventory.Selection, Inventory.MenuItems);
//         Inventory.UpdateTextColors();
//         UpdateMenu();
//     }

//     protected override void EastPressed()
//     {
//         // IncreaseItem(StarChartsInventory_Menu.MenuItems[StarChartsInventory_Menu.Selection]);
//     }

//     protected override void SouthPressed()
//     {
//         SetStateDirectly(ConsequentState);
//     }

//     private void IncreaseItem(MenuItem<StarChartsData.DataItem> item)
//     {
//         // Data.StarChartsData.IncreaseLevel(item.Item);
//         // item.Card.SetTextString(item.Item.DisplayData(Data.StarChartsData));
//     }

//     private void UpdateMenu()
//     {
//         if (Inventory.Selection == InventoryMenu.InventoryItem.Materials)
//             SetStateDirectly(new MaterialsMenu_State(ConsequentState));

//         else if (Inventory.Selection == InventoryMenu.InventoryItem.Fish)
//             SetStateDirectly(new FishMenu_State(ConsequentState));
//     }

//     protected override void LStickInput(Vector2 v2)
//     {
//         // MainMenuScene?.CatBoat.transform.Rotate(25 * Time.deltaTime * new Vector3(0, v2.x, 0), Space.World);
//         Cam.Io.SetObliqueness(v2);
//     }

//     protected override void RStickInput(Vector2 v2)
//     {
//         // if (MainMenuScene == null) return;
//         // MainMenuScene.CatBoat.transform.localScale = Vector3.one * 3 + (Vector3)v2 * 2;
//     }
// }