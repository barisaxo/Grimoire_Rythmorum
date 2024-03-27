using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Inventory;

public static class PuzzleSelector
{
    // public static State WeightedRandomPuzzleState(this TheoryPuzzleData data, State subsequentState) => Random.Range(0, 35) switch
    // {
    //     < 4 => new Puzzle_State(new NotePuzzle(), RandPuzzleType, subsequentState, ),
    //     < 7 => new Puzzle_State(new StepsPuzzle(), RandPuzzleType, subsequentState),
    //     < 10 => new Puzzle_State(new TriadPuzzle(), RandPuzzleType, subsequentState),
    //     < 15 => new Puzzle_State(new InvertedTriadPuzzle(), RandPuzzleType, subsequentState),
    //     < 20 => new Puzzle_State(new ScalePuzzle(), RandPuzzleType, subsequentState),
    //     < 25 => new Puzzle_State(new ModePuzzle(), RandPuzzleType, subsequentState),
    //     < 30 => new Puzzle_State(new SeventhChordPuzzle(), RandPuzzleType, subsequentState),
    //     _ => new Puzzle_State(new InvertedSeventhChordPuzzle(), RandPuzzleType, subsequentState),
    // };

    // static PuzzleType RandPuzzleType => Random.value > .5f ? PuzzleType.Theory : PuzzleType.Aural;


}
