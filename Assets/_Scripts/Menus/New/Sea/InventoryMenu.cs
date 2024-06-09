using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Two;

namespace Menus.Two
{
    public class InventoryMenu : IMenu
    {
        public InventoryMenu(InventoryData data, State subsequentState, IHeaderMenu menu)
        {
            Data = data;
            SubsequentState = subsequentState;
            HeaderMenu = menu;
        }

        public IData Data { get; }
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();
        readonly State SubsequentState;
        readonly IHeaderMenu HeaderMenu;
        public string GetDescription { get => Selection.Item.Description; }

        public string DisplayData(IItem item)
        {
            return item.Name + ": " + Data.GetDisplayLevel(item);
        }

        public IInputHandler Input => new MenuInputHandler()
        {
            Up = new ButtonInput(() =>
            {
                Selection = Layout.ScrollMenuItems(Dir.Up, this);
                ShowHideEast();
            }),

            Down = new ButtonInput(() =>
            {
                Selection = Layout.ScrollMenuItems(Dir.Down, this);
                ShowHideEast();
            }),

            South = new ButtonInput(() =>
            {
                ConsequentState = SubsequentState;
            }),

            East = new ButtonInput(() =>
            {
                ConsequentState = GetState;
            })
        };

        public IMenuScene Scene { get; } = new InventoryMenuScene();

        public State ConsequentState { get; set; }

        State GetState
        {
            get
            {
                if (!(Data.GetLevel(Selection.Item) > 0)) return null;

                if (Selection.Item is StarChart)
                    if (Manager.Io.Quests.GetLevel(new Navigation()) == 1)
                        return new DialogStart_State(new OverrideQuest_Dialogue(GetPuzzleState, new MenuState(HeaderMenu)));
                    else return GetPuzzleState;

                if (Selection.Item is Gramophone)
                    return new NewGramoPuzzle_State(SubsequentState, isPractice: false);

                return null;
            }
        }

        State GetPuzzleState
        {
            get
            {
                var puzzle = (IStarChart)new Sea.StarChartDifficultySetter(Manager.Io).DifficultyLevel;

                return new Puzzle_State(
                    GetPuzzle(puzzle),
                    GetPuzzleType(),
                    SubsequentState);
            }
        }

        void ShowHideEast()
        {
            if (Selection.Item is Gramophone or StarChart && (Data.GetLevel(Selection.Item) > 0))
                Scene.East.SetImageColor(Color.white).SetTextColor(Color.white);
            else
                Scene.East.SetImageColor(Color.clear).SetTextColor(Color.clear);
        }

        IPuzzle GetPuzzle(IStarChart starChart) => starChart switch
        {
            NotesT or NotesA => new NotePuzzle(),
            StepsT or StepsA => new StepsPuzzle(),
            TriadsT or TriadsA => new TriadPuzzle(),
            InvertedTriadsT or InvertedTriadsA => new InvertedTriadPuzzle(),
            IntervalsT or IntervalsA => new IntervalPuzzle(),
            ScalesT or ScalesA => new ScalePuzzle(),
            ModesT or ModesA => new ModePuzzle(),
            InversionsT or InversionsA => new InvertedIntervalPuzzle(),
            SeventhChordsA or SeventhChordsT => new SeventhChordPuzzle(),
            Inverted7thChordsA or Inverted7thChordsT => new InvertedSeventhChordPuzzle(),
            _ => throw new System.ArgumentException(Selection.Item.Name),
        };

        PuzzleType GetPuzzleType() => Selection.Item switch
        {
            NotesT or Inverted7thChordsT or
            SeventhChordsT or StepsT or IntervalsT or
            ModesT or ScalesT or InversionsT
            or InvertedTriadsT or TriadsT => PuzzleType.Theory,

            _ => PuzzleType.Aural,
        };


        public class InventoryMenuScene : IMenuScene
        {
            // public InventoryMenuScene() { Initialize(); }

            public void Initialize()
            {
                South.SetTextString("Back").SetImageColor(Color.white);
                East.SetTextString("Solve").SetImageColor(Color.clear).SetTextColor(Color.clear);
                ((IMenuScene)this).SetCardPos1(South);
                ((IMenuScene)this).SetCardPos2(East);
            }

            public void SelfDestruct()
            {
                Debug.Log(nameof(SelfDestruct));
                Hud?.SelfDestruct();
                Resources.UnloadUnusedAssets();
            }

            public Transform TF => null;

            public Card Hud { get; set; }
            public Card North { get; set; }
            public Card East { get; set; }
            public Card South { get; set; }
            public Card West { get; set; }
        }
    }
}