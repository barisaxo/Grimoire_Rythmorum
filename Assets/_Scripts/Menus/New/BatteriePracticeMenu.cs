using Datum;

namespace Menus
{
    public class BatteriePracticeMenu : IMenu
    {
        public BatteriePracticeMenu(State subsequentState)
        {
            // Data = coveMenuData;
            SubsequentState = subsequentState;
        }

        readonly State SubsequentState;
        public IData Data { get; } = Manager.Io.BatteriePracticeData;
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();

        public string DisplayData(IItem item)
        {
            return item.Name;//+ ": " + Data.GetDisplayLevel(item);
        }

        public string GetDescription
        {
            get
            {
                if (Unlocked(Selection.Item)) return Data.GetDescription(Selection.Item);

                return Selection.Item switch
                {
                    ERhythmCell => "Practice " + (10 - Data.GetLevel(new QRhythmCell())) + " more Quarter Note Batterie to unlock",
                    SRhythmCell => "Practice " + (10 - Data.GetLevel(new ERhythmCell())) + " more Eighth Note Batterie to unlock",
                    _ => Data.GetDescription(Selection.Item)
                };
            }

        }

        public IInputHandler Input => new MenuInputHandler()
        {
            Up = new ButtonInput(() =>
            {
                Selection = Layout.ScrollMenuItems(Dir.Up, this);
                UpdateButton();
            }),
            Down = new ButtonInput(() =>
            {
                Selection = Layout.ScrollMenuItems(Dir.Down, this);
                UpdateButton();
            }),
            East = new ButtonInput(() => ConsequentState = GetNewState),
            South = new ButtonInput(() => ConsequentState = SubsequentState),
        };

        private IMenuScene _scene;
        public IMenuScene Scene => _scene ??= new BatteriePracticeMenuScene();

        public State ConsequentState { get; set; }

        State GetNewState
        {
            get
            {
                if (!Unlocked(Selection.Item)) return null;

                return Selection.Item switch
                {
                    QRhythmCell => new MenuState(new QRhythmCellOptionMenu(new MenuState(this))),
                    ERhythmCell => new MenuState(new ERhythmCellOptionMenu(new MenuState(this))),
                    SRhythmCell => new MenuState(new SRhythmCellOptionMenu(new MenuState(this))),
                    _ => null
                };
            }
        }

        void UpdateButton()
        {
            if (Unlocked(Selection.Item)) ((BatteriePracticeMenuScene)Scene).ShowButtons();
            else ((BatteriePracticeMenuScene)Scene).HideButtons();
        }

        bool Unlocked(IItem item) => item switch
        {
            QRhythmCell => true,
            ERhythmCell => Data.GetLevel(new QRhythmCell()) > 9,
            SRhythmCell => Data.GetLevel(new ERhythmCell()) > 9,
            _ => false,
        };

    }
}