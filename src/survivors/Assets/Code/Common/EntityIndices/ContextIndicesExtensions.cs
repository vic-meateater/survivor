using System.Collections.Generic;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Features.Statuses.Indexing;
using Entitas;

namespace Code.Common.EntityIndices
{
    public static class ContextIndicesExtensions
    {
        public static HashSet<GameEntity> TargetStatusesOfType(this GameContext context, StatusTypeId statusTypeId,
            int targetID)
        {
            return ((EntityIndex<GameEntity, StatusKey>)context.GetEntityIndex(GameEntityIndices.StatusesOfType))
                .GetEntities(new StatusKey(targetID, statusTypeId));
        }
    }
}