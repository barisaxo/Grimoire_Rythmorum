using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheoryPuzzleState : State
{

    public TheoryPuzzleState()
    {
    }
    public DiatonicCircle Diatonic;
    public CircleOfFifths CircleOfFifths;
    protected override void PrepareState(Action callback)
    {

        Diatonic = new() { RotateSpeed = 1 };
        CircleOfFifths = new() { RotateSpeed = 1 };
        base.PrepareState(callback);
    }
    protected override void EngageState()
    {

        //RotateStars();
    }

    void RotateCircles()
    {
        Rotate().StartCoroutine();
        IEnumerator Rotate()
        {
            while (true)
            {
                yield return null;
                Diatonic.RotateCircle(Time.deltaTime);
                CircleOfFifths.RotateStars(Time.deltaTime);
            }
        }
    }


    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(Diatonic.Parent.GO.transform)) Diatonic.RotateCircle(-1);
        else if (go.transform.IsChildOf(CircleOfFifths.Parent.GO.transform)) CircleOfFifths.RotateStars(-1);
    }
}