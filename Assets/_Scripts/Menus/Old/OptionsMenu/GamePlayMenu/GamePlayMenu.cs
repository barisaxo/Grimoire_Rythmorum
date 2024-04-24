// using OLD;
// namespace OldMenus.OptionsMenu
// {
//     public class GamePlayMenu : Menu<GameplayData.DataItem, GamePlayMenu>
//     {
//         public GamePlayMenu() :
//             base(
//                 nameof(GamePlayMenu),
//                 new AlignLeft<GameplayData.DataItem>())
//         { }

//         public Menu<GameplayData.DataItem, GamePlayMenu> Initialize(GameplayData data)
//         {
//             this.UpdateAllItems(data);
//             return base.Initialize();
//         }
//     }

//     public static class GamePlayMenuSystems
//     {
//         public static string DisplayData(this GameplayData.DataItem item, GameplayData data)
//         {
//             return item.Name + ": " + data.GetData(item);
//         }

//         public static void UpdateAllItems(this GamePlayMenu menu, GameplayData data)
//         {
//             foreach (var item in menu.MenuItems)
//             {
//                 item.Card.SetTextString(item.Item.DisplayData(data));
//             }
//         }
//     }
// }
