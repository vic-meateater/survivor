using Code.Gameplay.Cameras.Systems;
using Code.Gameplay.Hero.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Hero
{
    public class HeroFeature : Feature
    {
        public HeroFeature(ISystemFactory systems)
        {
            Add(systems.Create<SetHeroDirectionByInputSystem>());
            Add(systems.Create<AnimateHeroMovementSystem>());
            Add(systems.Create<CameraFollowHeroSystem>());
        }
    }
}