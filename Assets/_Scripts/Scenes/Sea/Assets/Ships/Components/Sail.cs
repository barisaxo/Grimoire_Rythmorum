using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Sail : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    public MeshRenderer Mesh => _mesh;

    /// <summary>
    /// Modify the x of the transform scale to inflate/deflate the Sail.
    /// </summary>
    [HideInInspector] public const float Neutral = -0.09f;

    /// <summary>
    /// Modify the x of the transform scale to inflate/deflate the Sail.
    /// </summary>
    [HideInInspector] public const float Max = 1;

    /// <summary>
    /// How inflated you want the sail, 0f - 1f (internally clamped). 
    /// </summary>
    public void InflateSailLeft(float scale)
    {
        transform.localScale = new(ClampedScale(), 1, 1);

        float ClampedScale() =>
        Max * (scale < Neutral ? Neutral : scale > 1 ? 1 : scale);
    }

    /// <summary>
    /// How inflated you want the sail, 0f - 1f (internally clamped). 
    /// </summary>
    public void InflateSailRight(float scale)
    {
        transform.localScale = new(ClampedScale(), 1, 1);

        float ClampedScale() =>
        -Neutral * (scale > -Neutral ? -Neutral : scale < -1 ? -1 : scale);
    }


}
