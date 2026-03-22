using System;

namespace Code.Gameplay.Features.Abilities.Configs
{
    [Serializable]
    public class ProjectileSetup
    {
        public float Damage;
        public float Speed;
        public int Pierce;
        public float ContactRadius;
        public float LifeTime;
    }
}