using UnityEngine;

namespace Sea
{
    public interface ITelemeter
    {
        public Vector2 Origin(Transform tf) => new(tf.position.x, tf.position.z);
        public float GetDistance(Vector2 target, Transform tf) => Vector2.Distance(Origin(tf), target);
        public bool IsInRange(CapsuleCollider originCollider, CapsuleCollider targetCollider);
        public (Vector2 a, Vector2 b) ClosestPoints(CapsuleCollider originCollider, CapsuleCollider targetCollider);
    }

    public class RockTelemetry : ITelemeter
    {
        public bool IsInRange(CapsuleCollider originCollider, CapsuleCollider targetCollider)
        {
            return Vector3.Distance(originCollider.transform.position, targetCollider.transform.position) < 1.01f;
        }

        public (Vector2 a, Vector2 b) ClosestPoints(CapsuleCollider originCollider, CapsuleCollider targetCollider)
        {
            Vector3 a = targetCollider.ClosestPoint(originCollider.transform.position - (originCollider.transform.rotation * originCollider.center));
            Vector3 b = originCollider.ClosestPoint(targetCollider.transform.position - (targetCollider.transform.rotation * targetCollider.center));
            Vector3 a2 = targetCollider.ClosestPoint(b);
            Vector3 b2 = originCollider.ClosestPoint(a);
            Vector3 a3 = targetCollider.ClosestPoint(b2);
            Vector3 b3 = originCollider.ClosestPoint(a2);

            Vector2 a3v2 = new(a3.x, a3.z);
            Vector2 b3v2 = new(b3.x, b3.z);
            return (a3v2, b3v2);
        }
    }

    public class FishTelemetry : ITelemeter
    {
        public bool IsInRange(CapsuleCollider originCollider, CapsuleCollider targetCollider)
        {
            return Vector2.Distance(new(targetCollider.transform.position.x, targetCollider.transform.position.z),
                                    new(originCollider.transform.position.x, originCollider.transform.position.z)) < 1.01f;
        }

        public (Vector2 a, Vector2 b) ClosestPoints(CapsuleCollider originCollider, CapsuleCollider targetCollider)
        {
            return (new(targetCollider.transform.position.x, targetCollider.transform.position.z),
                    new(originCollider.transform.position.x, originCollider.transform.position.z));
        }
    }

    public class ShipTelemetry : ITelemeter
    {
        public bool IsInRange(CapsuleCollider originCollider, CapsuleCollider targetCollider)
        {
            return Vector2.Distance(new(targetCollider.transform.position.x, targetCollider.transform.position.z),
                                    new(originCollider.transform.position.x, originCollider.transform.position.z)) < 1.01f;
        }

        public (Vector2 a, Vector2 b) ClosestPoints(CapsuleCollider originCollider, CapsuleCollider targetCollider)
        {
            return (new(targetCollider.transform.position.x, targetCollider.transform.position.z),
                    new(originCollider.transform.position.x, originCollider.transform.position.z));
        }
    }

}
