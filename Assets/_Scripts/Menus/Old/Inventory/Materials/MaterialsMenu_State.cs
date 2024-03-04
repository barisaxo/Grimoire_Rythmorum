// using System;
// using OldMenus;
// using OldMenus.Inventory.Materials;
// using OldMenus.InventoryMenu;
// using UnityEngine;

// public class MaterialsMenu_State : State
// {
//     private readonly State ConsequentState;
//     private InventoryMenu Inventory;
//     private MaterialsMenu MaterialsInventory_Menu;
//     // readonly MainMenuScene MainMenuScene;

//     public MaterialsMenu_State(State consequentState)
//     {
//         ConsequentState = consequentState;
//     }

//     // public MaterialsMenu_State(State consequentState, InventoryMenu inventory)
//     // {
//     //     Inventory = inventory;
//     //     ConsequentState = consequentState;
//     // }

//     protected override void PrepareState(Action callback)
//     {
//         Inventory = Inventory == null ?
//             (InventoryMenu)new InventoryMenu().Initialize(InventoryMenu.InventoryItem.Materials) :
//             (InventoryMenu)Inventory.Initialize(InventoryMenu.InventoryItem.Materials);
//         MaterialsInventory_Menu = (MaterialsMenu)new MaterialsMenu().Initialize(Data.MaterialsData);
//         callback();
//     }

//     protected override void DisengageState()
//     {
//         //LoadSaveSystems.SaveCurrentGame();
//         Inventory.SelfDestruct();
//         MaterialsInventory_Menu.SelfDestruct();
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

//         // for (var i = 0; i < MaterialsInventory_Menu.MenuItems.Length; i++)
//         //     if (go.transform.IsChildOf(MaterialsInventory_Menu.MenuItems[i].Card.GO.transform))
//         //     {
//         //         if (MaterialsInventory_Menu.Selection.Item == i)
//         //         {
//         //             IncreaseItem(MaterialsInventory_Menu.MenuItems[i]);
//         //             return;
//         //         }

//         //         MaterialsInventory_Menu.Selection = MaterialsInventory_Menu.MenuItems[i];
//         //         MaterialsInventory_Menu.UpdateTextColors();
//         //         return;
//         //     }
//     }

//     protected override void DirectionPressed(Dir dir)
//     {
//         // if (dir == Dir.Reset) return;
//         MaterialsInventory_Menu.Selection = MaterialsInventory_Menu.Layout.ScrollMenuItems(dir, MaterialsInventory_Menu.Selection, MaterialsInventory_Menu.MenuItems);
//         MaterialsInventory_Menu.UpdateTextColors();
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
//         // IncreaseItem(MaterialsInventory_Menu.MenuItems[MaterialsInventory_Menu.Selection]);
//     }

//     protected override void SouthPressed()
//     {
//         SetStateDirectly(ConsequentState);
//     }

//     private void IncreaseItem(MenuItem<MaterialsData.DataItem> item)
//     {
//         // Data.MaterialsData.IncreaseLevel(item.Item);
//         // item.Card.SetTextString(item.Item.DisplayData(Data.MaterialsData));
//     }

//     private void UpdateMenu()
//     {
//         if (Inventory.Selection == InventoryMenu.InventoryItem.Fish)
//             SetStateDirectly(new FishMenu_State(ConsequentState));

//         // else if (Inventory.Selection == InventoryMenu.InventoryItem.GamePlay)
//         // SetStateDirectly(new GamePlayMenu_State(ConsequentState, MainMenuScene));
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