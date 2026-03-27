using Code.Gameplay.Features.Abilities;
using UnityEngine;

namespace Code.Gameplay.Features.Armaments.Factory
{
    public interface IArmamentFactory
    {
        GameEntity CreateProjectile(int level, Vector3 at);
        GameEntity CreateOrbitingBrick(int level, Vector3 at, float phase);
        GameEntity CreateEffectAura(AbilityID parentAbilityId, int producerId, int level);
    }
}