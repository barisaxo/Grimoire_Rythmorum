using UnityEngine;
using Data;

namespace Sea
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class SailFishPrefab : MonoBehaviour, IMAFish
    {
        public GameObject GO => gameObject;
        public GameObject ToInstantiate => Assets._sailFishPrefab.gameObject;

        [SerializeField] private CapsuleCollider _col;
        public CapsuleCollider Col => _col;

        public Data.IFish FishType => new SailFish();
    }
}