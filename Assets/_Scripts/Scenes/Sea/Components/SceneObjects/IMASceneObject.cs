using UnityEngine;

namespace Sea
{
    public interface ISceneObject
    {
        public ITelemeter Telemeter { get; }
        public ICollidable Collidable { get; }
        public IInteractable Interactable { get; }
        public ITriggerable Triggerable { get; }
        public IUpdatePosition UpdatePosition { get; }
        public GameObject GO { get; }
        public IInstantiable Instantiable { get; }
    }

    public interface IInstantiable
    {
        public void Instantiate();
    }
}
