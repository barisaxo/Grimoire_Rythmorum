namespace Menus.MainMenu
{
    public class MainMenu : Menu<MainMenu.MainMenuItem, MainMenu>
    {
        private MainMenuScene _scene;

        public MainMenu() : base(nameof(MainMenu))
        {
        }

        public int I { get; set; }
        
        public MainMenuScene Scene => _scene ??= new MainMenuScene();

        public override MenuLayoutStyle Style => MenuLayoutStyle.AlignRight;

        public override Menu<MainMenuItem, MainMenu> Initialize()
        {
            _ = Scene;
            return base.Initialize();
        }

        public override void SelfDestruct()
        {
            Scene.SelfDestruct();
            base.SelfDestruct();
        }

        public class MainMenuItem : DataEnum
        {
            public static readonly MainMenuItem Continue = new(0, "Continue");
            public static readonly MainMenuItem LoadGame = new(1, "Load Game");
            public static readonly MainMenuItem NewGame = new(2, "New Game");
            public static readonly MainMenuItem Options = new(3, "Options");
            public static readonly MainMenuItem HowToPlay = new(4, "How To Play");
            public static readonly MainMenuItem PracticeRoom = new(5, "Practice Room");
            public static readonly MainMenuItem Quit = new(6, "Quit");

            public MainMenuItem() : base(0, "")
            {
            }

            private MainMenuItem(int id, string name) : base(id, name)
            {
            }
        }
    }
}