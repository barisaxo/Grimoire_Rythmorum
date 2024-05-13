
// using Data;
// using Data.Player;
// using Menus.One;


// namespace Menus.Inventory
// {
//     public class PlayerMenu : IMenu
//     {
//         public PlayerMenu(PlayerData data) { Data = data; }

//         public IData Data { get; }
//         public MenuItem Selection { get; set; }
//         public MenuItem[] MenuItems { get; set; }
//         public Card Description { get; set; }
//         public IMenuLayout Layout { get; } = new LeftScroll();

//         public string GetDescription { get => Selection.Item.Description; }
//         public string DisplayData(DataEnum item)
//         {
//             return item.Name + ": " + Data.GetLevel(item) + " : " + Data.GetDisplayLevel(item);
//         }

//         public IInputHandler Input => new MenuInputHandler()
//         {
//             // North = new ButtonInput(IncreaseItem),
//             // West = new ButtonInput(DecreaseItem),
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