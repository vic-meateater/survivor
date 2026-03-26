using System;
using Entitas;

namespace Code.Gameplay.Features.Effects.System
{
    public class ProcessHealEffectSystem : IExecuteSystem
    {
        private IGroup<GameEntity> _effects;

        public ProcessHealEffectSystem(GameContext game)
        {
            _effects = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.HealEffect,
                    GameMatcher.EffectValue,
                    GameMatcher.TargetID
                ));
        }

        public void Execute()
        {
            foreach (GameEntity effect in _effects.GetEntities())
            {
                GameEntity target = effect.Target();
                
                effect.isProcessed = true;
                
                if(target.isDead) 
                    continue;

                if (target.hasMaxHP && target.hasMaxHP)
                {
                    float newValue = Math.Min(target.CurrentHP + effect.EffectValue, target.MaxHP);
                    target.ReplaceCurrentHP(newValue);
                }
            }
        }
    }
}