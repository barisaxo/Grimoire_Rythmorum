using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Data.Inventory;
using Muscopa;


namespace Menus.Inventory
{
    public class StarChartsMenu : IMenu
    {
        public StarChartsMenu(StarChartsData data, State subsequentState)
        {
            Data = data;
            SubsequentState = subsequentState;
            UnityEngine.Debug.Log(subsequentState);
        }
        readonly State SubsequentState;
        public IData Data { get; }
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();

        public string DisplayData(DataEnum item)
        {
            return item.Name + ": " + Data.GetDisplayLevel(item);
        }

        public IInputHandler Input => new MenuInputHandler()
        {
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, Selection, MenuItems)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, Selection, MenuItems)),
            East = new ButtonInput(DecreaseItem)
        };

        private void IncreaseItem()
        {
            Data.IncreaseLevel(Selection.Item);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

        private void DecreaseItem()
        {
            Data.DecreaseLevel(Selection.Item);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

        public State ConsequentState => new Puzzle_State(
            GetPuzzle(),
            GetPuzzleType(),
            SubsequentState);

        public IMenuScene Scene => null;

        IPuzzle GetPuzzle() => Selection.Item switch
        {
            _ when Selection.Item == StarChartsData.DataItem.NotesT ||
                    Selection.Item == StarChartsData.DataItem.NotesA => new NotePuzzle(),
            _ => throw new System.ArgumentException(),
        };

        PuzzleType GetPuzzleType() => Selection.Item switch
        {
            _ when Selection.Item == StarChartsData.DataItem.NotesT => PuzzleType.Theory,
            _ when Selection.Item == StarChartsData.DataItem.NotesA => PuzzleType.Aural,
            _ => throw new System.ArgumentException(),
        };
    }
}