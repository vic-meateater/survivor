using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Loot.Configs;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Loot.Factory
{
  public class LootFactory : ILootFactory
  {
    private readonly IIdentifierService _identifiers;
    private readonly IStaticDataService _staticData;

    public LootFactory(IIdentifierService identifiers, IStaticDataService staticData)
    {
      _identifiers = identifiers;
      _staticData = staticData;
    }

    public GameEntity CreateLootItem(LootTypeID lootTypeID, Vector3 at)
    {
      LootConfig lootConfig = _staticData.GetLootConfig(lootTypeID);
      return CreateEntity.Empty()
        .AddId(_identifiers.Next())
        .AddWorldPosition(at)
        .AddLootTypeID(lootTypeID)
        .AddViewPrefab(lootConfig.ViewPrefab)
        .With(x => x.AddExperience(lootConfig.Experience), when: lootConfig.Experience > 0)
        .With(x => x.AddEffectSetups(lootConfig.EffectSetups), when: !lootConfig.EffectSetups.IsNullOrEmpty())
        .With(x => x.AddStatusSetups(lootConfig.StatusSetups), when: !lootConfig.StatusSetups.IsNullOrEmpty())
        .With(x => x.isPullable = true);
    }
  }
}