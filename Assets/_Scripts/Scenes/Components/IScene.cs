using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScene
{
    public IScene Initialize()
    {
        Hud.Initialize();
        SetUp();
        return this;
    }

    public void SelfDestruct()
    {
        Hud.SelfDestruct();
        Destroy();
    }

    protected void SetUp();
    protected void Destroy();

    public IHud Hud { get; }
}