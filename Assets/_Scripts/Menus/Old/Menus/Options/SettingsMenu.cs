// using System;
// using Data;
// using Data.Options;

// namespace Menus.One
// {
//     public class SettingsMenu : IMenu
//     {
//         public SettingsMenu(SettingsData gd)
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
//                 if ((_selection = value) == SettingsData.DataItem.Tuning)
//                 {
//                     TuningNote ??= new((MusicTheory.KeyOf)Data.GetLevel(SettingsData.DataItem.Transpose));
//                 }
//                 else
//                 {
//                     TuningNote?.SelfDestruct();
//                     TuningNote = null;
//                 }
//             }
//         }
//         public MenuItem[] MenuItems { get; set; }
//         public Card Description { get; set; }
//         public IMenuLayout Layout { get; } = new AlignLeft();

//         public string GetDescription { get => Selection.Item.Description; }
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
//             Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
//             Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
//             // Right = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, this)),
//             // Left = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, this)),
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

//         public State ConsequentState => null;
//         public IMenuScene Scene => null;
//     }
// }