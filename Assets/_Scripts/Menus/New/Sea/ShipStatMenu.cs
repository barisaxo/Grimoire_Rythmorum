using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;


namespace Menus
{
    public class ShipStatMenu : IMenu
    {
        public ShipStatMenu(ActiveShipData data) { Data = data; }

        public IData Data { get; }
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();

        public string GetDescription { get => Selection.Item.Description; }
        public string DisplayData(IItem item)
        {
            return item.Name + ": " + Data.GetDisplayLevel(item);
        }

        public IInputHandler Input => new MenuInputHandler()
        {
            // North = new ButtonInput(IncreaseItem),
            // West = new ButtonInput(DecreaseItem),
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
            // Right = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, this)),
            // Left = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, this)),
        };

        private void IncreaseItem()
        {
            Data.AdjustLevel(Selection.Item, 1);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

        private void DecreaseItem()
        {
            Data.AdjustLevel(Selection.Item, -1);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

        public State ConsequentState => null;
        public IMenuScene Scene => null;
    }
}