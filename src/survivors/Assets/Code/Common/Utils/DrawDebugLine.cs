#if UNITY_EDITOR
using Entitas;
using UnityEngine;

namespace Code.Common.Utils
{
    public class DrawDebugLine : MonoBehaviour
    {
        private IGroup<GameEntity> _entities;

        private void Start()
        {
            _entities = Contexts.sharedInstance.game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Radius,
                    GameMatcher.TargetBuffer
                ));
        }

        private void OnDrawGizmos()
        {
            if (_entities == null) return;

            Gizmos.color = Color.green;

            foreach (GameEntity entity in _entities)
            {
                Gizmos.DrawWireSphere(entity.WorldPosition, entity.Radius);
            }
        }
    }
}
#endif