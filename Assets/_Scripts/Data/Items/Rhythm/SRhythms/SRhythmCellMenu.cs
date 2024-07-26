using Data.SRhythm;
using Data;
using MusicTheory.Rhythms;

namespace Menus
{
    public class SRhythmCellMenu : IMenu
    {
        public SRhythmCellMenu(State subsequentState)
        {
            SubsequentState = subsequentState;
        }

        readonly State SubsequentState;
        public IData Data { get; } = Manager.Io.SRhythmCellData;
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
                if (Unlocked(Selection.Item)) return "Not fully yet implemented";

                return
                    "Complete " +
                    Enumeration.FindId<SRhythmCellEnum>(Selection.Item.ID - 1).Name +
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
            item is SSSS ||
            Data.GetLevel(
                SRhythmCellEnum.ToItem(
                    Enumeration.FindId<SRhythmCellEnum>(Selection.Item.ID - 1)))
            > 0;


        State TrainingBattery => Selection.Item switch
        {
            SSSS => new BatteryTutorial_State(GetMeasures(CellShape.SSSS), GetSpecs(), Manager.Io.SRhythmCellData, Selection.Item, new MenuState(this)),
            Q => new BatteryTutorial_State(GetMeasures(CellShape.LL), GetSpecs(), Manager.Io.SRhythmCellData, Selection.Item, new MenuState(this)),
            EE => new BatteryTutorial_State(GetMeasures(CellShape.L), GetSpecs(), Manager.Io.SRhythmCellData, Selection.Item, new MenuState(this)),
            ESS => new BatteryTutorial_State(GetMeasures(CellShape.LSS), GetSpecs(), Manager.Io.SRhythmCellData, Selection.Item, new MenuState(this)),
            SSE => new BatteryTutorial_State(GetMeasures(CellShape.SSL), GetSpecs(), Manager.Io.SRhythmCellData, Selection.Item, new MenuState(this)),
            DES => new BatteryTutorial_State(GetMeasures(CellShape.LS), GetSpecs(), Manager.Io.SRhythmCellData, Selection.Item, new MenuState(this)),
            SDE => new BatteryTutorial_State(GetMeasures(CellShape.SL), GetSpecs(), Manager.Io.SRhythmCellData, Selection.Item, new MenuState(this)),
            SES => new BatteryTutorial_State(GetMeasures(CellShape.SLS), GetSpecs(), Manager.Io.SRhythmCellData, Selection.Item, new MenuState(this)),
            _ => throw new System.Exception(Selection.Item.Name)
        };

        private RhythmSpecs GetSpecs()
        {
            return new RhythmSpecs()
                .SetNumberOfMeasures(1)
                .SetSubDivision(SubDivisionTier.D2Only)
                ;
        }

        Measure[] GetMeasures(CellShape cs)
        {
            Measure[] rhythm = new Measure[1] {
            new (){Cells = new RhythmCell[4]{
                new RhythmCell()
                    .SetMetricLevel(MetricLevel.D2)
                    .SetQuantizement(Quantizement.Sixteenth)
                    .SetRhythmicShape(cs)
                    .SetCount(1),

                new RhythmCell()
                    .SetMetricLevel(MetricLevel.D2)
                    .SetQuantizement(Quantizement.Sixteenth)
                    .SetRhythmicShape(cs)
                    .SetCount(2),

                new RhythmCell()
                    .SetMetricLevel(MetricLevel.D2)
                    .SetQuantizement(Quantizement.Sixteenth)
                    .SetRhythmicShape(cs)
                    .SetCount(3),

                new RhythmCell()
                    .SetMetricLevel(MetricLevel.D2)
                    .SetQuantizement(Quantizement.Sixteenth)
                    .SetRhythmicShape(cs)
                    .SetCount(4),
        }}};

            return rhythm;
        }
    }


}