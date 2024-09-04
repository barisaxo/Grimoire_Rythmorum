using System;
using Datum;
using UnityEngine;

namespace Menus
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
                CurrentSub = SubMenus[_selection.Item.Id];
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
            new StandingsMenu(Manager.Standings),
            new LighthousesMenu(Manager.Lighthouse),
            new ShipStatMenu(Manager.ActiveShip),
            new PlayerStatsMenu(Manager.Player),
        };

        public IMenuLayout Layout { get; } = new ScrollingHeader();

        public IInputHandler Input => new MenuInputHandler()
        {
            R1 = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, this)),
            L1 = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, this)),
            South = new ButtonInput(() => { })
        };

        public string DisplayData(IItem item)
        {
            return item is not null ? item.Name : "";
        }

        public State ConsequentState { get; }

        private IMenuScene _scene;
        public IMenuScene Scene => _scene ??= new MenuScene();

        public class MenuScene : IMenuScene
        {
            public string Name { get; } = nameof(MenuScene);
            public void Initialize()
            {
                L1.SetTextColor(Color.white);
                R1.SetTextColor(Color.white);
            }

            public void SelfDestruct()
            {
                Hud?.SelfDestruct();
                Hud = null;
                South = null;
                West = null;
                East = null;
                North = null;
                L1 = null;
                R1 = null;
            }

            public Transform TF => null;

            public Card Hud { get; set; }
            public Card North { get; set; }
            public Card East { get; set; }
            public Card South { get; set; }
            public Card West { get; set; }
            public Card L1 { get; set; }
            public Card R1 { get; set; }
        }
    }

}
