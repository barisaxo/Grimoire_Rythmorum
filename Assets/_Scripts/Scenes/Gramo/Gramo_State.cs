using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gramo_State : State
{
    public GramoScene Scene;
    readonly State SubsequentState;
    public Gramo_State(State subsequentState) { SubsequentState = subsequentState; }

    protected override void PrepareState(Action callback)
    {
        Scene = new();
        // new(new MusicTheory.HarmonicFunction[] {
        //     MusicTheory.HarmonicFunction.Predominant,
        //     MusicTheory.HarmonicFunction.Tonic,
        //     MusicTheory.HarmonicFunction.Predominant,
        //     MusicTheory.HarmonicFunction.Dominant,
        // });
        Scene.DollyAnimation(callback);
    }

    protected override void EngageState()
    {
        Scene.CurSelection = Scene.Gramo.AnswerMesh2;
        Scene.HighlightDial();
    }

    protected override void DisengageState()
    {
        Scene.SelfDestruct();
    }

    protected override void EastPressed()
    {
        if (Scene.AllAnswered())
            if (Scene.CorrectAnswers())
                SetState(new CameraPan_State(
                    new DialogStart_State(
                        new EndGramo_Dialogue(
                            true,
                            SubsequentState,
                            100)),//TODOTODO needs scale
                    pan: Cam.StoredCamRot,
                    strafe: Cam.StoredCamPos,
                    speed: 5));
            else Debug.Log("Narp");
        else Debug.Log("Not all dials have been answered");
    }

    protected override void DirectionPressed(Dir dir)
    {
        switch (dir)
        {
            case Dir.Up:
                Scene.CurSelection = Scene.Gramo.ScrollDials(Scene.CurSelection, Dir.Up);
                Scene.HighlightDial();
                break;

            case Dir.Down:
                Scene.CurSelection = Scene.Gramo.ScrollDials(Scene.CurSelection, Dir.Down);
                Scene.HighlightDial();
                break;

            case Dir.Left:
                if (Scene.GetSpinningBool(Scene.CurSelection)) return;
                Scene.SetAnswer(Scene.ChangeAnswer(Dir.Left));
                Scene.SpinLeft(Scene.CurSelection).StartCoroutine();
                break;

            case Dir.Right:
                if (Scene.GetSpinningBool(Scene.CurSelection)) return;
                Scene.SetAnswer(Scene.ChangeAnswer(Dir.Right));
                Scene.SpinRight(Scene.CurSelection).StartCoroutine();
                break;
        }

        if (Scene.AllAnswered()) _ = Scene.ConfirmButton;
    }


}

public class GramoPractice_State : State
{
    public GramoScene Scene;
    readonly State SubsequentState;
    public GramoPractice_State(State subsequentState) { SubsequentState = subsequentState; }

    protected override void PrepareState(Action callback)
    {
        Scene = new();
        // new(new MusicTheory.HarmonicFunction[] {
        //     MusicTheory.HarmonicFunction.Predominant,
        //     MusicTheory.HarmonicFunction.Tonic,
        //     MusicTheory.HarmonicFunction.Predominant,
        //     MusicTheory.HarmonicFunction.Dominant,
        // });
        Scene.DollyAnimation(callback);
    }

    protected override void EngageState()
    {
        Scene.CurSelection = Scene.Gramo.AnswerMesh2;
        Scene.HighlightDial();
    }

    protected override void DisengageState()
    {
        Scene.SelfDestruct();
    }

    protected override void EastPressed()
    {
        if (Scene.AllAnswered())
            if (Scene.CorrectAnswers())
                SetState(new CameraPan_State(
                    new DialogStart_State(
                        new EndGramo_Dialogue(SubsequentState)),
                    pan: Cam.StoredCamRot,
                    strafe: Cam.StoredCamPos,
                    speed: 5));
            else Debug.Log("Narp");
        //TODO Bump sfx
        else Debug.Log("Not all dials have been answered");
    }

    protected override void DirectionPressed(Dir dir)
    {
        switch (dir)
        {
            case Dir.Up:
                Scene.CurSelection = Scene.Gramo.ScrollDials(Scene.CurSelection, Dir.Up);
                Scene.HighlightDial();
                break;

            case Dir.Down:
                Scene.CurSelection = Scene.Gramo.ScrollDials(Scene.CurSelection, Dir.Down);
                Scene.HighlightDial();
                break;

            case Dir.Left:
                if (Scene.GetSpinningBool(Scene.CurSelection)) return;
                Scene.SetAnswer(Scene.ChangeAnswer(Dir.Left));
                Scene.SpinLeft(Scene.CurSelection).StartCoroutine();
                break;

            case Dir.Right:
                if (Scene.GetSpinningBool(Scene.CurSelection)) return;
                Scene.SetAnswer(Scene.ChangeAnswer(Dir.Right));
                Scene.SpinRight(Scene.CurSelection).StartCoroutine();
                break;
        }

        if (Scene.AllAnswered()) _ = Scene.ConfirmButton;
    }


}
