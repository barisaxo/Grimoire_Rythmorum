using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSceneTest_State : State
{

    protected override void PrepareState(Action callback)
    {
        SeaScene = new();
        base.PrepareState(callback);
    }

    SeaScene SeaScene;



}