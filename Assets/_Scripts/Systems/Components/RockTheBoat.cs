using System.Collections.Generic;
using UnityEngine;

public class RockTheBoat
{
    private readonly List<(Transform transform, float amp, float period)> Boats = new();
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
    public void AddBoat(Transform t)
    {
        Boats.Add((
            transform: t,
            amp: Random.Range(7f, 9f),
            period: Random.value + .5f));
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
        foreach (var (transform, amp, period) in Boats)
            transform.rotation =
                Quaternion.Euler(new Vector3(
                    transform.localEulerAngles.x,
                    transform.localEulerAngles.y,
                    Mathf.Sin(Time.time * period) * amp));
    }
}

