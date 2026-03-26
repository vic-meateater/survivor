using Entitas;

namespace Code.Gameplay.Features.Movement.Systems
{
    public class OrbitCenterFollowSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _orbitCenters;
        private IGroup<GameEntity> _targets;

        public OrbitCenterFollowSystem(GameContext game)
        {
            _game = game;
            _orbitCenters = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.OrbitCenterPosition, 
                    GameMatcher.OrbitCenterFollowTarget));

            _targets = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Id,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (GameEntity orbitCenter in _orbitCenters)
            {
                GameEntity target = _game.GetEntityWithId(orbitCenter.OrbitCenterFollowTarget);
                if (target != null)
                    orbitCenter.ReplaceOrbitCenterPosition(target.WorldPosition);
            }
        }
    }
}