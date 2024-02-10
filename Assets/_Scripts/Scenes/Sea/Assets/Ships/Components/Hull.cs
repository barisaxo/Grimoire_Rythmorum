using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Hull : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    public MeshRenderer Mesh => _mesh;

    [SerializeField] private CapsuleCollider _col;
    public CapsuleCollider Col => _col;
}
