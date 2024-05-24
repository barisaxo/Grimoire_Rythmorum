using System;
using Data.Two;

namespace Menus.Two
{
    public class SeaMenu : IMenu, IHeaderMenu
    {
        public SeaMenu(Manager dm, State subsequentState)
        {
            ConsequentState = subsequentState;
            Manager = dm;
            Data = new SeaMenuData();
            CurrentSub = SubMenus[0];
            UnityEngine.Debug.Log(subsequentState);
        }

        readonly Manager Manager;
        public IData Data { get; }
        public Card Description { get; set; }

        private MenuItem _selection;
        public MenuItem Selection
        {
            get => _selection;
            set
            {
                _selection = value;
                CurrentSub = SubMenus[_selection.Item.ID];
            }
        }

        public string GetDescription => null;
        public MenuItem[] MenuItems { get; set; }
        public IMenu CurrentSub { get; private set; }
        private IMenu[] _subMenus;
        public IMenu[] SubMenus => _subMenus ??= new IMenu[] {
            new QuestsMenu(Manager.Quests),
            new InventoryMenu(Manager.Inventory, ConsequentState, this),
            // new StarChartsMenu(Manager.StarChart, Manager.Quests, ConsequentState, this),
            // new GramophoneMenu(Manager.Gramophones),
            new StandingsMenu(Manager.StandingData),
            new LighthousesMenu(Manager.Lighthouse),
            new ShipStatMenu(Manager.PlayerShip),
            new PlayerStatsMenu(Manager.Player),
        };

        public IMenuLayout Layout { get; } = new ScrollingHeader();

        public IInputHandler Input => new MenuInputHandler()
        {
            R1 = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, this)),
            L1 = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, this)),
        };

        public string DisplayData(IItem item)
        {
            return item is not null ? item.Name : "";
        }

        public State ConsequentState { get; }
        public IMenuScene Scene => null;
    }

}
