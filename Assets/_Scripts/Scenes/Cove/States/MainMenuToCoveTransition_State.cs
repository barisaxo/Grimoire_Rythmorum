using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuToCoveTransition_State : State
{
    MainMenuToCoveTransition_State()
    {
        Fade = true;
    }
    protected override void EngageState()
    {
        SetState(new SeaScene_State());
    }
}
