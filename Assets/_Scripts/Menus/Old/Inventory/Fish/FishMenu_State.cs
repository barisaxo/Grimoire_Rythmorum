// using System;
// using OldMenus;
// using OldMenus.Inventory.Fish;
// using OldMenus.InventoryMenu;
// using UnityEngine;

// public class FishMenu_State : State
// {
//     private readonly State ConsequentState;
//     private InventoryMenu Inventory;
//     private FishMenu FishMenu;
//     // readonly MainMenuScene MainMenuScene;

//     public FishMenu_State(State consequentState)
//     {
//         ConsequentState = consequentState;
//     }

//     // public FishMenu_State(State consequentState, InventoryMenu inventoryMenu)
//     // {
//     //     ConsequentState = consequentState;
//     //     Inventory = inventoryMenu;
//     // }

//     protected override void PrepareState(Action callback)
//     {
//         Inventory = Inventory == null ?
//             (InventoryMenu)new InventoryMenu().Initialize(InventoryMenu.InventoryItem.Fish) :
//             (InventoryMenu)Inventory.Initialize(InventoryMenu.InventoryItem.Fish);

//         FishMenu = (FishMenu)new FishMenu().Initialize(Data.FishData);
//         callback();
//     }

//     protected override void DisengageState()
//     {
//         //LoadSaveSystems.SaveCurrentGame();
//         Inventory.SelfDestruct();
//         FishMenu.SelfDestruct();
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

//         // for (var i = 0; i < FishInventory_Menu.MenuItems.Length; i++)
//         //     if (go.transform.IsChildOf(FishInventory_Menu.MenuItems[i].Card.GO.transform))
//         //     {
//         //         if (FishInventory_Menu.Selection.Item == i)
//         //         {
//         //             IncreaseItem(FishInventory_Menu.MenuItems[i]);
//         //             return;
//         //         }

//         //         FishInventory_Menu.Selection = FishInventory_Menu.MenuItems[i];
//         //         FishInventory_Menu.UpdateTextColors();
//         //         return;
//         //     }
//     }

//     protected override void DirectionPressed(Dir dir)
//     {
//         // if (dir == Dir.Reset) return;
//         FishMenu.Selection = FishMenu.Layout.ScrollMenuItems(dir, FishMenu.Selection, FishMenu.MenuItems);
//         FishMenu.UpdateTextColors();
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
//         // IncreaseItem(FishInventory_Menu.MenuItems[FishInventory_Menu.Selection]);
//     }

//     protected override void SouthPressed()
//     {
//         SetStateDirectly(ConsequentState);
//     }

//     private void IncreaseItem(MenuItem<FishData.DataItem> item)
//     {
//         // Data.FishData.IncreaseLevel(item.Item);
//         // item.Card.SetTextString(item.Item.DisplayData(Data.FishData));
//     }

//     private void UpdateMenu()
//     {
//         if (Inventory.Selection == InventoryMenu.InventoryItem.StarCharts)
//             SetStateDirectly(new StarChartsMenu_State(ConsequentState));

//         else if (Inventory.Selection == InventoryMenu.InventoryItem.Materials)
//             SetStateDirectly(new MaterialsMenu_State(ConsequentState));
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