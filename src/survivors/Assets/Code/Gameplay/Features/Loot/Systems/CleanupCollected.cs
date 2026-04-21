using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
  public class CleanupCollected : ICleanupSystem
  {
    private readonly GameContext _game;
    private readonly IGroup<GameEntity> _collected;

    public CleanupCollected(GameContext game)
    {
      _game = game;
      _collected = _game.GetGroup(GameMatcher.Collected);
    }
    public void Cleanup()
    {
      foreach (GameEntity collect in _collected)
      {
        collect.isDestructed = true;
      }
    }
  }
}