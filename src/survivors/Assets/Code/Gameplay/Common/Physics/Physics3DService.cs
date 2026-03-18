using System.Collections.Generic;
using Code.Gameplay.Common.Collisions;
using UnityEngine;

namespace Code.Gameplay.Common.Physics
{
    public class Physics3DService : IPhysics3DService
    {
         private static readonly RaycastHit[] Hits = new RaycastHit[128];
        private static readonly Collider[] OverlapHits = new Collider[128];

        private readonly ICollisionRegistry _collisionRegistry;

        public Physics3DService(ICollisionRegistry collisionRegistry)
        {
            _collisionRegistry = collisionRegistry;
        }

        public GameEntity Raycast(Vector3 origin, Vector3 direction, float distance, int layerMask)
        {
            int hitCount = UnityEngine.Physics.RaycastNonAlloc(origin, direction, Hits, distance, layerMask);

            for (int i = 0; i < hitCount; i++)
            {
                if (Hits[i].collider == null) continue;

                GameEntity entity = _collisionRegistry.Get<GameEntity>(Hits[i].collider.GetInstanceID());
                if (entity != null)
                    return entity;
            }

            return null;
        }

        public IEnumerable<GameEntity> RaycastAll(Vector3 origin, Vector3 direction, float distance, int layerMask)
        {
            int hitCount = UnityEngine.Physics.RaycastNonAlloc(origin, direction, Hits, distance, layerMask);

            for (int i = 0; i < hitCount; i++)
            {
                if (Hits[i].collider == null) continue;

                GameEntity entity = _collisionRegistry.Get<GameEntity>(Hits[i].collider.GetInstanceID());
                if (entity != null)
                    yield return entity;
            }
        }

        public IEnumerable<GameEntity> OverlapSphere(Vector3 position, float radius, int layerMask)
        {
            int hitCount = UnityEngine.Physics.OverlapSphereNonAlloc(position, radius, OverlapHits, layerMask);

            DrawDebug(position, radius);

            for (int i = 0; i < hitCount; i++)
            {
                if (OverlapHits[i] == null) continue;

                GameEntity entity = _collisionRegistry.Get<GameEntity>(OverlapHits[i].GetInstanceID());
                if (entity != null)
                    yield return entity;
            }
        }

        public int OverlapSphereNonAlloc(Vector3 position, float radius, int layerMask, GameEntity[] buffer)
        {
            int hitCount = UnityEngine.Physics.OverlapSphereNonAlloc(position, radius, OverlapHits, layerMask);

            int count = 0;
            for (int i = 0; i < hitCount; i++)
            {
                if (OverlapHits[i] == null) continue;

                GameEntity entity = _collisionRegistry.Get<GameEntity>(OverlapHits[i].GetInstanceID());
                if (entity == null) continue;

                if (count < buffer.Length)
                    buffer[count++] = entity;
            }

            return count;
        }

        private static void DrawDebug(Vector3 position, float radius)
        {
#if UNITY_EDITOR
            Debug.DrawRay(position, Vector3.up * radius, Color.green, 1f);
            Debug.DrawRay(position, Vector3.down * radius, Color.green, 1f);
            Debug.DrawRay(position, Vector3.left * radius, Color.green, 1f);
            Debug.DrawRay(position, Vector3.right * radius, Color.green, 1f);
            Debug.DrawRay(position, Vector3.forward * radius, Color.green, 1f);
            Debug.DrawRay(position, Vector3.back * radius, Color.green, 1f);
#endif
        }
    }
}