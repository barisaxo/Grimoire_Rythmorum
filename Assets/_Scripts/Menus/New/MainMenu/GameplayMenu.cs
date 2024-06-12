using System;
using Data;

namespace Menus
{
    public class GameplayMenu : IMenu
    {
        public GameplayMenu(GameplayData gd)
        {
            Data = gd;
        }

        TuningNote TuningNote;
        public IData Data { get; }
        MenuItem _selection;

        public MenuItem Selection
        {
            get => _selection;
            set
            {
                if ((_selection = value).Item is Tuning)
                {
                    TuningNote ??= new((MusicTheory.KeyOf)Data.GetLevel(new Transpose()));
                }
                else
                {
                    TuningNote?.SelfDestruct();
                    TuningNote = null;
                }
            }
        }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; } = new AlignLeft();

        public string GetDescription { get => Selection.Item.Description; }
        public string DisplayData(IItem item)
        {
            return item.Name + ": " + Data.GetDisplayLevel(item);
        }

        public IInputHandler Input => new MenuInputHandler()
        {
            R1 = new ButtonInput(() => TuningNote?.SelfDestruct()),
            L1 = new ButtonInput(() => TuningNote?.SelfDestruct()),
            North = new ButtonInput(IncreaseItem),
            West = new ButtonInput(DecreaseItem),
            South = new ButtonInput(() => TuningNote?.SelfDestruct()),
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
            // Right = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, this)),
            // Left = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, this)),
        };

        private void IncreaseItem()
        {
            Data.AdjustLevel(Selection.Item, 1);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

        private void DecreaseItem()
        {
            Data.AdjustLevel(Selection.Item, -1);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

        public State ConsequentState => null;
        public IMenuScene Scene => null;
    }
}