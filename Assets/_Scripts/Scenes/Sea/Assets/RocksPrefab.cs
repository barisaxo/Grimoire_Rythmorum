using UnityEngine;

namespace Sea
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class RocksPrefab : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider _col;
        public CapsuleCollider Col => _col;
    }

}