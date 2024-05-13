using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGramoPuzzle_State : State
{
    public NewGramoPuzzle_State()
    {
        Fade = true;
    }
    protected override void PrepareState(Action callback)
    {
        Cam.Io.Camera.transform.SetPositionAndRotation(Vector3.up * 15, Quaternion.identity);
        base.PrepareState(callback);
    }
    protected override void EngageState()
    {
        SetState(new Gramo_State());
    }
}
