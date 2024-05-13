

using Data.Two;

namespace Menus.Two
{
    public class MainMenu : IMenu
    {
        public MainMenu(Manager manager, Audio.AudioManager audioManager)
        {
            Manager = manager;
            AudioManager = audioManager;
            Data = new MainMenuData();
        }

        // readonly State SubsequentState;
        readonly Manager Manager;
        readonly Audio.AudioManager AudioManager;
        public IData Data { get; }
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }

        public IMenuLayout Layout { get; } = new AlignRight();

        public IInputHandler Input => new MenuInputHandler()
        {
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
        };

        public string GetDescription { get => null; }

        public string DisplayData(IItem item)
        {
            return null;
        }

        string IMenu.DisplayData(IItem item) => item.Name;

        public State ConsequentState => Selection.Item switch
        {
            Continue => new NewCoveScene_State(),
            // _ when Selection == MainMenuData.DataItem.NewGame => new NewCoveScene_State(),
            Options =>
                new MenuState(
                new OptionsMenu(Manager, AudioManager,
                new MenuState(new MainMenu(Manager, AudioManager)))),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        public IMenuScene Scene { get; } = new MainMenuScene();
    }
}