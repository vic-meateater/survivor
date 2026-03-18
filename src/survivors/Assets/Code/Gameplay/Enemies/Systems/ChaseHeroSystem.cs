using Code.Common.Extensions;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Enemies.Systems
{
    public class ChaseHeroSystem : IExecuteSystem
    {
        private IGroup<GameEntity> _enemies;
        private readonly IGroup<GameEntity> _heroes;

        public ChaseHeroSystem(GameContext game)
        {
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.Enemy, 
                GameMatcher.WorldPosition));
             _heroes = game.GetGroup(GameMatcher.Hero);
        }
        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            foreach (GameEntity enemy in _enemies.GetEntities())
            {
                Vector3 direction = hero.WorldPosition - enemy.WorldPosition;
                enemy.isMoving = direction.sqrMagnitude > 1.5f * 1.5f;
                enemy.ReplaceDirection(direction.normalized);
            }
        }
    }
}