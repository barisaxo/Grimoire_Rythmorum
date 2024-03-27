using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Data.Inventory;

namespace Menus.Inventory
{
    public class ShipMenu : IMenu
    {
        public ShipMenu(ShipData data) { Data = data; }

        public IData Data { get; }
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();

        public string DisplayData(DataEnum item)
        {
            return item.Name + ": " + Data.GetDisplayLevel(item);
        }

        public IInputHandler Input => new MenuInputHandler()
        {
            North = new ButtonInput(IncreaseItem),
            West = new ButtonInput(DecreaseItem),
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, Selection, MenuItems)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, Selection, MenuItems)),
            // Right = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, Selection, MenuItems)),
            // Left = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, Selection, MenuItems)),
        };

        private void IncreaseItem()
        {
            Data.IncreaseLevel(Selection.Item);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

        private void DecreaseItem()
        {
            Data.DecreaseLevel(Selection.Item);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

        public State ConsequentState => null;
        public IMenuScene Scene => null;
    }
}