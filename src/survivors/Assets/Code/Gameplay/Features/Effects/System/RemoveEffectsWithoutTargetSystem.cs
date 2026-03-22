using Entitas;

namespace Code.Gameplay.Features.Effects.System
{
    public class RemoveEffectsWithoutTargetSystem : IExecuteSystem
    {
        private IGroup<GameEntity> _effects;

        public RemoveEffectsWithoutTargetSystem(GameContext game)
        {
            _effects = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.DamageEffect,
                    GameMatcher.TargetID
                ));
        }

        public void Execute()
        {
            foreach (GameEntity effect in _effects.GetEntities())
            {
                GameEntity target = effect.Target();

                if (target == null)
                    effect.Destroy();
            }
        }
    }
}