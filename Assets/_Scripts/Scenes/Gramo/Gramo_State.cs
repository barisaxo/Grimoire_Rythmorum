using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gramo_State : State
{
    public GramoScene Scene;

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
        Scene.HighlightDial();
    }

    protected override void DisengageState()
    {
        Scene.SelfDestruct();
    }

    protected override void EastPressed()
    {
        if (Scene.AllAnswered() && Scene.CorrectAnswers())
            Debug.Log("Great Jorb!");
        else Debug.Log("Narp");
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
                Debug.Log((Scene.GetSpinningBool(Scene.CurSelection)));
                Debug.Log("left");
                if (Scene.GetSpinningBool(Scene.CurSelection)) return;
                Scene.SetAnswer(Scene.ChangeAnswer(Dir.Left));
                Scene.SpinLeft(Scene.CurSelection).StartCoroutine();
                break;

            case Dir.Right:
                if (Scene.GetSpinningBool(Scene.CurSelection)) return;
                Debug.Log("right");
                Scene.SetAnswer(Scene.ChangeAnswer(Dir.Right));
                Scene.SpinRight(Scene.CurSelection).StartCoroutine();
                break;
        }

        if (Scene.AllAnswered()) _ = Scene.ConfirmButton;
    }


}
