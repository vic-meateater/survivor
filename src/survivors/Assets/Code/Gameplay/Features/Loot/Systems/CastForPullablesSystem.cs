using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
  public class CastForPullablesSystem : IExecuteSystem
  {
    private readonly IPhysics3DService _physics3DService;
    private readonly IGroup<GameEntity> _looters;
    private readonly GameEntity[] _hitBuffer = new GameEntity[128];
    private readonly int _layerMask = CollisionLayer.Collectable.AsMask();

    public CastForPullablesSystem(GameContext game, IPhysics3DService physics3DService)
    {
      _physics3DService = physics3DService;
      _looters = game.GetGroup(
        GameMatcher.AllOf(
          GameMatcher.WorldPosition,
          GameMatcher.PickupRadius
        ));
    }

    public void Execute()
    {
      foreach (GameEntity loot in _looters)
      {
        for (int i = 0; i < LootInRadius(loot); i++)
        {
          if (_hitBuffer[i].isPullable)
          {
            _hitBuffer[i].isPullable = false;
            _hitBuffer[i].isPulling = true;
          }
        }

        ClearBuffer();
      }
    }

    private void ClearBuffer()
    {
      for (int i = 0; i < _hitBuffer.Length; i++)
      {
        _hitBuffer[i] = null;
      }
    }

    private int LootInRadius(GameEntity loot)
    {
      return _physics3DService.OverlapSphereNonAlloc(loot.WorldPosition, loot.PickupRadius, _layerMask, _hitBuffer);
    }
  }
}