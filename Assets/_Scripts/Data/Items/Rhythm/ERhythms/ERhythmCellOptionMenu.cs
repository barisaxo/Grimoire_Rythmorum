using Datum.ERhythm;
using Datum;

namespace Menus
{
    public class ERhythmCellOptionMenu : IMenu
    {
        public ERhythmCellOptionMenu(State subsequentState)
        {
            SubsequentState = subsequentState;
        }

        readonly State SubsequentState;
        public IData Data { get; } = new ERhythmCellMenuOptionData();
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();

        public string DisplayData(IItem item)
        {
            return item.Name;//+ ": " + Data.GetDisplayLevel(item);
        }

        public string GetDescription => !BatterieUnlocked ? "Complete Rhythm Cells to unlock" : string.Empty;

        public IInputHandler Input => new MenuInputHandler()
        {
            Up = new ButtonInput(() =>
            {
                Selection = Layout.ScrollMenuItems(Dir.Up, this);
                UpdateButtons();
            }),
            Down = new ButtonInput(() =>
            {
                Selection = Layout.ScrollMenuItems(Dir.Down, this);
                UpdateButtons();
            }),
            East = new ButtonInput(() => ConsequentState = GetNextState),
            South = new ButtonInput(() => ConsequentState = SubsequentState),
        };

        private IMenuScene _scene;
        public IMenuScene Scene => _scene ??= new BatteriePracticeMenuScene();

        public State ConsequentState { get; set; }

        public State GetNextState => Selection.Item switch
        {
            About => null,
            RhythmCells => new MenuState(new ERhythmCellMenu(new MenuState(this))),
            BatteriePractice => BatterieUnlocked ?
                new MenuState(new EBatterieOptionMenu(new MenuState(this)))
                // new BatteryTutorial_State(null, GetSpecs(), Manager.Io.BatteriePracticeData, new ERhythmCell(), new MenuState(this))
                : null,
            _ => throw new System.ArgumentException(),
        };

        void UpdateButtons()
        {
            if (BatterieUnlocked) ((BatteriePracticeMenuScene)Scene).ShowButtons();
            else ((BatteriePracticeMenuScene)Scene).HideButtons();
        }

        bool BatterieUnlocked => Selection.Item is About or RhythmCells || Manager.Io.ERhythmCellData.GetLevel(new EQE()) > 0;
    }
}