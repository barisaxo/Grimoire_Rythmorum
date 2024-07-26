using System;
using Data;
using UnityEngine;

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

        private IMenuScene _scene;
        public IMenuScene Scene => _scene ??= new MenuScene();

        public class MenuScene : IMenuScene
        {
            // public InventoryMenuScene() { Initialize(); }
            public string Name { get; } = nameof(MenuScene);
            public void Initialize()
            {
                South.SetTextString("Back").SetImageColor(Color.white);
                North.SetTextString("Increase").SetImageColor(Color.white);
                West.SetTextString("Decrease").SetImageColor(Color.white);
                ((IMenuScene)this).SetCardPos1(South);
                ((IMenuScene)this).SetCardPos2(West);
                ((IMenuScene)this).SetCardPos3(North);
            }

            public void SelfDestruct()
            {
                Hud?.SelfDestruct();
                Hud = null;
                South = null;
                West = null;
                East = null;
                North = null;
                L1 = null;
                R1 = null;
            }

            public Transform TF => null;

            public Card Hud { get; set; }
            public Card North { get; set; }
            public Card East { get; set; }
            public Card South { get; set; }
            public Card West { get; set; }
            public Card L1 { get; set; }
            public Card R1 { get; set; }
        }
    }
}