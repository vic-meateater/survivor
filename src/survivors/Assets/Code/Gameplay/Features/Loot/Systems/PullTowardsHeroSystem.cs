using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
  public class PullTowardsHeroSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _heroes;
    private readonly IGroup<GameEntity> _pullables;
    private List<GameEntity> _buffer = new(128);

    public PullTowardsHeroSystem(GameContext game)
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
        pullable.ReplaceDirection((hero.WorldPosition - pullable.WorldPosition).normalized);
        // pullable.ReplaceSpeed(pullable.Speed * 1.5f);
        // pullable.isMoving = true;
        // pullable.isMovementAvailable = true;
      }
    }
  }
}