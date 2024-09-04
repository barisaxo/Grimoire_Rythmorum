using Datum.QRhythm;
using Datum;

namespace Menus
{
    public class QTutorialMenu : IMenu
    {
        public QTutorialMenu(QRhythmCellData data, State subsequentState)
        {
            Data = data;
            SubsequentState = subsequentState;
        }

        readonly State SubsequentState;
        public IData Data { get; }
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
                if (Selection.Item is NotesT or NotesA) return "";

                IItem sc = StarChartEnum.ToItem(Enumeration.FindId<StarChartEnum>(Selection.Item.Id - 2));

                int solved = Manager.Io.Puzzles.GetLevel(GetPuzzle(sc));

                if (solved < 1)
                    return "solve " +
                        GetPuzzle(sc).GetType().ToString().SentenceCase() +
                        " to unlock";

                return "";
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

        State GetNewPuzzle => new StarChartPractice_State(
             GetPuzzle(Selection.Item),
             GetPuzzleType(Selection.Item),
             new MenuState(this)
             );

        public State GetTutorial => Selection.Item switch
        {
            NotesT or NotesA => new Video_State(Assets.NoteID, new MenuState(this)),
            StepsT or StepsA => new Video_State(Assets.Steps, new MenuState(this)),
            ScalesT or ScalesA => new Video_State(Assets.Scales, new MenuState(this)),
            IntervalsT or IntervalsA => new Video_State(Assets.Interval, new MenuState(this)),
            InvertedTriadsT or InvertedTriadsA => new Video_State(Assets.InvertedTriads, new MenuState(this)),
            ModesT or ModesA => new Video_State(Assets.Modes, new MenuState(this)),
            InversionsT or InversionsA => new Video_State(Assets.Inversions, new MenuState(this)),
            TriadsT or TriadsA => new Video_State(Assets.Triads, new MenuState(this)),
            SeventhChordsA or SeventhChordsT => new Video_State(Assets.SeventhChords, new MenuState(this)),
            Inverted7thChordsA or Inverted7thChordsT => new Video_State(Assets.InvertedSeventhChords, new MenuState(this)),
            _ => throw new System.ArgumentException(),
        };

        IPuzzle GetPuzzle(IItem item) => item switch
        {
            NotesT or NotesA => new NotePuzzle(),
            StepsT or StepsA => new StepsPuzzle(),
            ScalesT or ScalesA => new ScalePuzzle(),
            InvertedTriadsT or InvertedTriadsA => new InvertedTriadPuzzle(),
            ModesT or ModesA => new ModePuzzle(),
            IntervalsT or IntervalsA => new IntervalPuzzle(),
            InversionsT or InversionsA => new InvertedIntervalPuzzle(),
            TriadsT or TriadsA => new TriadPuzzle(),
            SeventhChordsA or SeventhChordsT => new SeventhChordPuzzle(),
            Inverted7thChordsA or Inverted7thChordsT => new InvertedSeventhChordPuzzle(),
            _ => throw new System.ArgumentException(),
        };

        PuzzleType GetPuzzleType(IItem item) => item switch
        {
            NotesT or Inverted7thChordsT or
            SeventhChordsT or StepsT or IntervalsT or
            ModesT or ScalesT or InversionsT
            or InvertedTriadsT or TriadsT => PuzzleType.Theory,

            _ => PuzzleType.Aural,
        };


        void UpdateButton()
        {
            if (!Unlocked(Selection.Item)) ((PracticeMenuScene)Scene).HideButtons();
            else ((PracticeMenuScene)Scene).ShowButtons();
        }
        void EastPressed()
        {
            if (!Unlocked(Selection.Item)) ConsequentState = null;
            else ConsequentState = GetNewPuzzle;
        }
        void NorthPressed()
        {

        }
        void WestPressed()
        {
            if (!Unlocked(Selection.Item)) ConsequentState = null;
            else ConsequentState = GetTutorial;
        }

        bool Unlocked(IItem item) =>
            (item is NotesT or NotesA) ||
                 Manager.Io.Puzzles.GetLevel(
                    GetPuzzle(
                        StarChartEnum.ToItem(
                            Enumeration.FindId<StarChartEnum>(Selection.Item.Id - 2))))
                                >= 10;
    }
}