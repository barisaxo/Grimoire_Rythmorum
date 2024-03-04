// namespace OldMenus.Inventory.StarCharts
// {
//     public class StarChartsMenu : Menu<StarChartsData.DataItem, StarChartsMenu>
//     {
//         private BackButton _back;

//         public StarChartsMenu() : base(nameof(StarChartsMenu), new LeftScroll<StarChartsData.DataItem>()) { }

//         public Menu<StarChartsData.DataItem, StarChartsMenu> Initialize(StarChartsData data)
//         {
//             this.UpdateAllItems(data);
//             return base.Initialize();
//         }
//     }

//     public static class StarChartsMenuSystems
//     {
//         public static string DisplayData(this StarChartsData.DataItem item, StarChartsData data) =>
//                item.Name + ": " + data.GetItemCount(item);

//         public static void UpdateAllItems(this StarChartsMenu menu, StarChartsData data)
//         {
//             foreach (var item in menu.MenuItems)
//             {
//                 item.Card.SetTextString(item.Item.DisplayData(data));
//             }
//         }
//     }
// }