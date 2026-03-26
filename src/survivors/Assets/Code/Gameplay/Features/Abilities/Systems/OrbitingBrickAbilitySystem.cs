using System.Collections.Generic;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Abilities.Systems
{
  public class OrbitingBrickAbilitySystem : IExecuteSystem
  {
    private readonly IStaticDataService _staticDataService;
    private readonly IArmamentFactory _armamentFactory;
    private readonly IGroup<GameEntity> _abilities;
    private readonly IGroup<GameEntity> _heroes;
    private readonly List<GameEntity> _buffer = new(64);

    public OrbitingBrickAbilitySystem(GameContext game, IStaticDataService staticDataService,
      IArmamentFactory armamentFactory)
    {
      _staticDataService = staticDataService;
      _armamentFactory = armamentFactory;

      _abilities = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.OrbitingBrickAbility,
          GameMatcher.CooldownUp
        ));

      _heroes = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Hero,
          GameMatcher.WorldPosition
        )
        .NoneOf(GameMatcher.Dead
        ));
    }

    public void Execute()
    {
      foreach (GameEntity ability in _abilities.GetEntities(_buffer))
      foreach (GameEntity hero in _heroes)
      {
        AbilityLevel abilityLevel = _staticDataService.GetAbilityLevel(AbilityID.OrbitingBrick, 1);

        int projectileCount = abilityLevel.ProjectileSetup.ProjectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
          float phase = (2 * Mathf.PI * i) / projectileCount;
          _armamentFactory.CreateOrbitingBrick(1, hero.WorldPosition, phase)
              .AddProducerID(hero.Id)
              .AddOrbitCenterPosition(hero.WorldPosition)
              .AddOrbitCenterFollowTarget(hero.Id)
              .isMoving = true
            ;
        }

        ability.PutOnCooldown(abilityLevel.Cooldown);
      }
    }
  }
}