namespace OldMenus.MainMenu
{
    public class MainMenu : Menu<MainMenu.MainMenuItem, MainMenu>
    {
        public MainMenu() :
            base(
                nameof(MainMenu),
                new AlignRight<MainMenuItem>())
        { }

        public class MainMenuItem : DataEnum
        {
            public static readonly MainMenuItem Continue = new(0, "Continue");
            // public static readonly MainMenuItem LoadGame = new(1, "Load Game");
            public static readonly MainMenuItem NewGame = new(1, "New Game");
            public static readonly MainMenuItem Options = new(2, "Options");
            // public static readonly MainMenuItem HowToPlay = new(4, "How To Play");
            // public static readonly MainMenuItem Quit = new(5, "Quit");

            public MainMenuItem() : base(0, "") { }

            private MainMenuItem(int id, string name) : base(id, name) { }
        }
    }
}