using Code.Gameplay.Hero.Behaviours;
using Code.Infrastructure.View.Registrar;

namespace Code.Gameplay.Hero.Registrars
{
    public class HeroAnimatorRegistrar : EntityComponentRegistrar
    {
        public HeroAnimator HeroAnimator;

        public override void RegisterComponents()
        {
            Entity.AddHeroAnimator(HeroAnimator);
        }

        public override void UnregisterComponents()
        {
            Entity.RemoveHeroAnimator();
        }
    }
}