
using Datum;
using Datum.BeatFishing;
using MusicTheory.Rhythms;

namespace Menus
{
    public class BeatFishingPracticeMenu : IMenu
    {
        public BeatFishingPracticeMenu(State subsequentState)
        {
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
                if (Unlocked(Selection.Item)) return "Fishing mini-game.";

                return
                    "Practice " +
                    Enumeration.FindId<BeatFishingPracticeEnum>(Selection.Item.ID - 1).Name +
                    " Beat Fishing to unlock";
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
        public IMenuScene Scene => _scene ??= new BatteriePracticeMenuScene();

        public State ConsequentState { get; set; }

        void UpdateButton()
        {
            if (!Unlocked(Selection.Item)) ((BatteriePracticeMenuScene)Scene).HideButtons();
            else ((BatteriePracticeMenuScene)Scene).ShowButtons();
        }

        void EastPressed()
        {
            if (!Unlocked(Selection.Item)) ConsequentState = null;
            else ConsequentState = new BeatFishingPractice_State(new MenuState(this), Selection.Item);
        }

        void WestPressed()
        {
            // if (!Unlocked(Selection.Item)) ConsequentState = null;
            // else ConsequentState = GetTutorial;
        }

        bool Unlocked(IItem item)
        {
            if (item is QQQQ) return true;

            return Data.GetLevel(
                        BeatFishingPracticeEnum.ToItem(
                            Enumeration.FindId<BeatFishingPracticeEnum>(Selection.Item.ID - 1)))
                   > 0;
        }
    }


}