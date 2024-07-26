using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class NoteIDTutorialDialogue : Dialogue
{
    public NoteIDTutorialDialogue(State consequentState) : base(consequentState) { }

    public override Dialogue Initiate()
    {
        MuteTypingSounds();
        FirstLine = StartLine;
        FirstLine.Responses[0].SetGoToLine(StartLine);
        return this;
    }

    Line _startLine;
    Line StartLine => _startLine ??= new Line("Note Identification", Replies())
        .SetVideoClip(Assets.NoteID)
    ;

    Response[] Replies() => new Response[]{
        new ("Play again"),
        new ("Try", new StarChartPractice_State(
            new NotePuzzle(),
            PuzzleType.Theory,
            ConsequentState
            )),
        new ("Quit", new FinishedRhythmCellTutorial_Dialogue(ConsequentState))
    };
}
