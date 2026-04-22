using UnityEngine;

namespace Code.Gameplay.Features.Loot.Factory
{
  public interface ILootFactory
  {
    GameEntity CreateLootItem(LootTypeID lootTypeID, Vector3 at);
  }
}