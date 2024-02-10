using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sea
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class LighthousePrefab : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider _col;
        public CapsuleCollider Col => _col;

        [SerializeField] private GameObject _light;
        public GameObject Light => _light;

        void Update()
        {
            if (Light != null && Light.activeInHierarchy)
            {
                Light.transform.Rotate(Time.deltaTime * 5 * Vector3.up);
            }
        }
    }
}
