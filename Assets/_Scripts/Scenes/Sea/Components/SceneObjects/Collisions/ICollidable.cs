using UnityEngine;

namespace Sea
{
    public interface ICollidable
    {
        public CapsuleCollider GetCollider { get; }
        public Vector2 CollisionResults(WorldMapScene scene, ITelemeter telemeter, Vector2 dir, RaycastHit hit);
    }

    public class NotCollidable : ICollidable
    {
        public NotCollidable(CapsuleCollider col) { GetCollider = col; }
        public CapsuleCollider GetCollider { get; private set; }
        public Vector2 CollisionResults(WorldMapScene scene, ITelemeter dm, Vector2 dir, RaycastHit hit) => dir;
    }

    public class RockCollision : ICollidable
    {
        public RockCollision(CapsuleCollider collider) => _collider = collider;

        private readonly CapsuleCollider _collider;
        public CapsuleCollider GetCollider => _collider;

        public Vector2 CollisionResults(WorldMapScene scene, ITelemeter telemeter, Vector2 dir, RaycastHit hit)
        {
            if (telemeter.IsInRange(GetCollider, scene.Ship.CapsuleCollider))
            {
                (Vector2 a, Vector2 b) = telemeter.ClosestPoints(GetCollider, scene.Ship.CapsuleCollider);

                if (Vector2.Distance(a, b) < .85f)
                {
                    Vector2 forward2D = new(scene.Ship.CapsuleCollider.transform.forward.x, scene.Ship.CapsuleCollider.transform.forward.z);
                    Vector2 shipPos = new(scene.Ship.CapsuleCollider.transform.position.x, scene.Ship.CapsuleCollider.transform.position.z);

                    if (IsMovingTowards(shipPos, forward2D * dir.y, b, 45))
                    {
                        Vector3 leftV3 = scene.Ship.GO.transform.rotation * Vector3.left;
                        Vector3 rightV3 = scene.Ship.GO.transform.rotation * Vector3.right;
                        Vector2 leftV2 = new(leftV3.x, leftV3.z);
                        Vector2 rightV2 = new(rightV3.x, rightV3.z);

                        dir.x += Vector2.Distance(a + leftV2, b) > Vector2.Distance(a + rightV2, b) ? -1 : 1;
                        dir.y *= Vector2.Distance(a, b);
                    }
                }
            }

            return dir;

            static bool IsMovingTowards(Vector2 transformPosition, Vector2 transformDirection, Vector2 targetPosition, float approach)
            {
                Vector2 toTarget = targetPosition - transformPosition;
                transformDirection.Normalize();
                toTarget.Normalize();
                float dotProduct = Vector2.Dot(transformDirection, toTarget);
                float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
                return angle.IsPOM(approach, 180);
            }
        }
    }


    public class ShipCollision : ICollidable
    {
        public ShipCollision(CapsuleCollider collider) => _collider = collider;

        private readonly CapsuleCollider _collider;
        public CapsuleCollider GetCollider => _collider;

        public Vector2 CollisionResults(WorldMapScene scene, ITelemeter _, Vector2 dir, RaycastHit hit)
        {
            int sign = hit.transform.rotation.eulerAngles.y.Smod(180).IsPOM(45, 90) ? -1 : 1;

            switch (scene.Ship.RotY)
            {
                case < 90:
                    dir.x += ((sign == 1 && scene.Ship.GO.transform.position.x < hit.transform.position.x) ||
                              (sign == -1 && scene.Ship.GO.transform.position.z < hit.transform.position.z)) ?
                        sign * -1 : sign;
                    break;

                case < 180:
                    dir.x += ((sign == 1 && scene.Ship.GO.transform.position.x > hit.transform.position.x) ||
                              (sign == -1 && scene.Ship.GO.transform.position.z < hit.transform.position.z)) ?
                        sign * -1 : sign;
                    break;

                case < 270:
                    dir.x += ((sign == 1 && scene.Ship.GO.transform.position.x > hit.transform.position.x) ||
                              (sign == -1 && scene.Ship.GO.transform.position.z > hit.transform.position.z)) ?
                        sign * -1 : sign;
                    break;

                case < 361:
                    dir.x += ((sign == 1 && scene.Ship.GO.transform.position.x < hit.transform.position.x) ||
                              (sign == -1 && scene.Ship.GO.transform.position.z > hit.transform.position.z)) ?
                        sign *= -1 : sign;
                    break;
            }

            if (hit.distance < .15f)
            {
                dir.y *= dir.y > 0 ? hit.distance : hit.distance * 1.5f;
            }

            return dir;
        }
    }
}
