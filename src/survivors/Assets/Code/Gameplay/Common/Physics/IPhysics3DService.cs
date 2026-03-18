using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Common.Physics
{
    public interface IPhysics3DService
    {
        GameEntity Raycast(Vector3 origin, Vector3 direction, float distance, int layerMask);
        IEnumerable<GameEntity> RaycastAll(Vector3 origin, Vector3 direction, float distance, int layerMask);
        IEnumerable<GameEntity> OverlapSphere(Vector3 position, float radius, int layerMask);
        int OverlapSphereNonAlloc(Vector3 position, float radius, int layerMask, GameEntity[] buffer);
    }
}