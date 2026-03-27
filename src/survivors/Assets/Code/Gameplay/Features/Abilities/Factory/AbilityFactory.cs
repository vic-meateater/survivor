using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Features.Abilities.Factory
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IIdentifierService _identifiers;
        private readonly IStaticDataService _staticDataService;

        public AbilityFactory(IIdentifierService identifiers, IStaticDataService staticDataService)
        {
            _identifiers = identifiers;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateProjectileAbility(int level)
        {
            AbilityLevel abilityLevel = _staticDataService.GetAbilityLevel(AbilityID.Projectile, level);
            
            return 
                CreateEntity.Empty()
                .AddId(_identifiers.Next())
                .AddAbilityID(AbilityID.Projectile)
                .AddCooldown(abilityLevel.Cooldown)
                .With(x => x.isProjectileAbility = true)
                .PutOnCooldown();
        }
        
        public GameEntity CreateOrbitingBrickAbility(int level)
        {
            AbilityLevel abilityLevel = _staticDataService.GetAbilityLevel(AbilityID.OrbitingBrick, level);
            
            return 
                CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddAbilityID(AbilityID.OrbitingBrick)
                    .AddCooldown(abilityLevel.Cooldown)
                    .With(x => x.isOrbitingBrickAbility = true)
                    .PutOnCooldown();
        }

        public GameEntity CreateDudeWordAuraAbility()
        {
            return CreateEntity.Empty()
                .AddId(_identifiers.Next())
                .AddAbilityID(AbilityID.DudeWordAura)
                .With(x => x.isDudeWordAbility = true);
        }
    }
}