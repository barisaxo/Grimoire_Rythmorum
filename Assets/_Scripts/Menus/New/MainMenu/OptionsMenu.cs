using System;
using Data;
using UnityEngine;

namespace Menus
{
    public class OptionsMenu : IHeaderMenu, IMenu
    {
        public OptionsMenu(Manager manager, Audio.AudioManager audioManager, State subsequentState)
        {
            SubsequentState = subsequentState;
            Manager = manager;
            Data = new SettingsData();
            Audio = audioManager;
            CurrentSub = SubMenus[0];
        }

        readonly State SubsequentState;
        readonly Manager Manager;
        readonly Audio.AudioManager Audio;
        public IData Data { get; }
        private MenuItem _selection;
        public MenuItem Selection
        {
            get => _selection;
            set
            {
                _selection = value;
                CurrentSub = SubMenus[_selection.Item.ID];
            }
        }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenu CurrentSub { get; private set; }
        private IMenu[] _subMenus;
        public IMenu[] SubMenus => _subMenus ??= new IMenu[] {
            new VolumeMenu(Manager.Volume, Audio),
            new GameplayMenu(Manager.Gameplay, new MenuState(this)),
            // new VolumeMenu(DataManager.Volume, Audio),
        };

        public IMenuLayout Layout { get; } = new Header();

        public IInputHandler Input => new MenuInputHandler()
        {
            R1 = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, this)),
            L1 = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, this)),
            South = new ButtonInput(() => { ConsequentState = SubsequentState; }),
            Select = new ButtonInput(() =>
            {
                if (InputKey.InputActions.Map.Start.IsPressed())
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                UnityEngine.Application.Quit();
            }),

            Start = new ButtonInput(() =>
            {
                if (InputKey.InputActions.Map.Select.IsPressed())
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                UnityEngine.Application.Quit();
            })
        };

        public string GetDescription { get => Selection.Item.Description; }
        public string DisplayData(IItem item)
        {
            return item.Name;
        }

        string IMenu.DisplayData(IItem item)
        {
            return item.Name;
        }

        public State ConsequentState { get; private set; }

        private IMenuScene _scene;
        public IMenuScene Scene => _scene ??= new MenuScene();

        public class MenuScene : IMenuScene
        {
            public string Name { get; } = nameof(MenuScene);
            public void Initialize()
            {
                L1.SetTextColor(Color.white);
                R1.SetTextColor(Color.white);
                _ = Quit;
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
            private Card _quit;
            public Card Quit => _quit ??= Hud.CreateChild(nameof(Quit), Hud.Canvas)
                .SetTextString("Quit <voffset=-0.07em><size=200%>-<size=75%> + <size=150%>+")
                .SetTMPPosition(-Cam.UIOrthoX + 1f, -Cam.UIOrthoY + .5f);
        }
    }

}
