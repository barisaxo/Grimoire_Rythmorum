using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Two;

namespace Menus.Two
{
    public class StarChartsMenu : IMenu
    {
        public StarChartsMenu(StarChartData data, QuestData questData, State subsequentState, IHeaderMenu menu)//
        {
            Data = data;
            QuestData = questData;
            SubsequentState = subsequentState;
            Menu = menu;
        }

        readonly IHeaderMenu Menu;
        readonly State SubsequentState;
        public IData Data { get; }
        readonly QuestData QuestData;
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();

        public string DisplayData(IItem item)
        {
            return item.Name + ": " + Data.GetDisplayLevel(item);
        }

        public string GetDescription { get => Selection.Item.Description; }

        public IInputHandler Input => new MenuInputHandler()
        {
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
        };

        public IMenuScene Scene => null;

        public State ConsequentState =>
        Data.GetLevel(Selection.Item) > 0 ?
        QuestData.GetLevel(new Navigation()) == 1 ?
            new DialogStart_State(new OverrideQuest_Dialogue(GetPuzzleState, new MenuState(Menu as IHeaderMenu))) :
            GetPuzzleState :
            null;

        State GetPuzzleState => new Puzzle_State(
            GetPuzzle(),
            GetPuzzleType(),
            SubsequentState
            );

        IPuzzle GetPuzzle() => Selection.Item switch
        {
            NotesT or NotesA => new NotePuzzle(),
            StepsT or StepsA => new StepsPuzzle(),
            InvertedTriadsT or InvertedTriadsA => new InvertedTriadPuzzle(),
            ModesT or ModesA => new ModePuzzle(),
            IntervalsT or IntervalsA => new IntervalPuzzle(),
            InversionsT or InversionsA => new InvertedIntervalPuzzle(),
            TriadsT or TriadsA => new TriadPuzzle(),
            ScalesT or ScalesA => new ScalePuzzle(),
            SeventhChordsA or SeventhChordsT => new SeventhChordPuzzle(),
            Inverted7thChordsA or Inverted7thChordsT => new InvertedSeventhChordPuzzle(),
            _ => throw new System.ArgumentException(),
        };

        PuzzleType GetPuzzleType() => Selection.Item switch
        {
            NotesT or Inverted7thChordsT or
            SeventhChordsT or StepsT or IntervalsT or
            ModesT or ScalesT or InversionsT
            or InvertedTriadsT or TriadsT => PuzzleType.Theory,

            _ => PuzzleType.Aural,
        };
    }
}