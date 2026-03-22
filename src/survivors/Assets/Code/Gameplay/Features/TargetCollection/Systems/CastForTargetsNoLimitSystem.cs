using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Physics;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection.Systems
{
    public class CastForTargetsNoLimitSystem : IExecuteSystem
    {
        private readonly IPhysics3DService _physics;
        private readonly IGroup<GameEntity> _ready;
        private readonly List<GameEntity> _buffer = new(64);
        private readonly GameEntity[] _targetHits = new GameEntity[128];

        public CastForTargetsNoLimitSystem(GameContext game, IPhysics3DService physics)
        {
            _physics = physics;
            _ready = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TargetBuffer,
                    GameMatcher.ReadyToCollectTargets,
                    GameMatcher.WorldPosition,
                    GameMatcher.Radius,
                    GameMatcher.LayerMask
                ).NoneOf(GameMatcher.TargetLimit)
            );
        }
        public void Execute()
        {
            foreach (GameEntity entity in _ready.GetEntities(_buffer))
            {
                entity.TargetBuffer.AddRange(TargetsInRadius(entity));
                
                if(!entity.isCollectTargetsContinuously)
                    entity.isReadyToCollectTargets = false;
            }
        }

        private IEnumerable<int> TargetsInRadius(GameEntity entity)
        {
            int count = _physics.OverlapSphereNonAlloc(
                entity.WorldPosition, entity.Radius, entity.LayerMask, _targetHits);

            for (int i = 0; i < count; i++)
            {
                if (_targetHits[i].Id != entity.Id)
                    yield return _targetHits[i].Id;
            }
        }
    }
}