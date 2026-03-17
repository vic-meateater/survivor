using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.Systems
{
    public class TurnAlongDirectionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;

        public TurnAlongDirectionSystem(GameContext game)
        {
            _movers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TurnAlongDirection,
                    GameMatcher.Transform,
                    GameMatcher.Direction));
        }
        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                if (mover.Direction == Vector3.zero) continue;
        
                mover.Transform.rotation = Quaternion.LookRotation(mover.Direction, Vector3.up);
            }
        }
    }
}