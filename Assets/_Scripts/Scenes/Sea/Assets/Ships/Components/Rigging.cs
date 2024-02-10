using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigging : MonoBehaviour
{
    [SerializeField] private GameObject _riggingMesh;
    public GameObject RiggingMesh => _riggingMesh;

    public Material RiggingMat;
}
