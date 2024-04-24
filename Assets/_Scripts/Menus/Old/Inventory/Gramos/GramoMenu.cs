// namespace OldMenus.Inventory.Gramo
// {
//     public class GramoMenu : Menu<GramosData.DataItem, GramoMenu>
//     {
//         private BackButton _back;

//         public GramoMenu() : base(nameof(GramoMenu), new LeftScroll<GramosData.DataItem>()) { }

//         public Menu<GramosData.DataItem, GramoMenu> Initialize(GramosData data)
//         {
//             this.UpdateAllItems(data);
//             return base.Initialize();
//         }
//     }

//     public static class GramoMenuSystems
//     {
//         public static string DisplayData(this GramosData.DataItem item, GramosData data) =>
//                item.Name + ": " + data.GetItemCount(item);

//         public static void UpdateAllItems(this GramoMenu menu, GramosData data)
//         {
//             foreach (var item in menu.MenuItems)
//             {
//                 item.Card.SetTextString(item.Item.DisplayData(data));
//             }
//         }
//     }
// }