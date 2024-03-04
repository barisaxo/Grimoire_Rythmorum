using System;
using Data;
using Data.Options;

namespace Menus.Options
{
    public class OptionsMenu : IHeaderMenu, IMenu
    {
        public OptionsMenu(DataManager dataManager, Audio.AudioManager audioManager, State subsequentState)
        {
            DataManager = dataManager;
            Data = new OptionsData();
            Audio = audioManager;
            CurrentSub = SubMenus[0];
            ConsequentState = subsequentState;
        }

        readonly DataManager DataManager;
        readonly Audio.AudioManager Audio;
        public IData Data { get; }
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
        public MenuItem[] MenuItems { get; set; }
        public IMenu CurrentSub { get; private set; }
        private IMenu[] _subMenus;
        public IMenu[] SubMenus => _subMenus ??= new IMenu[] {
            new VolumeMenu(DataManager.Volume, Audio),
            new SettingsMenu(DataManager.Settings),
            new VolumeMenu(DataManager.Volume, Audio),
        };

        public IMenuLayout Layout { get; } = new Header();

        public IInputHandler Input => new MenuInputHandler()
        {
            R1 = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, Selection, MenuItems)),
            L1 = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, Selection, MenuItems)),
        };

        public string DisplayData(DataEnum item)
        {
            return item.Name;
        }

        public State ConsequentState { get; }
        public IMenuScene Scene => null;
    }

}
