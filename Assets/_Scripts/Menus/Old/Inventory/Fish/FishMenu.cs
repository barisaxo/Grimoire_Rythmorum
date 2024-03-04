// namespace OldMenus.Inventory.Fish
// {
//     public class FishMenu : Menu<FishData.DataItem, FishMenu>
//     {
//         private BackButton _back;

//         public FishMenu() : base(nameof(FishMenu), new LeftScroll<FishData.DataItem>()) { }

//         public Menu<FishData.DataItem, FishMenu> Initialize(FishData data)
//         {
//             this.UpdateAllItems(data);
//             return base.Initialize();
//         }

//         public override IButtonInput North
//         {
//             get => base.North;
//             protected set => base.North = value;
//         }

//     }

//     public static class FishMenuSystems
//     {
//         public static string DisplayData(this FishData.DataItem item, FishData data) =>
//                item.Name + ": " + data.GetItemCount(item);

//         public static void UpdateAllItems(this FishMenu menu, FishData data)
//         {
//             foreach (var item in menu.MenuItems)
//             {
//                 item.Card.SetTextString(item.Item.DisplayData(data));
//             }
//         }
//     }
// }