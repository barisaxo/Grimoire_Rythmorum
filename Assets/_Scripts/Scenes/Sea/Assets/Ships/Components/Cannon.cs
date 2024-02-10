using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Cannon : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    public MeshRenderer Mesh => _mesh;
}
