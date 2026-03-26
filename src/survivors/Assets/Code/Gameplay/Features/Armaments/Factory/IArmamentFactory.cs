using UnityEngine;

namespace Code.Gameplay.Features.Armaments.Factory
{
    public interface IArmamentFactory
    {
        GameEntity CreateProjectile(int level, Vector3 at);
        GameEntity CreateOrbitingBrick(int level, Vector3 at, float phase);
    }
}