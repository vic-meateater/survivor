using Code.Gameplay.Features.CharacterStats;
using Code.Gameplay.Features.CharacterStats.Indexing;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Features.Statuses.Indexing;
using Entitas;
using Zenject;

namespace Code.Common.EntityIndices
{
    public class GameEntityIndices : IInitializable
    {
        public const string StatusesOfType = "StatusesOfType";
        public const string StatChanges = "StatChanges";
        
        private readonly GameContext _game;

        public GameEntityIndices(GameContext game)
        {
            _game = game;
        }
        public void Initialize()
        {
            _game.AddEntityIndex(new EntityIndex<GameEntity, StatusKey>(
                name: StatusesOfType,
                _game.GetGroup(GameMatcher.AllOf(
                    GameMatcher.StatusTypeID,
                    GameMatcher.TargetID,
                    GameMatcher.Status,
                    GameMatcher.Duration,
                    GameMatcher.TimeLeft)),
                getKey: GetTargetStatusKey, 
                new StatusKeyEqualityComparer()));
            
            _game.AddEntityIndex(new EntityIndex<GameEntity, StatKey>(
                name: StatChanges,
                _game.GetGroup(GameMatcher.AllOf(
                    GameMatcher.StatChange,
                    GameMatcher.TargetID
                    )),
                getKey: GetTargetStatKey, 
                new StatKeyEqualityComparer()));
        }

        private StatKey GetTargetStatKey(GameEntity entity, IComponent component)
        {
            return new StatKey(
                (component as TargetID)?.Value ?? entity.TargetID,
                (component as StatChange)?.Value ?? entity.StatChange);
        }

        private StatusKey GetTargetStatusKey(GameEntity entity, IComponent component)
        {
            return new StatusKey(
                (component as TargetID)?.Value ?? entity.TargetID,
                (component as StatusTypeIDComponent)?.Value ?? entity.StatusTypeID);
        }
    }
}