using Code.Gameplay.Enemies.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Enemies
{
    public sealed class EnemyDeathFeature : Feature
    {
        public EnemyDeathFeature(ISystemFactory systems)
        {
            Add(systems.Create<EnemyDeathSystem>());
            
            Add(systems.Create<FinalizeEnemyDeathProcessingSystem>());
        }
    }
}