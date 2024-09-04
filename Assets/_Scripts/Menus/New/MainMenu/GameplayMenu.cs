using System;
using Datum;
using UnityEngine;

namespace Menus
{
    public class GameplayMenu : IMenu
    {
        public GameplayMenu(GameplayData gd, State subsequentState)
        {
            Data = gd;
            // UnityEngine.Debug.Log(subsequentState.GetType());
            SubsequentState = subsequentState;
        }

        readonly State SubsequentState;
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
                    Audio.AudioManager.Io.BGMusic.VolumeLevelSetting = 0;
                    TuningNote ??= new((MusicTheory.KeyOf)Data.GetLevel(new Transpose()));
                }
                else
                {
                    Audio.AudioManager.Io.BGMusic.VolumeLevelSetting = Manager.Io.Volume.GetScaledLevel(new BGMusic());
                    TuningNote?.SelfDestruct();
                    TuningNote = null;
                }
                ShowHideEast();
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
            East = new ButtonInput(() => CalibrationMode()),
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

        void ShowHideEast()
        {
            if (Selection.Item is Calibrate)
                Scene.East.SetImageColor(Color.white).SetTextColor(Color.white);
            else
                Scene.East.SetImageColor(Color.clear).SetTextColor(Color.clear);
        }

        void CalibrationMode()
        {
            ConsequentState = Selection.Item is Calibrate ? new LatencyCalibration_State(SubsequentState) : null;
        }

        private State _conState = null;
        public State ConsequentState
        {
            get { var s = _conState; _conState = null; return s; }
            private set => _conState = value;
        }

        private IMenuScene _scene;
        public IMenuScene Scene => _scene ??= new GamePlayMenuScene();


        public class GamePlayMenuScene : IMenuScene
        {
            public string Name { get; } = nameof(GamePlayMenuScene);

            public void Initialize()
            {
                South.SetTextString("Back").SetImageColor(Color.white);
                North.SetTextString("Increase").SetImageColor(Color.white);
                West.SetTextString("Decrease").SetImageColor(Color.white);
                East.SetTextString("Calibrate").SetImageColor(Color.clear).SetTextColor(Color.clear);
                ((IMenuScene)this).SetCardPos1(South);
                ((IMenuScene)this).SetCardPos2(West);
                ((IMenuScene)this).SetCardPos3(North);
                ((IMenuScene)this).SetCardPos4(East);
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