using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection.Systems
{
    public class CollectTargetsIntervalSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private IGroup<GameEntity> _entities;

        public CollectTargetsIntervalSystem(GameContext game, ITimeService  timeService)
        {
            _timeService = timeService;

            _entities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.CollectsTargetInterval,
                    GameMatcher.CollectsTargetTimer,
                    GameMatcher.TargetBuffer
                ));
        }
        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                entity.ReplaceCollectsTargetTimer(entity.CollectsTargetTimer - _timeService.DeltaTime);

                if (entity.CollectsTargetTimer <= 0f)
                {
                    entity.isReadyToCollectTargets = true;
                    entity.ReplaceCollectsTargetTimer(entity.CollectsTargetInterval);
                }
            }
        }
    }
}