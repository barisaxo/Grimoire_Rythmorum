using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoveToSeaTransition_State : State
{
    protected override void EngageState()
    {
        Cam.Io.Camera.transform.SetParent(null);
        SetState(new SeaScene_State() { Fade = true });
        base.EngageState();
    }

    protected override void DisengageState()
    {
        CoveScene.Io.SelfDestruct();
    }
}
