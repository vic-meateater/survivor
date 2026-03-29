using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Enchants;

namespace Code.Gameplay.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        AbilityConfig GetAbilityConfig(AbilityID abilityID);
        AbilityLevel GetAbilityLevel(AbilityID abilityID, int level);
        EnchantConfig GetEnchantConfig(EnchantTypeID typeID);
    }
}