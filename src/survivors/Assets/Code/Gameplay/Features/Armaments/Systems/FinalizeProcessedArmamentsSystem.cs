using Code.Gameplay.Features.TargetCollection;
using Entitas;

namespace Code.Gameplay.Features.Armaments.System
{
    public class FinalizeProcessedArmamentsSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _armament;

        public FinalizeProcessedArmamentsSystem(GameContext game)
        {
            _armament = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Armament,
                    GameMatcher.Processed
                ));
        }

        public void Execute()
        {
            foreach (GameEntity armament in _armament)
            {
                armament.RemoveTargetCollectionComponents();
                armament.isDestructed = true;
            }
        }
    }
}