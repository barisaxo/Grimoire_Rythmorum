using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Two;
namespace Menus.Two
{
    public class GramophoneMenu : IMenu
    {
        public GramophoneMenu(GramophoneInventoryData data, State subsequentState)
        {
            Data = data;
            SubsequentState = subsequentState;
        }

        readonly State SubsequentState;
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
            North = new ButtonInput(IncreaseItem),
            West = new ButtonInput(DecreaseItem),
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
        };

        private void IncreaseItem()
        {
            // Data.AdjustLevel(Selection.Item, 1);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

        private void DecreaseItem()
        {
            // Data.AdjustLevel(Selection.Item, -1);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

        public State ConsequentState => new NewGramoPuzzle_State(SubsequentState, true);
        public IMenuScene Scene => null;
    }
}