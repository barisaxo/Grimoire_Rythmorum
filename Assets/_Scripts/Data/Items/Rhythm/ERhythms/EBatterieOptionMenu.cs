using Datum.ERhythm;
using Datum;
using MusicTheory.Rhythms;

namespace Menus
{
    public class EBatterieOptionMenu : IMenu
    {
        public EBatterieOptionMenu(State subsequentState)
        {
            SubsequentState = subsequentState;
        }

        readonly State SubsequentState;
        public IData Data { get; } = Manager.Io.EBatterieOptionData;
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();

        public string DisplayData(IItem item)
        {
            return item.Name.SentenceCase();//+ ": " + Data.GetDisplayLevel(item);
        }

        public string GetDescription => BatterieUnlocked ? string.Empty : Selection.Item switch
        {
            NoRestsOrTies => "Complete Rhythm Cells to unlock",
            Rests => "Practice " + (5 - Manager.Io.EBatterieOptionData.GetLevel(new NoRestsOrTies())) + " more previous Batterie to unlock",
            Ties => "Practice " + (5 - Manager.Io.EBatterieOptionData.GetLevel(new Rests())) + " more previous Batterie to unlock",
            RestsAndTies => "Practice " + (5 - Manager.Io.EBatterieOptionData.GetLevel(new Ties())) + " more previous Batterie to unlock",

            _ => throw new System.Exception(Selection.Item.Name)
        };

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

        public State GetNextState => BatterieUnlocked ?
                new BatteryTutorial_State(null, GetSpecs(), Manager.Io.EBatterieOptionData, Selection.Item, new MenuState(this), true)
                : null;

        void UpdateButtons()
        {
            if (BatterieUnlocked) ((BatteriePracticeMenuScene)Scene).ShowButtons();
            else ((BatteriePracticeMenuScene)Scene).HideButtons();
        }

        bool BatterieUnlocked => Selection.Item switch
        {
            NoRestsOrTies => Manager.Io.ERhythmCellData.GetLevel(new EQE()) > 0,
            Rests => Manager.Io.EBatterieOptionData.GetLevel(new NoRestsOrTies()) >= 5,
            Ties => Manager.Io.EBatterieOptionData.GetLevel(new Rests()) >= 5,
            RestsAndTies => Manager.Io.EBatterieOptionData.GetLevel(new Ties()) >= 5,
            _ => throw new System.Exception(Selection.Item.Name)
        };


        private RhythmSpecs GetSpecs()
        {
            return new RhythmSpecs()
                .SetNumberOfMeasures(4)
                .SetTies(Selection.Item is Ties or RestsAndTies)
                .SetRests(Selection.Item is Rests or RestsAndTies)
                .SetSubDivision(SubDivisionTier.D1Only)
                ;
        }

    }
}