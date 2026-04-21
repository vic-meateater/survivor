using System.Collections.Generic;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Statuses;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Features.Loot.Configs
{
  [CreateAssetMenu(menuName = "Survivors/Loot Config", fileName = "_lootConfig", order = 0)]
  public class LootConfig : ScriptableObject
  {
    public LootTypeID LootTypeID;
    public float Experience;
    public EntityBehaviour ViewPrefab;
    
    public List<EffectSetup> EffectSetups;
    public List<StatusSetup> StatusSetups;
  }
}