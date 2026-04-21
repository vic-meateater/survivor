using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Loot.Systems
{
  public class CollectThenNearHeroSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _heroes;
    private readonly IGroup<GameEntity> _pullables;
    private List<GameEntity> _buffer = new(128);

    public CollectThenNearHeroSystem(GameContext game)
    {
      _heroes = game.GetGroup(
        GameMatcher.AllOf(
          GameMatcher.Hero,
          GameMatcher.WorldPosition
        ));

      _pullables = game.GetGroup(
        GameMatcher.AllOf(
          GameMatcher.Pulling,
          GameMatcher.WorldPosition
        ));
    }

    public void Execute()
    {
      foreach (GameEntity hero in _heroes)
      foreach (GameEntity pullable in _pullables.GetEntities(_buffer))
      {
        if (Vector3.Distance(hero.WorldPosition, pullable.WorldPosition) <= .5f)
        {
          pullable.isCollected = true;
        }
      }
    }
  }
}