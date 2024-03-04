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
        public IDescription Description { get; }
        public IInstantiable Instantiator { get; }
    }

    public interface IDescription
    {
        public string Name { get; }
        public string Desc { get; }
    }

    public class SceneObjectDescription : IDescription
    {
        public SceneObjectDescription(string name)
        {
            Name = name;
            // Desc = desc;
        }
        public string Desc { get; private set; }
        public string Name { get; private set; }
    }

    public interface IInstantiable
    {
        public GameObject GetInstantiation();
        public GameObject ToInstantiate { get; }
        public Vector3 Scale { get; set; }
        public Vector3 Rot { get; set; }
    }

    // public class InstantiateFish : IInstantiable
    // {
    //     public InstantiateFish(GameObject toInstantiate)
    //     {
    //         ToInstantiate = toInstantiate;
    //     }

    //     public GameObject Instantiate()
    //     {
    //         return GameObject.Instantiate(ToInstantiate);
    //     }

    //     public GameObject ToInstantiate { get; private set; }
    //     public Vector3 Scale { get; set; }
    //     public Vector3 Rot { get; set; }
    // }

    public class ItemInstantiator : IInstantiable
    {
        public ItemInstantiator(GameObject toInstantiate, Vector3 scale, Vector3 rot)
        {
            ToInstantiate = toInstantiate;
            Scale = scale;
            Rot = rot;
        }

        // public InstantiateItem(GameObject toInstantiate)
        // {
        //     ToInstantiate = toInstantiate;
        // }

        public GameObject GetInstantiation()
        {
            return GameObject.Instantiate(ToInstantiate);
        }

        public GameObject ToInstantiate { get; private set; }
        public Vector3 Scale { get; set; }
        public Vector3 Rot { get; set; }
    }
}
