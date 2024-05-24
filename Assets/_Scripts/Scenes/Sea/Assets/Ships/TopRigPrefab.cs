using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ships
{
    public class TopRigPrefab : MonoBehaviour, Sea.IShipPrefab
    {
        [Header(nameof(Hull))]
        public Hull Hull;
        [Space(10)]

        [Header(nameof(Cannons))]
        public Cannon[] Cannons;

        [Header(nameof(Rig))]
        public Rig TopRig;


        public Hull _hull { get => Hull; }
        public Flag _flag { get => TopRig.Flag; }
        public Rig _rig { get => TopRig; }
        public Rigging _rigging { get => TopRig.Rigging; }
        public Sail[] _sails { get => TopRig.Sails; }
        public Mast[] _masts { get => TopRig.Masts; }
        public CapsuleCollider Collider { get => Hull.Col; }
        public Transform Transform => _hull.transform;
        public GameObject GO => gameObject;
    }
}
