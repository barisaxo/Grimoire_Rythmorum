using System;
using Sea;

public class SeaToCoveTransition_State : State
{
    protected override void PrepareState(Action callback)
    {
        WorldMapScene.Io.SelfDestruct();
        base.PrepareState(callback);
    }
    protected override void EngageState()
    {
        SetState(new NewCoveScene_State());
    }

}
