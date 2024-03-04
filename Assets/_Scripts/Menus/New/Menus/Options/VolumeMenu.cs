using System;
using Data;
using Data.Options;

namespace Menus.Options
{
    public class VolumeMenu : IMenu
    {
        public VolumeMenu(VolumeData vd, Audio.AudioManager am) { Data = vd; AM = am; }

        readonly Audio.AudioManager AM;
        public IData Data { get; }
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public IMenuLayout Layout { get; } = new TwoColumns();

        public string DisplayData(DataEnum item)
        {
            return item.Name + ": " + Data.GetDisplayLevel(item) + "%";
        }

        public IInputHandler Input => new MenuInputHandler()
        {
            North = new ButtonInput(IncreaseItem),
            West = new ButtonInput(DecreaseItem),
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, Selection, MenuItems)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, Selection, MenuItems)),
            Right = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, Selection, MenuItems)),
            Left = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, Selection, MenuItems)),
        };

        private void IncreaseItem()
        {
            Data.IncreaseLevel(Selection.Item);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
            AM.BGMusic.VolumeLevelSetting = (float)(Data.GetLevel(VolumeData.DataItem.BGMusic) * .01f);
            AM.SFX.VolumeLevelSetting = (float)(Data.GetLevel(VolumeData.DataItem.SoundFX) * .01f);
        }

        private void DecreaseItem()
        {
            Data.DecreaseLevel(Selection.Item);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
            AM.BGMusic.VolumeLevelSetting = (float)(Data.GetLevel(VolumeData.DataItem.BGMusic) * .01f);
            AM.SFX.VolumeLevelSetting = (float)(Data.GetLevel(VolumeData.DataItem.SoundFX) * .01f);
        }

        public State ConsequentState => null;
        public IMenuScene Scene => null;
    }
}