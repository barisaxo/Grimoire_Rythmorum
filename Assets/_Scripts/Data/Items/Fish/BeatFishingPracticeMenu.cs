
using Data;
using MusicTheory.Rhythms;

namespace Menus
{
    public class BeatFishingPracticeMenu : IMenu
    {
        public BeatFishingPracticeMenu(State subsequentState)
        {
            // Data = coveMenuData;
            SubsequentState = subsequentState;
        }

        readonly State SubsequentState;
        public IData Data { get; } = Manager.Io.BeatFishingPracticeData;
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
                    Enumeration.FindId<BeatFishingPracticeEnum>(Selection.Item.ID - 1).Name +
                    " to Unlock";

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

        void UpdateButton()
        {
            if (!Unlocked(Selection.Item)) ((PracticeMenuScene)Scene).HideButtons();
            else ((PracticeMenuScene)Scene).ShowButtons();
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
            (item is LVL1) ||
            (Data.GetLevel(
                BeatFishingPracticeEnum.ToItem(
                    Enumeration.FindId<BeatFishingPracticeEnum>(
                        Selection.Item.ID - 1)))
                 > 0);


        State TrainingBattery => Selection.Item switch
        {
            LVL1 => new AnglingPractice_State(new MenuState(this)),
            _ => throw new System.Exception(Selection.Item.Name)
        };

    }


}