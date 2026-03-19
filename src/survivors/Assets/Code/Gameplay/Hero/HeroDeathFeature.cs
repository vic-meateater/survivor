using Code.Gameplay.Hero.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Hero
{
    public sealed class HeroDeathFeature : Feature
    {
        public HeroDeathFeature(ISystemFactory systems)
        {
            Add(systems.Create<HeroDeathSystem>());
            
            Add(systems.Create<FinalizeHeroDeathProcessingSystem>());
        }
    }
}