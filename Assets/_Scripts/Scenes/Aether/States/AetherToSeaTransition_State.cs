using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherToSeaTransition_State : State
{
    protected override void EngageState()
    {
        Cam.Io.Camera.transform.SetParent(null);
        FadeToState(new SeaScene_State());
        base.EngageState();
    }

    protected override void DisengageState()
    {
        AetherScene.Io.SelfDestruct();
    }
}
