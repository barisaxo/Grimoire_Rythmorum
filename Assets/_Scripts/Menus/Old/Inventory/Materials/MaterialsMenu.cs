// namespace OldMenus.Inventory.Materials
// {
//     public class MaterialsMenu : Menu<MaterialsData.DataItem, MaterialsMenu>
//     {
//         private BackButton _back;

//         public MaterialsMenu() : base(nameof(MaterialsMenu), new LeftScroll<MaterialsData.DataItem>()) { }

//         public Menu<MaterialsData.DataItem, MaterialsMenu> Initialize(MaterialsData data)
//         {
//             this.UpdateAllItems(data);
//             return base.Initialize();
//         }
//     }

//     public static class MaterialsMenuSystems
//     {
//         public static string DisplayData(this MaterialsData.DataItem item, MaterialsData data) =>
//                item.Name + ": " + data.GetItemCount(item);

//         public static void UpdateAllItems(this MaterialsMenu menu, MaterialsData data)
//         {
//             foreach (var item in menu.MenuItems)
//             {
//                 item.Card.SetTextString(item.Item.DisplayData(data));
//             }
//         }
//     }
// }