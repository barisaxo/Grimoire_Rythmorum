using Data.QRhythm;
using Data;
using MusicTheory.Rhythms;

namespace Menus
{
    public class QRhythmCellOptionMenu : IMenu
    {
        public QRhythmCellOptionMenu(State subsequentState)
        {
            SubsequentState = subsequentState;
        }

        readonly State SubsequentState;
        public IData Data { get; } = new QRhythmCellMenuOptionData();
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
            RhythmCells => new MenuState(new QRhythmCellMenu(new MenuState(this))),
            BatteriePractice => BatterieUnlocked ?
                new BatteryTutorial_State(null, GetSpecs(), Manager.Io.BatteriePracticeData, new QRhythmCell(), new MenuState(this))
                : null,
            _ => throw new System.ArgumentException(),
        };

        void UpdateButtons()
        {
            if (BatterieUnlocked) ((PracticeMenuScene)Scene).ShowButtons();
            else ((PracticeMenuScene)Scene).HideButtons();
        }

        bool BatterieUnlocked => Selection.Item is About or RhythmCells || Manager.Io.QRhythmCellData.GetLevel(new QHQ()) > 0;


        private RhythmSpecs GetSpecs()
        {
            return new RhythmSpecs()
                .SetNumberOfMeasures(4)
                .SetTies(true)
                .SetRests(true)
                ;
        }

        // Measure[] GetMeasures()
        // {
        //     Measure[] rhythm = new Measure[1] {
        //     new (){Cells = new RhythmCell[1]{
        //         new RhythmCell()
        //             .SetMetricLevel(MetricLevel.Beat)
        //             .SetQuantizement(Quantizement.Quarter)
        //             .SetRhythmicShape(cs)
        //             .SetCount(1)
        // }}};

        //     return rhythm;
        // }
    }
}