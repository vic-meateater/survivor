using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Armaments.Factory
{
    public class ArmamentFactory : IArmamentFactory
    {
        private readonly IIdentifierService _identifiers;
        private readonly IStaticDataService _staticDataService;

        public ArmamentFactory(IIdentifierService identifiers, IStaticDataService staticDataService)
        {
            _identifiers = identifiers;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateProjectile(int level, Vector3 at)
        {
            AbilityLevel abilityLevel = _staticDataService.GetAbilityLevel(AbilityID.Projectile, level);
            ProjectileSetup setup = abilityLevel.ProjectileSetup;

            return CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddWorldPosition(at)
                    .AddSpeed(setup.Speed)
                    .AddDamage(setup.Damage)
                    .AddRadius(setup.ContactRadius)
                    .AddTargetBuffer(new List<int>(16))
                    .AddProcessedTargets(new List<int>(16))
                    .AddLayerMask(CollisionLayer.Enemy.AsMask())
                    .AddTargetLimit(setup.Pierce)
                    .AddViewPrefab(abilityLevel.ViewPrefab)
                    .AddSelfDestructTimer(setup.LifeTime)
                    .With(x => x.isMovementAvailable = true)
                    .With(x=>x.isReadyToCollectTargets = true)
                    .With(x => x.isArmament = true)
                    .With(x=>x.isCollectTargetsContinuously = true)
                ;
        }
    }
}