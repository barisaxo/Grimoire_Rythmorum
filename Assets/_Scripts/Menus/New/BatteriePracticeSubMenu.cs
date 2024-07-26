using Data;

namespace Menus
{
    public class BatteriePracticeSubMenu : IMenu
    {
        public BatteriePracticeSubMenu(State subsequentState)
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
                if (Selection.Item is ERhythmCell)
                {
                    if (!(Manager.Io.QRhythmCellData.GetLevel(new QHQ()) > 0))
                    {
                        return "Complete Quarter Notes to Unlock";
                    }
                }

                if (Selection.Item is SRhythmCell)
                {
                    if (!(Manager.Io.ERhythmCellData.GetLevel(new EQE()) > 0))
                    {
                        return "Complete Eighth Notes to Unlock";
                    }
                }
                return Data.GetDescription(Selection.Item);
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
            East = new ButtonInput(EastPressed),
            South = new ButtonInput(() => ConsequentState = SubsequentState),
            West = new ButtonInput(WestPressed),
        };

        private IMenuScene _scene;
        public IMenuScene Scene => _scene ??= new PracticeMenuScene();

        public State ConsequentState { get; set; }

        State GetNewState
        {
            get
            {
                if (Selection.Item is ERhythmCell)
                {
                    if (!(Manager.Io.QRhythmCellData.GetLevel(new QHQ()) > 0))
                    {
                        return null;//new MenuState(new Data.ERhythm.ERhythmCellMenuOptionData());
                    }
                }

                if (Selection.Item is SRhythmCell)
                {
                    if (!(Manager.Io.ERhythmCellData.GetLevel(new EQE()) > 0))
                    {
                        return null;//"Complete Eighth Notes to Unlock";
                    }
                }
                return null;
            }
        }
        // new StarChartPractice_State(
        //      GetPuzzle(Selection.Item),
        //      GetPuzzleType(Selection.Item),
        //      new MenuState(this)
        //      );

        public State GetTutorial => Selection.Item switch
        {
            QRhythmCell => null,
            ERhythmCell => null,
            SRhythmCell => null,
            _ => throw new System.ArgumentException(),
        };

        void UpdateButton()
        {
            if (!Unlocked(Selection.Item)) ((PracticeMenuScene)Scene).HideButtons();
            else ((PracticeMenuScene)Scene).ShowButtons();
        }
        void EastPressed()
        {
            if (!Unlocked(Selection.Item)) ConsequentState = null;
            else ConsequentState = null;
        }

        void NorthPressed()
        {

        }

        void WestPressed()
        {
            if (!Unlocked(Selection.Item)) ConsequentState = null;
            else ConsequentState = GetTutorial;
        }

        bool Unlocked(IItem item) => false;
        // (item is NotesT or NotesA) ||
        //      Manager.Io.Puzzles.GetLevel(
        //         GetPuzzle(
        //             StarChartEnum.ToItem(
        //                 Enumeration.FindId<StarChartEnum>(Selection.Item.ID - 2))))
        //                     >= 10;
    }
}