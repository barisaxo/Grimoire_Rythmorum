using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHud
{
    public Card Parent { get; }
    public IHud Initialize();
    public void SelfDestruct() { Parent?.SelfDestruct(); }
}

public class NoHud : IHud
{
    public Card Parent { get; } = null;
    public IHud Initialize() => this;
}