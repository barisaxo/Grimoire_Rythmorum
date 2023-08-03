
namespace Menus.OptionsMenu
{
    public static class VolumeMenuSystems
    {
        public static string DisplayData(this VolumeData.DataItem item, VolumeData data) =>
            item.Name + ": " + data.GetDisplayData(item) + "%";

        public static void UpdateAllItems(this VolumeMenu menu, VolumeData data)
        {
            foreach (var item in menu.MenuItems)
            {
                item.Card.SetTextString(item.Item.DisplayData(data));
            }
        }


    }
}