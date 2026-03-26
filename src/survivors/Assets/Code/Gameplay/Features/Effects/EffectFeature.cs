using Code.Gameplay.Features.Effects.System;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Effects
{
    public sealed class EffectFeature : Feature
    {
        public EffectFeature(ISystemFactory systems)
        {
            Add(systems.Create<RemoveEffectsWithoutTargetSystem>());
            Add(systems.Create<ProcessDamageEffectSystem>());
            Add(systems.Create<ProcessHealEffectSystem>());
            

            Add(systems.Create<CleanupProcessedEffects>());
        }
    }
}