

using Datum;

namespace Menus
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
            East = new ButtonInput(() => { }),

            Select = new ButtonInput(() =>
            {
                if (InputKey.InputActions.Map.Start.IsPressed())
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                UnityEngine.Application.Quit();
            }),

            Start = new ButtonInput(() =>
            {
                if (InputKey.InputActions.Map.Select.IsPressed())
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                UnityEngine.Application.Quit();
            })
        };

        public string GetDescription { get => null; }

        public string DisplayData(IItem item)
        {
            return null;
        }

        string IMenu.DisplayData(IItem item) => item.Name;

        public State ConsequentState => Selection.Item switch
        {
            Continue => Manager.Io.Misc.Get(new FirstInstantiateDialogue()) ?
                new NewCoveScene_State() :
                new DialogStart_State(new InstantiateDialogue()) { Fade = true },

            Options =>
                new MenuState(
                    new OptionsMenu(Manager, AudioManager, new MenuState(this))),
            _ => throw new System.ArgumentOutOfRangeException()
        };

        private IMenuScene _scene;
        public IMenuScene Scene => _scene ??= new MainMenuScene();
    }
}