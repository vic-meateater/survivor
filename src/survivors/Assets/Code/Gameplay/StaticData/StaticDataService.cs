using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Enchants;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<AbilityID, AbilityConfig> _abilityByID;
    private Dictionary<EnchantTypeID, EnchantConfig> _enchantByID;

    public void LoadAll()
    {
      LoadAbilities();
      LoadEnchants();
    }
    
    public AbilityConfig GetAbilityConfig(AbilityID abilityID)
    {
      if (_abilityByID.TryGetValue(abilityID, out AbilityConfig config))
        return config;

      throw new Exception($"Ability config for {abilityID} was not found");
    }

    public AbilityLevel GetAbilityLevel(AbilityID abilityID, int level)
    {
      AbilityConfig config = GetAbilityConfig(abilityID);

      if (level > config.Levels.Count)
        level = config.Levels.Count;

      return config.Levels[level - 1];
    }

    private void LoadAbilities()
    {
      _abilityByID = Resources.LoadAll<AbilityConfig>("Gameplay/Configs/Abilities")
        .ToDictionary(x => x.AbilityID, x => x);
    }

    //Enchants
    
    public EnchantConfig GetEnchantConfig(EnchantTypeID typeID)
    {
      if (_enchantByID.TryGetValue(typeID, out EnchantConfig config))
        return config;

      throw new Exception($"Enchant config for {typeID} was not found");
    }
    
    private void LoadEnchants()
    {
      _enchantByID = Resources.LoadAll<EnchantConfig>("Gameplay/Configs/Enchants")
        .ToDictionary(x => x.TypeID, x => x);
    }

  }
}