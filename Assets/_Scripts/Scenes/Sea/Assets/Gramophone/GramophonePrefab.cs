using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class GramophonePrefab : MonoBehaviour
{
    [SerializeField] private CapsuleCollider _col;
    public CapsuleCollider Col => _col;
}
