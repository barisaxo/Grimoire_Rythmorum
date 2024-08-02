using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gramo_State : State
{
    public GramoScene Scene;
    readonly State SubsequentState;
    readonly Datum.IGramophone Gramo;
    public Gramo_State(State subsequentState, Datum.IGramophone gramo)
    {
        SubsequentState = subsequentState;
        Gramo = gramo;
    }

    protected override void PrepareState(Action callback)
    {
        Scene = new(Gramo);
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
                            won: true,
                            subsequentState: SubsequentState,
                            patternsFound: (int)(1000f *
                                DataManager.Skill.GetBonusRatio(new Datum.Apophenia()) *
                                (Gramo.ID + 1) *
                                (UnityEngine.Random.value + 1f)
                                ))),
                    pan: Cam.StoredCamRot,
                    strafe: Cam.StoredCamPos,
                    speed: 5));
            else SetState(new CameraPan_State(
                    new DialogStart_State(
                        new EndGramo_Dialogue(
                            won: false,
                            subsequentState: SubsequentState,
                            patternsFound: 0
                            )),
                    pan: Cam.StoredCamRot,
                    strafe: Cam.StoredCamPos,
                    speed: 5));
        else Audio.SFX.PlayOneShot(BatterieAssets.MissStick);
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
    readonly Datum.IGramophone Gramo;

    public GramoPractice_State(State subsequentState, Datum.IGramophone gramo)
    {
        Gramo = gramo;
        SubsequentState = subsequentState;
    }

    protected override void PrepareState(Action callback)
    {
        Scene = new(Gramo);
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
            else Audio.SFX.PlayOneShot(BatterieAssets.MissStick);
        else Audio.SFX.PlayOneShot(BatterieAssets.MissStick);
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
