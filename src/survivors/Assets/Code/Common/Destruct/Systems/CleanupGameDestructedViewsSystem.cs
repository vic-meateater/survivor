using Entitas;
using UnityEngine;

namespace Code.Common.Destruct.Systems
{
    public class CleanupGameDestructedViewsSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public CleanupGameDestructedViewsSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Destructed,
                    GameMatcher.View));
        }

        public void Cleanup()
        {
            foreach (GameEntity entity in _entities.GetEntities())
            {
                entity.View.ReleaseEntity();
                Object.Destroy(entity.View.gameObject);
            }
        }
    }
}