using Code.Common.Destruct;
using Code.Gameplay.Enemies;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Armaments;
using Code.Gameplay.Features.CharacterStats;
using Code.Gameplay.Features.EffectApplication;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Enchants;
using Code.Gameplay.Features.Lifetime;
using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Movement;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Features.TargetCollection;
using Code.Gameplay.Hero;
using Code.Gameplay.Input;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View;

namespace Code.Gameplay
{
    public class BattleFeature : Feature
    {
        public BattleFeature(ISystemFactory systems)
        {
            Add(systems.Create<InputFeature>());
            Add(systems.Create<BindViewFeature>());
            Add(systems.Create<HeroFeature>());
            
            Add(systems.Create<MovementFeature>());
            Add(systems.Create<AbilityFeature>());
            Add(systems.Create<EnemiesFeature>());


            Add(systems.Create<TargetCollectionFeature>());
            Add(systems.Create<EffectApplicationFeature>());
            Add(systems.Create<ArmamentFeature>());
            
            Add(systems.Create<EnchantFeature>());
            Add(systems.Create<EffectFeature>());
            Add(systems.Create<StatusFeature>());
            Add(systems.Create<StatFeature>());
            
            Add(systems.Create<DeathFeature>());
            
            Add(systems.Create<LootingFeature>());

            Add(systems.Create<HeroDeathFeature>());
            Add(systems.Create<EnemyDeathFeature>());

            Add(systems.Create<ProcessDestructedFeature>());
        }
    }
}