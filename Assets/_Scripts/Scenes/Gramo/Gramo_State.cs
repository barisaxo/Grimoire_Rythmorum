using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gramophones;

//TODO HUD & Info (like in proto-bard)
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
        //     MusicTheory.HarmonicFunction.Subdominant,
        //     MusicTheory.HarmonicFunction.Tonic,
        //     MusicTheory.HarmonicFunction.Subdominant,
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

    protected override void SelectPressed()
    {
        SetState(new CameraPan_State(
                    new DialogStart_State(
                        new EndGramo_Dialogue(SubsequentState, false)),
                    pan: Cam.StoredCamRot,
                    strafe: Cam.StoredCamPos,
                    speed: 5));
    }


    protected override void EastPressed()
    {
        if (Scene.AllAnswered())
            if (Scene.CorrectAnswers())
            {
                DataManager.Player.AdjustLevel(new Datum.GramoSolved(), 1);

                DataManager.Player.AdjustLevel(new Datum.PatternsFound(), (int)(1000f *
                                DataManager.Skill.GetBonusRatio(new Datum.Apophenia()) *
                                (Gramo.Id + 1) *
                                (UnityEngine.Random.value + 1f)
                                ));
                SetState(new CameraPan_State(
                    new DialogStart_State(
                        new EndGramo_Dialogue(
                            won: true,
                            subsequentState: SubsequentState,
                            patternsFound: (int)(1000f *
                                DataManager.Skill.GetBonusRatio(new Datum.Apophenia()) *
                                (Gramo.Id + 1) *
                                (UnityEngine.Random.value + 1f)
                                ))),
                    pan: Cam.StoredCamRot,
                    strafe: Cam.StoredCamPos,
                    speed: 5));
            }
            else
            {
                DataManager.Player.AdjustLevel(new Datum.GramoFailed(), 1);

                SetState(new CameraPan_State(
                    new DialogStart_State(
                        new EndGramo_Dialogue(
                            won: false,
                            subsequentState: SubsequentState,
                            patternsFound: 0
                            )),
                    pan: Cam.StoredCamRot,
                    strafe: Cam.StoredCamPos,
                    speed: 5));
            }
        else Audio.SFX.PlayOneShot(BatterieAssets.MissStick);
    }

    protected override void DirectionPressed(Dir dir)
    {
        switch (dir)
        {
            case Dir.Up:
                Scene.CurSelection = Scene.Gramo.ScrollDials(Scene.CurSelection, dir);
                Scene.HighlightDial();
                break;

            case Dir.Down:
                Scene.CurSelection = Scene.Gramo.ScrollDials(Scene.CurSelection, dir);
                Scene.HighlightDial();
                break;

            case Dir.Left:
                if (Scene.GetSpinningBool(Scene.CurSelection)) return;
                Scene.SetAnswer(Scene.ChangeAnswer(dir, Scene.GetCurrentAnswer()));
                Scene.SpinLeft(Scene.CurSelection).StartCoroutine();
                break;

            case Dir.Right:
                if (Scene.GetSpinningBool(Scene.CurSelection)) return;
                Scene.SetAnswer(Scene.ChangeAnswer(dir, Scene.GetCurrentAnswer()));
                Scene.SpinRight(Scene.CurSelection).StartCoroutine();
                break;
        }

        if (Scene.AllAnswered()) _ = Scene.GramoHUD.ConfirmButton;
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

    protected override void SelectPressed()
    {
        SetState(new CameraPan_State(
                    new DialogStart_State(
                        new EndPractice_Dialogue(false, SubsequentState)),
                    pan: Cam.StoredCamRot,
                    strafe: Cam.StoredCamPos,
                    speed: 5));
    }

    protected override void EastPressed()
    {
        if (Scene.AllAnswered())
            if (Scene.CorrectAnswers())
                SetState(new CameraPan_State(
                    new DialogStart_State(
                        new EndPractice_Dialogue(true, SubsequentState)),
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
                Scene.SetAnswer(Scene.ChangeAnswer(Dir.Left, Scene.GetCurrentAnswer()));
                Scene.SpinLeft(Scene.CurSelection).StartCoroutine();
                break;

            case Dir.Right:
                if (Scene.GetSpinningBool(Scene.CurSelection)) return;
                Scene.SetAnswer(Scene.ChangeAnswer(Dir.Right, Scene.GetCurrentAnswer()));
                Scene.SpinRight(Scene.CurSelection).StartCoroutine();
                break;
        }

        if (Scene.AllAnswered()) _ = Scene.GramoHUD.ConfirmButton;
    }


}
