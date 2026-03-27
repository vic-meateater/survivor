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

      return CreateProjectileEntity(at, setup, abilityLevel)
        .AddParentAbility(AbilityID.Projectile);
    }

    public GameEntity CreateOrbitingBrick(int level, Vector3 at, float phase)
    {
      AbilityLevel abilityLevel = _staticDataService.GetAbilityLevel(AbilityID.OrbitingBrick, level);
      ProjectileSetup setup = abilityLevel.ProjectileSetup;

      return CreateProjectileEntity(at, setup, abilityLevel)
          .AddParentAbility(AbilityID.OrbitingBrick)
          .AddOrbitPhase(phase)
          .AddOrbitRadius(setup.OrbitRadius)
        ;
    }

    public GameEntity CreateEffectAura(AbilityID parentAbilityId, int producerId, int level)
    {
      AbilityLevel abilityLevel = _staticDataService.GetAbilityLevel(AbilityID.DudeWordAura, level);
      AuraSetup setup = abilityLevel.AuraSetup;

      return CreateEntity.Empty()
          .AddId(_identifiers.Next())
          .AddParentAbility(parentAbilityId)
          .AddViewPrefab(abilityLevel.ViewPrefab)
          .AddLayerMask(CollisionLayer.Enemy.AsMask())
          .AddRadius(setup.Radius)
          .AddCollectsTargetInterval(setup.Interval)
          .AddCollectsTargetTimer(0)
          .With(x => x.AddEffectSetups(abilityLevel.EffectSetups),
            when: !abilityLevel.EffectSetups.IsNullOrEmpty())
          .With(x => x.AddStatusSetups(abilityLevel.StatusSetups),
            when: !abilityLevel.StatusSetups.IsNullOrEmpty())
          .AddTargetBuffer(new List<int>(16))
          .AddProducerID(producerId)
          .AddWorldPosition(Vector3.zero)
          .With(x => x.isFollowingProducer = true)
        ;
    }

    private GameEntity CreateProjectileEntity(Vector3 at, ProjectileSetup setup, AbilityLevel abilityLevel)
    {
      return CreateEntity.Empty()
          .AddId(_identifiers.Next())
          .AddWorldPosition(at)
          .AddSpeed(setup.Speed)
          .With(x => x.AddEffectSetups(abilityLevel.EffectSetups),
            when: !abilityLevel.EffectSetups.IsNullOrEmpty())
          .With(x => x.AddStatusSetups(abilityLevel.StatusSetups),
            when: !abilityLevel.StatusSetups.IsNullOrEmpty())
          .With(x => x.AddTargetLimit(setup.Pierce), when: setup.Pierce > 0)
          .AddRadius(setup.ContactRadius)
          .AddTargetBuffer(new List<int>(16))
          .AddProcessedTargets(new List<int>(16))
          .AddLayerMask(CollisionLayer.Enemy.AsMask())
          .AddViewPrefab(abilityLevel.ViewPrefab)
          .AddSelfDestructTimer(setup.LifeTime)
          .With(x => x.isMovementAvailable = true)
          .With(x => x.isReadyToCollectTargets = true)
          .With(x => x.isArmament = true)
          .With(x => x.isCollectTargetsContinuously = true)
        ;
    }
  }
}