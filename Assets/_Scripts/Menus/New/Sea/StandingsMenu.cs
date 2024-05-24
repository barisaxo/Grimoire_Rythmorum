using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Two;

namespace Menus.Two
{
    public class StandingsMenu : IMenu
    {
        public StandingsMenu(StandingData standingData) { Data = standingData; }
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
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
        };

        public State ConsequentState => null;
        public IMenuScene Scene => null;
    }
}
