using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipSpecs
{
    public int Speed { get; }
    public int Range { get; }
    public int Storage { get; }
    public FishingType[] FishingTypes { get; }

}
public enum FishingType { Angling, Harpooning }