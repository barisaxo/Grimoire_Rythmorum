using System.Collections.Generic;
using UnityEngine;

public class RockTheBoat
{
    private readonly Dictionary<Transform, (float amp, float period, float offset)> keyValuePairs = new();

    private readonly List<Transform> Boats = new();
    private readonly List<(float amp, float period, float offset)> BoatRots = new();

    private bool _rocking;
    /// <summary>
    /// Setting this (un)subscribes SetSwayPos from MonoHelper.OnUpdate.
    /// </summary>
    public bool Rocking
    {
        get => _rocking;
        set
        {
            if (_rocking == true && value == true) return;
            if (_rocking = value) MonoHelper.OnUpdate += SetSwayPos;
            else MonoHelper.OnUpdate -= SetSwayPos;
        }
    }

    /// <summary>
    /// This does NOT subscribe SetSwayPos to MonoHelper.OnUpdate.
    /// </summary>
    public void AddBoat(Transform t, (float amp, float period, float offset) sway)
    {
        if (Boats.Contains(t)) return;

        BoatRots.Add(sway);
        Boats.Add(t);

        keyValuePairs.TryAdd(t, sway);
    }

    /// <summary>
    /// This does NOT subscribe SetSwayPos to MonoHelper.OnUpdate.
    /// </summary>
    public void RemoveBoat(Transform t)
    {
        if (keyValuePairs.ContainsKey(t))
        {
            Boats.Remove(t);
            BoatRots.Remove(keyValuePairs.GetValueOrDefault(t));
            keyValuePairs.Remove(t);
        }
    }

    /// <summary>
    /// This also unsubscribes SetSwayPos from MonoHelper.OnUpdate.
    /// </summary>
    public void ClearBoats() { Rocking = false; Boats.Clear(); }

    /// <summary>
    /// This also unsubscribes SetSwayPos from MonoHelper.OnUpdate.
    /// </summary>
    public void SelfDestruct() { Rocking = false; ClearBoats(); }

    private void SetSwayPos()
    {
        foreach (var boat in keyValuePairs)
        {
            // boat.Key.transform.Rotate(Vector3.forward * Mathf.Sin(Time.time * boat.Value.period) * boat.Value.amp);
            boat.Key.transform.rotation =
             Quaternion.Euler(
                new Vector3(
                    0,
                    boat.Key.transform.rotation.eulerAngles.y,
                    Mathf.Sin((Time.time + boat.Value.offset) * boat.Value.period) * boat.Value.amp * 180));

        }
    }

}


