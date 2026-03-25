using Entitas;

namespace Code.Gameplay.Features.Lifetime.Systems
{
    public class UnapplyStatusesOfDeadTargetSystem : IExecuteSystem
    {
        private IGroup<GameEntity> _statuses;
        private IGroup<GameEntity> _deads;

        public UnapplyStatusesOfDeadTargetSystem(GameContext game)
        {
            _statuses = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TargetID,
                    GameMatcher.Status
                ));

            _deads = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Id,
                    GameMatcher.Dead
                ));
        }

        public void Execute()
        {
            foreach (GameEntity dead in _deads)
            foreach (GameEntity status in _statuses)
            {
                if(status.TargetID == dead.Id)
                    status.isUnapplied = true;
            }
        }
    }
}