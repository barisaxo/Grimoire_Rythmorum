using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sea
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class BottleWithScrollPrefab : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider _col;
        public CapsuleCollider Col => _col != null ? _col :
            _col = GetComponentInChildren<CapsuleCollider>() != null ?
            GetComponentInChildren<CapsuleCollider>() : _col.gameObject.AddComponent<CapsuleCollider>();
    }
}