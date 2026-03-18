using Code.Gameplay.Enemies.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Enemies
{
    public class EnemiesFeature : Feature
    {
        public EnemiesFeature(ISystemFactory systems)
        {
            Add(systems.Create<ChaseHeroSystem>());
        }
    }
}