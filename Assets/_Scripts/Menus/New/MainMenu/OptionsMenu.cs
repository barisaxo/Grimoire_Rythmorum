using System;
using Data.Two;

namespace Menus.Two
{
    public class OptionsMenu : IHeaderMenu, IMenu
    {
        public OptionsMenu(Manager manager, Audio.AudioManager audioManager, State subsequentState)
        {
            Manager = manager;
            Data = new SettingsData();
            Audio = audioManager;
            CurrentSub = SubMenus[0];
            ConsequentState = subsequentState;
        }

        readonly Manager Manager;
        readonly Audio.AudioManager Audio;
        public IData Data { get; }
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
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenu CurrentSub { get; private set; }
        private IMenu[] _subMenus;
        public IMenu[] SubMenus => _subMenus ??= new IMenu[] {
            new VolumeMenu(Manager.Volume, Audio),
            new GameplayMenu(Manager.Gameplay),
            // new VolumeMenu(DataManager.Volume, Audio),
        };

        public IMenuLayout Layout { get; } = new Header();

        public IInputHandler Input => new MenuInputHandler()
        {
            R1 = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, this)),
            L1 = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, this)),
        };

        public string GetDescription { get => Selection.Item.Description; }
        public string DisplayData(IItem item)
        {
            return item.Name;
        }

        string IMenu.DisplayData(IItem item)
        {
            return item.Name;
        }

        public State ConsequentState { get; }
        public IMenuScene Scene => null;
    }

}
