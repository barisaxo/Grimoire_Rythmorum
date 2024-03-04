using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMenuScene
{
    public Transform TF { get; }
    public void Initialize();
    public void SelfDestruct();
}
