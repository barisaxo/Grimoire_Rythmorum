using Data.SRhythm;
using Data;
using MusicTheory.Rhythms;

namespace Menus
{
    public class SRhythmCellOptionMenu : IMenu
    {
        public SRhythmCellOptionMenu(State subsequentState)
        {
            SubsequentState = subsequentState;
        }

        readonly State SubsequentState;
        public IData Data { get; } = new SRhythmCellMenuOptionData();
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();

        public string DisplayData(IItem item)
        {
            return item.Name;//+ ": " + Data.GetDisplayLevel(item);
        }

        public string GetDescription => BatterieUnlocked ? "Complete Rhythm Cells to unlock" : string.Empty;

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
        public IMenuScene Scene => _scene ??= new PracticeMenuScene();

        public State ConsequentState { get; set; }

        public State GetNextState => Selection.Item switch
        {
            About => null,
            RhythmCells => new MenuState(new SRhythmCellMenu(new MenuState(this))),
            BatteriePractice => BatterieUnlocked ?
                new BatteryTutorial_State(null, GetSpecs(), Manager.Io.BatteriePracticeData, new SRhythmCell(), new MenuState(this))
                : null,
            _ => throw new System.ArgumentException(),
        };

        private RhythmSpecs GetSpecs()
        {
            return new RhythmSpecs()
                .SetNumberOfMeasures(4)
                .SetTies(true)
                .SetRests(true)
                .SetSubDivision(SubDivisionTier.D2Only)
                ;
        }

        void UpdateButtons()
        {
            if (BatterieUnlocked) ((PracticeMenuScene)Scene).ShowButtons();
            else ((PracticeMenuScene)Scene).HideButtons();
        }

        bool BatterieUnlocked =>
            Selection.Item is About or RhythmCells ||
            Manager.Io.SRhythmCellData.GetLevel(new SES()) > 0;
    }
}