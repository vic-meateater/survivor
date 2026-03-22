using System;
using System.Collections.Generic;
using Code.Gameplay.Common.Physics;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection.Systems
{
    public class CastForTargetsWithLimitSystem : IExecuteSystem, ITearDownSystem
    {
        private readonly IPhysics3DService _physics;
        private readonly IGroup<GameEntity> _ready;
        private readonly List<GameEntity> _buffer = new(64);
        private GameEntity[] _targetHits = new GameEntity[128];

        public CastForTargetsWithLimitSystem(GameContext game, IPhysics3DService physics)
        {
            _physics = physics;
            _ready = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TargetBuffer,
                    GameMatcher.TargetLimit,
                    GameMatcher.ProcessedTargets,
                    GameMatcher.ReadyToCollectTargets,
                    GameMatcher.WorldPosition,
                    GameMatcher.Radius,
                    GameMatcher.LayerMask
                ));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _ready.GetEntities(_buffer))
            {
                int count = CastTargets(entity);
                int limit = entity.hasTargetLimit ? (int) entity.TargetLimit : count;
                for (int i = 0; i < count && entity.TargetBuffer.Count < limit; i++)
                {
                    int targetId = _targetHits[i].Id;

                    if (targetId != entity.Id && !AlreadyProcessed(entity, targetId))
                    {
                        entity.TargetBuffer.Add(targetId);
                        entity.ProcessedTargets.Add(targetId);
                    }
                }

                if (!entity.isCollectTargetsContinuously)
                    entity.isReadyToCollectTargets = false;
            }
        }

        private int CastTargets(GameEntity entity)
        {
            return _physics.OverlapSphereNonAlloc(
                entity.WorldPosition, entity.Radius, entity.LayerMask, _targetHits);
        }

        private bool AlreadyProcessed(GameEntity entity, int targetID)
        {
            return entity.ProcessedTargets.Contains(targetID);
        }

        public void TearDown()
        {
            _targetHits = null;
        }
    }
}