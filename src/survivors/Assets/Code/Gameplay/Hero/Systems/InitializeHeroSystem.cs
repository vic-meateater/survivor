using Code.Gameplay.Features.Abilities.Factory;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Hero.Factory;
using Code.Gameplay.Levels;
using Entitas;

namespace Code.Gameplay.Hero.Systems
{
    public class InitializeHeroSystem : IInitializeSystem
    {
        private readonly IHeroFactory _heroFactory;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly IAbilityFactory _abilityFactory;
        private readonly IStatusApplier _statusApplier;

        public InitializeHeroSystem(
            IHeroFactory heroFactory, 
            ILevelDataProvider levelDataProvider, 
            IAbilityFactory abilityFactory,
            IStatusApplier statusApplier)
        {
            _heroFactory = heroFactory;
            _levelDataProvider = levelDataProvider;
            _abilityFactory = abilityFactory;
            _statusApplier = statusApplier;
        }

        public void Initialize()
        {
            GameEntity hero = _heroFactory.CreateHero(_levelDataProvider.StartPoint);
            _abilityFactory.CreateProjectileAbility(1);
            _abilityFactory.CreateOrbitingBrickAbility(1);
            _abilityFactory.CreateDudeWordAuraAbility();

            _statusApplier.ApplyStatus(new StatusSetup()
            {
                StatusTypeID = StatusTypeId.PoisonEnchant,
                Duration = 10
            }, hero.Id, hero.Id);
        }
    }
}