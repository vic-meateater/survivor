using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.CharacterStats;
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
            Dictionary<Stats, float> baseStats = InitStats.EmptyStatDictionary()
                    .With(x => x[Stats.Speed] = 3)
                    .With(x => x[Stats.MaxHp] = 100)
                ;
            
            return CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddWorldPosition(at)
                    .AddBaseStats(baseStats)
                    .AddStatModifiers(InitStats.EmptyStatDictionary())
                    .AddMaxHP(baseStats[Stats.MaxHp])
                    .AddCurrentHP(baseStats[Stats.MaxHp])
                    .AddDirection(Vector3.zero)
                    .AddSpeed(baseStats[Stats.Speed])
                    .AddExperience(0)
                    .AddViewPath("Gameplay/Hero/Hero")
                    .AddPickupRadius(1)
                    .With(x => x.isHero = true)
                    .With(x => x.isTurnAlongDirection = true)
                    .With(x => x.isMovementAvailable = true)
                ;
        }
    }
}