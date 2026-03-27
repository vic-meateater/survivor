using System.Collections.Generic;
using Code.Gameplay.Features.Armaments.Factory;
using Entitas;

namespace Code.Gameplay.Features.Abilities.Systems
{
  public class DudeWordAuraAbilitySystem : IExecuteSystem
  {
    private readonly IArmamentFactory _armamentFactory;
    private readonly IGroup<GameEntity> _abilities;
    private readonly List<GameEntity> _buffer = new(32);
    private readonly IGroup<GameEntity> _heroes;

    public DudeWordAuraAbilitySystem(GameContext game, IArmamentFactory armamentFactory)
    {
      _armamentFactory = armamentFactory;

      _abilities = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.DudeWordAbility)
        .NoneOf(GameMatcher.Active));

      _heroes =
        game.GetGroup(GameMatcher
          .AllOf(
            GameMatcher.Hero,
            GameMatcher.Id));
    }

    public void Execute()
    {
      foreach (GameEntity ability in _abilities.GetEntities(_buffer))
      foreach (GameEntity hero in _heroes)
      {
        _armamentFactory.CreateEffectAura(AbilityID.DudeWordAura, hero.Id, 1);

        ability.isActive = true;
      }
    }
  }
}