// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Data;
// using Data.Inventory;
// using Menus.One;


// namespace Menus.Inventory
// {
//        public class StarChartsMenu : IMenu
//        {
//               public StarChartsMenu(StarChartsData data, QuestData questData, State subsequentState, IHeaderMenu menu)
//               {
//                      Data = data;
//                      QuestData = questData;
//                      SubsequentState = subsequentState;
//                      Menu = menu;
//               }

//               readonly IHeaderMenu Menu;
//               readonly State SubsequentState;
//               public IData Data { get; }
//               readonly QuestData QuestData;
//               public MenuItem Selection { get; set; }
//               public MenuItem[] MenuItems { get; set; }
//               public Card Description { get; set; }
//               public IMenuLayout Layout { get; } = new LeftScroll();

//               public string DisplayData(DataEnum item)
//               {
//                      return item.Name + ": " + Data.GetDisplayLevel(item);
//               }

//               public string GetDescription { get => Selection.Item.Description; }

//               public IInputHandler Input => new MenuInputHandler()
//               {
//                      Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
//                      Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
//               };

//               // private void IncreaseItem()
//               // {
//               //     Data.IncreaseLevel(Selection.Item);
//               //     Selection.Card.SetTextString(DisplayData(Selection.Item));
//               // }

//               // private void DecreaseItem()
//               // {
//               //     Data.DecreaseLevel(Selection.Item);
//               //     Selection.Card.SetTextString(DisplayData(Selection.Item));
//               // }

//               public State ConsequentState => QuestData.GetLevel(QuestData.DataItem.StarChart) == 1 ?
//                   new DialogStart_State(new OverrideQuest_Dialogue(GetPuzzleState, new Menu_State(Menu as IHeaderMenu))) :
//                   GetPuzzleState;

//               State GetPuzzleState => new Puzzle_State(
//                     GetPuzzle(),
//                     GetPuzzleType(),
//                     SubsequentState,
//                     (StarChartsData.DataItem)Selection.Item);

//               public IMenuScene Scene => null;

//               IPuzzle GetPuzzle() => Selection.Item switch
//               {
//                      _ when Selection.Item == StarChartsData.DataItem.NotesT ||
//                             Selection.Item == StarChartsData.DataItem.NotesA => new NotePuzzle(),

//                      _ when Selection.Item == StarChartsData.DataItem.StepsT ||
//                             Selection.Item == StarChartsData.DataItem.StepsA => new StepsPuzzle(),

//                      _ when Selection.Item == StarChartsData.DataItem.InvertedTriadsT ||
//                             Selection.Item == StarChartsData.DataItem.InvertedTriadsA => new InvertedTriadPuzzle(),

//                      _ when Selection.Item == StarChartsData.DataItem.ModesT ||
//                             Selection.Item == StarChartsData.DataItem.ModesA => new ModePuzzle(),

//                      _ when Selection.Item == StarChartsData.DataItem.IntervalsT ||
//                             Selection.Item == StarChartsData.DataItem.IntervalsA => new IntervalPuzzle(),

//                      _ when Selection.Item == StarChartsData.DataItem.InversionsT ||
//                             Selection.Item == StarChartsData.DataItem.InversionsA => new InvertedIntervalPuzzle(),

//                      _ when Selection.Item == StarChartsData.DataItem.TriadsT ||
//                             Selection.Item == StarChartsData.DataItem.TriadsA => new TriadPuzzle(),

//                      _ when Selection.Item == StarChartsData.DataItem.ScalesT ||
//                             Selection.Item == StarChartsData.DataItem.ScalesA => new ScalePuzzle(),

//                      _ when Selection.Item == StarChartsData.DataItem.SeventhChordsA ||
//                             Selection.Item == StarChartsData.DataItem.SeventhChordsT => new SeventhChordPuzzle(),

//                      _ when Selection.Item == StarChartsData.DataItem.Inverted7thChordsA ||
//                             Selection.Item == StarChartsData.DataItem.Inverted7thChordsT => new InvertedSeventhChordPuzzle(),

//                      _ => throw new System.ArgumentException(),
//               };

//               PuzzleType GetPuzzleType() => Selection.Item switch
//               {
//                      _ when
//                             Selection.Item == StarChartsData.DataItem.NotesT ||
//                             Selection.Item == StarChartsData.DataItem.Inverted7thChordsT ||
//                             Selection.Item == StarChartsData.DataItem.SeventhChordsT ||
//                             Selection.Item == StarChartsData.DataItem.StepsT ||
//                             Selection.Item == StarChartsData.DataItem.IntervalsT ||
//                             Selection.Item == StarChartsData.DataItem.ModesT ||
//                             Selection.Item == StarChartsData.DataItem.ScalesT ||
//                             Selection.Item == StarChartsData.DataItem.InversionsT
//                                    => PuzzleType.Theory,

//                      _ => PuzzleType.Aural,
//               };
//        }
// }