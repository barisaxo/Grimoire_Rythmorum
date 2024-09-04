using Datum.QRhythm;
using Datum;
using MusicTheory.Rhythms;

namespace Menus
{
    public class QRhythmCellMenu : IMenu
    {
        public QRhythmCellMenu(State subsequentState)
        {
            // Data = coveMenuData;
            SubsequentState = subsequentState;
        }

        readonly State SubsequentState;
        public IData Data { get; } = Manager.Io.QRhythmCellData;
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
                    Enumeration.FindId<QRhythmCellEnum>(Selection.Item.Id - 1).Name +
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
            if (!Unlocked(Selection.Item)) { ConsequentState = null; return; }
            var video = Selection.Item switch
            {
                QQQQ => Assets.RhythmCellQQQQ,
                HH => Assets.RhythmCellHH,
                W => Assets.RhythmCellW,
                HQQ => Assets.RhythmCellHQQ,
                QQH => Assets.RhythmCellQQH,
                DHQ => Assets.RhythmCellDHQ,
                QDH => Assets.RhythmCellQDH,
                QHQ => Assets.RhythmCellQHQ,
                _ => throw new System.Exception("???")
            };
            ConsequentState = new Video_State(video, new MenuState(this));
        }

        bool Unlocked(IItem item) =>
            item is QQQQ ||
            Data.GetLevel(
                QRhythmCellEnum.ToItem(
                    Enumeration.FindId<QRhythmCellEnum>(Selection.Item.Id - 1)))
            > 0;


        State TrainingBattery => Selection.Item switch
        {
            QQQQ => new BatteryTutorial_State(GetMeasures(CellShape.SSSS), GetSpecs(), Manager.Io.QRhythmCellData, Selection.Item, new MenuState(this), true),
            HH => new BatteryTutorial_State(GetMeasures(CellShape.LL), GetSpecs(), Manager.Io.QRhythmCellData, Selection.Item, new MenuState(this), true),
            W => new BatteryTutorial_State(GetMeasures(CellShape.L), GetSpecs(), Manager.Io.QRhythmCellData, Selection.Item, new MenuState(this), true),
            HQQ => new BatteryTutorial_State(GetMeasures(CellShape.LSS), GetSpecs(), Manager.Io.QRhythmCellData, Selection.Item, new MenuState(this), true),
            QQH => new BatteryTutorial_State(GetMeasures(CellShape.SSL), GetSpecs(), Manager.Io.QRhythmCellData, Selection.Item, new MenuState(this), true),
            DHQ => new BatteryTutorial_State(GetMeasures(CellShape.LS), GetSpecs(), Manager.Io.QRhythmCellData, Selection.Item, new MenuState(this), true),
            QDH => new BatteryTutorial_State(GetMeasures(CellShape.SL), GetSpecs(), Manager.Io.QRhythmCellData, Selection.Item, new MenuState(this), true),
            QHQ => new BatteryTutorial_State(GetMeasures(CellShape.SLS), GetSpecs(), Manager.Io.QRhythmCellData, Selection.Item, new MenuState(this), true),
            _ => throw new System.Exception(Selection.Item.Name)
        };

        private RhythmSpecs GetSpecs()
        {
            return new RhythmSpecs()
                .SetNumberOfMeasures(1)
                ;
        }

        Measure[] GetMeasures(CellShape cs)
        {
            Measure[] rhythm = new Measure[1] {
            new (){Cells = new RhythmCell[1]{
                new RhythmCell()
                    .SetMetricLevel(MetricLevel.Beat)
                    .SetQuantizement(Quantizement.Quarter)
                    .SetRhythmicShape(cs)
                    .SetCount(1)
        }}};

            return rhythm;
        }
    }


}