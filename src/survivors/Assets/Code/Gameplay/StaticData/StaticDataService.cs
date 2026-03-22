using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<AbilityID, AbilityConfig> _abilityByID;

        public void LoadAll()
        {
            LoadAbilities();
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
    }
}