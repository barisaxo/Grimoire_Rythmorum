// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace OldMenus.InventoryMenu
// {
//     public class InventoryMenu : Menu<InventoryMenu.InventoryItem, InventoryMenu>
//     {
//         private BackButton _back;

//         public InventoryMenu() :
//             base(
//                 nameof(InventoryMenu),
//                 new ScrollingHeader<InventoryItem>())
//         { }

//         public BackButton Back => _back ??= new BackButton(Parent);

//         public override Menu<InventoryItem, InventoryMenu> Initialize()
//         {
//             _ = Back;
//             return base.Initialize();
//         }

//         public class InventoryItem : DataEnum
//         {
//             public static readonly InventoryItem Quests = new(0, "QUESTS");
//             public static readonly InventoryItem Materials = new(1, "MATS");
//             public static readonly InventoryItem Fish = new(2, "FISH");
//             public static readonly InventoryItem StarCharts = new(3, "CHARTS");
//             public static readonly InventoryItem Gramos = new(4, "GRAMOS");
//             public static readonly InventoryItem Ship = new(5, "SHIP");

//             public InventoryItem() : base(0, "") { }

//             private InventoryItem(int id, string name) : base(id, name) { }
//         }
//     }
// }