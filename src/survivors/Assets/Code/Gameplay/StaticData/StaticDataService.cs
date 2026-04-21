using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Enchants;
using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Loot.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<AbilityID, AbilityConfig> _abilityByID;
    private Dictionary<EnchantTypeID, EnchantConfig> _enchantByID;
    private Dictionary<LootTypeID, LootConfig> _lootByID;

    public void LoadAll()
    {
      LoadAbilities();
      LoadEnchants();
      LoadLoot();
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
    
    //Loot
    
    public LootConfig GetLootConfig(LootTypeID lootTypeID)
    {
      if (_lootByID.TryGetValue(lootTypeID, out LootConfig config))
        return config;

      throw new Exception($"Loot config for {lootTypeID} was not found");
    }
    
    private void LoadLoot()
    {
      _lootByID = Resources.LoadAll<LootConfig>("Gameplay/Configs/Loot")
        .ToDictionary(x => x.LootTypeID, x => x);
    }
  }
}