using System;
using Data;

namespace Menus
{
    public class VolumeMenu : IMenu
    {
        public VolumeMenu(VolumeData vd, Audio.AudioManager am)//, State subsequentState)
        {
            Data = vd;
            AM = am;
            // ConsequentState = subsequentState;
        }

        readonly Audio.AudioManager AM;
        public IData Data { get; }
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; } = new TwoColumns();

        public string GetDescription { get => Selection.Item.Description; }

        public string DisplayData(IItem item)
        {
            return item.Name + ": " + Data.GetDisplayLevel(item) + "%";
        }

        public IInputHandler Input => new MenuInputHandler()
        {
            North = new ButtonInput(IncreaseItem),
            West = new ButtonInput(DecreaseItem),
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
            Right = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, this)),
            Left = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, this)),
            // East = new ButtonInput(() => { })
        };

        private void IncreaseItem()
        {
            Data.AdjustLevel(Selection.Item, 1);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
            AM.BGMusic.VolumeLevelSetting = (float)(Data.GetLevel(new BGMusic()) * .01f);
            AM.SFX.VolumeLevelSetting = (float)(Data.GetLevel(new SoundFX()) * .01f);
        }

        private void DecreaseItem()
        {
            Data.AdjustLevel(Selection.Item, -1);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
            AM.BGMusic.VolumeLevelSetting = (float)(Data.GetLevel(new BGMusic()) * .01f);
            AM.SFX.VolumeLevelSetting = (float)(Data.GetLevel(new SoundFX()) * .01f);
        }

        public State ConsequentState => null;
        public IMenuScene Scene => null;
    }
}