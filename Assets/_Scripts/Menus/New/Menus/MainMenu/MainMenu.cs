
using Data;
using Data.Main;
using Menus.Options;

namespace Menus.Main
{
    public class MainMenu : IMenu
    {
        public MainMenu(DataManager dataManager, Audio.AudioManager audioManager)
        {
            Data = new MainMenuData(dataManager);
            DataManager = dataManager;
            AudioManager = audioManager;
            // CurrentSub = SubMenus[0];
        }

        // readonly State SubsequentState;
        readonly DataManager DataManager;
        readonly Audio.AudioManager AudioManager;
        public IData Data { get; }
        // private MenuItem _selection;
        public MenuItem Selection { get; set; }
        // {
        //     get => _selection;
        //     set
        //     {
        //         _selection = value;
        //         CurrentSub = SubMenus[_selection.Item.Id];
        //     }
        // }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        // public IMenu CurrentSub { get; private set; }
        // private IMenu[] _subMenus;
        // public IMenu[] SubMenus => _subMenus ??= new IMenu[] {
        //     new OptionsMenu(DataManager, AudioManager),
        // };

        public IMenuLayout Layout { get; } = new AlignRight();

        public IInputHandler Input => new MenuInputHandler()
        {
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
        };

        public string GetDescription { get => Selection.Item.Description; }
        public string DisplayData(DataEnum item)
        {
            return item.Name;
        }

        public State ConsequentState => Selection.Item switch
        {
            _ when Selection == MainMenuData.DataItem.Continue => new NewCoveScene_State(),
            // _ when Selection == MainMenuData.DataItem.NewGame => new NewCoveScene_State(),
            _ when Selection == MainMenuData.DataItem.Options =>
                new Menu_State(
                    new OptionsMenu(DataManager, AudioManager,
                        new Menu_State(new MainMenu(DataManager, AudioManager)))),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        public IMenuScene Scene { get; } = new MainMenuScene();
    }
}