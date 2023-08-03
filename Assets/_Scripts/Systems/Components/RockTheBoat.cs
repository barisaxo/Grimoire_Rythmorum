using System.Collections.Generic;
using UnityEngine;

public class RockTheBoat
{
    private readonly List<(Transform transform, float amp, float period)> Boats = new();
    private bool _rocking;

    public bool Rocking
    {
        get => _rocking;
        set
        {
            if (_rocking = value) MonoHelper.OnUpdate += SetNewSwayPos;
            else MonoHelper.OnUpdate -= SetNewSwayPos;
        }
    }

    public void AddBoat(Transform t)
    {
        Boats.Add((
            transform: t,
            amp: Random.Range(7f, 9f),
            period: Random.value + .5f));
    }

    private void SetNewSwayPos()
    {
        foreach (var (transform, amp, period) in Boats)
            transform.rotation =
                Quaternion.Euler(new Vector3(
                    transform.localEulerAngles.x,
                    transform.localEulerAngles.y,
                    Mathf.Sin(Time.time * period) * amp));
    }
}

