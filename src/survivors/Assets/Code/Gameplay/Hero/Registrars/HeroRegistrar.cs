using Code.Common.Extensions;
using Code.Infrastructure.View.Registrar;
using UnityEngine;

namespace Code.Gameplay.Hero.Registrars
{
    public class HeroRegistrar : EntityComponentRegistrar
    {
        public float MaxHP = 100f;
        public float Speed = 2f;

        public override void RegisterComponents()
        {
            Entity
                .AddWorldPosition(transform.position)
                .AddDirection(Vector3.zero)
                .AddMaxHP(MaxHP)
                .AddCurrentHP(MaxHP)
                .AddSpeed(Speed)
                .With(x => x.isHero = true)
                .With(x => x.isTurnAlongDirection = true)
                ;
        }

        public override void UnregisterComponents()
        {
            Entity
                .RemoveWorldPosition()
                .RemoveDirection()
                .RemoveSpeed()
                .With(x => x.isHero = false)
                .With(x => x.isTurnAlongDirection = false);
        }
    }
}