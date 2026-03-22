using System.Collections.Generic;
using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Gameplay.Features.Cooldowns.Systems
{
    public class CooldownSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> cooldownables;
        private readonly List<GameEntity> _buffer = new(64);

        public CooldownSystem(GameContext game, ITimeService time)
        {
            _time = time;
            cooldownables = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Cooldown, 
                    GameMatcher.CooldownLeft
                    ));
        }
        public void Execute()
        {
            foreach (GameEntity cooldownable in cooldownables.GetEntities(_buffer))
            {
                cooldownable.ReplaceCooldownLeft(cooldownable.CooldownLeft - _time.DeltaTime);

                if (cooldownable.CooldownLeft <= 0)
                {
                    cooldownable.isCooldownUp = true;
                    cooldownable.RemoveCooldownLeft();
                }
            }
        }
    }
}