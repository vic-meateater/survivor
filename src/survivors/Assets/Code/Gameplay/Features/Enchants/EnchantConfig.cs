using System.Collections.Generic;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Statuses;
using UnityEngine;

namespace Code.Gameplay.Features.Enchants
{
  [CreateAssetMenu(menuName = "Survivors/Enchan Config", fileName = "_enchantConfig")]
  public class EnchantConfig : ScriptableObject
  {
    public EnchantTypeID TypeID;
    public List<EffectSetup> EffectSetups;
    public List<StatusSetup> StatusSetups;
  }
}