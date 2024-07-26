using Data.ERhythm;
using Data;
using MusicTheory.Rhythms;

namespace Menus
{
    public class ERhythmCellMenu : IMenu
    {
        public ERhythmCellMenu(State subsequentState)
        {
            // Data = coveMenuData;
            SubsequentState = subsequentState;
        }

        readonly State SubsequentState;
        public IData Data { get; } = Manager.Io.ERhythmCellData;
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
                if (Unlocked(Selection.Item)) return "";

                return
                    "Complete " +
                    Enumeration.FindId<ERhythmCellEnum>(Selection.Item.ID - 1).Name +
                    " to Unlock";

            }
        }

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
            East = new ButtonInput(EastPressed),
            South = new ButtonInput(() => ConsequentState = SubsequentState),
            West = new ButtonInput(WestPressed),
        };

        private IMenuScene _scene;
        public IMenuScene Scene => _scene ??= new PracticeMenuScene();

        public State ConsequentState { get; set; }

        void UpdateButtons()
        {
            if (Unlocked(Selection.Item)) ((PracticeMenuScene)Scene).ShowButtons();
            else ((PracticeMenuScene)Scene).HideButtons();
        }

        void EastPressed()
        {
            if (!Unlocked(Selection.Item)) ConsequentState = null;
            else ConsequentState = TrainingBattery;
        }

        void WestPressed()
        {
            // if (!Unlocked(Selection.Item)) ConsequentState = null;
            // else ConsequentState = GetTutorial;
        }

        bool Unlocked(IItem item) =>
            item is EEEE ||
            Data.GetLevel(
                ERhythmCellEnum.ToItem(
                    Enumeration.FindId<ERhythmCellEnum>(Selection.Item.ID - 1)))
            > 0;


        State TrainingBattery => Selection.Item switch
        {
            EEEE => new BatteryTutorial_State(GetMeasures(CellShape.SSSS), GetSpecs(), Manager.Io.ERhythmCellData, Selection.Item, new MenuState(this)),
            QQ => new BatteryTutorial_State(GetMeasures(CellShape.LL), GetSpecs(), Manager.Io.ERhythmCellData, Selection.Item, new MenuState(this)),
            H => new BatteryTutorial_State(GetMeasures(CellShape.L), GetSpecs(), Manager.Io.ERhythmCellData, Selection.Item, new MenuState(this)),
            QEE => new BatteryTutorial_State(GetMeasures(CellShape.LSS), GetSpecs(), Manager.Io.ERhythmCellData, Selection.Item, new MenuState(this)),
            EEQ => new BatteryTutorial_State(GetMeasures(CellShape.SSL), GetSpecs(), Manager.Io.ERhythmCellData, Selection.Item, new MenuState(this)),
            DQE => new BatteryTutorial_State(GetMeasures(CellShape.LS), GetSpecs(), Manager.Io.ERhythmCellData, Selection.Item, new MenuState(this)),
            EDQ => new BatteryTutorial_State(GetMeasures(CellShape.SL), GetSpecs(), Manager.Io.ERhythmCellData, Selection.Item, new MenuState(this)),
            EQE => new BatteryTutorial_State(GetMeasures(CellShape.SLS), GetSpecs(), Manager.Io.ERhythmCellData, Selection.Item, new MenuState(this)),
            _ => throw new System.Exception(Selection.Item.Name)
        };

        private RhythmSpecs GetSpecs()
        {
            return new RhythmSpecs()
                .SetNumberOfMeasures(1)
                .SetSubDivision(SubDivisionTier.D1Only)
                ;
        }

        Measure[] GetMeasures(CellShape cs)
        {
            Measure[] rhythm = new Measure[1] {
            new (){Cells = new RhythmCell[2]{
                new RhythmCell()
                    .SetMetricLevel(MetricLevel.D1)
                    .SetQuantizement(Quantizement.Eighth)
                    .SetRhythmicShape(cs)
                    .SetCount(1),

                new RhythmCell()
                    .SetMetricLevel(MetricLevel.D1)
                    .SetQuantizement(Quantizement.Eighth)
                    .SetRhythmicShape(cs)
                    .SetCount(3)
        }}};

            return rhythm;
        }
    }


}