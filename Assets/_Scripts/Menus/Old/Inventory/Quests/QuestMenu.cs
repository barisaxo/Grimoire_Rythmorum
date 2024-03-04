// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Data;
// using Data.Inventory;

// namespace Menus.Inventory
// {
//     public class QuestsMenu : IMenu
//     {

//         public QuestsMenu(QuestsData gd)
//         {
//             Data = gd;
//         }

//         TuningNote TuningNote;
//         public IData Data { get; }
//         MenuItem _selection;

//         public MenuItem Selection
//         {
//             get => _selection;
//             set
//             {
//                 if ((_selection = value) == QuestsData.DataItem.Tuning)
//                 {
//                     TuningNote ??= new((MusicTheory.KeyOf)Data.GetLevel(QuestsData.DataItem.Transpose));
//                 }
//                 else
//                 {
//                     TuningNote?.SelfDestruct();
//                     TuningNote = null;
//                 }
//             }
//         }
//         public MenuItem[] MenuItems { get; set; }
//         public IMenuLayout Layout { get; } = new AlignLeft();

//         public string DisplayData(DataEnum item)
//         {
//             return item.Name + ": " + Data.GetDisplayLevel(item);
//         }

//         public IInputHandler Input => new MenuInputHandler()
//         {
//             R1 = new ButtonInput(() => TuningNote?.SelfDestruct()),
//             L1 = new ButtonInput(() => TuningNote?.SelfDestruct()),
//             North = new ButtonInput(IncreaseItem),
//             West = new ButtonInput(DecreaseItem),
//             South = new ButtonInput(() => TuningNote?.SelfDestruct()),
//             Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, Selection, MenuItems)),
//             Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, Selection, MenuItems)),
//             // Right = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, Selection, MenuItems)),
//             // Left = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, Selection, MenuItems)),
//         };

//         private void IncreaseItem()
//         {
//             Data.IncreaseLevel(Selection.Item);
//             Selection.Card.SetTextString(DisplayData(Selection.Item));
//         }

//         private void DecreaseItem()
//         {
//             Data.DecreaseLevel(Selection.Item);
//             Selection.Card.SetTextString(DisplayData(Selection.Item));
//         }

//         public State SubsequentState => null;
//         public IMenuScene Scene => null;
//     }
// }