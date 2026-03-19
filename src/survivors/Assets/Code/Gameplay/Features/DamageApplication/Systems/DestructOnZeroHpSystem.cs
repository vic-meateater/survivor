using Entitas;

namespace Code.Gameplay.Features.DamageApplication.Systems
{
    public class DestructOnZeroHpSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public DestructOnZeroHpSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.CurrentHP);
        }
        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                if (entity.CurrentHP <= 0)
                    entity.isDestructed = true;
            }
        }
    }
}