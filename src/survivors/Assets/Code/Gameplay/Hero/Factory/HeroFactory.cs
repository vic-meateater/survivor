using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Hero.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IIdentifierService _identifiers;

        public HeroFactory(IIdentifierService identifiers)
        {
            _identifiers = identifiers;
        }

        public GameEntity CreateHero(Vector3 at)
        {
            return CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddWorldPosition(at)
                    .AddDirection(Vector3.zero)
                    .AddMaxHP(100)
                    .AddCurrentHP(100)
                    .AddSpeed(3)
                    .AddViewPath("Gameplay/Hero/Hero")
                    .With(x => x.isHero = true)
                    .With(x => x.isTurnAlongDirection = true)
                    .With(x => x.isMovementAvailable = true)
                ;
        }
    }
}